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
            labelClientLog = new Label();
            labelGameLog = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // btnConnectServer
            // 
            btnConnectServer.Location = new Point(12, 8);
            btnConnectServer.Name = "btnConnectServer";
            btnConnectServer.Size = new Size(382, 41);
            btnConnectServer.TabIndex = 0;
            btnConnectServer.Text = "서버 연결";
            btnConnectServer.UseVisualStyleBackColor = true;
            btnConnectServer.Click += BtnAcceptIncoming_Click;
            // 
            // btnDisconnectServer
            // 
            btnDisconnectServer.Location = new Point(400, 8);
            btnDisconnectServer.Name = "btnDisconnectServer";
            btnDisconnectServer.Size = new Size(372, 41);
            btnDisconnectServer.TabIndex = 1;
            btnDisconnectServer.Text = "연결 해제";
            btnDisconnectServer.UseVisualStyleBackColor = true;
            btnDisconnectServer.Click += BtnDisconnectServer_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 70);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 2;
            label1.Text = "유저 로그";
            // 
            // labelClientLog
            // 
            labelClientLog.BorderStyle = BorderStyle.FixedSingle;
            labelClientLog.Location = new Point(12, 85);
            labelClientLog.Name = "labelClientLog";
            labelClientLog.Size = new Size(228, 656);
            labelClientLog.TabIndex = 3;
            // 
            // labelGameLog
            // 
            labelGameLog.BorderStyle = BorderStyle.FixedSingle;
            labelGameLog.Location = new Point(263, 85);
            labelGameLog.Name = "labelGameLog";
            labelGameLog.Size = new Size(509, 656);
            labelGameLog.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(263, 70);
            label2.Name = "label2";
            label2.Size = new Size(59, 15);
            label2.TabIndex = 4;
            label2.Text = "게임 로그";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 761);
            Controls.Add(labelGameLog);
            Controls.Add(label2);
            Controls.Add(labelClientLog);
            Controls.Add(label1);
            Controls.Add(btnDisconnectServer);
            Controls.Add(btnConnectServer);
            Name = "Form1";
            Text = "Main Server";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnConnectServer;
        private Button btnDisconnectServer;
        private Label label1;
        private Label labelClientLog;
        private Label labelGameLog;
        private Label label2;
    }
}
