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

            // 클라이언트 이벤트 연결
            mClient.MessageReceived += OnMessageReceived;
            mClient.Disconnected += OnDisconnected;


            UpdateUiConnectedState(false);


        }



        // UI 활성/비활성화
        private void UpdateUiConnectedState(bool connected)
        {
            BtnServerConnect.Enabled = !connected;
            textBoxIP.Enabled = !connected;
            textBoxPortNum.Enabled = !connected;
            BtnGameStart.Enabled = connected;
        }


        // 서버 연결 버튼 클릭
        private async void BtnServerConnect_Click(object sender, EventArgs e)
        {
            // 입력값 읽기
            string ip = (textBoxIP.Text ?? string.Empty).Trim();
            string port = (textBoxPortNum.Text ?? string.Empty).Trim();

            // IP / Port 검증
            if (!mClient.SetServerIPAddress(ip) || !mClient.SetServerPort(port))
            {
                MessageBox.Show("잘못된 IP 또는 Port입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 시도: 연결 실패 시 단순 팝업(요청사항)
            try
            {
                await mClient.ConnectToServerAsync();
                UpdateUiConnectedState(true);
                MessageBox.Show("서버에 연결되었습니다.", "연결 성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                // 연결 실패하면 "연결 실패" 팝업만 띄움 (프로그램 종료 X)
                MessageBox.Show("연결 실패", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // 게임 시작 버튼 클릭
        private async void BtnGameStart_Click(object sender, EventArgs e)
        {
            // 서버에 매칭 요청
            await mClient.SendData("[01]");
        }


        private void OnMessageReceived(string msg)
        {
            if (msg.Contains("MATCHED"))
            {
                this.Invoke(new Action(() => 
                    {
                        mGameForm = new Game(mClient);
                        mGameForm.Show();
                    }));
            }
            else if (msg.Contains("WIN_BY_DISCONNECT"))
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("상대방 연결 끊김. 승리!", "게임 종료");
                    mGameForm?.Close();
                }));
            }

        }
        private void OnDisconnected()
        {
            this.Invoke(new Action(() =>
            {
                MessageBox.Show("서버 연결이 끊어졌습니다.");
                UpdateUiConnectedState(false);
            }));
        }

    }
}
