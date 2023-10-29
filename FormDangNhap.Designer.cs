namespace WFAMinShop
{
    partial class FormDangNhap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDangNhap));
            this.btnDn = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.txtAC = new System.Windows.Forms.TextBox();
            this.txtPW = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkboxShowPW = new System.Windows.Forms.CheckBox();
            this.lbER_TK = new System.Windows.Forms.Label();
            this.lbER_MK = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDn
            // 
            this.btnDn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDn.Location = new System.Drawing.Point(12, 336);
            this.btnDn.Name = "btnDn";
            this.btnDn.Size = new System.Drawing.Size(100, 35);
            this.btnDn.TabIndex = 3;
            this.btnDn.Text = "Đăng nhập";
            this.btnDn.UseVisualStyleBackColor = true;
            this.btnDn.Click += new System.EventHandler(this.btnDn_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(142, 336);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(100, 35);
            this.btnThoat.TabIndex = 4;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // txtAC
            // 
            this.txtAC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAC.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAC.Location = new System.Drawing.Point(109, 202);
            this.txtAC.Name = "txtAC";
            this.txtAC.Size = new System.Drawing.Size(133, 26);
            this.txtAC.TabIndex = 0;
            this.txtAC.Leave += new System.EventHandler(this.txtAC_Leave);
            // 
            // txtPW
            // 
            this.txtPW.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPW.Location = new System.Drawing.Point(109, 257);
            this.txtPW.Name = "txtPW";
            this.txtPW.PasswordChar = '*';
            this.txtPW.Size = new System.Drawing.Size(133, 26);
            this.txtPW.TabIndex = 1;
            this.txtPW.Leave += new System.EventHandler(this.txtPW_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(244)))), ((int)(((byte)(235)))));
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 205);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "Tên tài khoản";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(244)))), ((int)(((byte)(235)))));
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 260);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "Mật khẩu";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(244)))), ((int)(((byte)(235)))));
            this.label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(61, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 31);
            this.label3.TabIndex = 7;
            this.label3.Text = "Đăng nhập";
            // 
            // checkboxShowPW
            // 
            this.checkboxShowPW.AutoSize = true;
            this.checkboxShowPW.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(244)))), ((int)(((byte)(235)))));
            this.checkboxShowPW.Location = new System.Drawing.Point(19, 304);
            this.checkboxShowPW.Name = "checkboxShowPW";
            this.checkboxShowPW.Size = new System.Drawing.Size(109, 17);
            this.checkboxShowPW.TabIndex = 2;
            this.checkboxShowPW.Text = "Hiển thị mật khẩu";
            this.checkboxShowPW.UseVisualStyleBackColor = false;
            this.checkboxShowPW.CheckedChanged += new System.EventHandler(this.checkboxShowPW_CheckedChanged);
            // 
            // lbER_TK
            // 
            this.lbER_TK.AutoSize = true;
            this.lbER_TK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(244)))), ((int)(((byte)(235)))));
            this.lbER_TK.ForeColor = System.Drawing.Color.Red;
            this.lbER_TK.Location = new System.Drawing.Point(111, 231);
            this.lbER_TK.Name = "lbER_TK";
            this.lbER_TK.Size = new System.Drawing.Size(79, 13);
            this.lbER_TK.TabIndex = 9;
            this.lbER_TK.Text = "Tài khoản rỗng";
            this.lbER_TK.Visible = false;
            // 
            // lbER_MK
            // 
            this.lbER_MK.AutoSize = true;
            this.lbER_MK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(244)))), ((int)(((byte)(235)))));
            this.lbER_MK.ForeColor = System.Drawing.Color.Red;
            this.lbER_MK.Location = new System.Drawing.Point(111, 288);
            this.lbER_MK.Name = "lbER_MK";
            this.lbER_MK.Size = new System.Drawing.Size(76, 13);
            this.lbER_MK.TabIndex = 10;
            this.lbER_MK.Text = "Mật khẩu rỗng";
            this.lbER_MK.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(244)))), ((int)(((byte)(235)))));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.label4.Location = new System.Drawing.Point(139, 389);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = " Đăng ký";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            this.label4.MouseEnter += new System.EventHandler(this.label4_MouseEnter);
            this.label4.MouseLeave += new System.EventHandler(this.label4_MouseLeave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(244)))), ((int)(((byte)(235)))));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            this.label5.Location = new System.Drawing.Point(39, 389);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Chưa có tài khoản?";
            // 
            // FormDangNhap
            // 
            this.AcceptButton = this.btnDn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btnThoat;
            this.ClientSize = new System.Drawing.Size(734, 411);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbER_MK);
            this.Controls.Add(this.lbER_TK);
            this.Controls.Add(this.checkboxShowPW);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPW);
            this.Controls.Add(this.txtAC);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnDn);
            this.MaximizeBox = false;
            this.Name = "FormDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng Nhập";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormDangNhap_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDn;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.TextBox txtAC;
        private System.Windows.Forms.TextBox txtPW;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkboxShowPW;
        private System.Windows.Forms.Label lbER_MK;
        private System.Windows.Forms.Label lbER_TK;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

