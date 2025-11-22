using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions; // thư viện kiểm tra mật khẩu ký tự

namespace DangNhap
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        string taiKhoanDung = "admin";
        string sdtDung = "0123456789";

        string tkNhap, sdtNhap, mkMoi, xacNhanMK;


        public void getInfor()
        {
            taiKhoanDung = "admin";
            sdtDung = "0123456789";

            tkNhap = txtUsername.Text.Trim();
            sdtNhap = txtNumberPhone.Text.Trim();
            mkMoi = txtNewPass.Text.Trim();
            xacNhanMK = txtConfirmNewPass.Text.Trim();
        }

        public Boolean checkInfor()
        {
            getInfor();
            if (tkNhap == "" || sdtNhap == "" || mkMoi == "" || xacNhanMK == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return false;
            }

            if (tkNhap != taiKhoanDung || sdtNhap != sdtDung)
            {
                MessageBox.Show("Thông tin tài khoản hoặc SĐT không đúng ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // (?=.*[a-z]): Phải có ít nhất 1 chữ thường
            // (?=.*[A-Z]): Phải có ít nhất 1 chữ hoa
            // (?=.*\d): Phải có ít nhất 1 số
            // (?=.*[^\da-zA-Z]): Phải có ít nhất 1 ký tự đặc biệt (không phải chữ hay số)
            // .{8,}: Độ dài tổng cộng phải từ 8 ký tự trở lên
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";

            if (Regex.IsMatch(mkMoi, pattern) == false)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 8 ký tự, bao gồm:\n- Chữ hoa (A-Z)\n- Chữ thường (a-z)\n- Số (0-9)\n- Ký tự đặc biệt (@, #, !,...)",
                                "Mật khẩu yếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (mkMoi != xacNhanMK)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }



            return true;
        }

        private void btnXacnhan_Click(object sender, EventArgs e)
        {
            if (checkInfor())
            {
                // --- CẬP NHẬT MẬT KHẨU MỚI VÀO KHO CHUNG ---
                Program.MatKhauGlobal = txtNewPass.Text.Trim();
                // -------------------------------------------

                MessageBox.Show("Đổi mật khẩu thành công!\nMật khẩu mới đã được cập nhật.", "Thông báo");
                this.Close();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult respond;
            respond = MessageBox.Show("Bạn có chắc muốn hủy không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respond == DialogResult.Yes)
                this.Close();
        }

        private void ckShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (ckShowPass.Checked == true)
            {
                txtNewPass.PasswordChar = '\0';
                txtConfirmNewPass.PasswordChar = '\0';
            }
            else
            {
                txtNewPass.PasswordChar = '*';
                txtConfirmNewPass.PasswordChar = '*';
            }
        }
    }
}
