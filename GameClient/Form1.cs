using GameSocket;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameClient
{
    public partial class FingFong : Form
    {
        private readonly TCPSocketClient mClient;
        private Game? mGameForm;

        public FingFong()
        {
            InitializeComponent();

            mClient = new TCPSocketClient();

            // Ŭ���̾�Ʈ �̺�Ʈ ����
            mClient.MessageReceived += OnMessageReceived;
            mClient.Disconnected += OnDisconnected;


            UpdateUiConnectedState(false);


        }



        // UI Ȱ��/��Ȱ��ȭ
        private void UpdateUiConnectedState(bool connected)
        {
            BtnServerConnect.Enabled = !connected;
            textBoxIP.Enabled = !connected;
            textBoxPortNum.Enabled = !connected;
            BtnGameStart.Enabled = connected;
        }


        // ���� ���� ��ư Ŭ��
        private async void BtnServerConnect_Click(object sender, EventArgs e)
        {
            // �Է°� �б�
            string ip = (textBoxIP.Text ?? string.Empty).Trim();
            string port = (textBoxPortNum.Text ?? string.Empty).Trim();

            // IP / Port ����
            if (!mClient.SetServerIPAddress(ip) || !mClient.SetServerPort(port))
            {
                MessageBox.Show("�߸��� IP �Ǵ� Port�Դϴ�.", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // �õ�: ���� ���� �� �ܼ� �˾�(��û����)
            try
            {
                await mClient.ConnectToServerAsync();
                UpdateUiConnectedState(true);
                MessageBox.Show("������ ����Ǿ����ϴ�.", "���� ����", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                // ���� �����ϸ� "���� ����" �˾��� ��� (���α׷� ���� X)
                MessageBox.Show("���� ����", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // ���� ���� ��ư Ŭ��
        private async void BtnGameStart_Click(object sender, EventArgs e)
        {
            // ������ ��Ī ��û
            await mClient.SendData("[01]");
        }


        private void OnMessageReceived(string msg)
        {
            // "MATCHED|LEFT" "MATCHED|RIGHT"
            if (msg.Contains("MATCHED"))
            {
                this.Invoke(new Action(() =>
                    {
                        bool isLeft = msg.Contains("LEFT");

                        mGameForm = new Game(mClient, isLeft);
                        mGameForm.Show();
                    }));
            }


        }
        private void OnDisconnected()
        {
            this.Invoke(new Action(() =>
            {
                MessageBox.Show("���� ������ ���������ϴ�.");
                UpdateUiConnectedState(false);
            }));
        }
    }
}
