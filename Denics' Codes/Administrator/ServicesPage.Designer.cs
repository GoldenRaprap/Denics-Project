namespace Denics.Administrator
{
    partial class ServicesPage
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
            Servicestb = new DataGridView();
            Addbtn = new Button();
            editbtn = new Button();
            txtServiceName = new TextBox();
            removebtn = new Button();
            serviceNamelabel = new Label();
            cmbStatus = new ComboBox();
            archivebtn = new Button();
            labelstats = new Label();
            btnShowArchived = new Button();
            btnShowActive = new Button();
            btnRestore = new Button();
            SideBarBackground = new Panel();
            ReportButton = new Panel();
            HomeButton = new Panel();
            ServicesButton = new Panel();
            AvailabilityButton = new Panel();
            AppointmentButton = new Panel();
            PatientButton = new Panel();
            DoctorButton = new Panel();
            TopHeader = new Panel();
            Top_lbl = new Label();
            ((System.ComponentModel.ISupportInitialize)Servicestb).BeginInit();
            SideBarBackground.SuspendLayout();
            TopHeader.SuspendLayout();
            SuspendLayout();
            // 
            // Servicestb
            // 
            Servicestb.AllowUserToAddRows = false;
            Servicestb.AllowUserToDeleteRows = false;
            Servicestb.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Servicestb.Location = new Point(403, 119);
            Servicestb.Margin = new Padding(3, 2, 3, 2);
            Servicestb.Name = "Servicestb";
            Servicestb.ReadOnly = true;
            Servicestb.RowHeadersWidth = 51;
            Servicestb.Size = new Size(255, 369);
            Servicestb.TabIndex = 0;
            Servicestb.CellClick += Servicestb_CellClick;
            // 
            // Addbtn
            // 
            Addbtn.Location = new Point(132, 243);
            Addbtn.Margin = new Padding(3, 2, 3, 2);
            Addbtn.Name = "Addbtn";
            Addbtn.Size = new Size(97, 22);
            Addbtn.TabIndex = 1;
            Addbtn.Text = "Add";
            Addbtn.UseVisualStyleBackColor = true;
            Addbtn.Click += Addbtn_Click;
            // 
            // editbtn
            // 
            editbtn.Location = new Point(244, 243);
            editbtn.Margin = new Padding(3, 2, 3, 2);
            editbtn.Name = "editbtn";
            editbtn.Size = new Size(97, 22);
            editbtn.TabIndex = 3;
            editbtn.Text = "Edit";
            editbtn.UseVisualStyleBackColor = true;
            editbtn.Click += editbtn_Click;
            // 
            // txtServiceName
            // 
            txtServiceName.Location = new Point(188, 138);
            txtServiceName.Margin = new Padding(3, 2, 3, 2);
            txtServiceName.Name = "txtServiceName";
            txtServiceName.Size = new Size(153, 23);
            txtServiceName.TabIndex = 5;
            // 
            // removebtn
            // 
            removebtn.Location = new Point(188, 278);
            removebtn.Margin = new Padding(3, 2, 3, 2);
            removebtn.Name = "removebtn";
            removebtn.Size = new Size(97, 22);
            removebtn.TabIndex = 7;
            removebtn.Text = "Remove";
            removebtn.UseVisualStyleBackColor = true;
            removebtn.Click += removebtn_Click_1;
            // 
            // serviceNamelabel
            // 
            serviceNamelabel.AutoSize = true;
            serviceNamelabel.Location = new Point(132, 141);
            serviceNamelabel.Name = "serviceNamelabel";
            serviceNamelabel.Size = new Size(45, 15);
            serviceNamelabel.TabIndex = 10;
            serviceNamelabel.Text = "Name: ";
            // 
            // cmbStatus
            // 
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(188, 182);
            cmbStatus.Margin = new Padding(3, 2, 3, 2);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(153, 23);
            cmbStatus.TabIndex = 11;
            // 
            // archivebtn
            // 
            archivebtn.Location = new Point(132, 418);
            archivebtn.Margin = new Padding(3, 2, 3, 2);
            archivebtn.Name = "archivebtn";
            archivebtn.Size = new Size(97, 22);
            archivebtn.TabIndex = 12;
            archivebtn.Text = "Archive";
            archivebtn.UseVisualStyleBackColor = true;
            archivebtn.Click += archivebtn_Click;
            // 
            // labelstats
            // 
            labelstats.AutoSize = true;
            labelstats.Location = new Point(135, 185);
            labelstats.Name = "labelstats";
            labelstats.Size = new Size(42, 15);
            labelstats.TabIndex = 13;
            labelstats.Text = "Status:";
            // 
            // btnShowArchived
            // 
            btnShowArchived.Location = new Point(132, 380);
            btnShowArchived.Margin = new Padding(3, 2, 3, 2);
            btnShowArchived.Name = "btnShowArchived";
            btnShowArchived.Size = new Size(209, 22);
            btnShowArchived.TabIndex = 14;
            btnShowArchived.Text = "Show Archived";
            btnShowArchived.UseVisualStyleBackColor = true;
            btnShowArchived.Click += btnShowArchived_Click;
            // 
            // btnShowActive
            // 
            btnShowActive.Location = new Point(132, 349);
            btnShowActive.Margin = new Padding(3, 2, 3, 2);
            btnShowActive.Name = "btnShowActive";
            btnShowActive.Size = new Size(209, 22);
            btnShowActive.TabIndex = 15;
            btnShowActive.Text = "Show Active";
            btnShowActive.UseVisualStyleBackColor = true;
            btnShowActive.Click += btnShowActive_Click;
            // 
            // btnRestore
            // 
            btnRestore.Location = new Point(244, 418);
            btnRestore.Margin = new Padding(3, 2, 3, 2);
            btnRestore.Name = "btnRestore";
            btnRestore.Size = new Size(97, 22);
            btnRestore.TabIndex = 16;
            btnRestore.Text = "Restore";
            btnRestore.UseVisualStyleBackColor = true;
            btnRestore.Click += btnRestore_Click;
            // 
            // SideBarBackground
            // 
            SideBarBackground.BackColor = Color.CornflowerBlue;
            SideBarBackground.Controls.Add(ReportButton);
            SideBarBackground.Controls.Add(HomeButton);
            SideBarBackground.Controls.Add(ServicesButton);
            SideBarBackground.Controls.Add(AvailabilityButton);
            SideBarBackground.Controls.Add(AppointmentButton);
            SideBarBackground.Controls.Add(PatientButton);
            SideBarBackground.Controls.Add(DoctorButton);
            SideBarBackground.Dock = DockStyle.Left;
            SideBarBackground.Location = new Point(0, 0);
            SideBarBackground.Name = "SideBarBackground";
            SideBarBackground.Size = new Size(75, 561);
            SideBarBackground.TabIndex = 33;
            // 
            // ReportButton
            // 
            ReportButton.BackgroundImage = Properties.Resources.IconReport;
            ReportButton.BackgroundImageLayout = ImageLayout.Stretch;
            ReportButton.Cursor = Cursors.Hand;
            ReportButton.Location = new Point(16, 349);
            ReportButton.Name = "ReportButton";
            ReportButton.Size = new Size(40, 40);
            ReportButton.TabIndex = 3;
            // 
            // HomeButton
            // 
            HomeButton.BackgroundImage = Properties.Resources.IconWhiteHome;
            HomeButton.BackgroundImageLayout = ImageLayout.Stretch;
            HomeButton.Cursor = Cursors.Hand;
            HomeButton.Location = new Point(16, 16);
            HomeButton.Name = "HomeButton";
            HomeButton.Size = new Size(40, 40);
            HomeButton.TabIndex = 2;
            // 
            // ServicesButton
            // 
            ServicesButton.BackgroundImage = Properties.Resources.IconService;
            ServicesButton.BackgroundImageLayout = ImageLayout.Stretch;
            ServicesButton.Cursor = Cursors.Hand;
            ServicesButton.Location = new Point(16, 303);
            ServicesButton.Name = "ServicesButton";
            ServicesButton.Size = new Size(40, 40);
            ServicesButton.TabIndex = 2;
            // 
            // AvailabilityButton
            // 
            AvailabilityButton.BackgroundImage = Properties.Resources.Iconcalendar;
            AvailabilityButton.BackgroundImageLayout = ImageLayout.Stretch;
            AvailabilityButton.Cursor = Cursors.Hand;
            AvailabilityButton.Location = new Point(16, 211);
            AvailabilityButton.Name = "AvailabilityButton";
            AvailabilityButton.Size = new Size(40, 40);
            AvailabilityButton.TabIndex = 2;
            // 
            // AppointmentButton
            // 
            AppointmentButton.BackgroundImage = Properties.Resources.IconBook;
            AppointmentButton.BackgroundImageLayout = ImageLayout.Stretch;
            AppointmentButton.Cursor = Cursors.Hand;
            AppointmentButton.Location = new Point(16, 257);
            AppointmentButton.Name = "AppointmentButton";
            AppointmentButton.Size = new Size(40, 40);
            AppointmentButton.TabIndex = 1;
            // 
            // PatientButton
            // 
            PatientButton.BackgroundImage = Properties.Resources.IconPatient;
            PatientButton.BackgroundImageLayout = ImageLayout.Stretch;
            PatientButton.Cursor = Cursors.Hand;
            PatientButton.Location = new Point(16, 119);
            PatientButton.Name = "PatientButton";
            PatientButton.Size = new Size(40, 40);
            PatientButton.TabIndex = 1;
            // 
            // DoctorButton
            // 
            DoctorButton.BackgroundImage = Properties.Resources.IconDoctor;
            DoctorButton.BackgroundImageLayout = ImageLayout.Stretch;
            DoctorButton.Cursor = Cursors.Hand;
            DoctorButton.Location = new Point(16, 165);
            DoctorButton.Name = "DoctorButton";
            DoctorButton.Size = new Size(40, 40);
            DoctorButton.TabIndex = 0;
            // 
            // TopHeader
            // 
            TopHeader.BackColor = Color.RoyalBlue;
            TopHeader.Controls.Add(Top_lbl);
            TopHeader.Dock = DockStyle.Top;
            TopHeader.Location = new Point(75, 0);
            TopHeader.Name = "TopHeader";
            TopHeader.Size = new Size(659, 65);
            TopHeader.TabIndex = 34;
            // 
            // Top_lbl
            // 
            Top_lbl.AutoSize = true;
            Top_lbl.Font = new Font("Sitka Subheading", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Top_lbl.ForeColor = SystemColors.ControlLight;
            Top_lbl.Location = new Point(20, 9);
            Top_lbl.Name = "Top_lbl";
            Top_lbl.Size = new Size(308, 47);
            Top_lbl.TabIndex = 0;
            Top_lbl.Text = "Service Management";
            // 
            // ServicesPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(734, 561);
            Controls.Add(TopHeader);
            Controls.Add(SideBarBackground);
            Controls.Add(btnRestore);
            Controls.Add(btnShowActive);
            Controls.Add(btnShowArchived);
            Controls.Add(labelstats);
            Controls.Add(archivebtn);
            Controls.Add(cmbStatus);
            Controls.Add(serviceNamelabel);
            Controls.Add(removebtn);
            Controls.Add(txtServiceName);
            Controls.Add(editbtn);
            Controls.Add(Addbtn);
            Controls.Add(Servicestb);
            Name = "ServicesPage";
            Text = "Service Management";
            Load += ServicesPage_Load;
            ((System.ComponentModel.ISupportInitialize)Servicestb).EndInit();
            SideBarBackground.ResumeLayout(false);
            TopHeader.ResumeLayout(false);
            TopHeader.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView Servicestb;
        private Button Addbtn;
        private Button editbtn;
        private TextBox txtServiceName;
        private Button removebtn;
        private Label serviceNamelabel;
        private ComboBox cmbStatus;
        private Button archivebtn;
        private Label labelstats;
        private Button btnShowArchived;
        private Button btnShowActive;
        private Button btnRestore;
        private Panel SideBarBackground;
        private Panel ReportButton;
        private Panel HomeButton;
        private Panel ServicesButton;
        private Panel AvailabilityButton;
        private Panel AppointmentButton;
        private Panel PatientButton;
        private Panel DoctorButton;
        private Panel TopHeader;
        private Label Top_lbl;
    }
}