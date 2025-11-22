namespace PhongDaoTao
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvDanhSachSV = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboLopCu = new System.Windows.Forms.ComboBox();
            this.cboLopMoi = new System.Windows.Forms.ComboBox();
            this.btnChuyen = new System.Windows.Forms.Button();
            this.lblTieuDeCu = new System.Windows.Forms.Label();
            this.lblTieuDeMoi = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachSV)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDanhSachSV
            // 
            this.dgvDanhSachSV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachSV.Location = new System.Drawing.Point(22, 12);
            this.dgvDanhSachSV.Name = "dgvDanhSachSV";
            this.dgvDanhSachSV.Size = new System.Drawing.Size(562, 198);
            this.dgvDanhSachSV.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lớp Cũ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Lớp Mới";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnChuyen);
            this.groupBox1.Controls.Add(this.cboLopMoi);
            this.groupBox1.Controls.Add(this.cboLopCu);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(22, 228);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chuyển lớp";
            // 
            // cboLopCu
            // 
            this.cboLopCu.FormattingEnabled = true;
            this.cboLopCu.Location = new System.Drawing.Point(80, 27);
            this.cboLopCu.Name = "cboLopCu";
            this.cboLopCu.Size = new System.Drawing.Size(111, 27);
            this.cboLopCu.TabIndex = 4;
            this.cboLopCu.SelectedIndexChanged += new System.EventHandler(this.cboLopCu_SelectedIndexChanged);
            // 
            // cboLopMoi
            // 
            this.cboLopMoi.FormattingEnabled = true;
            this.cboLopMoi.Location = new System.Drawing.Point(80, 60);
            this.cboLopMoi.Name = "cboLopMoi";
            this.cboLopMoi.Size = new System.Drawing.Size(111, 27);
            this.cboLopMoi.TabIndex = 4;
            // 
            // btnChuyen
            // 
            this.btnChuyen.Location = new System.Drawing.Point(209, 35);
            this.btnChuyen.Name = "btnChuyen";
            this.btnChuyen.Size = new System.Drawing.Size(97, 40);
            this.btnChuyen.TabIndex = 4;
            this.btnChuyen.Text = "Chuyển lớp";
            this.btnChuyen.UseVisualStyleBackColor = true;
            this.btnChuyen.Click += new System.EventHandler(this.btnChuyen_Click);
            // 
            // lblTieuDeCu
            // 
            this.lblTieuDeCu.BackColor = System.Drawing.Color.PapayaWhip;
            this.lblTieuDeCu.Location = new System.Drawing.Point(365, 251);
            this.lblTieuDeCu.Name = "lblTieuDeCu";
            this.lblTieuDeCu.Size = new System.Drawing.Size(223, 31);
            this.lblTieuDeCu.TabIndex = 4;
            // 
            // lblTieuDeMoi
            // 
            this.lblTieuDeMoi.BackColor = System.Drawing.Color.PapayaWhip;
            this.lblTieuDeMoi.Location = new System.Drawing.Point(365, 288);
            this.lblTieuDeMoi.Name = "lblTieuDeMoi";
            this.lblTieuDeMoi.Size = new System.Drawing.Size(223, 27);
            this.lblTieuDeMoi.TabIndex = 5;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OldLace;
            this.ClientSize = new System.Drawing.Size(596, 371);
            this.Controls.Add(this.lblTieuDeMoi);
            this.Controls.Add(this.lblTieuDeCu);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvDanhSachSV);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachSV)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDanhSachSV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboLopMoi;
        private System.Windows.Forms.ComboBox cboLopCu;
        private System.Windows.Forms.Button btnChuyen;
        private System.Windows.Forms.Label lblTieuDeCu;
        private System.Windows.Forms.Label lblTieuDeMoi;
    }
}