using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLySinhVienApp
{
    public class frmSinhVien : Form
    {
        private Label lblTitle;
        private GroupBox grpThongTin;
        private GroupBox grpDanhSach;
        private GroupBox grpChucNang;

        private Label lblMaSV, lblHoLot, lblTenSV, lblNgaySinh, lblPhai, lblSDT, lblQueQuan, lblMaLop;
        private TextBox txtMaSV, txtHoLot, txtTenSV, txtSDT, txtQueQuan;
        private DateTimePicker dtpNgaySinh;
        private RadioButton radNam, radNu;
        private ComboBox cboMaLop;

        private Button btnThem, btnSua, btnXoa, btnLamMoi, btnTimKiem;
        private TextBox txtTimKiem;
        private Label lblTimKiem;

        private DataGridView dgvSinhVien;

        public frmSinhVien()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Quản Lý Sinh Viên - DH23IT";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Times New Roman", 12);
            this.BackColor = Color.WhiteSmoke;

            lblTitle = new Label();
            lblTitle.Text = "QUẢN LÝ HỒ SƠ SINH VIÊN";
            lblTitle.Font = new Font("Times New Roman", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.DarkBlue;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point((this.ClientSize.Width - lblTitle.Width) / 2, 10);
            lblTitle.Anchor = AnchorStyles.Top;
            this.Controls.Add(lblTitle);

            grpThongTin = new GroupBox();
            grpThongTin.Text = "Thông tin chi tiết";
            grpThongTin.Location = new Point(20, 50);
            grpThongTin.Size = new Size(940, 200);
            grpThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(grpThongTin);

            int col1_LabelX = 30;
            int col1_InputX = 130;
            int col2_LabelX = 350;
            int col2_InputX = 450;
            int col3_LabelX = 670;
            int col3_InputX = 750;

            int row1 = 30, row2 = 70, row3 = 110, row4 = 150;

            lblMaSV = CreateLabel("Mã SV:", col1_LabelX, row1);
            txtMaSV = CreateTextBox(col1_InputX, row1, 150);

            lblHoLot = CreateLabel("Họ Lót:", col2_LabelX, row1);
            txtHoLot = CreateTextBox(col2_InputX, row1, 180);

            lblTenSV = CreateLabel("Tên:", col3_LabelX, row1);
            txtTenSV = CreateTextBox(col3_InputX, row1, 150);

            lblNgaySinh = CreateLabel("Ngày Sinh:", col1_LabelX, row2);
            dtpNgaySinh = new DateTimePicker();
            dtpNgaySinh.Format = DateTimePickerFormat.Short;
            dtpNgaySinh.Location = new Point(col1_InputX, row2);
            dtpNgaySinh.Size = new Size(150, 25);
            grpThongTin.Controls.Add(dtpNgaySinh);

            lblPhai = CreateLabel("Giới tính:", col2_LabelX, row2);
            radNam = new RadioButton { Text = "Nam", Location = new Point(col2_InputX, row2), Checked = true, AutoSize = true };
            radNu = new RadioButton { Text = "Nữ", Location = new Point(col2_InputX + 60, row2), AutoSize = true };
            grpThongTin.Controls.Add(radNam);
            grpThongTin.Controls.Add(radNu);

            lblMaLop = CreateLabel("Lớp:", col3_LabelX, row2);
            cboMaLop = new ComboBox();
            cboMaLop.Location = new Point(col3_InputX, row2);
            cboMaLop.Size = new Size(150, 25);
            cboMaLop.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMaLop.Items.AddRange(new string[] { "DH23IT01", "DH23PM", "DH23QT", "DH23AV" });
            grpThongTin.Controls.Add(cboMaLop);

            lblSDT = CreateLabel("SĐT:", col1_LabelX, row3);
            txtSDT = CreateTextBox(col1_InputX, row3, 150);

            lblQueQuan = CreateLabel("Quê Quán:", col2_LabelX, row3);
            txtQueQuan = CreateTextBox(col2_InputX, row3, 450);

            grpChucNang = new GroupBox();
            grpChucNang.Text = "";
            grpChucNang.Location = new Point(20, 260);
            grpChucNang.Size = new Size(940, 70);
            grpChucNang.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(grpChucNang);

            lblTimKiem = new Label { Text = "Tìm kiếm (Mã/Tên):", Location = new Point(20, 25), AutoSize = true };
            txtTimKiem = new TextBox { Location = new Point(160, 22), Size = new Size(200, 27) };
            btnTimKiem = CreateButton("Tìm kiếm", 370, 20, Color.Teal);

            grpChucNang.Controls.Add(lblTimKiem);
            grpChucNang.Controls.Add(txtTimKiem);
            grpChucNang.Controls.Add(btnTimKiem);

            int btnWidth = 100;
            int startX = 500;
            int gap = 110;

            btnThem = CreateButton("Thêm", startX, 20, Color.ForestGreen);
            btnSua = CreateButton("Sửa", startX + gap, 20, Color.Orange);
            btnXoa = CreateButton("Xóa", startX + gap * 2, 20, Color.Firebrick);
            btnLamMoi = CreateButton("Làm mới", startX + gap * 3, 20, Color.Gray);

            grpDanhSach = new GroupBox();
            grpDanhSach.Text = "Danh sách sinh viên";
            grpDanhSach.Location = new Point(20, 340);
            grpDanhSach.Size = new Size(940, 300);
            grpDanhSach.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(grpDanhSach);

            dgvSinhVien = new DataGridView();
            dgvSinhVien.Location = new Point(10, 25);
            dgvSinhVien.Size = new Size(920, 265);
            dgvSinhVien.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.ReadOnly = true;
            dgvSinhVien.AllowUserToAddRows = false;

            dgvSinhVien.Columns.Add("MaSV", "Mã SV");
            dgvSinhVien.Columns.Add("HoLot", "Họ Lót");
            dgvSinhVien.Columns.Add("TenSV", "Tên");
            dgvSinhVien.Columns.Add("NgaySinh", "Ngày Sinh");
            dgvSinhVien.Columns.Add("Phai", "Phái");
            dgvSinhVien.Columns.Add("MaLop", "Lớp");
            dgvSinhVien.Columns.Add("SDT", "SĐT");
            dgvSinhVien.Columns.Add("QueQuan", "Quê Quán");

            dgvSinhVien.Rows.Add("CN23001", "Nguyễn Văn", "An", "15/01/2000", "Nam", "DH23IT01", "0901111111", "Hà Nội");
            dgvSinhVien.Rows.Add("CN23002", "Trần Thị", "Bình", "20/05/2001", "Nữ", "DH23IT01", "0902222222", "Hải Phòng");

            grpDanhSach.Controls.Add(dgvSinhVien);

            dgvSinhVien.CellClick += DgvSinhVien_CellClick;
        }

        private Label CreateLabel(string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Location = new Point(x, y);
            lbl.AutoSize = true;
            grpThongTin.Controls.Add(lbl);
            return lbl;
        }

        private TextBox CreateTextBox(int x, int y, int width)
        {
            TextBox txt = new TextBox();
            txt.Location = new Point(x, y);
            txt.Size = new Size(width, 25);
            grpThongTin.Controls.Add(txt);
            return txt;
        }

        private Button CreateButton(string text, int x, int y, Color color)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Location = new Point(x, y);
            btn.Size = new Size(100, 35);
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            grpChucNang.Controls.Add(btn);
            return btn;
        }

        private void DgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSinhVien.Rows[e.RowIndex];
                txtMaSV.Text = row.Cells["MaSV"].Value.ToString();
                txtHoLot.Text = row.Cells["HoLot"].Value.ToString();
                txtTenSV.Text = row.Cells["TenSV"].Value.ToString();
                txtMaSV.Enabled = false;
            }
        }

        private void ResetForm()
        {
            txtMaSV.Enabled = true;
            txtMaSV.Clear();
            txtHoLot.Clear();
            txtTenSV.Clear();
            txtSDT.Clear();
            txtQueQuan.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            radNam.Checked = true;
            if (cboMaLop.Items.Count > 0) cboMaLop.SelectedIndex = 0;
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmSinhVien());
        }
    }
}