using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq;

namespace PhongDaoTao
{
    public partial class Form2 : Form
    {
        string connectionString = @"Data Source=YOUR_SERVER_NAME;Initial Catalog=QuanLySinhVien;Integrated Security=True";

        public Form2()
        {
            InitializeComponent();
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadLopVaoComboBox();
            cboLopCu.SelectedIndexChanged += cboLopCu_SelectedIndexChanged;
        }

        private void LoadLopVaoComboBox()
        {
            using (SqlConnection connection = GetConnection())
            {
                string query = "SELECT MaLop, TenLop FROM Lop ORDER BY TenLop";
                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                DataTable dtLop = new DataTable();
                da.Fill(dtLop);

                cboLopCu.DataSource = dtLop;
                cboLopCu.DisplayMember = "TenLop";
                cboLopCu.ValueMember = "MaLop";

                cboLopMoi.DataSource = dtLop.Copy();
                cboLopMoi.DisplayMember = "TenLop";
                cboLopMoi.ValueMember = "MaLop";

                cboLopCu.SelectedIndex = -1;
                cboLopMoi.SelectedIndex = -1;
            }
        }

        private void cboLopCu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLopCu.SelectedValue is string maLopHienTai && !string.IsNullOrEmpty(maLopHienTai))
            {
                try
                {
                    using (SqlConnection connection = GetConnection())
                    {
                        string query = "SELECT MaSV, HoTen, MaLop FROM SinhVien WHERE MaLop = @MaLopHienTai";
                        SqlDataAdapter da = new SqlDataAdapter(query, connection);
                        da.SelectCommand.Parameters.AddWithValue("@MaLopHienTai", maLopHienTai);

                        DataTable dtSV = new DataTable();
                        da.Fill(dtSV);
                        dgvDanhSachSV.DataSource = dtSV;

                        if (dgvDanhSachSV.Columns.Contains("MaLop"))
                        {
                            dgvDanhSachSV.Columns["MaLop"].HeaderText = "Mã Lớp";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải sinh viên: " + ex.Message, "Lỗi SQL");
                    dgvDanhSachSV.DataSource = null;
                }
            }
            else
            {
                dgvDanhSachSV.DataSource = null;
            }
        }

        private void btnChuyen_Click(object sender, EventArgs e)
        {
            if (cboLopMoi.SelectedValue == null || cboLopCu.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ Lớp Cũ và Lớp Mới.", "Cảnh báo");
                return;
            }
            if (dgvDanhSachSV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sinh viên để chuyển lớp.", "Cảnh báo");
                return;
            }
            if (cboLopCu.SelectedValue.ToString() == cboLopMoi.SelectedValue.ToString())
            {
                MessageBox.Show("Lớp mới phải khác lớp cũ.", "Cảnh báo");
                return;
            }

            string maLopMoi = cboLopMoi.SelectedValue.ToString();
            string tenLopMoi = cboLopMoi.Text;
            int rowsAffected = 0;

            string updateQuery = "UPDATE SinhVien SET MaLop = @MaLopMoi WHERE MaSV = @MaSV";

            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
            {
                cmd.Parameters.Add("@MaLopMoi", SqlDbType.NVarChar).Value = maLopMoi;
                cmd.Parameters.Add("@MaSV", SqlDbType.NVarChar);

                try
                {
                    conn.Open();

                    foreach (DataGridViewRow row in dgvDanhSachSV.SelectedRows)
                    {
                        if (row.Cells["MaSV"].Value != null)
                        {
                            string maSV = row.Cells["MaSV"].Value.ToString();

                            cmd.Parameters["@MaSV"].Value = maSV;
                            rowsAffected += cmd.ExecuteNonQuery();
                        }
                    }

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Đã chuyển {rowsAffected} sinh viên sang lớp {tenLopMoi} thành công.",
                                        "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        cboLopCu.SelectedValue = maLopMoi;
                        cboLopMoi.SelectedIndex = -1;
                    }
                    else
                    {
                        MessageBox.Show("Không có sinh viên nào được chuyển.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi chuyển lớp: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}