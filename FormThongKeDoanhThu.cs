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
    public partial class FormThongKeDoanhThu : Form
    {
        public FormThongKeDoanhThu()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        string chuoiketnoi = "Data Source=DESKTOP-H22E3HT;Initial Catalog=MinShopWFA;Integrated Security=True";
        SqlCommand cmd;
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
            conn = new SqlConnection(chuoiketnoi);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void FormThongKe_Load(object sender, EventArgs e)
        {
            for (int i = DateTime.Now.Year; i >= 2023; i--)
            {
                cboY.Items.Add(i);
            }
        }
        string ExScalar(string sql)
        {
            Ketnoi();
            cmd = new SqlCommand(sql, conn);
            string r = cmd.ExecuteScalar().ToString();
            Ngatketnoi();
            return r;
        }

        private void btnTk_Click(object sender, EventArgs e)
        {
            string sql;
            string y = cboY.Text;
            string m = cboM.Text;
            if (m.Length > 3)
            {
                sql = $"select Sum(Quantity * Price) from HoaDonNhap h inner join ChiTietHDN c on h.HDN_Id = c.HDN_Id where Year(NgayNhap) = {y} ";
                lb_TongNhap.Text = ExScalar(sql);
                sql = $"select Sum(Quantity * Price) from HoaDonBan h inner join ChiTietHDB c on h.HDB_Id = c.HDB_Id where Year(NgayBan) = {y} ";
                lb_TongBan.Text = ExScalar(sql);
            }
            else
            {
                sql = $"select Sum(Quantity * Price) from HoaDonNhap h inner join ChiTietHDN c on h.HDN_Id = c.HDN_Id where Year(NgayNhap) = '{y}' and MONTH(NgayNhap) = '{m}'";
                lb_TongNhap.Text = ExScalar(sql);
                sql = $"select Sum(Quantity * Price) from HoaDonBan h inner join ChiTietHDB c on h.HDB_Id = c.HDB_Id where Year(NgayBan) = '{y}' and MONTH(NgayBan) = '{m}'";
                lb_TongBan.Text = ExScalar(sql);
            }
            if (Equals(lb_TongNhap.Text, ""))
            {
                lb_TongNhap.Text = "0";
            }
            if (Equals(lb_TongBan.Text, ""))
            {
                lb_TongBan.Text = "0";
            }
            lb_DoanhThu.Text = (int.Parse(lb_TongBan.Text) - int.Parse(lb_TongNhap.Text)).ToString();
        }
    }
}
