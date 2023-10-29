using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace WFAMinShop
{
    public partial class FormDangNhap : Form
    {
        public FormDangNhap()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        SqlCommand cmd;
        string chuoiketnoi = "Data Source=DESKTOP-H22E3HT;Initial Catalog=MinShopWFA;Integrated Security=True";

        private void Ketnoi()
        {
            conn = new SqlConnection();
            conn.ConnectionString = chuoiketnoi;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }
        void ngatketnoi()
        {
            conn=new SqlConnection(chuoiketnoi);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        private void btnDn_Click(object sender, EventArgs e)
        {
            Ketnoi();
            cmd = new SqlCommand();
            cmd.CommandText = "Author_Login";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@Taikhoan", txtAC.Text);
            cmd.Parameters.AddWithValue("@Matkhau", txtPW.Text);
            int i = (int)cmd.ExecuteScalar();   // Thực thi store procedure
            ngatketnoi();

            if (i != -1)
            {
                FormMinShop _F = new FormMinShop
                {
                    code = i,
                    user = txtAC.Text
                };
                _F.Show();
                Close();
            }
            else
            {
                MessageBox.Show($"Sai tên đăng nhập hoặc mật khẩu",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkboxShowPW_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxShowPW.Checked)
            {
                txtPW.PasswordChar = '\0';
            }
            else
            {
                txtPW.PasswordChar = '*';
            }
        }

        private void txtAC_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAC.Text))
            {
                lbER_TK.Visible = true;
            }
            else
            {
                lbER_TK.Visible = false;
            }
        }

        private void txtPW_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPW.Text))
            {
                lbER_MK.Visible = true;
            }
            else
            {
                lbER_MK.Visible = false;
            }
        }

        private void FormDangNhap_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 1)
            {
                Application.Exit();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            FormDangKy _F = new FormDangKy();
            _F.Show();
            Close();
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Underline);
            label4.ForeColor = Color.FromArgb(142, 142, 142);
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
            label4.ForeColor = Color.FromArgb(142, 142, 142);
        }
    }
}
