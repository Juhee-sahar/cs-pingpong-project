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


            UpdateUiConnectedState(false, "");


        }



        // UI Ȱ��/��Ȱ��ȭ
        private void UpdateUiConnectedState(bool connected, string state)
        {
            if (state == "")
            {
                BtnServerConnect.Enabled = !connected;
                textBoxIP.Enabled = !connected;
                textBoxPortNum.Enabled = !connected;
                BtnGameStart.Enabled = connected;
            }
            else if (state == "game")
            {
                BtnServerConnect.Enabled = !connected;
                textBoxIP.Enabled = !connected;
                textBoxPortNum.Enabled = !connected;
                BtnGameStart.Enabled = !connected;
            }

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
                UpdateUiConnectedState(true, "");
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
            UpdateUiConnectedState(true, "game");
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


                        // ���� �˾� ��ġ ����
                        mGameForm.StartPosition = FormStartPosition.Manual;
                        // Form1(FingFong) �����ʿ� �ٷ� ���̱�
                        int newX = this.Location.X + this.Width;  // ������ �� ��ǥ
                        int newY = this.Location.Y;               // ���� ����
                        mGameForm.Location = new Point(newX, newY);


                        // Game ���� ���� �� �̺�Ʈ ����
                        mGameForm.FormClosed += GameForm_FormClosed;

                        mGameForm.Show();
                    }));
            }


        }


        // Game �� ���� ����
        private async void GameForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            // UI�� ���� ���·� �ǵ���
            UpdateUiConnectedState(true, "");

            // ������ �˸�
            await mClient.SendData("[03]GIVEUP");
        }


        private void OnDisconnected()
        {
            this.Invoke(new Action(() =>
            {
                MessageBox.Show("���� ������ ���������ϴ�.");
                UpdateUiConnectedState(false, "");
            }));
        }
    }
}