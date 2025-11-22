using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DangNhap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtUsername_Leave(null, null);
            txtPassword_Leave(null, null);

            // Chuyển focus
            this.ActiveControl = lblTitle;
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Tên đăng nhập")
            {
                txtUsername.Text = "";
                txtUsername.ForeColor = Color.Black;
            }
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                txtUsername.Text = "Tên đăng nhập";
                txtUsername.ForeColor = Color.Gray;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Mật khẩu")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
                txtPassword.PasswordChar = '*';
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.Text = "Mật khẩu";
                txtPassword.ForeColor = Color.Gray;
                txtPassword.PasswordChar = '\0';
            }
        }

        string taiKhoanDung = "admin";
        string matKhauDung = "123";
        string tkNhap, mkNhap;

        public void getInfor()
        {
            tkNhap = txtUsername.Text.Trim();
            mkNhap = txtPassword.Text.Trim();
        }

        public bool checkLogin()
        {
            getInfor();

            if (tkNhap == "" || mkNhap == "")
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (tkNhap == Program.TaiKhoanGlobal && mkNhap == Program.MatKhauGlobal)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void lklblQuen_MK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (checkLogin() == true)
            {
                MessageBox.Show("Đăng nhập thành công!", "Chào mừng");

                // --- MỞ FORM CHÍNH (MAIN) ---
                // Giả sử form chính tên là frmMain
                // Nếu chưa có thì vào Project -> Add Windows Form -> đặt tên frmMain

                // frmMain f = new frmMain(); 
                // f.Show(); // Hiện form chính

                this.Hide(); // Ẩn form đăng nhập đi
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            DialogResult respond;
            respond = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respond == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void ckShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (ckShowPass.Checked == true)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                // Ẩn mật khẩu
                txtPassword.PasswordChar = '*';
            }

        }
    }
}
