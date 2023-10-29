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
    public partial class FormTK_NCC : Form
    {
        public FormTK_NCC()
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
            conn=new SqlConnection(chuoiketnoi);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void Load_Data(string s)
        {
            dt =  new DataTable();
            Ketnoi();

            sql = $"select NCC_ID, NCC_Name, NCC_PhoneNumbers, NCC_Address  from NhaCungCap where NCC_ID like '%{s}%' and NCC_Name like '%{s}%' and NCC_PhoneNumbers like '%{s}%' and NCC_Address like '%{s}%' ";
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            dgv.DataSource = dt;

            dgv.Columns[0].HeaderText = "Mã NCC";
            dgv.Columns[1].HeaderText = "Tên NCC";
            dgv.Columns[2].HeaderText = "Số điện thoại";
            dgv.Columns[3].HeaderText = "Địa chỉ";

            Ngatketnoi();
        }

        private void FormTK_NCC_Load(object sender, EventArgs e)
        {
            Load_Data("");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Load_Data(textBox1.Text);
        }
    }
}
