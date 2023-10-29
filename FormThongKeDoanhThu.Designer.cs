namespace WFAMinShop
{
    partial class FormThongKeDoanhThu
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboY = new System.Windows.Forms.ComboBox();
            this.cboM = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lb_TongNhap = new System.Windows.Forms.Label();
            this.lb_TongBan = new System.Windows.Forms.Label();
            this.lb_DoanhThu = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnTk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(471, 76);
            this.label1.TabIndex = 0;
            this.label1.Text = "THỐNG KÊ DOANH THU";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Năm";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(190, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tháng";
            // 
            // cboY
            // 
            this.cboY.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboY.FormattingEnabled = true;
            this.cboY.Location = new System.Drawing.Point(62, 107);
            this.cboY.Name = "cboY";
            this.cboY.Size = new System.Drawing.Size(106, 21);
            this.cboY.TabIndex = 3;
            // 
            // cboM
            // 
            this.cboM.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboM.FormattingEnabled = true;
            this.cboM.Items.AddRange(new object[] {
            "Cả năm",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cboM.Location = new System.Drawing.Point(234, 107);
            this.cboM.Name = "cboM";
            this.cboM.Size = new System.Drawing.Size(121, 21);
            this.cboM.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(100, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Tổng tiền hàng nhập : ";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(101, 219);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Tổng tiền hàng bán : ";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(100, 269);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Tiền thu về :";
            // 
            // lb_TongNhap
            // 
            this.lb_TongNhap.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lb_TongNhap.AutoSize = true;
            this.lb_TongNhap.Location = new System.Drawing.Point(252, 169);
            this.lb_TongNhap.Name = "lb_TongNhap";
            this.lb_TongNhap.Size = new System.Drawing.Size(13, 13);
            this.lb_TongNhap.TabIndex = 8;
            this.lb_TongNhap.Text = "0";
            // 
            // lb_TongBan
            // 
            this.lb_TongBan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lb_TongBan.AutoSize = true;
            this.lb_TongBan.Location = new System.Drawing.Point(252, 219);
            this.lb_TongBan.Name = "lb_TongBan";
            this.lb_TongBan.Size = new System.Drawing.Size(13, 13);
            this.lb_TongBan.TabIndex = 9;
            this.lb_TongBan.Text = "0";
            // 
            // lb_DoanhThu
            // 
            this.lb_DoanhThu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lb_DoanhThu.AutoSize = true;
            this.lb_DoanhThu.Location = new System.Drawing.Point(252, 269);
            this.lb_DoanhThu.Name = "lb_DoanhThu";
            this.lb_DoanhThu.Size = new System.Drawing.Size(13, 13);
            this.lb_DoanhThu.TabIndex = 10;
            this.lb_DoanhThu.Text = "0";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(380, 269);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "VNĐ";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(380, 219);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "VNĐ";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(380, 169);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(30, 13);
            this.label12.TabIndex = 11;
            this.label12.Text = "VNĐ";
            // 
            // btnTk
            // 
            this.btnTk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnTk.Location = new System.Drawing.Point(383, 106);
            this.btnTk.Name = "btnTk";
            this.btnTk.Size = new System.Drawing.Size(75, 21);
            this.btnTk.TabIndex = 14;
            this.btnTk.Text = "Thống kê";
            this.btnTk.UseVisualStyleBackColor = true;
            this.btnTk.Click += new System.EventHandler(this.btnTk_Click);
            // 
            // FormThongKeDoanhThu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(495, 323);
            this.Controls.Add(this.btnTk);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lb_DoanhThu);
            this.Controls.Add(this.lb_TongBan);
            this.Controls.Add(this.lb_TongNhap);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboM);
            this.Controls.Add(this.cboY);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormThongKeDoanhThu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormThongKe";
            this.Load += new System.EventHandler(this.FormThongKe_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboY;
        private System.Windows.Forms.ComboBox cboM;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lb_TongNhap;
        private System.Windows.Forms.Label lb_TongBan;
        private System.Windows.Forms.Label lb_DoanhThu;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnTk;
    }
}