namespace GameClient
{
    partial class Game
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            playerLeft = new PictureBox();
            playerRight = new PictureBox();
            ball = new PictureBox();
            GameTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)playerLeft).BeginInit();
            ((System.ComponentModel.ISupportInitialize)playerRight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ball).BeginInit();
            SuspendLayout();
            // 
            // playerLeft
            // 
            playerLeft.Image = Properties.Resources.player;
            playerLeft.Location = new Point(13, 133);
            playerLeft.Margin = new Padding(4);
            playerLeft.Name = "playerLeft";
            playerLeft.Size = new Size(40, 160);
            playerLeft.SizeMode = PictureBoxSizeMode.StretchImage;
            playerLeft.TabIndex = 0;
            playerLeft.TabStop = false;
            // 
            // playerRight
            // 
            playerRight.Image = Properties.Resources.someone;
            playerRight.Location = new Point(730, 133);
            playerRight.Margin = new Padding(4);
            playerRight.Name = "playerRight";
            playerRight.Size = new Size(40, 160);
            playerRight.SizeMode = PictureBoxSizeMode.StretchImage;
            playerRight.TabIndex = 1;
            playerRight.TabStop = false;
            playerRight.Click += playerRight_Click;
            // 
            // ball
            // 
            ball.Image = Properties.Resources.ball;
            ball.Location = new Point(387, 188);
            ball.Margin = new Padding(4);
            ball.Name = "ball";
            ball.Size = new Size(40, 40);
            ball.SizeMode = PictureBoxSizeMode.StretchImage;
            ball.TabIndex = 2;
            ball.TabStop = false;
            // 
            // GameTimer
            // 
            GameTimer.Enabled = true;
            GameTimer.Interval = 20;
            GameTimer.Tick += GameTimerEvent;
            // 
            // Game
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(782, 433);
            Controls.Add(ball);
            Controls.Add(playerRight);
            Controls.Add(playerLeft);
            DoubleBuffered = true;
            Margin = new Padding(4);
            Name = "Game";
            Text = "Me 0 : 0 Someone";
            KeyDown += KeyIsDown;
            KeyUp += KeyIsUp;
            ((System.ComponentModel.ISupportInitialize)playerLeft).EndInit();
            ((System.ComponentModel.ISupportInitialize)playerRight).EndInit();
            ((System.ComponentModel.ISupportInitialize)ball).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox playerLeft;
        private PictureBox playerRight;
        private PictureBox ball;
        private System.Windows.Forms.Timer GameTimer;
    }
}