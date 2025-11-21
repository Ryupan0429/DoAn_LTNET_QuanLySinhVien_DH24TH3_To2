namespace DangNhap
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            panel1 = new Panel();
            btnDangNhap = new Button();
            panel3 = new Panel();
            txtPassword = new TextBox();
            pictureBox4 = new PictureBox();
            panel2 = new Panel();
            txtUsername = new TextBox();
            pictureBox3 = new PictureBox();
            lblTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.Image = Properties.Resources.Banner;
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            resources.ApplyResources(pictureBox2, "pictureBox2");
            pictureBox2.Name = "pictureBox2";
            pictureBox2.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnDangNhap);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(lblTitle);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // btnDangNhap
            // 
            btnDangNhap.BackColor = Color.Goldenrod;
            resources.ApplyResources(btnDangNhap, "btnDangNhap");
            btnDangNhap.ForeColor = Color.White;
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.UseVisualStyleBackColor = false;
            // 
            // panel3
            // 
            panel3.BackColor = Color.White;
            panel3.Controls.Add(txtPassword);
            panel3.Controls.Add(pictureBox4);
            resources.ApplyResources(panel3, "panel3");
            panel3.Name = "panel3";
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.White;
            txtPassword.BorderStyle = BorderStyle.None;
            resources.ApplyResources(txtPassword, "txtPassword");
            txtPassword.Name = "txtPassword";
            txtPassword.Enter += txtPassword_Enter;
            txtPassword.Leave += txtPassword_Leave;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.Icon_Passwork;
            resources.ApplyResources(pictureBox4, "pictureBox4");
            pictureBox4.Name = "pictureBox4";
            pictureBox4.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(txtUsername);
            panel2.Controls.Add(pictureBox3);
            resources.ApplyResources(panel2, "panel2");
            panel2.Name = "panel2";
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.White;
            txtUsername.BorderStyle = BorderStyle.None;
            resources.ApplyResources(txtUsername, "txtUsername");
            txtUsername.Name = "txtUsername";
            txtUsername.Enter += txtUsername_Enter;
            txtUsername.Leave += txtUsername_Leave;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.Icon_User;
            resources.ApplyResources(pictureBox3, "pictureBox3");
            pictureBox3.Name = "pictureBox3";
            pictureBox3.TabStop = false;
            // 
            // lblTitle
            // 
            resources.ApplyResources(lblTitle, "lblTitle");
            lblTitle.ForeColor = Color.RoyalBlue;
            lblTitle.Name = "lblTitle";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Panel panel1;
        private Label lblTitle;
        private Panel panel2;
        private TextBox txtUsername;
        private PictureBox pictureBox3;
        private Panel panel3;
        private TextBox txtPassword;
        private PictureBox pictureBox4;
        private Button btnDangNhap;
    }
}
