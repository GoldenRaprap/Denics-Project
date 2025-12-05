namespace Denics.FrontPage
{
    partial class OTPVerification
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
            components = new System.ComponentModel.Container();
            label1 = new Label();
            label2 = new Label();
            boxOTP1 = new TextBox();
            boxOTP2 = new TextBox();
            boxOTP3 = new TextBox();
            boxOTP4 = new TextBox();
            boxOTP5 = new TextBox();
            boxOTP6 = new TextBox();
            buttonVerify = new Button();
            lblResendOTP = new Label();
            lbltime = new Label();
            label3 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Sitka Heading", 24F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(280, 12);
            label1.Name = "label1";
            label1.Size = new Size(262, 47);
            label1.TabIndex = 0;
            label1.Text = "OTP Verification ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(250, 72);
            label2.Name = "label2";
            label2.Size = new Size(311, 25);
            label2.TabIndex = 1;
            label2.Text = "Confirm you OTP sent to your Email";
            // 
            // boxOTP1
            // 
            boxOTP1.BackColor = Color.DarkGray;
            boxOTP1.Font = new Font("Segoe UI", 48F, FontStyle.Bold, GraphicsUnit.Point, 0);
            boxOTP1.Location = new Point(115, 121);
            boxOTP1.Margin = new Padding(0);
            boxOTP1.Multiline = true;
            boxOTP1.Name = "boxOTP1";
            boxOTP1.Size = new Size(71, 96);
            boxOTP1.TabIndex = 2;
            boxOTP1.TextAlign = HorizontalAlignment.Center;
            // 
            // boxOTP2
            // 
            boxOTP2.BackColor = Color.DarkGray;
            boxOTP2.Font = new Font("Segoe UI", 48F, FontStyle.Bold);
            boxOTP2.Location = new Point(213, 121);
            boxOTP2.Margin = new Padding(0);
            boxOTP2.Multiline = true;
            boxOTP2.Name = "boxOTP2";
            boxOTP2.Size = new Size(71, 96);
            boxOTP2.TabIndex = 3;
            boxOTP2.TextAlign = HorizontalAlignment.Center;
            // 
            // boxOTP3
            // 
            boxOTP3.BackColor = Color.DarkGray;
            boxOTP3.Font = new Font("Segoe UI", 48F, FontStyle.Bold);
            boxOTP3.Location = new Point(318, 121);
            boxOTP3.Margin = new Padding(0);
            boxOTP3.Multiline = true;
            boxOTP3.Name = "boxOTP3";
            boxOTP3.Size = new Size(71, 96);
            boxOTP3.TabIndex = 4;
            boxOTP3.TextAlign = HorizontalAlignment.Center;
            // 
            // boxOTP4
            // 
            boxOTP4.BackColor = Color.DarkGray;
            boxOTP4.Font = new Font("Segoe UI", 48F, FontStyle.Bold);
            boxOTP4.Location = new Point(419, 121);
            boxOTP4.Margin = new Padding(0);
            boxOTP4.Multiline = true;
            boxOTP4.Name = "boxOTP4";
            boxOTP4.Size = new Size(71, 96);
            boxOTP4.TabIndex = 5;
            boxOTP4.TextAlign = HorizontalAlignment.Center;
            // 
            // boxOTP5
            // 
            boxOTP5.BackColor = Color.DarkGray;
            boxOTP5.Font = new Font("Segoe UI", 48F, FontStyle.Bold);
            boxOTP5.Location = new Point(529, 121);
            boxOTP5.Margin = new Padding(0);
            boxOTP5.Multiline = true;
            boxOTP5.Name = "boxOTP5";
            boxOTP5.Size = new Size(71, 96);
            boxOTP5.TabIndex = 6;
            boxOTP5.TextAlign = HorizontalAlignment.Center;
            // 
            // boxOTP6
            // 
            boxOTP6.BackColor = Color.DarkGray;
            boxOTP6.Font = new Font("Segoe UI", 48F, FontStyle.Bold);
            boxOTP6.Location = new Point(634, 121);
            boxOTP6.Margin = new Padding(0);
            boxOTP6.Multiline = true;
            boxOTP6.Name = "boxOTP6";
            boxOTP6.Size = new Size(71, 96);
            boxOTP6.TabIndex = 7;
            boxOTP6.TextAlign = HorizontalAlignment.Center;
            // 
            // buttonVerify
            // 
            buttonVerify.BackColor = Color.CornflowerBlue;
            buttonVerify.Font = new Font("Sitka Text", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonVerify.ForeColor = Color.White;
            buttonVerify.Location = new Point(318, 320);
            buttonVerify.Name = "buttonVerify";
            buttonVerify.Size = new Size(172, 38);
            buttonVerify.TabIndex = 8;
            buttonVerify.Text = "Verify";
            buttonVerify.UseVisualStyleBackColor = false;
            buttonVerify.Click += buttonVerify_Click;
            // 
            // lblResendOTP
            // 
            lblResendOTP.AutoSize = true;
            lblResendOTP.BackColor = Color.Transparent;
            lblResendOTP.Cursor = Cursors.Hand;
            lblResendOTP.Font = new Font("Sitka Text", 12F);
            lblResendOTP.ForeColor = Color.White;
            lblResendOTP.Location = new Point(356, 378);
            lblResendOTP.Name = "lblResendOTP";
            lblResendOTP.Size = new Size(98, 23);
            lblResendOTP.TabIndex = 9;
            lblResendOTP.Text = "Resend OTP";
            lblResendOTP.Click += lblResendOTP_Click;
            // 
            // lbltime
            // 
            lbltime.BackColor = Color.Transparent;
            lbltime.Font = new Font("Sitka Text", 12F);
            lbltime.ForeColor = Color.White;
            lbltime.Location = new Point(213, 250);
            lbltime.Name = "lbltime";
            lbltime.Size = new Size(387, 27);
            lbltime.TabIndex = 10;
            lbltime.Text = "00:00";
            lbltime.TextAlign = ContentAlignment.TopCenter;
            // 
            // label3
            // 
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Sitka Text", 12F);
            label3.ForeColor = Color.White;
            label3.Location = new Point(213, 287);
            label3.Name = "label3";
            label3.Size = new Size(387, 21);
            label3.TabIndex = 11;
            label3.Text = "Resend OTP after times up";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // OTPVerification
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Setting_bg;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(lbltime);
            Controls.Add(lblResendOTP);
            Controls.Add(buttonVerify);
            Controls.Add(boxOTP6);
            Controls.Add(boxOTP5);
            Controls.Add(boxOTP4);
            Controls.Add(boxOTP3);
            Controls.Add(boxOTP2);
            Controls.Add(boxOTP1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "OTPVerification";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "OTPVerification";
            Load += OTPVerification_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox boxOTP1;
        private TextBox boxOTP2;
        private TextBox boxOTP3;
        private TextBox boxOTP4;
        private TextBox boxOTP5;
        private TextBox boxOTP6;
        private Button buttonVerify;
        private Label lblResendOTP;
        private Label lbltime;
        private Label label3;
        private System.Windows.Forms.Timer timer1;
    }
}