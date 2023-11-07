using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFAMinShop
{
    public partial class FormQL_NhanVien : Form
    {
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;
        SqlConnection conn;
        string connString = $"Data Source=DESKTOP-H22E3HT;Initial Catalog=MinShopWFA;Integrated Security=True";
        void ketnoi()
        {
            conn = new SqlConnection();
            conn.ConnectionString = connString;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }
        void ngatketnoi()
        {
            conn = new SqlConnection(connString);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        public FormQL_NhanVien()
        {
            InitializeComponent();
        }

        private void FormQLNhanVien_Load(object sender, EventArgs e)
        {
            Load_NhanVien();
        }

        private void Load_NhanVien()
        {
            ketnoi();
            da = new SqlDataAdapter(
                "Select * from NhanVien",
                conn);
            dt = new DataTable();
            da.Fill(dt);
            dgvNV.DataSource = dt;
            ngatketnoi();

            dgvNV.Columns[0].HeaderText = "Mã NV";
            dgvNV.Columns[1].HeaderText = "Tên NV";
            dgvNV.Columns[2].HeaderText = "GT";
            dgvNV.Columns[3].HeaderText = "Ngày sinh";
            dgvNV.Columns[4].HeaderText = "Số điện thoại";
            dgvNV.Columns[5].HeaderText = "Địa chỉ";

            NgaySinh.MaxDate = DateTime.Now;
        }

        private void dgvNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            if (row >= 0)
            {
                txtMaNv.Text = dgvNV[0, row].Value.ToString();
                txtTenNv.Text = dgvNV[1, row].Value.ToString();
                cboGT.Text = dgvNV[2, row].Value.ToString();
                NgaySinh.Text = dgvNV[3, row].Value.ToString();
                txtSDT.Text = dgvNV[4, row].Value.ToString();
                txtDiaChi.Text = dgvNV[5, row].Value.ToString();
                txtMaNv.ReadOnly = true;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void LamMoi()
        {
            txtDiaChi.Clear();
            txtMaNv.Clear();
            txtTenNv.Clear();
            NgaySinh.Refresh();
            txtSDT.Clear();
            cboGT.SelectedIndex = -1;
            txtMaNv.ReadOnly = false;
            txtMaNv.Focus();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        bool KiemTraRong()
        {
            if (txtMaNv.Text == ""
                || txtTenNv.Text == ""
                || txtDiaChi.Text == ""
                || txtSDT.Text == ""
                || cboGT.Text == ""
                || NgaySinh.Text == "")
                return false;
            return true;
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            string NV_Id = txtMaNv.Text.Trim().ToUpper();
            string NV_Name = txtTenNv.Text.Trim();
            string NV_Sex = cboGT.Text.ToString().Trim();
            string NV_Birthday = NgaySinh.Text.Trim();
            string NV_PhoneNumbers = txtSDT.Text.Trim();
            string NV_Address = txtDiaChi.Text.Trim();
            if (!KiemTraTonTai(NV_Id))
            {
                if (!KiemTraRong())
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK);
                    return;
                }
                string sql = $"Insert Into NhanVien " +
                             $"Values ('{NV_Id}', N'{NV_Name}', N'{NV_Sex}', '{NV_Birthday}', N'{NV_PhoneNumbers}', N'{NV_Address}')";
                Excute_SQLCommand(sql);
                MessageBox.Show("Thêm thành công!!", "Thông báo", MessageBoxButtons.OK);
                LamMoi();
            }
            else
            {
                MessageBox.Show($"Mã nhân viên đã tồn tại!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool KiemTraTonTai(string NV_Id)
        {
            ketnoi();
            cmd = new SqlCommand($"select Count(*) from NhanVien where NV_Id = '{NV_Id}'", conn);
            int i = (int)cmd.ExecuteScalar();
            ngatketnoi();
            return i > 0;
        }
        private void Excute_SQLCommand(string sql)
        {
            ketnoi();
            cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            Load_NhanVien();
            ngatketnoi();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string NV_Id = txtMaNv.Text.Trim();
            string NV_Name = txtTenNv.Text.Trim();
            string NV_Sex = cboGT.Text;
            string NV_Birthday = NgaySinh.Text.Trim();
            string NV_PhoneNumbers = txtSDT.Text.Trim();
            string NV_Address = txtDiaChi.Text.Trim();
            if (!KiemTraRong())
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK);
                return;
            }
            string sql = $"Update NhanVien " +
                         $"Set NV_Name = N'{NV_Name}'," +
                             $"NV_Sex = N'{NV_Sex}'," +
                             $"NV_Birthday = '{NV_Birthday}'," +
                             $"NV_PhoneNumbers = '{NV_PhoneNumbers}'," +
                             $"NV_Address = N'{NV_Address}' " +
                         $"Where NV_Id = '{NV_Id}'";

            Excute_SQLCommand(sql);
            MessageBox.Show("Sửa thành công!!", "Thông báo", MessageBoxButtons.OK);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (KiemTraTonTai(txtMaNv.Text) == true)
            {
                DialogResult c = MessageBox.Show("Bạn có muốn xóa nhân viên này không!", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (c == DialogResult.Yes)
                {
                    string sql = $"Delete NhanVien " +
                        $"Where NV_Id = N'{txtMaNv.Text}'";
                    MessageBox.Show("Xóa thành công!!", "Thông báo", MessageBoxButtons.OK);
                    Excute_SQLCommand(sql);
                    LamMoi();
                }
            }

            else
            {
                MessageBox.Show("Nhân viên không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            ketnoi();
            da = new SqlDataAdapter(
                $"Select * from NhanVien where NV_Name like N'%{textBoxSearch.Text}%'",
                conn);
            dt = new DataTable();
            da.Fill(dt);
            dgvNV.DataSource = dt;
            ngatketnoi();

            dgvNV.Columns[0].HeaderText = "Mã NV";
            dgvNV.Columns[1].HeaderText = "Tên NV";
            dgvNV.Columns[2].HeaderText = "GT";
            dgvNV.Columns[3].HeaderText = "Ngày sinh";
            dgvNV.Columns[4].HeaderText = "Số điện thoại";
            dgvNV.Columns[5].HeaderText = "Địa chỉ";
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            if (!char.IsControl(e.KeyChar) && txtSDT.Text.Length > 10)
            {
                e.Handled = true;
            }
        }
    }
}
