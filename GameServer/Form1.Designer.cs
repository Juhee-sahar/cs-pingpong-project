namespace GameServer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnConnectServer = new Button();
            btnDisconnectServer = new Button();
            label1 = new Label();
            label2 = new Label();
            textBoxClientLog = new TextBox();
            textBoxGameLog = new TextBox();
            SuspendLayout();
            // 
            // btnConnectServer
            // 
            btnConnectServer.Location = new Point(21, 11);
            btnConnectServer.Margin = new Padding(4);
            btnConnectServer.Name = "btnConnectServer";
            btnConnectServer.Size = new Size(475, 55);
            btnConnectServer.TabIndex = 0;
            btnConnectServer.Text = "서버 연결";
            btnConnectServer.UseVisualStyleBackColor = true;
            btnConnectServer.Click += BtnAcceptIncoming_Click;
            // 
            // btnDisconnectServer
            // 
            btnDisconnectServer.Location = new Point(504, 11);
            btnDisconnectServer.Margin = new Padding(4);
            btnDisconnectServer.Name = "btnDisconnectServer";
            btnDisconnectServer.Size = new Size(465, 55);
            btnDisconnectServer.TabIndex = 1;
            btnDisconnectServer.Text = "연결 해제";
            btnDisconnectServer.UseVisualStyleBackColor = true;
            btnDisconnectServer.Click += BtnDisconnectServer_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 78);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(74, 20);
            label1.TabIndex = 2;
            label1.Text = "유저 로그";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(352, 78);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(74, 20);
            label2.TabIndex = 4;
            label2.Text = "게임 로그";
            // 
            // textBoxClientLog
            // 
            textBoxClientLog.BackColor = Color.Black;
            textBoxClientLog.ForeColor = Color.Lime;
            textBoxClientLog.Location = new Point(12, 96);
            textBoxClientLog.Multiline = true;
            textBoxClientLog.Name = "textBoxClientLog";
            textBoxClientLog.ReadOnly = true;
            textBoxClientLog.ScrollBars = ScrollBars.Vertical;
            textBoxClientLog.Size = new Size(323, 576);
            textBoxClientLog.TabIndex = 6;
            // 
            // textBoxGameLog
            // 
            textBoxGameLog.BackColor = Color.Black;
            textBoxGameLog.ForeColor = Color.Lime;
            textBoxGameLog.Location = new Point(352, 96);
            textBoxGameLog.Multiline = true;
            textBoxGameLog.Name = "textBoxGameLog";
            textBoxGameLog.ReadOnly = true;
            textBoxGameLog.ScrollBars = ScrollBars.Vertical;
            textBoxGameLog.Size = new Size(617, 576);
            textBoxGameLog.TabIndex = 7;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 693);
            Controls.Add(textBoxGameLog);
            Controls.Add(textBoxClientLog);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnDisconnectServer);
            Controls.Add(btnConnectServer);
            Margin = new Padding(4);
            Name = "Form1";
            Text = "Main Server";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnConnectServer;
        private Button btnDisconnectServer;
        private Label label1;
        private Label label2;
        private TextBox textBoxClientLog;
        private TextBox textBoxGameLog;
    }
}
