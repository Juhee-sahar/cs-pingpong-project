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

            // �̺�Ʈ ����
            mServer.ClientConnected += OnClientConnected;
            mServer.ClientDisconnected += OnClientDisconnected;
            mServer.ClientMessageReceived += OnClientMessageReceived;

            // �ʱ� UI
            UpdateUiConnectedState(false);

        }


        // UI Ȱ��/��Ȱ��ȭ
        private void UpdateUiConnectedState(bool connected)
        {
            btnConnectServer.Enabled = !connected;
            btnDisconnectServer.Enabled = connected;
        }


        // ���� ���� ��ư
        private async void BtnAcceptIncoming_Click(object sender, System.EventArgs e)
        {
            UpdateUiConnectedState(true);
            AppendGameLog("[���� ����]");
            await mServer.StartServerListeningAsync();
        }

        // ���� ���� ���� ��ư
        private void BtnDisconnectServer_Click(object sender, EventArgs e)
        {
            mServer.StopServer();
            AppendGameLog("[���� ����]");
            UpdateUiConnectedState(false);
        }

        // Ŭ���̾�Ʈ ���� ����
        private void OnClientConnected(string ip)
        {
            this.Invoke(new Action(() =>
            {
                AppendClientLog($"[����] {ip}");
            }));
        }

        // Ŭ���̾�Ʈ ���� ����
        private void OnClientDisconnected(string ip)
        {
            this.Invoke(new Action(() =>
            {
                Console.WriteLine("��������");
                AppendClientLog($"[���� ����] {ip}");

                // �������� ����� ��
                string opponent = mManager.HandleSearchOpponent(ip);
                if (opponent != "")
                {
                    // ���濡�� ���� �¸� �޼��� ����
                    _ = mServer.SendToClient(opponent, "[05]WIN_BY_DISCONNECT");

                    mManager.DeleteActiveMatches(ip);
                    AppendGameLog($"[���� ����] {ip} ���� ����� {opponent} �¸�");
                }
            }));
        }


        // Ŭ���̾�Ʈ �޼���
        private void OnClientMessageReceived(string ip, string msg)
        {
            string code = msg.Substring(0, 4); // [01], [02], [03], [04] ...


            // [01] ��Ī ��û
            if (code == "[01]")
            {
                AppendGameLog($"[��Ī ��û] {ip}");
                // ���� ���� ��û�ϰ�,, ��Ī �� ������ ���
                var (p1, p2, session) = mManager.HandleActiveMatches(ip);

                // ���� ���� �˸�, �α�
                if (p1 != null && p2 != null && session != null)
                {
                    _ = mServer.SendToClient(p1, "MATCHED|LEFT");
                    _ = mServer.SendToClient(p2, "MATCHED|RIGHT");

                    AppendGameLog($"[��Ī �Ϸ�] {p1} - {p2}");

                    // ���� ����
                    StartGameLoop(session);
                }
            }
            // [02] �÷��̾� �Է� (UP/DOWN)
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
            // [03] �÷��̾� ����
            else if (code == "[03]")
            {
                // "[03]GIVEUP"
                string opponent = mManager.HandleSearchOpponent(ip);
                if (opponent != "")
                {
                    // ���濡�� ���� �¸� �޼��� ����
                    _ = mServer.SendToClient(opponent, "[05]WIN_BY_DISCONNECT");

                    mManager.DeleteActiveMatches(ip);
                    AppendGameLog($"[���� ����] {ip} ���� ȭ�� �������� {opponent} �¸�");
                }
            }
        }

        private async void StartGameLoop(GameSession session)
        {
            AppendGameLog($"[���� ����] {session.PlayerLeft} : {session.PlayerRight}");

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

                // ���� ����
                string winner = session.GameState.WinnerIP;
                string loser = session.GameState.LoserIP;
                await mServer.SendToClient(winner, "[05]WIN");
                await mServer.SendToClient(loser, "[05]LOSER");

                AppendGameLog($"[���� ����] {winner} WIN : LOSE {loser}");

                mManager.DeleteActiveMatches(winner);
            });
        }


        // ���� �α� �󺧿� ���
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

        // ���� �α� �󺧿� ���
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