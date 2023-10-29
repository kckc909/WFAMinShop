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
    public partial class FormTK_NhanVien : Form
    {
        public FormTK_NhanVien()
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
            conn = new SqlConnection(chuoiketnoi);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        private void FormTK_NhanVien_Load(object sender, EventArgs e)
        {
            Load_Data("");
        }
        private void Load_Data(string s)
        {
            dt = new DataTable();
            string sql = "SELECT NV_Id, NV_Name, NV_Sex, NV_Birthday, NV_Address, NV_PhoneNumbers " +
                " from NhanVien" +
                $" where NV_Id like N'%{s}%' or" +
                $" NV_Name like N'%{s}%' or" +
                $" NV_Sex like N'%{s}%' or" +
                $" NV_Birthday like N'%{s}%' or" +
                $" NV_Address like N'%{s}%' or" +
                $" NV_PhoneNumbers like N'%{s}%'";
            Ketnoi();
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            Ngatketnoi();
            dgv.DataSource = dt;

            dgv.Columns[0].HeaderText = "Mã NV";
            dgv.Columns[1].HeaderText = "Tên NV";
            dgv.Columns[2].HeaderText = "Giới tính";
            dgv.Columns[3].HeaderText = "Ngày sinh";
            dgv.Columns[4].HeaderText = "Địa chỉ";
            dgv.Columns[5].HeaderText = "Số điện thoại";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Load_Data(textBox1.Text);
        }
    }
}
