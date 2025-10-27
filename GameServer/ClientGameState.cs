using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace GameServer
{
    internal class ClientGameState
    {
        public string PlayerLeftIP { get; private set; }
        public string PlayerRightIP { get; private set; }

        private int screenWidth = 620;
        private int screenHeight = 340;

        private int paddleWidth = 40;
        private int paddleHeight = 160;
        private int paddleOffset = 9; // 창과의 거리
        private int paddleSpeed = 10;

        private int ballSize = 40; // 공의 지름 (대략 값, 필요 시 조정)
        private int ballX;
        private int ballY;
        private int ballXSpeed;
        private int ballYSpeed;

        private int playerLeftY;
        private int playerRightY;

        private int playerLeftScore;
        private int playerRightScore;

        private Random rand = new Random();

        public bool IsGameOver { get; private set; } = false;
        public string WinnerIP { get; private set; } = string.Empty;
        public string LoserIP { get; private set; } = string.Empty;


        public ClientGameState(string playerLeft, string playerRight)
        {
            PlayerLeftIP = playerLeft;
            PlayerRightIP = playerRight;

            ResetGame();
        }


        // 게임 초기화
        private void ResetGame()
        {
            playerLeftY = screenHeight / 2 - paddleHeight / 2;
            playerRightY = screenHeight / 2 - paddleHeight / 2;

            ballX = screenWidth / 2;
            ballY = screenHeight / 2;

            ballXSpeed = rand.Next(0, 2) == 0 ? 4 : -4;
            ballYSpeed = rand.Next(0, 2) == 0 ? 4 : -4;

            playerLeftScore = 0;
            playerRightScore = 0;

            IsGameOver = false;

            WinnerIP = string.Empty;
            LoserIP = string.Empty;
        }

        // 클라이언트 입력 반영
        public void HandlePlayerMove(string playerIP, string direction)
        {
            if (playerIP == PlayerLeftIP)
            {
                if (direction == "UP" && playerLeftY > 0)
                    playerLeftY -= paddleSpeed;
                else if (direction == "DOWN" && playerLeftY + paddleHeight < screenHeight)
                    playerLeftY += paddleSpeed;
            }
            else if (playerIP == PlayerRightIP)
            {
                if (direction == "UP" && playerRightY > 0)
                    playerRightY -= paddleSpeed;
                else if (direction == "DOWN" && playerRightY + paddleHeight < screenHeight)
                    playerRightY += paddleSpeed;
            }
        }

        // 한 프레임(틱)마다 업데아투
        public void UpdateGameState()
        {
            if (IsGameOver) return;

            int nextBallX = ballX + ballXSpeed;
            int nextBallY = ballY + ballYSpeed;

            int leftPaddleX = paddleOffset;
            int leftPaddleRight = leftPaddleX + paddleWidth;
            int rightPaddleX = screenWidth - paddleOffset - paddleWidth;

            int ballCenterX = ballX + ballSize / 2;
            int ballCenterY = ballY + ballSize / 2;
            int nextBallCenterX = nextBallX + ballSize / 2;
            int nextBallCenterY = nextBallY + ballSize / 2;

            // ==== 왼쪽 패들 예측 충돌 ====
            if (nextBallCenterX - ballSize / 2 <= leftPaddleRight &&
                ballCenterY >= playerLeftY &&
                ballCenterY <= playerLeftY + paddleHeight)
            {
                ballXSpeed = Math.Abs(ballXSpeed);
                RandomizeBallAngle();
            }

            // ==== 오른쪽 패들 예측 충돌 ====
            if (nextBallCenterX + ballSize / 2 >= rightPaddleX &&
                ballCenterY >= playerRightY &&
                ballCenterY <= playerRightY + paddleHeight)
            {
                ballXSpeed = -Math.Abs(ballXSpeed);
                RandomizeBallAngle();
            }

            // ==== 위치 갱신 (이제 이동) ====
            ballX = nextBallX;
            ballY = nextBallY;

            // ==== 벽 충돌 ====
            if (ballY <= 0 || ballY + ballSize >= screenHeight)
                ballYSpeed = -ballYSpeed;

            // ==== 득점 체크 ====
            if (ballX + ballSize < 0)
            {
                playerRightScore++;
                CheckGameEnd();
                ResetBall();
            }
            else if (ballX > screenWidth)
            {
                playerLeftScore++;
                CheckGameEnd();
                ResetBall();
            }
        }

        private void RandomizeBallAngle()
        {
            int[] variations = { 5, 6, 8, 9, 10 };
            int yChange = variations[rand.Next(variations.Length)];
            if (ballYSpeed < 0) ballYSpeed = -yChange;
            else ballYSpeed = yChange;
        }

        private void ResetBall()
        {
            ballX = screenWidth / 2;
            ballY = screenHeight / 2;
            ballXSpeed = rand.Next(0, 2) == 0 ? 4 : -4;
            ballYSpeed = rand.Next(0, 2) == 0 ? 4 : -4;
        }

        private void CheckGameEnd()
        {
            if (playerLeftScore >= 5)
            {
                IsGameOver = true;
                WinnerIP = PlayerLeftIP;
                LoserIP = PlayerRightIP;
            }
            else if (playerRightScore >= 5)
            {
                IsGameOver = true;
                WinnerIP = PlayerRightIP;
                LoserIP = PlayerLeftIP;
            }
        }

        // 클라이언트 전송용 문자열 생성
        public string GetGameStatePacket()
        {
            // 예: [04]STATE|ballX,ballY,leftY,rightY,leftScore,rightScore
            return $"[04]STATE|{ballX},{ballY},{playerLeftY},{playerRightY},{playerLeftScore},{playerRightScore}";
        }
    }
}
