using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFAMinShop
{
    // server = "Ten server"; database = "ten csdl"; uid ; pwd
    // DataSource = ; Initial Catalog ; Integrated Sercurity = True;
    public partial class FormMinShop : Form
    {
        public int code;
        public string user;
        public FormMinShop()
        {
            InitializeComponent();
        }

        private void FormMinShop_FormClosing(object sender, FormClosingEventArgs e)
        {
            DongFormCon();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormDangNhap().Show();
            Close();
        }

        private Form KiemTraTonTaiForm(Type _typeForm)
        {
            foreach (Form _f in this.MdiChildren)
            {
                if (_f.GetType() == _typeForm)
                {
                    return _f;
                }
            }
            return null;
        }

        private void DongFormCon()
        {
            pictureBox1.Visible = false;
            foreach (Form _mdiF in this.MdiChildren)
            {
                _mdiF.Close();
            }
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void quảnLýSảnPhảmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form new_Form= KiemTraTonTaiForm(typeof(FormQL_MatHang));
            if (new_Form!= null)
            {
                new_Form.Activate();
            }
            else
            {
                DongFormCon();
                FormQL_MatHang _Form = new FormQL_MatHang
                {
                    MdiParent = this
                };
                _Form.Show();
            }
        }

        private void trangChủToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DongFormCon();
            pictureBox1.Visible = true;
        }

        private void quảnLýNhânViênToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form new_Form= KiemTraTonTaiForm(typeof(FormQL_NhanVien));
            if (new_Form!= null)
            {
                new_Form.Activate();
            }
            else
            {
                DongFormCon();
                FormQL_NhanVien _Form = new FormQL_NhanVien();
                _Form.MdiParent = this;
                _Form.Show();
            }
        }

        private void FormMinShop_Load(object sender, EventArgs e)
        {
            CapQuyen();
        }
        public void CapQuyen()
        {
            if (code == 0)
            {
                menuStrip1.Items[0].Enabled = true;
                menuStrip1.Items[1].Enabled = true;
                menuStrip1.Items[2].Enabled = true;
                menuStrip1.Items[3].Enabled = true;
                menuStrip1.Items[4].Enabled = true;
                menuStrip1.Items[5].Enabled = true;
            }
            else if (code == 1)
            {
                QuanLyToolStripMenuItem.DropDownItems["QLNV"].Enabled = false;
                QuanLyToolStripMenuItem.DropDownItems["QLMH"].Enabled = false;
                menuStrip1.Items[4].Enabled = false;
                menuStrip1.Items[5].Enabled = false;
            }
            else if (code == -1)
            {
                Close();
            }
        }

        private void FormMinShop_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 1)
            {
                Application.Exit();
            }
        }

        private void quảnLýKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form new_Form= KiemTraTonTaiForm(typeof(FormQL_KhachHang));
            if (new_Form!= null)
            {
                new_Form.Activate();
            }
            else
            {
                DongFormCon();
                FormQL_KhachHang _Form = new FormQL_KhachHang
                {
                    MdiParent = this
                };
                _Form.Show();
            }
        }

        private void quảnLýNhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form new_Form= KiemTraTonTaiForm(typeof(FormQL_NCC));
            if (new_Form!= null)
            {
                new_Form.Activate();
            }
            else
            {
                DongFormCon();
                FormQL_NCC _Form = new FormQL_NCC()
                {
                    MdiParent = this
                };
                _Form.Show();
            }
        }

        private void hóaĐơnBánHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form new_Form= KiemTraTonTaiForm(typeof(FormHD_BanHang));
            if (new_Form!= null)
            {
                new_Form.Activate();
            }
            else
            {
                DongFormCon();
                FormHD_BanHang _Form = new FormHD_BanHang()
                {
                    MdiParent = this,
                    user = this.user
                };

                _Form.Show();
            }
        }

        private void hóaĐơnNhậpHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form new_Form = KiemTraTonTaiForm(typeof(FormHD_NhapHang));
            if (new_Form != null)
            {
                new_Form.Activate();
            }
            else
            {
                DongFormCon();
                FormHD_NhapHang _Form = new FormHD_NhapHang()
                {
                    MdiParent = this,
                    user = user
                };

                _Form.Show();
            }
        }

        private void đăngKýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDangKy _F = new FormDangKy();
            _F._DangKyHo = true;
            _F.Show();
        }

        private void hàngTrongKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form new_Form = KiemTraTonTaiForm(typeof(FormTK_MatHang));
            if (new_Form != null)
            {
                new_Form.Activate();
            }
            else
            {
                DongFormCon();
                FormTK_MatHang _Form = new FormTK_MatHang()
                {
                    MdiParent = this
                };

                _Form.Show();
            }
        }

        private void tìmKiếmNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form new_Form = KiemTraTonTaiForm(typeof(FormTK_NhanVien));
            if (new_Form != null)
            {
                new_Form.Activate();
            }
            else
            {
                DongFormCon();
                FormTK_NhanVien _Form = new FormTK_NhanVien()
                {
                    MdiParent = this
                };

                _Form.Show();
            }
        }

        private void tìmKiếmKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form new_Form = KiemTraTonTaiForm(typeof(FormTK_KH));
            if (new_Form != null)
            {
                new_Form.Activate();
            }
            else
            {
                DongFormCon();
                FormTK_KH _Form = new FormTK_KH()
                {
                    MdiParent = this
                };

                _Form.Show();
            }
        }
        
        private void tìmKiếmNhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form new_Form = KiemTraTonTaiForm(typeof(FormTK_NCC));
            if (new_Form != null)
            {
                new_Form.Activate();
            }
            else
            {
                DongFormCon();
                FormTK_NCC _Form = new FormTK_NCC()
                {
                    MdiParent = this
                };

                _Form.Show();
            }
        }

        private void tìmKiếmHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form new_Form = KiemTraTonTaiForm(typeof(FormTK_HoaDon));
            if (new_Form != null)
            {
                new_Form.Activate();
            }
            else
            {
                DongFormCon();
                FormTK_HoaDon _Form = new FormTK_HoaDon()
                {
                    MdiParent = this
                };

                _Form.Show();
            }
        }

        private void theoThángToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form new_Form = KiemTraTonTaiForm(typeof(FormThongKeDoanhThu));
            if (new_Form != null)
            {
                new_Form.Activate();
            }
            else
            {
                DongFormCon();
                FormThongKeDoanhThu _Form = new FormThongKeDoanhThu()
                {
                    MdiParent = this
                };

                _Form.Show();
            }
        }

        private void hàngBánChạyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form new_Form = KiemTraTonTaiForm(typeof(FormThongKeHangHoa));
            if (new_Form != null)
            {
                new_Form.Activate();
            }
            else
            {
                DongFormCon();
                FormThongKeHangHoa _Form = new FormThongKeHangHoa()
                {
                    MdiParent = this
                };

                _Form.Show();
            }
        }
    }
}
