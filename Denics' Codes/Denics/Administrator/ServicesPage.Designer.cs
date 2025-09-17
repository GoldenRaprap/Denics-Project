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
            txtServiceID = new TextBox();
            removebtn = new Button();
            serviceIDlabel = new Label();
            serviceNamelabel = new Label();
            SideBarBackground = new Panel();
            NotificationButton = new Panel();
            ReportButton = new Panel();
            HomeButton = new Panel();
            ServicesButton = new Panel();
            AvailabilityButton = new Panel();
            AppointmentButton = new Panel();
            PatientButton = new Panel();
            DoctorButton = new Panel();
            ((System.ComponentModel.ISupportInitialize)Servicestb).BeginInit();
            SideBarBackground.SuspendLayout();
            SuspendLayout();
            // 
            // Servicestb
            // 
            Servicestb.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Servicestb.Location = new Point(189, 146);
            Servicestb.Margin = new Padding(3, 2, 3, 2);
            Servicestb.Name = "Servicestb";
            Servicestb.RowHeadersWidth = 51;
            Servicestb.Size = new Size(258, 369);
            Servicestb.TabIndex = 0;
            Servicestb.CellClick += Servicestb_CellClick;
            // 
            // Addbtn
            // 
            Addbtn.Location = new Point(174, 103);
            Addbtn.Margin = new Padding(3, 2, 3, 2);
            Addbtn.Name = "Addbtn";
            Addbtn.Size = new Size(82, 22);
            Addbtn.TabIndex = 1;
            Addbtn.Text = "Add";
            Addbtn.UseVisualStyleBackColor = true;
            Addbtn.Click += Addbtn_Click;
            // 
            // editbtn
            // 
            editbtn.Location = new Point(385, 103);
            editbtn.Margin = new Padding(3, 2, 3, 2);
            editbtn.Name = "editbtn";
            editbtn.Size = new Size(82, 22);
            editbtn.TabIndex = 3;
            editbtn.Text = "Edit";
            editbtn.UseVisualStyleBackColor = true;
            editbtn.Click += editbtn_Click;
            // 
            // txtServiceName
            // 
            txtServiceName.Location = new Point(267, 66);
            txtServiceName.Margin = new Padding(3, 2, 3, 2);
            txtServiceName.Name = "txtServiceName";
            txtServiceName.Size = new Size(200, 23);
            txtServiceName.TabIndex = 5;
            // 
            // txtServiceID
            // 
            txtServiceID.Location = new Point(267, 37);
            txtServiceID.Margin = new Padding(3, 2, 3, 2);
            txtServiceID.Name = "txtServiceID";
            txtServiceID.Size = new Size(56, 23);
            txtServiceID.TabIndex = 6;
            // 
            // removebtn
            // 
            removebtn.Location = new Point(281, 103);
            removebtn.Margin = new Padding(3, 2, 3, 2);
            removebtn.Name = "removebtn";
            removebtn.Size = new Size(82, 22);
            removebtn.TabIndex = 7;
            removebtn.Text = "Remove";
            removebtn.UseVisualStyleBackColor = true;
            removebtn.Click += removebtn_Click_1;
            // 
            // serviceIDlabel
            // 
            serviceIDlabel.AutoSize = true;
            serviceIDlabel.Location = new Point(232, 40);
            serviceIDlabel.Name = "serviceIDlabel";
            serviceIDlabel.Size = new Size(24, 15);
            serviceIDlabel.TabIndex = 9;
            serviceIDlabel.Text = "ID: ";
            // 
            // serviceNamelabel
            // 
            serviceNamelabel.AutoSize = true;
            serviceNamelabel.Location = new Point(171, 66);
            serviceNamelabel.Name = "serviceNamelabel";
            serviceNamelabel.Size = new Size(85, 15);
            serviceNamelabel.TabIndex = 10;
            serviceNamelabel.Text = "Service Name: ";
            // 
            // SideBarBackground
            // 
            SideBarBackground.BackColor = Color.CornflowerBlue;
            SideBarBackground.Controls.Add(NotificationButton);
            SideBarBackground.Controls.Add(ReportButton);
            SideBarBackground.Controls.Add(HomeButton);
            SideBarBackground.Controls.Add(ServicesButton);
            SideBarBackground.Controls.Add(AvailabilityButton);
            SideBarBackground.Controls.Add(AppointmentButton);
            SideBarBackground.Controls.Add(PatientButton);
            SideBarBackground.Controls.Add(DoctorButton);
            SideBarBackground.Location = new Point(0, 0);
            SideBarBackground.Name = "SideBarBackground";
            SideBarBackground.Size = new Size(75, 561);
            SideBarBackground.TabIndex = 8;
            // 
            // NotificationButton
            // 
            NotificationButton.BackgroundImage = Properties.Resources.Bell;
            NotificationButton.BackgroundImageLayout = ImageLayout.Stretch;
            NotificationButton.Location = new Point(16, 395);
            NotificationButton.Name = "NotificationButton";
            NotificationButton.Size = new Size(40, 40);
            NotificationButton.TabIndex = 4;
            NotificationButton.Click += NotificationButton_Click;
            // 
            // ReportButton
            // 
            ReportButton.BackgroundImage = Properties.Resources.report;
            ReportButton.BackgroundImageLayout = ImageLayout.Stretch;
            ReportButton.Location = new Point(16, 349);
            ReportButton.Name = "ReportButton";
            ReportButton.Size = new Size(40, 40);
            ReportButton.TabIndex = 3;
            ReportButton.Click += ReportButton_Click;
            // 
            // HomeButton
            // 
            HomeButton.BackgroundImage = Properties.Resources.white_home;
            HomeButton.BackgroundImageLayout = ImageLayout.Stretch;
            HomeButton.Location = new Point(16, 25);
            HomeButton.Name = "HomeButton";
            HomeButton.Size = new Size(40, 40);
            HomeButton.TabIndex = 2;
            HomeButton.Click += HomeButton_Click;
            // 
            // ServicesButton
            // 
            ServicesButton.BackgroundImage = Properties.Resources.service;
            ServicesButton.BackgroundImageLayout = ImageLayout.Stretch;
            ServicesButton.Location = new Point(16, 303);
            ServicesButton.Name = "ServicesButton";
            ServicesButton.Size = new Size(40, 40);
            ServicesButton.TabIndex = 2;
            ServicesButton.Click += ServicesButton_Click;
            // 
            // AvailabilityButton
            // 
            AvailabilityButton.BackgroundImage = Properties.Resources.calendar;
            AvailabilityButton.BackgroundImageLayout = ImageLayout.Stretch;
            AvailabilityButton.Location = new Point(16, 211);
            AvailabilityButton.Name = "AvailabilityButton";
            AvailabilityButton.Size = new Size(40, 40);
            AvailabilityButton.TabIndex = 2;
            AvailabilityButton.Click += AvailabilityButton_Click;
            // 
            // AppointmentButton
            // 
            AppointmentButton.BackgroundImage = Properties.Resources.Book;
            AppointmentButton.BackgroundImageLayout = ImageLayout.Stretch;
            AppointmentButton.Location = new Point(16, 257);
            AppointmentButton.Name = "AppointmentButton";
            AppointmentButton.Size = new Size(40, 40);
            AppointmentButton.TabIndex = 1;
            AppointmentButton.Click += AppointmentButton_Click;
            // 
            // PatientButton
            // 
            PatientButton.BackgroundImage = Properties.Resources.patient;
            PatientButton.BackgroundImageLayout = ImageLayout.Stretch;
            PatientButton.Location = new Point(16, 119);
            PatientButton.Name = "PatientButton";
            PatientButton.Size = new Size(40, 40);
            PatientButton.TabIndex = 1;
            PatientButton.Click += PatientButton_Click;
            // 
            // DoctorButton
            // 
            DoctorButton.BackgroundImage = Properties.Resources.doctor;
            DoctorButton.BackgroundImageLayout = ImageLayout.Stretch;
            DoctorButton.Location = new Point(16, 165);
            DoctorButton.Name = "DoctorButton";
            DoctorButton.Size = new Size(40, 40);
            DoctorButton.TabIndex = 0;
            DoctorButton.Click += DoctorButton_Click;
            // 
            // ServicesPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 561);
            Controls.Add(serviceNamelabel);
            Controls.Add(serviceIDlabel);
            Controls.Add(SideBarBackground);
            Controls.Add(removebtn);
            Controls.Add(txtServiceID);
            Controls.Add(txtServiceName);
            Controls.Add(editbtn);
            Controls.Add(Addbtn);
            Controls.Add(Servicestb);
            Name = "ServicesPage";
            Text = "Service Management";
            Load += ServicesPage_Load;
            ((System.ComponentModel.ISupportInitialize)Servicestb).EndInit();
            SideBarBackground.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView Servicestb;
        private Button Addbtn;
        private Button editbtn;
        private TextBox txtServiceName;
        private TextBox txtServiceID;
        private Button removebtn;
        private Label serviceIDlabel;
        private Label serviceNamelabel;
        private Panel SideBarBackground;
        private Panel NotificationButton;
        private Panel ReportButton;
        private Panel HomeButton;
        private Panel ServicesButton;
        private Panel AvailabilityButton;
        private Panel AppointmentButton;
        private Panel PatientButton;
        private Panel DoctorButton;

    }
}