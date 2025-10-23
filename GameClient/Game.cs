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

        int ballXspeed = 4;
        int ballYspeed = 4;
        int playerSpeed = 8;
        bool goDown, goUp;

        int playerScore = 0;
        int someoneScore = 0;

        public Game(TCPSocketClient client)
        {
            InitializeComponent();
            mClient = client;

            // 서버 수신 이벤트 연결
            mClient.MessageReceived += OnServerMessage;
        }

        private async void GameTimerEvent(object sender, EventArgs e)
        {
            // 기본 이동 로직 (본인)
            MoveBallAndPlayer();

            // 좌표를 서버로 전송
            string msg = $"[03]{player.Top},{player.Left},{ball.Top},{ball.Left}";
            await mClient.SendData(msg);
        }

        private void MoveBallAndPlayer()
        {
            ball.Top += ballYspeed;
            ball.Left += ballXspeed;

            if (goDown && player.Bottom < this.ClientSize.Height)
                player.Top += playerSpeed;
            if (goUp && player.Top > 0)
                player.Top -= playerSpeed;

            // 충돌, 점수처리
            if (ball.Top < 0 || ball.Bottom > this.ClientSize.Height)
                ballYspeed = -ballYspeed;

            if (ball.Left < -5)
            {
                ball.Left = 300;
                someoneScore++;
            }

            if (ball.Right > this.ClientSize.Width + 5)
            {
                ball.Left = 300;
                playerScore++;

                // 내가 5점 달성하면 승리 전송
                if (playerScore >= 5)
                {
                    _ = mClient.SendData("[02]WIN");
                    GameOver("You Win!");
                }
            }
        }

        private void OnServerMessage(string msg)
        {
            // [03]playerX,playerY,ballX,ballY 수신 시 반영
            if (msg.StartsWith("[03]"))
            {
                string data = msg.Substring(4);
                string[] parts = data.Split(',');

                if (parts.Length == 4)
                {
                    int.TryParse(parts[0], out int oppTop);
                    int.TryParse(parts[1], out int oppLeft);
                    int.TryParse(parts[2], out int ballTop);
                    int.TryParse(parts[3], out int ballLeft);

                    this.Invoke(new Action(() =>
                    {
                        someone.Top = oppTop;
                        someone.Left = oppLeft;
                        ball.Top = ballTop;
                        ball.Left = ballLeft;
                    }));
                }
            }
            else if (msg.StartsWith("[02]"))
            {
                this.Invoke(new Action(() =>
                {
                    GameOver("You Lose!");
                }));
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
    }
}
