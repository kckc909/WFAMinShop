using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WFAMinShop
{
    public partial class HoaDon : Form
    {
        public HoaDon()
        {
            InitializeComponent();
        }

        public string MaHD = "";
        public string MaNv = "";
        public string MaKH = "";
        public string TenKH = "";
        public string Ngay = "";
        public string SDT = "";
        public List<ThongTinHoaDon> lst = new List<ThongTinHoaDon>();

        private void HoaDon_Load(object sender, EventArgs e)
        {
            int tongtien = 0;

            lbMaHD.Text = MaHD;
            lbMaNV.Text = MaNv;
            lbMaKH.Text = MaKH; 
            lbTenKH.Text = TenKH;
            lbNgay.Text = Ngay;
            lbSDT.Text = SDT;

            foreach(var it in lst)
            {
                int thanhtien = it.Solg * it.GiaBan;

                ListViewItem item = new ListViewItem(it.MaMH);
                item.SubItems.Add(it.TenMH);
                item.SubItems.Add(it.Solg.ToString());
                item.SubItems.Add(it.GiaBan.ToString());
                item.SubItems.Add(thanhtien.ToString());

                lstV.Items.Add(item);
                tongtien += thanhtien;
            }
            lbTongTien.Text = tongtien.ToString();
        }

        private void HoaDon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                Close();
        }
    }
    public class ThongTinHoaDon
    {
        public string MaMH = "";
        public string TenMH = "";
        public int Solg = 0;
        public int GiaBan = 0;
    }
}
