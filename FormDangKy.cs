using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFAMinShop
{
    public partial class FormDangKy : Form
    {
        public bool _DangKyHo = false;
        public FormDangKy()
        {
            InitializeComponent();
        }

        SqlConnection conn;
        SqlCommand cmd;
        string chuoiketnoi = "Data Source=DESKTOP-H22E3HT;Initial Catalog=MinShopWFA;Integrated Security=True";

        string sdt = "";

        private void Ketnoi()
        {
            conn = new SqlConnection()
            {
                ConnectionString = chuoiketnoi
            };
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }
        private void Ngatketnoi()
        {
            conn = new SqlConnection(chuoiketnoi);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string MaNV = txtMa.Text.Trim().ToUpper();
            string Tk = txtTaiKhoan.Text;
            string Mk = txtMatKhau.Text;
            if (KiemTraTonTaiUser(MaNV, out sdt))
            {
                if (Equals(sdt, txtSDT.Text))
                {
                    if (Equals(Tk, ""))
                    {
                        MessageBox.Show("Tên tài khoản rỗng!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTaiKhoan.Focus();
                        return;
                    }
                    if (KiemTraTaiKhoan(Tk))
                    {
                        if (Mk.Trim().Length > 1)
                        {
                            if (Equals(Mk, txtNhapLaiMK.Text))
                            {
                                if (KiemTraUserDaCoTaiKhoanChua(MaNV))
                                {
                                    ThemTaiKhoan(Tk, Mk, MaNV);
                                    MessageBox.Show("Thêm tài khoản thành công!!","Thông báo", MessageBoxButtons.OK);
                                    
                                }
                                else
                                {
                                    // Tạo lại tài khoản
                                    DialogResult c = MessageBox.Show("Bạn đã có tài khoản! Bạn có muốn tạo lại tài khoản không?",
                                         "Lỗi",
                                         MessageBoxButtons.YesNoCancel,
                                         MessageBoxIcon.Question);
                                    if (c == DialogResult.Yes)
                                    {
                                        SuaTaiKhoan(Tk, Mk, MaNV);
                                        MessageBox.Show("Tạo lại tài khoản thành công!!", "Thông báo");
                                    }
                                }
                                if (!_DangKyHo)
                                {
                                    labelDN_Click(sender, e);
                                }
                            }
                            else
                            {
                                // Báo lỗi Phải giống mật khẩu
                                MessageBox.Show("Nhập lại mật khẩu phải giống mật khẩu!!!", "Lỗi", MessageBoxButtons.OK);
                                txtNhapLaiMK.Focus();
                            }
                        }
                        else
                        {
                            // Mật khẩu phải dài hơn 6 kí tự
                            MessageBox.Show("Mật khẩu phải dài hơn 1 kí tự!!", "Lỗi", MessageBoxButtons.OK);
                            txtMatKhau.Focus();
                        }
                    }
                    else
                    {
                        // Tên tài khoản đã tồn tại
                        MessageBox.Show("Tên tài khoản đã tồn tại, vui lòng nhập tên tài khoản khác!",
                                "Lỗi",
                                MessageBoxButtons.OK);
                        txtTaiKhoan.Focus();
                    }
                }
                else
                {
                    // Số điện thoại không đúng
                    MessageBox.Show("Số điện thoại của bạn không đúng!",
                                "Lỗi",
                                MessageBoxButtons.OK);
                    txtSDT.Focus();
                }
            }
            else
            {
                // Bạn phải là nhân viên mới có thể đăng ký
                MessageBox.Show("Mã nhân viên không tồn tại, bạn phải là nhân viên mới có thể đăng ký!",
                                "Lỗi",
                                MessageBoxButtons.OK);
                txtMa.Focus();
            }
        }
        private void ThemTaiKhoan(string Tk, string Mk, string MaNV)
        {
            Ketnoi();

            cmd = new SqlCommand("insert into Account_Password (TaiKhoan, MatKhau, Quyen, MaDinhDanh) " +
                                $"Values ('{Tk}', '{Mk}', '1', '{MaNV}') ", conn);
            cmd.ExecuteNonQuery();

            Ngatketnoi();
        }

        private void SuaTaiKhoan(string Tk, string Mk, string MaNV)
        {
            Ketnoi();

            string sql = $"update Account_Password set TaiKhoan = '{Tk}', MatKhau = '{Mk}' where MaDinhDanh = '{MaNV}'";

            cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            Ngatketnoi();
        }

        private bool KiemTraTonTaiUser(string ma, out string sdt)
        {
            Ketnoi();
            sdt = " ";
            ma = ma.Trim().ToUpper();
            cmd = new SqlCommand($"select NV_Id, NV_Name, NV_PhoneNumbers from NhanVien", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            txtTen.Clear();
            while (dr.Read())
            {
                string id = dr["NV_Id"].ToString();
                if (Equals(id, ma))
                {
                    txtTen.BackColor = Color.White;
                    txtTen.Text = dr["NV_Name"].ToString();
                    sdt = dr["NV_PhoneNumbers"].ToString();
                    return true;
                }
            }
            Ngatketnoi();
            txtTen.Text = "Không tồn tại nhân viên này!";
            txtTen.BackColor = Color.FromArgb(255, 80, 0);
            return false;
        }
        private bool KiemTraTaiKhoan(string tk)
        {
            Ketnoi();
            cmd = new SqlCommand($"select count (*) from Account_Password where TaiKhoan = '{tk}' ", conn);
            Ngatketnoi();
            int i = (int)cmd.ExecuteScalar();
            if (i == 0)
            {
                // Chưa có ai có tài khoản giống
                return true;
            }
            // Đã có người dùng tài khoản này
            return false;
        }
        private bool KiemTraUserDaCoTaiKhoanChua(string ma)
        {
            Ketnoi();
            cmd = new SqlCommand($"select count (*) from Account_Password where MaDinhDanh = '{ma}'", conn);
            int i = (int)cmd.ExecuteScalar();
            Ngatketnoi();
            if (i == 0)
            {
                // Chưa có tài khoản
                return true;
            }
            // Đã có tài khoản
            return false;
        }

        private void txtMa_Leave(object sender, EventArgs e)
        {
            KiemTraTonTaiUser(txtMa.Text, out sdt);
        }

        private void FormDangKy_Load(object sender, EventArgs e)
        {
            txtTen.BackColor = Color.White;
            if (_DangKyHo)
            {
                label5.Visible = false;
                labelDN.Visible = false;
            }
            else
            {
                label5.Visible = true;
                labelDN.Visible = true;
            }
        }

        private void FormDangKy_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 1)
            {
                Application.Exit();
            }
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            labelDN.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Underline);
            labelDN.ForeColor = Color.FromArgb(100, 100, 100);
        }

        private void labelDN_MouseLeave(object sender, EventArgs e)
        {
            labelDN.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
            labelDN.ForeColor = Color.FromArgb(142, 142, 142);
        }

        private void labelDN_Click(object sender, EventArgs e)
        {
            FormDangNhap _F = new FormDangNhap();
            _F.Show();
            Close();
        }
    }
}
