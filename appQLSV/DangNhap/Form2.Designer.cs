namespace DangNhap
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
            label1 = new Label();
            txtUsername = new TextBox();
            label2 = new Label();
            txtNumberPhone = new TextBox();
            label3 = new Label();
            txtNewPass = new TextBox();
            label4 = new Label();
            btnXacnhan = new Button();
            btnHuy = new Button();
            groupBox1 = new GroupBox();
            ckShowPass = new CheckBox();
            label5 = new Label();
            txtConfirmNewPass = new TextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(95, 35);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(288, 35);
            label1.TabIndex = 0;
            label1.Text = "Khôi Phục Mật Khẩu";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(216, 26);
            txtUsername.Margin = new Padding(4);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(200, 30);
            txtUsername.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(10, 34);
            label2.Name = "label2";
            label2.Size = new Size(159, 22);
            label2.TabIndex = 2;
            label2.Text = "Tên đặt đăng nhập:";
            // 
            // txtNumberPhone
            // 
            txtNumberPhone.Location = new Point(216, 64);
            txtNumberPhone.Margin = new Padding(4);
            txtNumberPhone.Name = "txtNumberPhone";
            txtNumberPhone.Size = new Size(200, 30);
            txtNumberPhone.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(10, 72);
            label3.Name = "label3";
            label3.Size = new Size(120, 22);
            label3.TabIndex = 2;
            label3.Text = "Số điện thoại:";
            // 
            // txtNewPass
            // 
            txtNewPass.Location = new Point(216, 102);
            txtNewPass.Margin = new Padding(4);
            txtNewPass.Name = "txtNewPass";
            txtNewPass.PasswordChar = '*';
            txtNewPass.Size = new Size(200, 30);
            txtNewPass.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(10, 110);
            label4.Name = "label4";
            label4.Size = new Size(124, 22);
            label4.TabIndex = 2;
            label4.Text = "Mật khẩu mới:";
            // 
            // btnXacnhan
            // 
            btnXacnhan.BackColor = Color.MediumSeaGreen;
            btnXacnhan.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnXacnhan.Location = new Point(71, 307);
            btnXacnhan.Name = "btnXacnhan";
            btnXacnhan.Size = new Size(150, 40);
            btnXacnhan.TabIndex = 3;
            btnXacnhan.Text = "Xác nhận";
            btnXacnhan.UseVisualStyleBackColor = false;
            btnXacnhan.Click += btnXacnhan_Click;
            // 
            // btnHuy
            // 
            btnHuy.BackColor = Color.Red;
            btnHuy.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnHuy.Location = new Point(264, 307);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(150, 40);
            btnHuy.TabIndex = 3;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = false;
            btnHuy.Click += btnHuy_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ckShowPass);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtConfirmNewPass);
            groupBox1.Controls.Add(txtNewPass);
            groupBox1.Controls.Add(txtNumberPhone);
            groupBox1.Controls.Add(txtUsername);
            groupBox1.Location = new Point(25, 86);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(447, 215);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // ckShowPass
            // 
            ckShowPass.AutoSize = true;
            ckShowPass.Font = new Font("Times New Roman", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ckShowPass.Location = new Point(266, 186);
            ckShowPass.Name = "ckShowPass";
            ckShowPass.Size = new Size(150, 23);
            ckShowPass.TabIndex = 3;
            ckShowPass.Text = "Hiện thị mật khẩu";
            ckShowPass.UseVisualStyleBackColor = true;
            ckShowPass.CheckedChanged += ckShowPass_CheckedChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(10, 148);
            label5.Name = "label5";
            label5.Size = new Size(199, 22);
            label5.TabIndex = 2;
            label5.Text = "Xác nhận mật khẩu mới:";
            // 
            // txtConfirmNewPass
            // 
            txtConfirmNewPass.Location = new Point(216, 140);
            txtConfirmNewPass.Margin = new Padding(4);
            txtConfirmNewPass.Name = "txtConfirmNewPass";
            txtConfirmNewPass.PasswordChar = '*';
            txtConfirmNewPass.Size = new Size(200, 30);
            txtConfirmNewPass.TabIndex = 1;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(11F, 22F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 361);
            Controls.Add(groupBox1);
            Controls.Add(btnHuy);
            Controls.Add(btnXacnhan);
            Controls.Add(label1);
            Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quên mật khẩu?";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtUsername;
        private Label label2;
        private TextBox txtNumberPhone;
        private Label label3;
        private TextBox txtNewPass;
        private Label label4;
        private Button btnXacnhan;
        private Button btnHuy;
        private GroupBox groupBox1;
        private Label label5;
        private TextBox txtConfirmNewPass;
        private CheckBox ckShowPass;
    }
}