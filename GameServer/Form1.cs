using GameSocket;
using System.ComponentModel;
using System.Windows.Forms;

namespace GameServer
{
    public partial class Form1 : Form
    {
        TCPSocketServer mServer;
        GameManager mManager;


        public Form1()
        {
            InitializeComponent();

            mServer = new TCPSocketServer();
            mManager = new GameManager();

            // 이벤트 연결
            mServer.ClientConnected += OnClientConnected;
            mServer.ClientDisconnected += OnClientDisconnected;
            mServer.ClientMessageReceived += OnClientMessageReceived;

            // 초기 UI
            UpdateUiConnectedState(false);

        }


        // UI 활성/비활성화
        private void UpdateUiConnectedState(bool connected)
        {
            btnConnectServer.Enabled = !connected;
            btnDisconnectServer.Enabled = connected;
        }


        // 서버 연결 버튼
        private async void BtnAcceptIncoming_Click(object sender, System.EventArgs e)
        {
            UpdateUiConnectedState(true);
            AppendGameLog("[서버 연결]");
            await mServer.StartServerListeningAsync();
        }

        // 서버 연결 해제 버튼
        private void BtnDisconnectServer_Click(object sender, EventArgs e)
        {
            mServer.StopServer();
            AppendGameLog("[서버 중지]");
            UpdateUiConnectedState(false);
        }

        // 클라이언트 연결 성공
        private void OnClientConnected(string ip)
        {
            this.Invoke(new Action(() =>
            {
                AppendClientLog($"[접속] {ip}");
            }));
        }

        // 클라이언트 접속 끊김
        private void OnClientDisconnected(string ip)
        {
            this.Invoke(new Action(() =>
            {
                Console.WriteLine("연결해제");
                AppendClientLog($"[연결 해제] {ip}");

                // 게임중인 대상일 때
                string opponent = mManager.HandleSearchOpponent(ip);
                if (opponent != "")
                {
                    // 상대방에게 게임 승리 메세지 전송
                    _ = mServer.SendToClient(opponent, "[05]WIN_BY_DISCONNECT");

                    mManager.DeleteActiveMatches(ip);
                    AppendGameLog($"[연결 끊김] {ip} 접속 종료로 {opponent} 승리");
                }
            }));
        }


        // 클라이언트 메세지
        private void OnClientMessageReceived(string ip, string msg)
        {
            string code = msg.Substring(0, 4); // [01], [02], [03], [04] ...


            // [01] 매칭 요청
            if (code == "[01]")
            {
                AppendGameLog($"[매칭 요청] {ip}");
                // 게임 시작 요청하고,, 매칭 될 때까지 대기
                var (p1, p2, session) = mManager.HandleActiveMatches(ip);

                // 게임 시작 알림, 로그
                if (p1 != null && p2 != null && session != null)
                {
                    _ = mServer.SendToClient(p1, "MATCHED|LEFT");
                    _ = mServer.SendToClient(p2, "MATCHED|RIGHT");

                    AppendGameLog($"[매칭 완료] {p1} - {p2}");

                    // 게임 시작
                    StartGameLoop(session);
                }
            }
            // [02] 플레이어 입력 (UP/DOWN)
            else if (code == "[02]")
            {
                // "[02]MOVING|UP" / "[02]MOVING|DOWN" 
                string[] parts = msg.Split('|');

                if (parts.Length < 2) return;

                string direction = parts[1];

                var session = mManager.GetGameSession(ip);

                if (session != null)
                {
                    session.GameState.HandlePlayerMove(ip, direction);
                }

            }
            // [03] 플레이어 포기
            else if (code == "[03]")
            {
                // "[03]GIVEUP"
                string opponent = mManager.HandleSearchOpponent(ip);
                if (opponent != "")
                {
                    // 상대방에게 게임 승리 메세지 전송
                    _ = mServer.SendToClient(opponent, "[05]WIN_BY_DISCONNECT");

                    mManager.DeleteActiveMatches(ip);
                    AppendGameLog($"[게임 포기] {ip} 게임 화면 닫음으로 {opponent} 승리");
                }
            }
        }

        private async void StartGameLoop(GameSession session)
        {
            AppendGameLog($"[게임 시작] {session.PlayerLeft} : {session.PlayerRight}");

            await Task.Run(async () =>
            {
                while (!session.GameState.IsGameOver)
                {
                    session.GameState.UpdateGameState();

                    string packet = session.GameState.GetGameStatePacket();

                    await mServer.SendToClient(session.PlayerLeft, packet);
                    await mServer.SendToClient(session.PlayerRight, packet);

                    await Task.Delay(50);
                }

                // 게임 종료
                string winner = session.GameState.WinnerIP;
                string loser = session.GameState.LoserIP;
                await mServer.SendToClient(winner, "[05]WIN");
                await mServer.SendToClient(loser, "[05]LOSER");

                AppendGameLog($"[게임 종료] {winner} WIN : LOSE {loser}");

                mManager.DeleteActiveMatches(winner);
            });
        }


        // 유저 로그 라벨에 출력
        private void AppendClientLog(string msg)
        {
            if (textBoxClientLog.InvokeRequired)
            {
                this.Invoke(new Action(() => textBoxClientLog.AppendText(msg + Environment.NewLine)));
            }
            else
            {
                textBoxClientLog.AppendText(msg + Environment.NewLine);
            }
        }

        // 게임 로그 라벨에 출력
        private void AppendGameLog(string msg)
        {
            if (textBoxGameLog.InvokeRequired)
            {
                this.Invoke(new Action(() => textBoxGameLog.AppendText(msg + Environment.NewLine)));
            }
            else
            {
                textBoxGameLog.AppendText(msg + Environment.NewLine);
            }
        }
    }
}