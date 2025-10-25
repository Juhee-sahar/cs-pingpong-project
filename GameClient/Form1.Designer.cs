namespace GameClient
{
    partial class FingFong
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
            textBoxIP = new TextBox();
            label1 = new Label();
            label2 = new Label();
            textBoxPortNum = new TextBox();
            BtnServerConnect = new Button();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            BtnGameStart = new Button();
            SuspendLayout();
            // 
            // textBoxIP
            // 
            textBoxIP.Font = new Font("맑은 고딕", 12F);
            textBoxIP.Location = new Point(120, 13);
            textBoxIP.Margin = new Padding(4, 4, 4, 4);
            textBoxIP.Name = "textBoxIP";
            textBoxIP.Size = new Size(450, 34);
            textBoxIP.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 12F);
            label1.Location = new Point(13, 19);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(72, 28);
            label1.TabIndex = 1;
            label1.Text = "아이피";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 12F);
            label2.Location = new Point(13, 61);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(99, 28);
            label2.TabIndex = 3;
            label2.Text = "포트 번호";
            // 
            // textBoxPortNum
            // 
            textBoxPortNum.Font = new Font("맑은 고딕", 12F);
            textBoxPortNum.Location = new Point(120, 55);
            textBoxPortNum.Margin = new Padding(4, 4, 4, 4);
            textBoxPortNum.Name = "textBoxPortNum";
            textBoxPortNum.Size = new Size(450, 34);
            textBoxPortNum.TabIndex = 2;
            // 
            // BtnServerConnect
            // 
            BtnServerConnect.Font = new Font("맑은 고딕", 12F);
            BtnServerConnect.Location = new Point(13, 97);
            BtnServerConnect.Margin = new Padding(4, 4, 4, 4);
            BtnServerConnect.Name = "BtnServerConnect";
            BtnServerConnect.Size = new Size(556, 50);
            BtnServerConnect.TabIndex = 4;
            BtnServerConnect.Text = "서버 접속";
            BtnServerConnect.UseVisualStyleBackColor = true;
            BtnServerConnect.Click += BtnServerConnect_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 40F);
            label3.Location = new Point(76, 264);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(426, 89);
            label3.TabIndex = 5;
            label3.Text = "핑  퐁  게 임";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 16F);
            label4.Location = new Point(13, 388);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(200, 37);
            label4.TabIndex = 6;
            label4.Text = "핑 ~ 핑 ~ 핑 ~";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("맑은 고딕", 26F);
            label5.Location = new Point(350, 192);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(220, 60);
            label5.TabIndex = 7;
            label5.Text = "퐁 ~ 퐁 ~";
            // 
            // BtnGameStart
            // 
            BtnGameStart.Font = new Font("맑은 고딕", 12F);
            BtnGameStart.Location = new Point(13, 462);
            BtnGameStart.Margin = new Padding(4, 4, 4, 4);
            BtnGameStart.Name = "BtnGameStart";
            BtnGameStart.Size = new Size(556, 48);
            BtnGameStart.TabIndex = 8;
            BtnGameStart.Text = "게임 시작";
            BtnGameStart.UseVisualStyleBackColor = true;
            BtnGameStart.Click += BtnGameStart_Click;
            // 
            // FingFong
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 553);
            Controls.Add(BtnGameStart);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(BtnServerConnect);
            Controls.Add(label2);
            Controls.Add(textBoxPortNum);
            Controls.Add(label1);
            Controls.Add(textBoxIP);
            Margin = new Padding(4, 4, 4, 4);
            Name = "FingFong";
            Text = "Fing Fong";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxIP;
        private Label label1;
        private Label label2;
        private TextBox textBoxPortNum;
        private Button BtnServerConnect;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button BtnGameStart;
    }
}
