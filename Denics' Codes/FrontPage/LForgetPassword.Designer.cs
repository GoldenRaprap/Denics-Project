namespace Denics.FrontPage
{
    partial class Forgot_Password
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
            label2 = new Label();
            txtboxEmail = new TextBox();
            label3 = new Label();
            btnSendCode = new Button();
            btnVerify = new Button();
            label4 = new Label();
            txtboxCode = new TextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Sitka Heading", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(30, 22);
            label1.Name = "label1";
            label1.Size = new Size(240, 47);
            label1.TabIndex = 0;
            label1.Text = "Reset Password";
            // 
            // label2
            // 
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Sitka Subheading", 15.7499981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(30, 69);
            label2.Name = "label2";
            label2.Size = new Size(371, 30);
            label2.TabIndex = 1;
            label2.Text = "Fill up form to reset the password";
            // 
            // txtboxEmail
            // 
            txtboxEmail.Font = new Font("Sitka Text", 11.249999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtboxEmail.Location = new Point(112, 169);
            txtboxEmail.Multiline = true;
            txtboxEmail.Name = "txtboxEmail";
            txtboxEmail.Size = new Size(453, 33);
            txtboxEmail.TabIndex = 2;
            txtboxEmail.TextChanged += textBox1_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Sitka Text", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(112, 145);
            label3.Name = "label3";
            label3.Size = new Size(158, 23);
            label3.TabIndex = 3;
            label3.Text = "Enter email address";
            // 
            // btnSendCode
            // 
            btnSendCode.BackColor = Color.CornflowerBlue;
            btnSendCode.Font = new Font("Sitka Text", 12F);
            btnSendCode.ForeColor = Color.White;
            btnSendCode.Location = new Point(571, 168);
            btnSendCode.Name = "btnSendCode";
            btnSendCode.Size = new Size(151, 33);
            btnSendCode.TabIndex = 4;
            btnSendCode.Text = "Send Code ";
            btnSendCode.UseVisualStyleBackColor = false;
            btnSendCode.Click += btnSendCode_Click;
            // 
            // btnVerify
            // 
            btnVerify.BackColor = Color.CornflowerBlue;
            btnVerify.Font = new Font("Sitka Text", 12F);
            btnVerify.ForeColor = Color.White;
            btnVerify.Location = new Point(571, 260);
            btnVerify.Name = "btnVerify";
            btnVerify.Size = new Size(151, 33);
            btnVerify.TabIndex = 7;
            btnVerify.Text = " Verify";
            btnVerify.UseVisualStyleBackColor = false;
            btnVerify.Click += btnVerify_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Sitka Text", 12F);
            label4.ForeColor = Color.White;
            label4.Location = new Point(112, 236);
            label4.Name = "label4";
            label4.Size = new Size(92, 23);
            label4.TabIndex = 6;
            label4.Text = "Enter Code";
            // 
            // txtboxCode
            // 
            txtboxCode.Font = new Font("Sitka Text", 11.249999F);
            txtboxCode.Location = new Point(112, 260);
            txtboxCode.Multiline = true;
            txtboxCode.Name = "txtboxCode";
            txtboxCode.Size = new Size(453, 33);
            txtboxCode.TabIndex = 5;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.GradientActiveCaption;
            button1.Font = new Font("Segoe UI", 12F);
            button1.Location = new Point(199, 363);
            button1.Name = "button1";
            button1.Size = new Size(408, 41);
            button1.TabIndex = 8;
            button1.Text = "Cancel";
            button1.UseVisualStyleBackColor = false;
            // 
            // Forgot_Password
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Setting_bg;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(btnVerify);
            Controls.Add(label4);
            Controls.Add(txtboxCode);
            Controls.Add(btnSendCode);
            Controls.Add(label3);
            Controls.Add(txtboxEmail);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Forgot_Password";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Forgot_Password";
            Load += Forgot_Password_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtboxEmail;
        private Label label3;
        private Button btnSendCode;
        private Button btnVerify;
        private Label label4;
        private TextBox txtboxCode;
        private Button button1;
    }
}