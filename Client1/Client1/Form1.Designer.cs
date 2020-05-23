namespace Client1
{
    partial class Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_Wrt = new System.Windows.Forms.TextBox();
            this.btn_Send = new System.Windows.Forms.Button();
            this.txt_Show = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txt_Wrt
            // 
            this.txt_Wrt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Wrt.Location = new System.Drawing.Point(2, 540);
            this.txt_Wrt.Multiline = true;
            this.txt_Wrt.Name = "txt_Wrt";
            this.txt_Wrt.Size = new System.Drawing.Size(331, 65);
            this.txt_Wrt.TabIndex = 0;
            this.txt_Wrt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Wrt_KeyDown);
            // 
            // btn_Send
            // 
            this.btn_Send.BackColor = System.Drawing.Color.Yellow;
            this.btn_Send.FlatAppearance.BorderSize = 0;
            this.btn_Send.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Send.Font = new System.Drawing.Font("새굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Send.Location = new System.Drawing.Point(332, 540);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(72, 65);
            this.btn_Send.TabIndex = 1;
            this.btn_Send.Text = "전송";
            this.btn_Send.UseVisualStyleBackColor = false;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // txt_Show
            // 
            this.txt_Show.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.txt_Show.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Show.Font = new System.Drawing.Font("배달의민족 도현", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_Show.Location = new System.Drawing.Point(12, 31);
            this.txt_Show.Name = "txt_Show";
            this.txt_Show.ReadOnly = true;
            this.txt_Show.Size = new System.Drawing.Size(380, 491);
            this.txt_Show.TabIndex = 2;
            this.txt_Show.Text = "";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(404, 612);
            this.Controls.Add(this.txt_Show);
            this.Controls.Add(this.btn_Send);
            this.Controls.Add(this.txt_Wrt);
            this.DoubleBuffered = true;
            this.Name = "Main";
            this.Text = "Chatting";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Wrt;
        private System.Windows.Forms.Button btn_Send;
        private System.Windows.Forms.RichTextBox txt_Show;
    }
}

