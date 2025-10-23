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
            textBoxIP.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 129);
            textBoxIP.Location = new Point(97, 15);
            textBoxIP.Name = "textBoxIP";
            textBoxIP.Size = new Size(338, 43);
            textBoxIP.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 16F);
            label1.Location = new Point(12, 21);
            label1.Name = "label1";
            label1.Size = new Size(79, 30);
            label1.TabIndex = 1;
            label1.Text = "아이피";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 16F);
            label2.Location = new Point(455, 21);
            label2.Name = "label2";
            label2.Size = new Size(109, 30);
            label2.TabIndex = 3;
            label2.Text = "포트 번호";
            // 
            // textBoxPortNum
            // 
            textBoxPortNum.Font = new Font("맑은 고딕", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 129);
            textBoxPortNum.Location = new Point(570, 15);
            textBoxPortNum.Name = "textBoxPortNum";
            textBoxPortNum.Size = new Size(152, 43);
            textBoxPortNum.TabIndex = 2;
            // 
            // BtnServerConnect
            // 
            BtnServerConnect.Font = new Font("맑은 고딕", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            BtnServerConnect.Location = new Point(753, 15);
            BtnServerConnect.Name = "BtnServerConnect";
            BtnServerConnect.Size = new Size(219, 43);
            BtnServerConnect.TabIndex = 4;
            BtnServerConnect.Text = "서버 접속";
            BtnServerConnect.UseVisualStyleBackColor = true;
            BtnServerConnect.Click += BtnServerConnect_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 40F);
            label3.Location = new Point(375, 207);
            label3.Name = "label3";
            label3.Size = new Size(252, 72);
            label3.TabIndex = 5;
            label3.Text = "핑~ 퐁 ~";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 26F);
            label4.Location = new Point(45, 299);
            label4.Name = "label4";
            label4.Size = new Size(260, 47);
            label4.TabIndex = 6;
            label4.Text = "핑 ~ 핑 ~ 핑 ~";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("맑은 고딕", 26F);
            label5.Location = new Point(666, 180);
            label5.Name = "label5";
            label5.Size = new Size(176, 47);
            label5.TabIndex = 7;
            label5.Text = "퐁 ~ 퐁 ~";
            // 
            // BtnGameStart
            // 
            BtnGameStart.Font = new Font("맑은 고딕", 20F);
            BtnGameStart.Location = new Point(275, 460);
            BtnGameStart.Name = "BtnGameStart";
            BtnGameStart.Size = new Size(447, 65);
            BtnGameStart.TabIndex = 8;
            BtnGameStart.Text = "게임 시작";
            BtnGameStart.UseVisualStyleBackColor = true;
            BtnGameStart.Click += BtnGameStart_Click;
            // 
            // FingFong
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(BtnGameStart);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(BtnServerConnect);
            Controls.Add(label2);
            Controls.Add(textBoxPortNum);
            Controls.Add(label1);
            Controls.Add(textBoxIP);
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
