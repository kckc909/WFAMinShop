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
    public partial class FormTK_KH : Form
    {
        public FormTK_KH()
        {
            InitializeComponent();
        }

        SqlConnection conn;
        SqlDataAdapter da;
        DataTable dt;
        string chuoiketnoi = "Data Source=DESKTOP-H22E3HT;Initial Catalog=MinShopWFA;Integrated Security=True";
        string sql;

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

        private void Load_Data(string s)
        {
            dt = new DataTable();
            Ketnoi();

            sql = $"select KH_Id, KH_Name, KH_Sex, KH_PhoneNumbers, KH_Address from KhachHang where KH_Id like '%{s}%' or KH_Name like '%{s}%' or KH_PhoneNumbers like '%{s}%' or KH_Address like '%{s}%' or KH_Sex like '%{s}%'";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            dgv.DataSource = dt;

            dgv.Columns[0].HeaderText = "Mã KH";
            dgv.Columns[1].HeaderText = "Họ tên";
            dgv.Columns[2].HeaderText = "Giới tính";
            dgv.Columns[3].HeaderText = "Số điện thoại";
            dgv.Columns[4].HeaderText = "Địa chỉ";

            Ngatketnoi();
        }

        private void FormTK_KH_Load(object sender, EventArgs e)
        {
            Load_Data("");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Load_Data(textBox1.Text);
        }
    }
}
