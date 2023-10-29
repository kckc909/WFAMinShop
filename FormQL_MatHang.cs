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
    public partial class FormQL_MatHang : Form
    {
        public FormQL_MatHang()
        {
            InitializeComponent();
        }
        // khái báo biến
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        SqlDataReader dr;
        DataTable table;
        string link = "Data Source=DESKTOP-H22E3HT;Initial Catalog=MinShopWFA;Integrated Security=True";

        private void Ketnoi()
        {
            conn = new SqlConnection(link);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        private void Ngatketnoi()
        {
            conn = new SqlConnection(link);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void FormThemMatHang_Load(object sender, EventArgs e)
        {
            Ketnoi();

            LoadData_PhiKetNoi();

            adapter = new SqlDataAdapter("Select * from LoaiHang", conn);
            table = new DataTable();
            adapter.Fill(table);
            cboMaLoai.DataSource = table;
            cboMaLoai.DisplayMember = "LH_Name";
            cboMaLoai.ValueMember = "LH_ID";
            cboMaLoai.SelectedIndex = -1;
            Ngatketnoi();
        }

        private void FormThemMatHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            Ngatketnoi();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (kiemtratontai(txtMaMH.Text))
            {
                string sql = $"Delete from MatHang where MH_Id = '{txtMaMH.Text}'";
                DialogResult c =  MessageBox.Show($"Bạn có muốn xóa mặt hàng không?", "Hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (c == DialogResult.Yes)
                {
                    ThucThiCommand(sql);
                    MessageBox.Show($"Xóa thành công!", "Thông báo");
                    DialogResult = DialogResult.OK;
                }
            }
            else
            {
                MessageBox.Show($"Mặt hàng không tồn tại!!!", "Lỗi",MessageBoxButtons.OK , MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cboMaLoai.SelectedIndex = -1;
            txtMaMH.Clear();
            txtTenMH.Clear();
            txtMoTa.Clear();
            txtDonVi.Clear();
            txtSoLg.Clear();
            txtMaMH.ReadOnly = false;

            Ketnoi();
            DataView dv;
            adapter = new SqlDataAdapter("Select * from MatHang", conn);
            table = new DataTable();
            adapter.Fill(table);

            dv = table.DefaultView;
            dgvMatHang.DataSource = dv;
            Ngatketnoi();
        }

        private void LoadData_PhiKetNoi()
        {
            adapter = new SqlDataAdapter("select * from MatHang", conn);
            table = new DataTable();
            table.Clear();
            adapter.Fill(table);
            dgvMatHang.DataSource = table;

            Doi_Ten_Cot_DGV();
        }

        private void Doi_Ten_Cot_DGV()
        {
            dgvMatHang.Columns[0].HeaderText = "Mã MH";
            dgvMatHang.Columns[1].HeaderText = "Tên MH";
            dgvMatHang.Columns[2].HeaderText = "Mô tả";
            dgvMatHang.Columns[3].HeaderText = "Số lượng";
            dgvMatHang.Columns[4].HeaderText = "Giá nhập";
            dgvMatHang.Columns[4].DefaultCellStyle.Format = "#,###";
            dgvMatHang.Columns[5].HeaderText = "Giá bán";
            dgvMatHang.Columns[5].DefaultCellStyle.Format = "#,###";
            dgvMatHang.Columns[6].HeaderText = "Đơn vị";
            dgvMatHang.Columns[7].HeaderText = "Mã Loại";
        }

        private void dgvMatHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Lấy chỉ số hàng
            int r = e.RowIndex;
            if (!(r < 0))
            {
                txtMaMH.Text = dgvMatHang[0, r].Value.ToString();
                txtTenMH.Text = dgvMatHang[1, r].Value.ToString();
                txtMoTa.Text = dgvMatHang[2, r].Value.ToString();
                txtSoLg.Text = dgvMatHang[3, r].Value.ToString();
                txtGiaNhap.Text = dgvMatHang[4, r].Value.ToString();
                txtGiaBan.Text = dgvMatHang[5, r].Value.ToString();
                txtDonVi.Text = dgvMatHang[6, r].Value.ToString();
                cboMaLoai.SelectedValue = dgvMatHang[7, r].Value.ToString();

                txtMaMH.ReadOnly = true;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!Check_Null())
            {
                MessageBox.Show("Dữ liệu bị trống hoặc bị sai, hãy kiểm tra lại !!!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // nhap lieu
            string MaMH = txtMaMH.Text;
            string TenMH = txtTenMH.Text;
            string MoTa = txtMoTa.Text;
            string SoLg = txtSoLg.Text;
            string GiaN = txtGiaNhap.Text;
            string donVi = txtDonVi.Text;
            string maloai = cboMaLoai.SelectedValue.ToString();
            string GiaB = txtGiaBan.Text;
            if (kiemtratontai(MaMH))
            {
                MessageBox.Show($"Mã mặt hàng đã tồn tại!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.OK;
                txtMaMH.Focus();
            }
            else
            {
                // Thay doi csdl
                string sql = $"Insert into MatHang (MH_Id, MH_Name, MH_Description, MH_Quantity, MH_Price, MH_S_Price, MH_Unit, LH_Id) " +
                    $"values (N'{MaMH}', N'{TenMH}', N'{MoTa}', {SoLg}, '{GiaN}', {GiaB}, N'{donVi}', N'{maloai}')";
                ThucThiCommand(sql);
            }
        }
        private bool kiemtratontai(string MaMH)
        {
            Ketnoi();
            string sql = $"select Count(*) from MatHang where MH_Id = N'{MaMH}'";
            cmd = new SqlCommand(sql, conn);
            int n = (int)cmd.ExecuteScalar();
            Ngatketnoi();
            if (n > 0) return true;
            else return false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if  (!Check_Null())
            {
                MessageBox.Show("Dữ liệu bị trống hoặc bị sai, hãy kiểm tra lại !!!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string MaMH = txtMaMH.Text;
            string TenMH = txtTenMH.Text;
            string MoTa = txtMoTa.Text;
            string SoLg = txtSoLg.Text;
            string Gia = txtGiaNhap.Text;
            string GiaB = txtGiaBan.Text;
            string donVi = txtDonVi.Text;
            string maloai = cboMaLoai.SelectedValue.ToString();

            string sql = $"Update MatHang " +
                             $"Set MH_Name = N'{TenMH}', " +
                             $"MH_Description = N'{MoTa}', " +
                             $"MH_Quantity = '{SoLg}', " +
                             $"MH_Price = '{Gia}', " + 
                             $"MH_S_Price = '{GiaB}', " + 
                             $"MH_Unit = N'{donVi}', " +
                             $"LH_Id = '{maloai}' " +
                         $"Where MH_Id = '{MaMH}'";
            ThucThiCommand(sql);
        }

        private void ThucThiCommand(string sql)
        {
            Ketnoi();
            cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            LoadData_PhiKetNoi();
            Ngatketnoi();
        }

        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            Ketnoi();
            DataView dv;
            adapter = new SqlDataAdapter("Select * from MatHang", conn);
            table = new DataTable();
            adapter.Fill(table);

            dv = table.DefaultView;
            dv.RowFilter = $"MH_Name like '%{textBoxSearch.Text}% or MH_Descreption like '%{textBoxSearch.Text}%'";
            dgvMatHang.DataSource = dv;
            Doi_Ten_Cot_DGV();
            Ngatketnoi();
        }
        private bool Check_Null()
        {
            int a;
            if (Equals("", txtMaMH.Text) || 
                Equals("", txtTenMH.Text) || 
                Equals("", txtMoTa.Text) ||
                Equals("", txtSoLg.Text) ||
                Equals("", txtGiaNhap.Text) ||
                Equals("", txtGiaBan.Text) ||
                Equals("", txtDonVi.Text) ||
                Equals("", cboMaLoai.Text)||
                !int.TryParse(txtSoLg.Text, out a) ||
                !int.TryParse(txtGiaNhap.Text, out a)
                )
                return false;
            return true;
        }

        private void txtSoLg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}


