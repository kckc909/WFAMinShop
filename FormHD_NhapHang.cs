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
    public partial class FormHD_NhapHang : Form
    {
        public string user;
        public FormHD_NhapHang()
        {
            InitializeComponent();
        }

        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        string chuoiketnoi = "Data Source=DESKTOP-H22E3HT;Initial Catalog=MinShopWFA;Integrated Security=True";

        private void Ketnoi()
        {
            conn = new SqlConnection
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        void LamMoi()
        {
            cboNCC.SelectedIndex = -1;
            txtSL_HoanTra.Clear();
            txtSL_Them.Clear();
            txtTotal_Price.Clear();
            txtSearch.Clear();
            dgvMH_Nhap.Rows.Clear();
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void Load_Data(string NCC_Id)
        {
            da = new SqlDataAdapter("select m.MH_Id, m.MH_Name, m.MH_Description, m.MH_Quantity, m.MH_Price, m.MH_Unit " +
                "from MatHang m left join ChiTietHDN ct on m.MH_Id = ct.MH_Id left join HoaDonNhap h on h.HDN_Id = ct.HDN_Id left join NhaCungCap n on n.NCC_Id = h.NCC_Id " +
                $"where n.NCC_Id = '{NCC_Id}'", conn);
            dt = new DataTable();
            da.Fill(dt);
            dgvMH_NCC_CC.DataSource = dt;

            dgvMH_NCC_CC.Columns[0].HeaderText = "Mã MH";
            dgvMH_NCC_CC.Columns[1].HeaderText = "Tên";
            dgvMH_NCC_CC.Columns[2].HeaderText = "Mô tả";
            dgvMH_NCC_CC.Columns[3].HeaderText = "Số lượng";
            dgvMH_NCC_CC.Columns[4].HeaderText = "Giá";
            dgvMH_NCC_CC.Columns[5].HeaderText = "Đơn vị tính";
        }

        private void Load_NCC(string s)
        {
            Ketnoi();
            da = new SqlDataAdapter($"select * from NhaCungCap where NCC_Id like '%{s}%' ", conn);
            dt = new DataTable();
            da.Fill(dt);

            cboNCC.DataSource = dt;
            cboNCC.ValueMember = "NCC_Id";
            cboNCC.DisplayMember = "NCC_Name";

            Ngatketnoi();
        }

        private void FormNhapHang_Load(object sender, EventArgs e)
        {
            Ketnoi();
            da = new SqlDataAdapter("select nv.NV_Id, nv.NV_Name " +
                "from  Account_Password ac left join NhanVien nv on ac.MaDinhDanh = nv.NV_Id " +
                $"where ac.TaiKhoan = '{user}' ", conn);
            dt = new DataTable();
            da.Fill(dt);
            Ngatketnoi();

            txt_Id.Text = dt.Rows[0][0].ToString();
            txt_Name.Text = dt.Rows[0][1].ToString();
            txtDay.Text = DateTime.Now.ToShortDateString();

            dgvMH_Nhap.Columns.Add("col0", "Mã MH");
            dgvMH_Nhap.Columns.Add("col1", "Tên MH");
            dgvMH_Nhap.Columns.Add("col2", "Số lượng");
            dgvMH_Nhap.Columns.Add("col3", "Giá");
            dgvMH_Nhap.Columns[3].DefaultCellStyle.Format = "#,###";
            dgvMH_Nhap.Columns.Add("col4", "Đơn vị");
            dgvMH_Nhap.Columns.Add("col5", "Thành tiền");

            Load_NCC("");
            Load_Data("00000");
        }

        private void cboNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNCC.SelectedIndex != -1)
            {
                Load_Data(cboNCC.SelectedValue.ToString());
                textBox1.Text = cboNCC.SelectedValue.ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool check = true;
            if (dgvMH_NCC_CC.SelectedRows.Count > 0)
            {
                int solg;
                if (int.TryParse(txtSL_Them.Text, out solg))
                {
                    // Mã tên lượng giá đv
                    var r = dgvMH_NCC_CC.SelectedRows[0];
                    string id = r.Cells[0].Value.ToString();
                    string name = r.Cells[1].Value.ToString();
                    int Gia = int.Parse(r.Cells[4].Value.ToString());
                    string dv = r.Cells[5].Value.ToString();
                    foreach (DataGridViewRow row in dgvMH_Nhap.Rows)
                    {
                        if (row.Cells[0].Value is null) continue;
                        if (Equals(row.Cells[0].Value.ToString(), id))
                        {
                            solg += int.Parse(row.Cells[2].Value.ToString());
                            row.Cells[2].Value = solg;
                            row.Cells[5].Value = int.Parse(row.Cells[3].Value.ToString()) * solg;
                            check = false;
                            break;
                        }
                    }

                    if (check)
                    {
                        dgvMH_Nhap.Rows.Add(id, name, solg, Gia, dv, solg * Gia);
                    }
                }
                else
                {
                    MessageBox.Show("Số lượng nhập không hợp lệ!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show($"Chưa chọn mặt hàng nào!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.OK;
            }
            TongTien();
        }

        private void btnHoanTra_Click(object sender, EventArgs e)
        {
            if (dgvMH_Nhap.SelectedRows.Count > 0)
            {
                if (dgvMH_Nhap.SelectedRows[0].Cells[0].Value == null) return;
                int solg = int.Parse(dgvMH_Nhap.SelectedRows[0].Cells[2].Value.ToString());
                int hoantra;
                if (int.TryParse(txtSL_HoanTra.Text, out hoantra))
                {
                    if (solg > hoantra)
                    {
                        solg -= hoantra;
                        dgvMH_Nhap.SelectedRows[0].Cells[2].Value = solg;
                        dgvMH_Nhap.SelectedRows[0].Cells[5].Value = int.Parse(dgvMH_Nhap.SelectedRows[0].Cells[3].Value.ToString()) * solg;
                    }
                    else
                    {
                        dgvMH_Nhap.Rows.Remove(dgvMH_Nhap.SelectedRows[0]);
                    }
                }
                else
                {
                    MessageBox.Show("Số lượng hoàn trả không hợp lệ!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn mặt hàng nào!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            TongTien();
        }
        private void TongTien()
        {
            int tong = 0;
            foreach (DataGridViewRow r in dgvMH_Nhap.Rows)
            {
                if (r.Cells[0].Value != null)
                {
                    tong += int.Parse(r.Cells[2].Value.ToString()) * int.Parse(r.Cells[3].Value.ToString());
                }
            }
            txtTotal_Price.Text = tong.ToString();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            string sql;

            sql = "select top (1) HDN_Id from HoaDonNhap order by HDN_Id desc ";
            string MaHDN = TaoMaNoiTiep(ExScalar(sql));
            string NV_Id = txt_Id.Text;
            if (cboNCC.SelectedValue == null)
            {
                MessageBox.Show("Chưa chọn nhà cung cấp nào !", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            string NCC_Id = cboNCC.SelectedValue.ToString();
            string NgayNhap = txtDay.Text;

            // Tạo hóa đơn
            HoaDon hd = new HoaDon() 
            {
                MaHD = MaHDN, MaNv = NV_Id, MaKH = NCC_Id, Ngay = NgayNhap
            };
            List<ThongTinHoaDon> lst = new List<ThongTinHoaDon>();

            sql = $"INSERT INTO HoaDonNhap (HDN_Id, NV_Id, NCC_Id, NgayNhap) VALUES ('{MaHDN}', '{NV_Id}', '{NCC_Id}', '{NgayNhap}')";
            ExNonQuery(sql);

            foreach (DataGridViewRow row in dgvMH_Nhap.Rows)
            {
                if (row.Cells[0].Value is null) continue;

                string MaMH = row.Cells[0].Value.ToString();
                string Solg = row.Cells[2].Value.ToString();
                string Gia = row.Cells[3].Value.ToString();
                string donvi = row.Cells[4].Value.ToString();

                ThongTinHoaDon tt = new ThongTinHoaDon() { 
                    MaMH = MaMH, Solg = int.Parse(Solg), GiaBan = int.Parse(Gia), 
                    TenMH = ExScalar($"select MH_Name from MatHang where MH_Id = '{MaMH}'") 
                };
                lst.Add(tt);

                // Ghi vào Chi tiết hóa đơn nhập
                sql = $"INSERT INTO ChiTietHDN (HDN_Id, MH_Id, Quantity, Price, Unit) VALUES ('{MaHDN}', '{MaMH}', '{Solg}', '{Gia}', '{donvi}')";
                ExNonQuery(sql);

                // Tăng số lượng mặt hàng trong kho
                sql = $"Update MatHang set MH_Quantity += {Solg} where MH_Id = '{MaMH}' ";
                ExNonQuery(sql);
            }

            hd.lst = lst;
            hd.ShowDialog();
            MessageBox.Show("Hóa đơn đã được lưu!", "Thông báo", MessageBoxButtons.OK);
        }
        private string TaoMaNoiTiep(string macu)
        {
            int i = int.Parse(macu.Substring(3)) + 1;
            return macu.Substring(0, 3) + $"{i}";
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Load_NCC(textBox1.Text);
            }
        }
        private string ExScalar(string sql)
        {
            Ketnoi();
            cmd = new SqlCommand(sql, conn);
            string r = cmd.ExecuteScalar().ToString();
            Ngatketnoi();
            return r;
        }
        private void ExNonQuery(string sql)
        {
            Ketnoi();
            cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            Ngatketnoi();
        }
    }
}
