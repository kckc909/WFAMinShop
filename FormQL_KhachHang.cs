using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace WFAMinShop
{
    public partial class FormQL_KhachHang : Form
    {
        public FormQL_KhachHang()
        {
            InitializeComponent();
        }
        
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

        private void Load_KH()
        {
            ketnoi();
            da = new SqlDataAdapter(
                "Select * from KhachHang",
                conn);
            dt = new DataTable();
            da.Fill(dt);
            dgvKH.DataSource = dt;
            ngatketnoi();

            dgvKH.Columns[0].HeaderText = "Mã KH";
            dgvKH.Columns[1].HeaderText = "Tên KH";
            dgvKH.Columns[2].HeaderText = "GT";
            dgvKH.Columns[3].HeaderText = "Số điện thoại";
            dgvKH.Columns[4].HeaderText = "Địa chỉ";
        }

        private void dgvKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            if (row >= 0)
            {
                txtMaKH.Text = dgvKH[0, row].Value.ToString();
                txtTenKH.Text = dgvKH[1, row].Value.ToString();
                cboGT.Text = dgvKH[2, row].Value.ToString();
                txtSDT.Text = dgvKH[3, row].Value.ToString();
                txtDiaChi.Text = dgvKH[4, row].Value.ToString();
                txtMaKH.ReadOnly = true;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtDiaChi.Clear();
            txtMaKH.Clear();
            txtTenKH.Clear();
            txtSDT.Clear();
            cboGT.SelectedIndex = -1;
            txtMaKH.ReadOnly = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
        private bool KiemTraTonTai(string KH_Id)
        {
            ketnoi();
            cmd = new SqlCommand($"select Count(*) from KhachHang where KH_Id = '{KH_Id}'", conn);
            int i = (int)cmd.ExecuteScalar();
            ngatketnoi();
            return i > 0;
        }
        private void Excute_SQLCommand(string sql)
        {
            ketnoi();
            cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            Load_KH();
            ngatketnoi();
        }

        bool KiemTraRong()
        {
            if (txtMaKH.Text == "" || txtTenKH.Text == "" || cboGT.Text == "" || txtDiaChi.Text == "" || txtSDT.Text == "")
            {
                return false;
            }
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string KH_Id = txtMaKH.Text.Trim();
            string KH_Name = txtTenKH.Text.Trim();
            string KH_Sex = cboGT.Text.ToString().Trim();
            string KH_PhoneNumbers = txtSDT.Text.Trim();
            string KH_Address = txtDiaChi.Text.Trim();
            if (!KiemTraTonTai(KH_Id))
            {
                if (!KiemTraRong())
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK);
                    return;
                }
                string sql = $"Insert Into KhachHang" +
                             $"Values ('{KH_Id}', N'{KH_Name}', N'{KH_Sex}', N'{KH_PhoneNumbers}', N'{KH_Address}')";
                Excute_SQLCommand(sql);
            }
            else
            {
                MessageBox.Show($"Mã nhân viên đã tồn tại!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!KiemTraRong())
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK);
                return;
            }
            string KH_Id = txtMaKH.Text.Trim();
            string KH_Name = txtTenKH.Text.Trim();
            string KH_Sex = cboGT.Text;
            string KH_PhoneNumbers = txtSDT.Text.Trim();
            string KH_Address = txtDiaChi.Text.Trim();


            string sql = $"Update KhachHang " +
                         $"Set KH_Name = N'{KH_Name}'," +
                             $"KH_Sex = N'{KH_Sex}'," +
                             $"KH_PhoneNumbers = '{KH_PhoneNumbers}'," +
                             $"KH_Address = N'{KH_Address}' " +
                         $"Where KH_Id = '{KH_Id}'";

            Excute_SQLCommand(sql);
        }

        int Scalar(string sql)
        {
            ketnoi();
            cmd = new SqlCommand(sql, conn);
            int r = (int)cmd.ExecuteScalar();
            ngatketnoi();
            return r;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (KiemTraTonTai(txtMaKH.Text) == true)
            {
                DialogResult c = MessageBox.Show("Bạn có muốn xóa khách hàng này không!", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (c == DialogResult.Yes)
                {
                    if (Scalar($"select count(*) from KhachHang k inner join HoaDonBan h on k.KH_Id = h.KH_Id where k.KH_Id = '{txtMaKH.Text}'") > 0)
                    {
                        MessageBox.Show("Không thể xóa khách hàng đã mua hàng tại cửa hàng !!", "Lỗi", MessageBoxButtons.OK);
                        return;
                    }
                    string sql = $"Delete KhachHang " +
                        $"Where KH_Id = N'{txtMaKH.Text}'";
                    Excute_SQLCommand(sql);
                }
            }

            else
            {
                MessageBox.Show("Khách hàng không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormQLKhachHang_Load(object sender, EventArgs e)
        {
            Load_KH();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            ketnoi();
            da = new SqlDataAdapter(
                $"Select * from KhachHang where KH_Name like N'%{textBoxSearch.Text}%'",
                conn);
            dt = new DataTable();
            da.Fill(dt);
            dgvKH.DataSource = dt;
            ngatketnoi();

            dgvKH.Columns[0].HeaderText = "Mã KH";
            dgvKH.Columns[1].HeaderText = "Tên KH";
            dgvKH.Columns[2].HeaderText = "GT";
            dgvKH.Columns[3].HeaderText = "Số điện thoại";
            dgvKH.Columns[4].HeaderText = "Địa chỉ";
        }
    }
}
