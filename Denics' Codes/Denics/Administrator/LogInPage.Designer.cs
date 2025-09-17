namespace Denics.Administrator
{
    partial class LogInPage
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
            Emailtxtb = new TextBox();
            Passwordtxtb = new TextBox();
            LogInbtn = new Button();
            Registerbtn = new Button();
            Emaillable = new Label();
            Passwordlabel = new Label();
            SuspendLayout();
            // 
            // Emailtxtb
            // 
            Emailtxtb.Location = new Point(223, 85);
            Emailtxtb.Name = "Emailtxtb";
            Emailtxtb.Size = new Size(240, 23);
            Emailtxtb.TabIndex = 0;
            // 
            // Passwordtxtb
            // 
            Passwordtxtb.Location = new Point(223, 131);
            Passwordtxtb.Name = "Passwordtxtb";
            Passwordtxtb.Size = new Size(240, 23);
            Passwordtxtb.TabIndex = 1;
            // 
            // LogInbtn
            // 
            LogInbtn.Location = new Point(223, 190);
            LogInbtn.Name = "LogInbtn";
            LogInbtn.Size = new Size(75, 23);
            LogInbtn.TabIndex = 2;
            LogInbtn.Text = "Log In";
            LogInbtn.UseVisualStyleBackColor = true;
            LogInbtn.Click += LogInbtn_Click;
            // 
            // Registerbtn
            // 
            Registerbtn.Location = new Point(388, 190);
            Registerbtn.Name = "Registerbtn";
            Registerbtn.Size = new Size(75, 23);
            Registerbtn.TabIndex = 3;
            Registerbtn.Text = "Register";
            Registerbtn.UseVisualStyleBackColor = true;
            // 
            // Emaillable
            // 
            Emaillable.AutoSize = true;
            Emaillable.Location = new Point(223, 67);
            Emaillable.Name = "Emaillable";
            Emaillable.Size = new Size(42, 15);
            Emaillable.TabIndex = 4;
            Emaillable.Text = "Email: ";
            // 
            // Passwordlabel
            // 
            Passwordlabel.AutoSize = true;
            Passwordlabel.Location = new Point(223, 113);
            Passwordlabel.Name = "Passwordlabel";
            Passwordlabel.Size = new Size(63, 15);
            Passwordlabel.TabIndex = 5;
            Passwordlabel.Text = "Password: ";
            // 
            // LogInPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 361);
            Controls.Add(Passwordlabel);
            Controls.Add(Emaillable);
            Controls.Add(Registerbtn);
            Controls.Add(LogInbtn);
            Controls.Add(Passwordtxtb);
            Controls.Add(Emailtxtb);
            Name = "LogInPage";
            Text = "LogIn Page";
            Load += LogInPage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox Emailtxtb;
        private TextBox Passwordtxtb;
        private Button LogInbtn;
        private Button Registerbtn;
        private Label Emaillable;
        private Label Passwordlabel;
    }
}