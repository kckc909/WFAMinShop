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
    public partial class FormHD_BanHang : Form
    {
        public string user;
        public FormHD_BanHang()
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
            conn=new SqlConnection(chuoiketnoi);
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
            txtSearch.Clear();
            txtTotal_Price.Text = "0";
            dgvMH_Ban.Rows.Clear();
            TimKiem();
            txtMaKH.Clear();
            Load_KhachHang();
            cboKH.SelectedIndex = 0;
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }
        private void Load_Data()
        {
            Ketnoi();
            da = new SqlDataAdapter("select MH_Id, MH_Name, MH_Description, MH_Quantity, MH_S_Price, MH_Unit " +
                                    "from MatHang", conn);
            dt = new DataTable();
            da.Fill(dt);
            dgvMH_Kho.DataSource = dt;

            DoiTenCot_Kho();

            Ngatketnoi();
        }
        private void Load_KhachHang(string s = "")
        {
            Ketnoi();
            da = new SqlDataAdapter($"Select KH_Id, KH_Name from KhachHang where KH_Id like '%{s}%' ", conn);
            dt = new DataTable();
            da.Fill(dt);

            cboKH.DataSource = dt;
            cboKH.ValueMember = "KH_Id";
            cboKH.DisplayMember = "KH_Name";

            Ngatketnoi();
        }

        private void FormBanHang_Load(object sender, EventArgs e)
        {
            txtTotal_Price.Text = "0";
            Load_Data();
            Load_KhachHang();

            Ketnoi();
            da = new SqlDataAdapter("select nv.NV_Id, nv.NV_Name " +
                "from  Account_Password ac left join NhanVien nv on ac.MaDinhDanh = nv.NV_Id " +
                $"where ac.TaiKhoan = '{user}' ", conn);
            dt = new DataTable();
            da.Fill(dt);
            Ngatketnoi();

            txtNV_Id.Text = dt.Rows[0][0].ToString();
            txtNV_Name.Text = dt.Rows[0][1].ToString();
            txtDate.Text = DateTime.Now.ToShortDateString();

            DoiTenCot_Ban();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool check = true;
            if (dgvMH_Kho.SelectedRows.Count > 0)
            {
                int solg;
                if (int.TryParse(txtSL_Them.Text, out solg))
                {
                    var r = dgvMH_Kho.SelectedRows[0];
                    string id = r.Cells[0].Value.ToString();
                    string name = r.Cells[1].Value.ToString();
                    int soluongton = int.Parse(r.Cells[3].Value.ToString());
                    int Gia = int.Parse(r.Cells[4].Value.ToString());
                    string dv = r.Cells[5].Value.ToString();

                    foreach (DataGridViewRow row in dgvMH_Ban.Rows)
                    {
                        if (row.Cells[0].Value == null) continue;
                        int solgmoi = solg + int.Parse(row.Cells[2].Value.ToString());
                        if (Equals(row.Cells[0].Value.ToString(), id))
                        {
                            if (solgmoi > soluongton)
                            {
                                MessageBox.Show("Số lượng lớn hơn số lượng có!!!");
                                check = false; 
                                break;
                            }
                            row.Cells[2].Value = solgmoi;
                            row.Cells[5].Value = int.Parse(row.Cells[3].Value.ToString()) * int.Parse(row.Cells[2].Value.ToString());
                            check = false;
                            break;
                        }
                    }
                    if (check)
                    {
                        dgvMH_Ban.Rows.Add(id, name, solg, Gia, dv, solg * Gia);
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

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvMH_Ban.SelectedRows.Count > 0)
            {
                if (dgvMH_Ban.SelectedRows[0].Cells[0].Value == null) return;
                int solg;
                if (int.TryParse(dgvMH_Ban.SelectedRows[0].Cells[2].Value.ToString(), out solg))
                {
                    int hoantra = int.Parse(txtSL_HoanTra.Text);
                    if (solg > hoantra)
                    {
                        solg -= hoantra;
                        dgvMH_Ban.SelectedRows[0].Cells[2].Value = solg ;
                        dgvMH_Ban.SelectedRows[0].Cells[5].Value = solg * int.Parse(dgvMH_Ban.SelectedRows[0].Cells[3].Value.ToString());
                    }
                    else
                    {
                        dgvMH_Ban.Rows.Remove(dgvMH_Ban.SelectedRows[0]);
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            TimKiem(txtSearch.Text);
        }
        private void TimKiem(string s = "")
        {
            Ketnoi();
            da = new SqlDataAdapter("select MH_Id, MH_Name, MH_Description, MH_Quantity, MH_S_Price, MH_Unit " +
                                    "from MatHang " +
                                    $"where MH_Name like N'%{s}%' or MH_Id like N'%{s}%' ", conn);
            dt = new DataTable();
            da.Fill(dt);
            dgvMH_Kho.DataSource = dt;

            DoiTenCot_Kho();

            Ngatketnoi();
        }

        void DoiTenCot_Kho()
        {
            dgvMH_Kho.Columns[0].HeaderText = "ID";
            dgvMH_Kho.Columns[1].HeaderText = "Tên";
            dgvMH_Kho.Columns[2].HeaderText = "Mô tả";
            dgvMH_Kho.Columns[3].HeaderText = "Số lượng";
            dgvMH_Kho.Columns[4].HeaderText = "Giá bán";
            dgvMH_Kho.Columns[4].DefaultCellStyle.Format = "#,###";
            dgvMH_Kho.Columns[5].HeaderText = "Đơn vị tính";
        }
        void DoiTenCot_Ban()
        {
            dgvMH_Ban.Columns.Add("col0", "Mã MH");
            dgvMH_Ban.Columns.Add("col1", "Tên MH");
            dgvMH_Ban.Columns.Add("col2", "Số lượng");
            dgvMH_Ban.Columns.Add("col3", "Giá bán");
            dgvMH_Ban.Columns[3].DefaultCellStyle.Format = "#,###";
            dgvMH_Ban.Columns.Add("col4", "Đơn vị");
            dgvMH_Ban.Columns.Add("col5", "Thành tiền");
        }
        // Thêm Hóa đơn bán và tạo chỉ tiết của hóa đơn bán đó
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            string sql = "select top (1) HDB_Id from HoaDonBan order by HDB_Id desc ";
            string MaHDB = TaoMaNoiTiep(Scalar(sql));
            string NV_Id = txtNV_Id.Text;
            if (cboKH.SelectedValue == null)
            {
                MessageBox.Show("Chưa chọn khách hàng !", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            string KH_Id = cboKH.SelectedValue.ToString();
            string NgayBan = txtDate.Text;
            if (dgvMH_Ban.Rows.Count < 1)
            {
                MessageBox.Show("Chưa mua mặt hàng nào!", "Lỗi", MessageBoxButtons.OK);
                return;
            }
            HienThiHoaDon(MaHDB, NV_Id, KH_Id, NgayBan);
            LamMoi();
            LuuHoaDon(MaHDB, NV_Id, KH_Id, NgayBan);
            MessageBox.Show("Hóa đơn đã được lưu!", "Thông báo", MessageBoxButtons.OK);
        }
        void LuuHoaDon(string MaHDB, string NV_Id,string KH_Id,string NgayBan)
        {
            string sql = $"INSERT INTO HoaDonBan (HDB_Id, NV_Id, KH_Id, NgayBan) VALUES ('{MaHDB}', '{NV_Id}', '{KH_Id}', '{NgayBan}')";
            ExNonQuery(sql);

            foreach (DataGridViewRow row in dgvMH_Ban.Rows)
            {
                if (row.Cells[0].Value is null) continue;

                string MaMH = row.Cells[0].Value.ToString();
                string Solg = row.Cells[2].Value.ToString();
                string Gia = row.Cells[3].Value.ToString();
                string donvi = row.Cells[4].Value.ToString();

                // Ghi vào Chi tiết hóa đơn bán
                sql = $"INSERT INTO ChiTietHDB (HDB_Id, MH_Id, Quantity, Price, Unit) VALUES ('{MaHDB}', '{MaMH}', '{Solg}', '{Gia}', '{donvi}')";
                ExNonQuery(sql);

                // Giảm số lượng mặt hàng trong kho
                sql = $"Update MatHang set MH_Quantity -= {Solg} where MH_Id = '{MaMH}' ";
                ExNonQuery(sql);
            }
        }
        void HienThiHoaDon(string MaHDB, string NV_Id, string KH_Id, string NgayBan)
        {
            // Tạo hóa đơn ảo
            HoaDon hd = new HoaDon()
            {
                MaHD = MaHDB,
                MaNv = NV_Id,
                MaKH = KH_Id,
                Ngay = NgayBan
            };
            List<ThongTinHoaDon> lst = new List<ThongTinHoaDon>();

            foreach (DataGridViewRow row in dgvMH_Ban.Rows)
            {
                if (row.Cells[0].Value is null) continue;

                string MaMH = row.Cells[0].Value.ToString();
                string Solg = row.Cells[2].Value.ToString();
                string Gia = row.Cells[3].Value.ToString();

                // Thêm thông tin cho hóa đơn
                ThongTinHoaDon tt = new ThongTinHoaDon();
                tt.MaMH = MaMH;
                tt.Solg = int.Parse(Solg);
                tt.GiaBan = int.Parse(Gia);
                tt.TenMH = Scalar($"select MH_Name from MatHang where MH_Id = '{MaMH}'");
                lst.Add(tt);
            }
            hd.lst = lst;
            hd.ShowDialog();
        }
        private string TaoMaNoiTiep (string macu)
        {
            int i = int.Parse(macu.Substring(3)) + 1;
            return macu.Substring(0, 3) + $"{i}";
        }
        private void TongTien()
        {
            int tong = 0;
            foreach (DataGridViewRow r in dgvMH_Ban.Rows)
            {
                if (r.Cells[0].Value != null)
                {
                    tong += int.Parse(r.Cells[2].Value.ToString()) * int.Parse(r.Cells[3].Value.ToString());
                }
            }
            txtTotal_Price.Text = tong.ToString();
        }

        private void cboKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKH.SelectedValue != null)
                txtMaKH.Text = cboKH.SelectedValue.ToString() ;
        }

        private void txtMaKH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Load_KhachHang(txtMaKH.Text);
            }
        }
        private string Scalar(string s)
        {
            Ketnoi();
            cmd = new SqlCommand(s, conn);
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
