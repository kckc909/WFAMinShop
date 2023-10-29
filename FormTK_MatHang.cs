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
    public partial class FormTK_MatHang : Form
    {
        public FormTK_MatHang()
        {
            InitializeComponent();
        }

        SqlConnection conn;
        SqlDataAdapter da;
        DataTable dt;
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

        private void FormTK_SanPham_Load(object sender, EventArgs e)
        {
            Load_Data("");
        }
        private void Load_Data(string s)
        {
            Ketnoi();
            string sql = $"select m.MH_Id, m.MH_Name, m.MH_Description, m.MH_Quantity, m.MH_Price, m.MH_S_Price, m.MH_Unit, l.LH_Name, ncc.NCC_Name from LoaiHang l inner join MatHang m on l.LH_Id = m.LH_Id inner join ChiTietHDN c on m.MH_Id = c.MH_Id inner join HoaDonNhap hn on hn.HDN_Id = c.HDN_Id inner join NhaCungCap ncc on ncc.NCC_Id = hn.NCC_Id where  m.MH_Id like '%{s}%' or m.MH_Name like '%{s}%' or m.MH_Description like '%{s}%' or m.MH_Quantity like '%{s}%' or m.MH_Price like '%{s}%' or m.MH_S_Price like '%{s}%' or m.MH_Unit like '%{s}%' or l.LH_Name like '%{s}%' or ncc.NCC_Name like '%{s}%'";
            
            da = new SqlDataAdapter(sql, conn);
            dt = new DataTable();
            da.Fill(dt);
            dgv.DataSource = dt;

            dgv.Columns[0].HeaderText = "Mã MH";
            dgv.Columns[1].HeaderText = "Tên MH";
            dgv.Columns[2].HeaderText = "Mô tả";
            dgv.Columns[3].HeaderText = "Số lượng";
            dgv.Columns[4].HeaderText = "Giá nhập (VNĐ)";
            dgv.Columns[4].DefaultCellStyle.Format = "#,###";
            dgv.Columns[5].HeaderText = "Giá bán (VNĐ)";
            dgv.Columns[5].DefaultCellStyle.Format = "#,###";
            dgv.Columns[5].HeaderText = "Đơn vị tính";
            dgv.Columns[6].HeaderText = "Loại hàng";
            dgv.Columns[7].HeaderText = "Nhà cung cấp";

            Ngatketnoi(); 
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Load_Data(textBox1.Text);
        }
    }
}
