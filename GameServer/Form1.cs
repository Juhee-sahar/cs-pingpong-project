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

            await mServer.StartServerListeningAsync();
        }

        // ���� ���� ���� ��ư
        private void BtnDisconnectServer_Click(object sender, EventArgs e)
        {
            mServer.StopServer();
            AppendGameLog("���� ������.");
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
                    _ = mServer.SendToClient(opponent, "[02]WIN_BY_DISCONNECT");

                    mManager.DeleteActiveMatches(ip);
                    AppendGameLog($"[{ip}] ���� ����� {opponent} �¸�");
                }
            }));
        }


        // Ŭ���̾�Ʈ �޼���
        private void OnClientMessageReceived(string ip, string msg)
        {
            string code = msg.Substring(0, 4); // [01], [02], [03]


            // ���ӽ��� ��û
            if (code == "[01]")
            {
                AppendGameLog($"��Ī ��û : [{ip}]");
                // ���� ���� ��û�ϰ�,, ��Ī �� ������ ���
                var (p1, p2) = mManager.HandleActiveMatches(ip);

                // ���� ���� �˸�, �α�
                if(p1 !=null && p2 != null)
                {
                    _ = mServer.SendToClient(p1, "MATCHED");
                    _ = mServer.SendToClient(p2, "MATCHED");

                    AppendGameLog($"��Ī �Ϸ� : [{p1}], [{p2}]");
                }
            }
            // ���� ���������� ��
            else if (code == "[02]")
            {
                // ���� �¸� ��ǥ �޼��� ������ �޼��� ���� ��
                string opponent = mManager.HandleSearchOpponent(ip);
                
                if (opponent != "")
                {
                    _ = mServer.SendToClient(opponent, msg);
                    mManager.DeleteActiveMatches(ip);

                    AppendGameLog($"[{ip}] �� [{opponent}] ��");
                }
            }
            else if (code == "[03]")
            {
                // ���� ��� ����
                string opponent = mManager.HandleSearchOpponent(ip);

                // ���� ���� ��
                if (opponent != "")
                {
                    // ��� Ŭ���̾�Ʈ���� ���� ����
                    _ = mServer.SendToClient(opponent, msg);
                }

            }

        }


        // ���� �α� �󺧿� ���
        private void AppendClientLog(string msg)
        {
            labelClientLog.Text += msg + Environment.NewLine;
        }

        // ���� �α� �󺧿� ���
        private void AppendGameLog(string msg)
        {
            labelGameLog.Text += msg + Environment.NewLine;
        }
    }
}

