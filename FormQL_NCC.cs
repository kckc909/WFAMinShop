using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFAMinShop
{
    public partial class FormQL_NCC : Form
    {
        public FormQL_NCC()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        SqlDataAdapter da;
        DataTable dt;
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

        private void Ngatketnoi()
        {
            conn=new SqlConnection(chuoiketnoi);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        private void FormQLNCC_Load(object sender, EventArgs e)
        {
            Load_();
        }
        private void Load_()
        {
            Ketnoi();
            da = new SqlDataAdapter(
                "Select * from NhaCungCap",
                conn);
            dt = new DataTable();
            da.Fill(dt);
            dgvNCC.DataSource = dt;
            Ngatketnoi();

            dgvNCC.Columns[0].HeaderText = "Mã NCNC";
            dgvNCC.Columns[1].HeaderText = "Tên NCC";
            dgvNCC.Columns[2].HeaderText = "Số điện thoại";
            dgvNCC.Columns[3].HeaderText = "Địa chỉ";
        }
        private bool KiemTraTonTai (string NCC_ID)
        {
            Ketnoi();
            cmd = new SqlCommand($"select Count(*) from NhaCungCap where NCC_Id = '{NCC_ID}'", conn);
            int i = (int)cmd.ExecuteScalar();
            Ngatketnoi();
            return i > 0;
        }

        bool KiemTraRong()
        {
            if (txtMaNCC.Text == ""
                || txtTenNCC.Text == ""
                || txtDiaChi.Text == ""
                || txtSDT.Text == "")
                return false;
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string NCC_Id = txtMaNCC.Text.Trim();
            string NCC_Name = txtTenNCC.Text.Trim();
            string NCC_PhoneNumbers = txtSDT.Text.Trim();
            string NCC_Address = txtDiaChi.Text.Trim();
            if (!KiemTraTonTai(NCC_Id))
            {
                if (!KiemTraRong())
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK);
                    return;
                }
                string sql = $"Insert Into NhaCungCap " +
                             $"Values ('{NCC_Id}', N'{NCC_Name}', N'{NCC_PhoneNumbers}', N'{NCC_Address}')";
                Excute_SQLCommand(sql);
            }
            else
            {
                MessageBox.Show($"Mã nhà cung cấp đã tồn tại!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Excute_SQLCommand(string sql)
        {
            Ketnoi();
            cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            Load_();
            Ngatketnoi();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string NCC_Id = txtMaNCC.Text.Trim();
            string NCC_Name = txtTenNCC.Text.Trim();
            string NCC_PhoneNumbers = txtSDT.Text.Trim();
            string NCC_Address = txtDiaChi.Text.Trim();

            if (!KiemTraRong())
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK);
                return;
            }

            string sql = $"Update NhaCungCap " +
                         $"Set NCC_Name = N'{NCC_Name}'," +
                             $"NCC_PhoneNumbers = '{NCC_PhoneNumbers}'," +
                             $"NCC_Address = N'{NCC_Address}' " +
                         $"Where NCC_Id = '{NCC_Id}'";

            Excute_SQLCommand(sql);
        }

        int Scalar(string sql)
        {
            Ketnoi();
            cmd = new SqlCommand(sql, conn);
            int r = (int)cmd.ExecuteScalar();
            Ngatketnoi();
            return r;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (KiemTraTonTai(txtMaNCC.Text))
            {
                DialogResult c = MessageBox.Show("Bạn có muốn xóa nhà cung cấp không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question); 
                if (c == DialogResult.Yes)
                {
                    if (Scalar($"select count(*) from NhaCungCap k inner join HoaDonNhap h on k.NCC_Id = h.NCC_Id where k.NCC_Id = '{txtMaNCC.Text}'") > 0)
                    {
                        MessageBox.Show("Không thể xóa nhà cung cấp đã cung cấp hàng cho cửa hàng!!", "Lỗi", MessageBoxButtons.OK);
                        return;
                    }
                    string sql = $"delete NhaCungCap where NCC_Id = '{txtMaNCC.Text}'";
                    Excute_SQLCommand(sql);
                }
            }
            else
            {
                MessageBox.Show("Không tồn tại nhà cung cấp!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenNCC.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            txtMaNCC.Clear();
            txtMaNCC.ReadOnly = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            Ketnoi();
            da = new SqlDataAdapter(
                $"Select * from NhaCungCap where NCC_Name like N'%{textBoxSearch.Text}%'",
                conn);
            dt = new DataTable();
            da.Fill(dt);
            dgvNCC.DataSource = dt;
            Ngatketnoi();

            dgvNCC.Columns[0].HeaderText = "Mã NCNC";
            dgvNCC.Columns[1].HeaderText = "Tên NCC";
            dgvNCC.Columns[2].HeaderText = "Số điện thoại";
            dgvNCC.Columns[3].HeaderText = "Địa chỉ";
        }

        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            if (row >= 0)
            {
                txtMaNCC.Text = dgvNCC[0, row].Value.ToString();
                txtTenNCC.Text = dgvNCC[1, row].Value.ToString();
                txtSDT.Text = dgvNCC[2, row].Value.ToString();
                txtDiaChi.Text = dgvNCC[3, row].Value.ToString();
                txtMaNCC.ReadOnly = true;
            }
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {

            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            if (txtSDT.Text.Length > 10 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
