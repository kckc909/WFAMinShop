using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFAMinShop
{
    public partial class FormTK_HoaDon : Form
    {
        public FormTK_HoaDon()
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

        private void Ngatketnoi()
        {
            conn=new SqlConnection(chuoiketnoi);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        void Load_HDBan(string bd, string kt)
        {
            Ketnoi();
            string sql = $"select * from HoaDonBan where NgayBan >= '{bd}' and NgayBan <= '{kt}'";
            cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            Ngatketnoi();

            while (reader.Read())
            {
                dgv.Rows.Add(reader[0], reader[1], reader[2], reader[3]);
            }   
        }

        void Load_HDNhap(string bd, string kt)
        {
            Ketnoi();
            string sql = $"select * from HoaDonNhap where NgayNhap >= '{bd}' and NgayNhap <= '{kt}'";
            cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            Ngatketnoi();

            while (reader.Read())
            {
                dgv.Rows.Add(reader[0], reader[1], reader[2], reader[3]);
            }
        }

        void _Load()
        {
            dgv.Rows.Clear(); 
            if (cbo.SelectedIndex == 0)
            {
                Load_HDBan(timeS.Value.ToShortDateString(), timeE.Value.ToShortDateString());
                Load_HDNhap(timeS.Value.ToShortDateString(), timeE.Value.ToShortDateString());
            }
            else if (cbo.SelectedIndex == 1)
            {
                Load_HDBan(timeS.Value.ToShortDateString(), timeE.Value.ToShortDateString());
            }
            else if (cbo.SelectedIndex == 2)
            {
                Load_HDNhap(timeS.Value.ToShortDateString(), timeE.Value.ToShortDateString());
            }
        }

        private void FormTK_HoaDon_Load(object sender, EventArgs e)
        {

            dgv.Columns.Add("c0", "Mã HD");
            dgv.Columns.Add("c1", "Nhân viên");
            dgv.Columns.Add("c2", "Khách hàng");
            dgv.Columns.Add("c3", "Ngày");

            timeE.MaxDate = DateTime.Now;
            timeS.MinDate = new DateTime(year: 2023, month: 1, day: 1);

            timeS.Value = timeS.MinDate;
            cbo.SelectedIndex = 0;

            _Load();
        }

        private void timeS_ValueChanged(object sender, EventArgs e)
        {
            _Load();
            timeE.MinDate = timeS.Value;
        }

        private void timeE_ValueChanged(object sender, EventArgs e)
        {
            _Load();
            timeS.MaxDate = timeE.Value;
        }

        private void dgv_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (dgv[0, r].Value is null || r < 0)
            {
                return;
            }
            string MaHD = dgv[0, r].Value.ToString();

            if (MaHD[2] == 'B')
            {
                HoaDonBan(MaHD);
            }
            else if (MaHD[2] == 'N')
            {
                HoaDonNhap(MaHD);
            }
            else
            {
                MessageBox.Show("Không tồn tại mã hóa đơn " + MaHD);
            }
        }
        private void HoaDonNhap(string MaHD)
        {
            HoaDon hd = new HoaDon();
            hd.MaHD = MaHD;
            hd.MaNv = Scalar($"select NV_Id from HoaDonNhap where HDN_Id = '{MaHD}'");
            hd.MaKH = Scalar($"select NCC_Id from HoaDonNhap where HDN_Id = '{MaHD}'");
            hd.TenKH = Scalar($"select NCC_Name from NhaCungCap where NCC_Id = '{hd.MaKH}'");
            hd.SDT = Scalar($"select NCC_PhoneNumbers from NhaCungCap where NCC_Id = '{hd.MaKH}'");
            hd.Ngay = Scalar($"select NgayNhap from HoaDonNhap where HDN_Id = '{MaHD}'");
            hd.lst = NoiDung($"select c.MH_Id, MH_Name, Quantity, Price from ChiTietHDN c inner join MatHang m on m.MH_Id = c.MH_Id where HDN_Id = '{MaHD}'");
            hd.Show();
        }
        private void HoaDonBan(string MaHD)
        {
            HoaDon hd = new HoaDon();
            hd.MaHD = MaHD;
            hd.MaNv = Scalar($"select NV_Id from HoaDonBan where HDB_Id = '{MaHD}'");
            hd.MaKH = Scalar($"select KH_Id from HoaDonBan where HDB_Id = '{MaHD}'");
            hd.TenKH = Scalar($"select KH_Name from KhachHang where KH_Id = '{hd.MaKH}'");
            hd.SDT = Scalar($"select KH_PhoneNumbers from KhachHang where KH_Id = '{hd.MaKH}'");
            hd.Ngay = Scalar($"select NgayBan from HoaDonBan where HDB_Id = '{MaHD}'");
            hd.lst = NoiDung($"select c.MH_Id, MH_Name, Quantity, Price from ChiTietHDB c inner join MatHang m on m.MH_Id = c.MH_Id where HDB_Id = '{MaHD}'");
            hd.Show();
        }
        string Scalar(string sql)
        {
            Ketnoi();
            cmd = new SqlCommand(sql, conn);
            string r = cmd.ExecuteScalar().ToString();
            Ngatketnoi();
            return r;
        }
        List<ThongTinHoaDon> NoiDung(string sql)
        {
            // Ma ten solg gia
            List<ThongTinHoaDon> lst = new List<ThongTinHoaDon>();
            Ketnoi();
            cmd = new SqlCommand(sql, conn);
            var reader = cmd.ExecuteReader();
            Ngatketnoi();
            while (reader.Read())
            {
                ThongTinHoaDon tt = new ThongTinHoaDon()
                {
                    MaMH = reader[0].ToString(),
                    TenMH = reader[1].ToString(),
                    Solg = int.Parse(reader[2].ToString()),
                    GiaBan = int.Parse(reader[3].ToString())
                };
                lst.Add(tt);
            }

            return lst;
        }

        private void cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Load();
        }
    }
}
