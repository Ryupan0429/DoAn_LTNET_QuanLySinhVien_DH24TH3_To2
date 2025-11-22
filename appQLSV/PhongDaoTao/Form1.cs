using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq;

namespace PhongDaoTao
{
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source=YOUR_SERVER_NAME;Initial Catalog=QuanLySinhVien;Integrated Security=True";
        DataSet ds = new DataSet("dsQLSV");
        SqlDataAdapter daKhoa, daLop, daMonHoc;
        string currentModule = "";
        string currentMode = "VIEW";

        public Form1()
        {
            InitializeComponent();
            InitializeControls();
            InitializeModuleSelection();
        }

        private void InitializeControls()
        {
            dgDSKhoa.Click += DataGridView_Click;
            dgDSLop.Click += DataGridView_Click;
            dgDSHocPhan.Click += DataGridView_Click;
            radKhoa.CheckedChanged += new EventHandler(ModuleRadioButton_CheckedChanged);
            radLop.CheckedChanged += new EventHandler(ModuleRadioButton_CheckedChanged);
            radHP.CheckedChanged += new EventHandler(ModuleRadioButton_CheckedChanged);
        }

        private void InitializeModuleSelection()
        {
            dgDSLop.Visible = dgDSHocPhan.Visible = false;
            btnLuu.Visible = btnHuy.Visible = false;
            btnChuyenLop.Visible = false;
            radKhoa.Checked = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadAllAdaptersAndData();
            SetMode("VIEW");
        }

        private void LoadAllAdaptersAndData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                daKhoa = new SqlDataAdapter("select MaKhoa, TenKhoa from Khoa", conn);
                daKhoa.Fill(ds, "tblKhoa");
                dgDSKhoa.DataSource = ds.Tables["tblKhoa"];
                PrepareCommands(daKhoa, conn);

                daLop = new SqlDataAdapter("select MaLop, TenLop, MaKhoa, KhoaSo, SiSo from Lop", conn);
                daLop.Fill(ds, "tblLop");
                dgDSLop.DataSource = ds.Tables["tblLop"];
                PrepareCommands(daLop, conn);

                daMonHoc = new SqlDataAdapter("select MaHP, TenHP, SoTinChi, NamHoc, LoaiHP, MaKhoa from HocPhan", conn);
                daMonHoc.Fill(ds, "tblMonHoc");
                dgDSHocPhan.DataSource = ds.Tables["tblMonHoc"];
                PrepareCommands(daMonHoc, conn);
            }
        }

        private void PrepareCommands(SqlDataAdapter da, SqlConnection conn)
        {
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            da.InsertCommand = builder.GetInsertCommand();
            da.UpdateCommand = builder.GetUpdateCommand();
            da.DeleteCommand = builder.GetDeleteCommand();
        }

        private DataGridView GetActiveDataGridView()
        {
            if (currentModule == "KHOA") return dgDSKhoa;
            if (currentModule == "LOP") return dgDSLop;
            if (currentModule == "HOCPHAN") return dgDSHocPhan;
            return null;
        }

        private DataTable GetActiveDataTable()
        {
            if (currentModule == "KHOA") return ds.Tables["tblKhoa"];
            if (currentModule == "LOP") return ds.Tables["tblLop"];
            if (currentModule == "HOCPHAN") return ds.Tables["tblMonHoc"];
            return null;
        }

        private SqlDataAdapter GetActiveAdapter()
        {
            if (currentModule == "KHOA") return daKhoa;
            if (currentModule == "LOP") return daLop;
            if (currentModule == "HOCPHAN") return daMonHoc;
            return null;
        }

        private void DataGridView_Click(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (currentMode != "VIEW" || dgv.SelectedRows.Count == 0) return;

            DataRowView drv = (DataRowView)dgv.SelectedRows[0].DataBoundItem;
            LoadDetailsToControls(drv.Row);

            SetMode("VIEW");
        }

        private void LoadDetailsToControls(DataRow dr)
        {
            ClearInputFields();

            if (currentModule == "KHOA")
            {
                txtMaKhoa.Text = dr["MaKhoa"].ToString();
                txtTenKhoa.Text = dr["TenKhoa"].ToString();
            }
            else if (currentModule == "LOP")
            {
                txtMaLop.Text = dr["MaLop"].ToString();
                txtTenLop.Text = dr["TenLop"].ToString();
            }
            else if (currentModule == "HOCPHAN")
            {
                txtMaHP.Text = dr["MaHP"].ToString();
                txtTenHP.Text = dr["TenHP"].ToString();

                if (dr["SoTinChi"] != DBNull.Value)
                    numericUpDown.Value = Convert.ToInt32(dr["SoTinChi"]);

                txtNamHoc.Text = dr["NamHoc"].ToString();

                string loaiHP = dr["LoaiHP"].ToString();
                radBatBuoc.Checked = (loaiHP == "Bắt buộc");
                radTienQuyet.Checked = (loaiHP == "Tiên quyết");
                radTuChon.Checked = (loaiHP == "Tự chọn");
            }
        }

        private void ModuleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked)
            {
                if (rb == radKhoa) SwitchModule("KHOA");
                else if (rb == radLop) SwitchModule("LOP");
                else if (rb == radHP) SwitchModule("HOCPHAN");
            }
        }

        private void SwitchModule(string module)
        {
            currentModule = module;
            dgDSKhoa.Visible = (module == "KHOA");
            dgDSLop.Visible = (module == "LOP");
            dgDSHocPhan.Visible = (module == "HOCPHAN");
            btnChuyenLop.Visible = (module == "LOP");

            if (module == "LOP")
            {
                SqlDataAdapter da = GetActiveAdapter();
                DataTable dt = GetActiveDataTable();
                if (da != null && dt != null)
                {
                    dt.Clear();
                    da.Fill(ds, dt.TableName);
                }
            }

            SetInputControlsVisibility(module);
            ClearInputFields();
            SetMode("VIEW");
        }

        private void SetMode(string mode)
        {
            currentMode = mode;
            bool isEditMode = (mode == "EDIT" || mode == "ADD");
            DataGridView activeDGV = GetActiveDataGridView();
            int selectedCount = (activeDGV != null) ? activeDGV.SelectedRows.Count : 0;

            btnThem.Enabled = !isEditMode;
            btnSua.Enabled = !isEditMode && (selectedCount == 1);
            btnXoa.Enabled = !isEditMode && (selectedCount == 1);
            btnLuu.Visible = isEditMode;
            btnHuy.Visible = isEditMode;

            SetInputControlsEnabled(isEditMode);
            if (activeDGV != null) activeDGV.Enabled = !isEditMode;

            if (currentModule == "KHOA") txtMaKhoa.ReadOnly = (mode == "EDIT");
            else if (currentModule == "LOP") txtMaLop.ReadOnly = (mode == "EDIT");
            else if (currentModule == "HOCPHAN") txtMaHP.ReadOnly = (mode == "EDIT");
        }

        private void SetInputControlsVisibility(string module)
        {
            bool isKhoa = (module == "KHOA");
            bool isLop = (module == "LOP");
            bool isHocPhan = (module == "HOCPHAN");

            txtMaKhoa.Visible = txtTenKhoa.Visible = isKhoa;
            txtMaLop.Visible = txtTenLop.Visible = isLop;

            txtMaHP.Visible = txtTenHP.Visible = numericUpDown.Visible = txtNamHoc.Visible = isHocPhan;
            radBatBuoc.Visible = radTienQuyet.Visible = radTuChon.Visible = isHocPhan;
        }

        private void SetInputControlsEnabled(bool enabled)
        {
            if (currentModule == "KHOA")
            {
                txtMaKhoa.Enabled = txtTenKhoa.Enabled = enabled;
            }
            else if (currentModule == "LOP")
            {
                txtMaLop.Enabled = txtTenLop.Enabled = enabled;
            }
            else if (currentModule == "HOCPHAN")
            {
                txtMaHP.Enabled = txtTenHP.Enabled = numericUpDown.Enabled = txtNamHoc.Enabled = enabled;
                radBatBuoc.Enabled = radTienQuyet.Enabled = radTuChon.Enabled = enabled;
            }
        }

        private void ClearInputFields()
        {
            txtMaKhoa.Text = txtTenKhoa.Text = "";
            txtMaLop.Text = txtTenLop.Text = "";
            txtMaHP.Text = txtTenHP.Text = "";
            numericUpDown.Value = 0;
            txtNamHoc.Text = "";
            radBatBuoc.Checked = radTienQuyet.Checked = radTuChon.Checked = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ClearInputFields();
            SetMode("ADD");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (GetActiveDataGridView().SelectedRows.Count == 0) return;
            SetMode("EDIT");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataGridView activeDGV = GetActiveDataGridView();
            if (activeDGV?.SelectedRows.Count == 0) return;

            DataRowView drv = (DataRowView)activeDGV.SelectedRows[0].DataBoundItem;
            DataRow rowToDelete = drv.Row;

            if (currentModule == "HOCPHAN" && rowToDelete["LoaiHP"].ToString() == "Bắt buộc")
            {
                MessageBox.Show("Không thể xóa Học phần Bắt buộc.", "Ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Xóa {currentModule} có mã: {rowToDelete[0]}?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            rowToDelete.Delete();
            MessageBox.Show("Đã đánh dấu xóa. Vui lòng nhấn Lưu để cập nhật CSDL.", "Thông báo");
            SetMode("VIEW");
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            DataTable activeTable = GetActiveDataTable();
            SqlDataAdapter activeAdapter = GetActiveAdapter();
            if (activeTable == null) return;

            try
            {
                if (currentMode == "ADD")
                {
                    DataRow newRow = activeTable.NewRow();
                    if (currentModule == "KHOA")
                    {
                        newRow["MaKhoa"] = txtMaKhoa.Text.Trim();
                        newRow["TenKhoa"] = txtTenKhoa.Text.Trim();
                    }
                    else if (currentModule == "HOCPHAN")
                    {
                        string loaiHP = radBatBuoc.Checked ? "Bắt buộc" : radTienQuyet.Checked ? "Tiên quyết" : "Tự chọn";
                        newRow["MaHP"] = txtMaHP.Text.Trim();
                        newRow["TenHP"] = txtTenHP.Text.Trim();
                        newRow["SoTinChi"] = (int)numericUpDown.Value;
                        newRow["NamHoc"] = txtNamHoc.Text.Trim();
                        newRow["LoaiHP"] = loaiHP;
                    }
                    activeTable.Rows.Add(newRow);
                }
                else if (currentMode == "EDIT")
                {
                    DataRowView drv = (DataRowView)GetActiveDataGridView().SelectedRows[0].DataBoundItem;
                    DataRow rowToEdit = drv.Row;

                    rowToEdit.BeginEdit();
                    if (currentModule == "KHOA")
                    {
                        rowToEdit["TenKhoa"] = txtTenKhoa.Text.Trim();
                    }
                    else if (currentModule == "HOCPHAN")
                    {
                        string loaiHP = radBatBuoc.Checked ? "Bắt buộc" : radTienQuyet.Checked ? "Tiên quyết" : "Tự chọn";
                        rowToEdit["TenHP"] = txtTenHP.Text.Trim();
                        rowToEdit["SoTinChi"] = (int)numericUpDown.Value;
                        rowToEdit["NamHoc"] = txtNamHoc.Text.Trim();
                        rowToEdit["LoaiHP"] = loaiHP;
                    }
                    rowToEdit.EndEdit();
                }

                activeAdapter.Update(ds, activeTable.TableName);
                MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo");

                activeTable.Clear();
                activeAdapter.Fill(ds, activeTable.TableName);
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Lỗi CSDL: Mã lỗi {ex.Number} - {ex.Message}", "Thao tác thất bại");
                ds.Tables[activeTable.TableName].RejectChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi");
            }
            finally
            {
                SetMode("VIEW");
                ClearInputFields();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ds.RejectChanges();
            MessageBox.Show("Đã hủy bỏ các thao tác chưa lưu.", "Thông báo");
            ClearInputFields();
            SetMode("VIEW");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult traloi = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (traloi == DialogResult.Yes)
                Application.Exit();
        }

        private void btnChuyenLop_Click(object sender, EventArgs e)
        {
            if (currentModule != "LOP") return;

            Form2 form2 = new Form2();
            form2.ShowDialog();

            SwitchModule("LOP");
        }
    }
}