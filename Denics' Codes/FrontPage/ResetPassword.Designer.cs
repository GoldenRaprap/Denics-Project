namespace Denics.FrontPage
{
    partial class ResetPassword
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
            btnUpdatePass = new Button();
            label4 = new Label();
            txtboxConfirmPass = new TextBox();
            label3 = new Label();
            txtboxNewPass = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnUpdatePass
            // 
            btnUpdatePass.BackColor = Color.CornflowerBlue;
            btnUpdatePass.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUpdatePass.ForeColor = Color.White;
            btnUpdatePass.Location = new Point(195, 309);
            btnUpdatePass.Name = "btnUpdatePass";
            btnUpdatePass.Size = new Size(408, 41);
            btnUpdatePass.TabIndex = 15;
            btnUpdatePass.Text = "Update Password";
            btnUpdatePass.UseVisualStyleBackColor = false;
            btnUpdatePass.Click += btnUpdatePass_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Sitka Text", 12F);
            label4.ForeColor = Color.White;
            label4.Location = new Point(148, 209);
            label4.Name = "label4";
            label4.Size = new Size(146, 23);
            label4.TabIndex = 14;
            label4.Text = "Confirm Password";
            // 
            // txtboxConfirmPass
            // 
            txtboxConfirmPass.Font = new Font("Sitka Text", 11.249999F);
            txtboxConfirmPass.Location = new Point(148, 233);
            txtboxConfirmPass.Multiline = true;
            txtboxConfirmPass.Name = "txtboxConfirmPass";
            txtboxConfirmPass.Size = new Size(491, 39);
            txtboxConfirmPass.TabIndex = 13;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Sitka Text", 12F);
            label3.ForeColor = Color.White;
            label3.Location = new Point(148, 121);
            label3.Name = "label3";
            label3.Size = new Size(162, 23);
            label3.TabIndex = 11;
            label3.Text = "Enter new password";
            // 
            // txtboxNewPass
            // 
            txtboxNewPass.Font = new Font("Sitka Text", 11.249999F);
            txtboxNewPass.Location = new Point(148, 145);
            txtboxNewPass.Multiline = true;
            txtboxNewPass.Name = "txtboxNewPass";
            txtboxNewPass.Size = new Size(491, 39);
            txtboxNewPass.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Sitka Heading", 24F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(161, 36);
            label1.Name = "label1";
            label1.Size = new Size(475, 47);
            label1.TabIndex = 8;
            label1.Text = "Please enter your new password";
            // 
            // ResetPassword
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Setting_bg;
            ClientSize = new Size(800, 450);
            Controls.Add(btnUpdatePass);
            Controls.Add(label4);
            Controls.Add(txtboxConfirmPass);
            Controls.Add(label3);
            Controls.Add(txtboxNewPass);
            Controls.Add(label1);
            Name = "ResetPassword";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ResetPassword";
            Load += ResetPassword_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnUpdatePass;
        private Label label4;
        private TextBox txtboxConfirmPass;
        private Label label3;
        private TextBox txtboxNewPass;
        private Label label1;
    }
}