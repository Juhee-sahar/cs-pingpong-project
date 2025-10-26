using GameSocket;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GameClient
{
    public partial class Game : Form
    {
        private readonly TCPSocketClient mClient;
        private readonly bool isLeft;

        bool goDown, goUp;
        int speed = 10;

        public Game(TCPSocketClient client, bool isLeftPlayer)
        {
            InitializeComponent();
            mClient = client;
            isLeft = isLeftPlayer;

            // 서버 수신 이벤트 연결
            mClient.MessageReceived += OnServerMessage;

            this.KeyDown += KeyIsDown;
            this.KeyUp += KeyIsUp;
        }

        private async void GameTimerEvent(object sender, EventArgs e)
        {
            // 기본 이동 로직 (본인)
            MovePlayer();

            // 키 이동 시 "[02]MOVING|UP" / "[02]MOVING|DOWN" 전송
            if (goUp)
            {
                await mClient.SendData("[02]MOVING|UP");
            }
            else if (goDown)
            {
                await mClient.SendData("[02]MOVING|DOWN");
            }

        }

        private void MovePlayer()
        {
            if (isLeft)
            {
                if (goUp && playerLeft.Top > 0)
                    playerLeft.Top -= speed;
                if (goDown && playerLeft.Top < this.ClientSize.Height - playerLeft.Height)
                    playerLeft.Top += speed;
            }
            else
            {
                if (goUp && playerRight.Top > 0)
                    playerRight.Top -= speed;
                if (goDown && playerRight.Top < this.ClientSize.Height - playerRight.Height)
                    playerRight.Top += speed;
            }

        }

        private void OnServerMessage(string msg)
        {
            try
            {
                // [04]STATE|ballX,ballY,playerLeftY,playerRightY,scoreLeft,scoreRight
                if (msg.StartsWith("[04]STATE|"))
                {
                    string data = msg.Substring(10); // [04]STATE| 제외
                    string[] parts = data.Split(',');

                    if (parts.Length == 6)
                    {
                        int.TryParse(parts[0], out int ballX);
                        int.TryParse(parts[1], out int ballY);
                        int.TryParse(parts[2], out int playerLeftY);
                        int.TryParse(parts[3], out int playerRightY);
                        int.TryParse(parts[4], out int leftScore);
                        int.TryParse(parts[5], out int rightScore);

                        this.Invoke(new Action(() =>
                        {
                            ball.Left = ballX;
                            ball.Top = ballY;
                            playerLeft.Top = playerLeftY;
                            playerRight.Top = playerRightY;

                            // 점수 업데이트
                            if (isLeft)
                                this.Text = $"Me {leftScore} : {rightScore} Someone";
                            else
                                this.Text = $"Me {rightScore} : {leftScore} Someone";
                        }));
                    }

                }
                else if (msg.StartsWith("[05]"))
                {
                    if (msg.Contains("WIN_BY_DISCONNECT"))
                        this.Invoke(new Action(() => GameOver("상대방 기권으로 승리!")));
                    else if (msg.Contains("WIN"))
                        this.Invoke(new Action(() => GameOver("승리!")));
                    else if (msg.Contains("LOSER"))
                        this.Invoke(new Action(() => GameOver("패배!")));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("OnServerMessage 예외: " + ex.Message);
            }
        }

        private void GameOver(string message)
        {
            GameTimer.Stop();
            MessageBox.Show(message, "Result");
            Close();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) goDown = true;
            if (e.KeyCode == Keys.Up) goUp = true;
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) goDown = false;
            if (e.KeyCode == Keys.Up) goUp = false;
        }

        private void playerRight_Click(object sender, EventArgs e)
        {

        }
    }
}