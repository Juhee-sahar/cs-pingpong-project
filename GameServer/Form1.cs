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

            await mServer.StartServerListeningAsync();
        }

        // 서버 연결 해제 버튼
        private void BtnDisconnectServer_Click(object sender, EventArgs e)
        {
            mServer.StopServer();
            AppendGameLog("서버 중지됨.");
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
                    _ = mServer.SendToClient(opponent, "[02]WIN_BY_DISCONNECT");

                    mManager.DeleteActiveMatches(ip);
                    AppendGameLog($"[{ip}] 접속 종료로 {opponent} 승리");
                }
            }));
        }


        // 클라이언트 메세지
        private void OnClientMessageReceived(string ip, string msg)
        {
            string code = msg.Substring(0, 4); // [01], [02], [03]


            // 게임시작 요청
            if (code == "[01]")
            {
                AppendGameLog($"매칭 요청 : [{ip}]");
                // 게임 시작 요청하고,, 매칭 될 때까지 대기
                var (p1, p2) = mManager.HandleActiveMatches(ip);

                // 게임 시작 알림, 로그
                if(p1 !=null && p2 != null)
                {
                    _ = mServer.SendToClient(p1, "MATCHED");
                    _ = mServer.SendToClient(p2, "MATCHED");

                    AppendGameLog($"매칭 완료 : [{p1}], [{p2}]");
                }
            }
            // 게임 정상적으로 끝
            else if (code == "[02]")
            {
                // 먼저 승리 목표 달성한 곳에서 메세지 보낼 것
                string opponent = mManager.HandleSearchOpponent(ip);
                
                if (opponent != "")
                {
                    _ = mServer.SendToClient(opponent, msg);
                    mManager.DeleteActiveMatches(ip);

                    AppendGameLog($"[{ip}] 승 [{opponent}] 패");
                }
            }
            else if (code == "[03]")
            {
                // 대전 상대 정보
                string opponent = mManager.HandleSearchOpponent(ip);

                // 정보 있을 시
                if (opponent != "")
                {
                    // 상대 클라이언트에게 정보 전송
                    _ = mServer.SendToClient(opponent, msg);
                }

            }

        }


        // 유저 로그 라벨에 출력
        private void AppendClientLog(string msg)
        {
            labelClientLog.Text += msg + Environment.NewLine;
        }

        // 게임 로그 라벨에 출력
        private void AppendGameLog(string msg)
        {
            labelGameLog.Text += msg + Environment.NewLine;
        }
    }
}

