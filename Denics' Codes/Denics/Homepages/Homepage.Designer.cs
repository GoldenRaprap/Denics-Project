namespace Denics.Homepages
{
    partial class Homepage
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
            TopBackDrop = new Panel();
            Book_Now = new Label();
            Services = new Label();
            Contact = new Label();
            About_Us = new Label();
            Home = new Label();
            Logo = new Label();
            TopBackDrop.SuspendLayout();
            SuspendLayout();
            // 
            // TopBackDrop
            // 
            TopBackDrop.BackColor = Color.CornflowerBlue;
            TopBackDrop.Controls.Add(Book_Now);
            TopBackDrop.Controls.Add(Services);
            TopBackDrop.Controls.Add(Contact);
            TopBackDrop.Controls.Add(About_Us);
            TopBackDrop.Controls.Add(Home);
            TopBackDrop.Controls.Add(Logo);
            TopBackDrop.Location = new Point(0, 0);
            TopBackDrop.Name = "TopBackDrop";
            TopBackDrop.Size = new Size(900, 50);
            TopBackDrop.TabIndex = 0;
            // 
            // Book_Now
            // 
            Book_Now.AutoSize = true;
            Book_Now.ForeColor = Color.White;
            Book_Now.Location = new Point(786, 20);
            Book_Now.Name = "Book_Now";
            Book_Now.Size = new Size(62, 15);
            Book_Now.TabIndex = 5;
            Book_Now.Text = "Book Now";
            // 
            // Services
            // 
            Services.AutoSize = true;
            Services.ForeColor = Color.White;
            Services.Location = new Point(607, 20);
            Services.Name = "Services";
            Services.Size = new Size(49, 15);
            Services.TabIndex = 4;
            Services.Text = "Services";
            Services.Click += Services_Click;
            // 
            // Contact
            // 
            Contact.AutoSize = true;
            Contact.ForeColor = Color.White;
            Contact.Location = new Point(492, 20);
            Contact.Name = "Contact";
            Contact.Size = new Size(49, 15);
            Contact.TabIndex = 3;
            Contact.Text = "Contact";
            Contact.Click += Contact_Click;
            // 
            // About_Us
            // 
            About_Us.AutoSize = true;
            About_Us.ForeColor = Color.White;
            About_Us.Location = new Point(366, 20);
            About_Us.Name = "About_Us";
            About_Us.Size = new Size(56, 15);
            About_Us.TabIndex = 2;
            About_Us.Text = "About Us";
            About_Us.Click += About_Us_Click;
            // 
            // Home
            // 
            Home.AutoSize = true;
            Home.ForeColor = Color.White;
            Home.Location = new Point(235, 20);
            Home.Name = "Home";
            Home.Size = new Size(40, 15);
            Home.TabIndex = 1;
            Home.Text = "Home";
            Home.Click += Home_Click;
            // 
            // Logo
            // 
            Logo.AutoSize = true;
            Logo.ForeColor = Color.White;
            Logo.Location = new Point(40, 20);
            Logo.Name = "Logo";
            Logo.Size = new Size(34, 15);
            Logo.TabIndex = 0;
            Logo.Text = "Logo";
            // 
            // Homepage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 500);
            Controls.Add(TopBackDrop);
            Name = "Homepage";
            Text = "Homepage";
            Load += Homepage_Load;
            TopBackDrop.ResumeLayout(false);
            TopBackDrop.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel TopBackDrop;
        private Label About_Us;
        private Label Home;
        private Label Logo;
        private Label Book_Now;
        private Label Services;
        private Label Contact;
    }
}