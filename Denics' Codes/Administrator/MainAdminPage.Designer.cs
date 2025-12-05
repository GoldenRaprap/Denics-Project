namespace Denics.Administrator
{
    partial class MainAdminPage
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
            SideBarBackground = new Panel();
            Exit_pnl = new Panel();
            Logout_btn = new Panel();
            ReportButton = new Panel();
            HomeButton = new Panel();
            ServicesButton = new Panel();
            AvailabilityButton = new Panel();
            AppointmentButton = new Panel();
            PatientButton = new Panel();
            DoctorButton = new Panel();
            TopHeader = new Panel();
            Top_lbl = new Label();
            Body_pnl = new Panel();
            ViewReport_pnl = new Panel();
            ViewReport_lbl = new Label();
            Report_pnl_img = new Panel();
            Services_pnl = new Panel();
            Service_lbl = new Label();
            Service_pnl_img = new Panel();
            backViewAppointments_pnl = new Panel();
            ViewAppointment_lbl = new Label();
            Booking_pnl_img = new Panel();
            Schedule_pnl = new Panel();
            Schedulepanel_lbl = new Label();
            Schedule_pnl_img = new Panel();
            Doctors_pnl = new Panel();
            Doctorpanel_lbl = new Label();
            Doctor_pnl_img = new Panel();
            User_Panel = new Panel();
            User_lbl = new Label();
            User_pnl_img = new Panel();
            SideBarBackground.SuspendLayout();
            Exit_pnl.SuspendLayout();
            TopHeader.SuspendLayout();
            Body_pnl.SuspendLayout();
            ViewReport_pnl.SuspendLayout();
            Services_pnl.SuspendLayout();
            backViewAppointments_pnl.SuspendLayout();
            Schedule_pnl.SuspendLayout();
            Doctors_pnl.SuspendLayout();
            User_Panel.SuspendLayout();
            SuspendLayout();
            // 
            // SideBarBackground
            // 
            SideBarBackground.BackColor = Color.CornflowerBlue;
            SideBarBackground.Controls.Add(Exit_pnl);
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
            SideBarBackground.TabIndex = 0;
            // 
            // Exit_pnl
            // 
            Exit_pnl.Controls.Add(Logout_btn);
            Exit_pnl.Dock = DockStyle.Bottom;
            Exit_pnl.Location = new Point(0, 501);
            Exit_pnl.Name = "Exit_pnl";
            Exit_pnl.Size = new Size(75, 60);
            Exit_pnl.TabIndex = 6;
            // 
            // Logout_btn
            // 
            Logout_btn.Anchor = AnchorStyles.Top;
            Logout_btn.BackgroundImage = Properties.Resources.IconLogout;
            Logout_btn.BackgroundImageLayout = ImageLayout.Stretch;
            Logout_btn.Cursor = Cursors.Hand;
            Logout_btn.Location = new Point(16, 8);
            Logout_btn.Name = "Logout_btn";
            Logout_btn.Size = new Size(40, 40);
            Logout_btn.TabIndex = 5;
            Logout_btn.Click += Logout_btn_Click;
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
            TopHeader.Size = new Size(809, 65);
            TopHeader.TabIndex = 1;
            // 
            // Top_lbl
            // 
            Top_lbl.AutoSize = true;
            Top_lbl.Font = new Font("Sitka Subheading", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Top_lbl.ForeColor = SystemColors.ControlLight;
            Top_lbl.Location = new Point(23, 9);
            Top_lbl.Name = "Top_lbl";
            Top_lbl.Size = new Size(111, 47);
            Top_lbl.TabIndex = 1;
            Top_lbl.Text = "Admin";
            // 
            // Body_pnl
            // 
            Body_pnl.Controls.Add(ViewReport_pnl);
            Body_pnl.Controls.Add(Services_pnl);
            Body_pnl.Controls.Add(backViewAppointments_pnl);
            Body_pnl.Controls.Add(Schedule_pnl);
            Body_pnl.Controls.Add(Doctors_pnl);
            Body_pnl.Controls.Add(User_Panel);
            Body_pnl.Dock = DockStyle.Fill;
            Body_pnl.Location = new Point(75, 65);
            Body_pnl.Name = "Body_pnl";
            Body_pnl.Size = new Size(809, 496);
            Body_pnl.TabIndex = 2;
            // 
            // ViewReport_pnl
            // 
            ViewReport_pnl.BackColor = Color.RoyalBlue;
            ViewReport_pnl.Controls.Add(ViewReport_lbl);
            ViewReport_pnl.Controls.Add(Report_pnl_img);
            ViewReport_pnl.Cursor = Cursors.Hand;
            ViewReport_pnl.Location = new Point(23, 284);
            ViewReport_pnl.Name = "ViewReport_pnl";
            ViewReport_pnl.Size = new Size(391, 69);
            ViewReport_pnl.TabIndex = 5;
            ViewReport_pnl.Click += ReportButton_Click;
            // 
            // ViewReport_lbl
            // 
            ViewReport_lbl.AutoSize = true;
            ViewReport_lbl.Font = new Font("Sitka Subheading", 20.2499981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ViewReport_lbl.ForeColor = SystemColors.ControlLight;
            ViewReport_lbl.Location = new Point(72, 11);
            ViewReport_lbl.Name = "ViewReport_lbl";
            ViewReport_lbl.Size = new Size(163, 39);
            ViewReport_lbl.TabIndex = 3;
            ViewReport_lbl.Text = "View Report";
            ViewReport_lbl.Click += ReportButton_Click;
            // 
            // Report_pnl_img
            // 
            Report_pnl_img.BackColor = Color.CornflowerBlue;
            Report_pnl_img.BackgroundImage = Properties.Resources.IconReport;
            Report_pnl_img.BackgroundImageLayout = ImageLayout.Stretch;
            Report_pnl_img.Location = new Point(3, 3);
            Report_pnl_img.Name = "Report_pnl_img";
            Report_pnl_img.Size = new Size(63, 63);
            Report_pnl_img.TabIndex = 2;
            Report_pnl_img.Click += ReportButton_Click;
            // 
            // Services_pnl
            // 
            Services_pnl.BackColor = Color.RoyalBlue;
            Services_pnl.Controls.Add(Service_lbl);
            Services_pnl.Controls.Add(Service_pnl_img);
            Services_pnl.Cursor = Cursors.Hand;
            Services_pnl.Location = new Point(23, 192);
            Services_pnl.Name = "Services_pnl";
            Services_pnl.Size = new Size(194, 69);
            Services_pnl.TabIndex = 4;
            Services_pnl.Click += ServicesButton_Click;
            // 
            // Service_lbl
            // 
            Service_lbl.AutoSize = true;
            Service_lbl.Font = new Font("Sitka Subheading", 20.2499981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Service_lbl.ForeColor = SystemColors.ControlLight;
            Service_lbl.Location = new Point(69, 11);
            Service_lbl.Name = "Service_lbl";
            Service_lbl.Size = new Size(115, 39);
            Service_lbl.TabIndex = 3;
            Service_lbl.Text = "Services";
            Service_lbl.Click += ServicesButton_Click;
            // 
            // Service_pnl_img
            // 
            Service_pnl_img.BackColor = Color.CornflowerBlue;
            Service_pnl_img.BackgroundImage = Properties.Resources.IconService;
            Service_pnl_img.BackgroundImageLayout = ImageLayout.Stretch;
            Service_pnl_img.Location = new Point(3, 3);
            Service_pnl_img.Name = "Service_pnl_img";
            Service_pnl_img.Size = new Size(63, 63);
            Service_pnl_img.TabIndex = 2;
            Service_pnl_img.Click += ServicesButton_Click;
            // 
            // backViewAppointments_pnl
            // 
            backViewAppointments_pnl.BackColor = Color.RoyalBlue;
            backViewAppointments_pnl.Controls.Add(ViewAppointment_lbl);
            backViewAppointments_pnl.Controls.Add(Booking_pnl_img);
            backViewAppointments_pnl.Cursor = Cursors.Hand;
            backViewAppointments_pnl.Location = new Point(23, 25);
            backViewAppointments_pnl.Name = "backViewAppointments_pnl";
            backViewAppointments_pnl.Size = new Size(391, 69);
            backViewAppointments_pnl.TabIndex = 4;
            backViewAppointments_pnl.Click += AppointmentButton_Click;
            // 
            // ViewAppointment_lbl
            // 
            ViewAppointment_lbl.AutoSize = true;
            ViewAppointment_lbl.Font = new Font("Sitka Subheading", 20.2499981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ViewAppointment_lbl.ForeColor = SystemColors.ControlLight;
            ViewAppointment_lbl.Location = new Point(72, 11);
            ViewAppointment_lbl.Name = "ViewAppointment_lbl";
            ViewAppointment_lbl.Size = new Size(247, 39);
            ViewAppointment_lbl.TabIndex = 3;
            ViewAppointment_lbl.Text = "View Appointments";
            ViewAppointment_lbl.Click += AppointmentButton_Click;
            // 
            // Booking_pnl_img
            // 
            Booking_pnl_img.BackColor = Color.CornflowerBlue;
            Booking_pnl_img.BackgroundImage = Properties.Resources.IconBook;
            Booking_pnl_img.BackgroundImageLayout = ImageLayout.Stretch;
            Booking_pnl_img.Location = new Point(3, 3);
            Booking_pnl_img.Name = "Booking_pnl_img";
            Booking_pnl_img.Size = new Size(63, 63);
            Booking_pnl_img.TabIndex = 2;
            Booking_pnl_img.Click += AppointmentButton_Click;
            // 
            // Schedule_pnl
            // 
            Schedule_pnl.BackColor = Color.RoyalBlue;
            Schedule_pnl.Controls.Add(Schedulepanel_lbl);
            Schedule_pnl.Controls.Add(Schedule_pnl_img);
            Schedule_pnl.Cursor = Cursors.Hand;
            Schedule_pnl.Location = new Point(220, 192);
            Schedule_pnl.Name = "Schedule_pnl";
            Schedule_pnl.Size = new Size(194, 69);
            Schedule_pnl.TabIndex = 4;
            Schedule_pnl.Click += AvailabilityButton_Click;
            // 
            // Schedulepanel_lbl
            // 
            Schedulepanel_lbl.AutoSize = true;
            Schedulepanel_lbl.Font = new Font("Sitka Subheading", 20.2499981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Schedulepanel_lbl.ForeColor = SystemColors.ControlLight;
            Schedulepanel_lbl.Location = new Point(69, 11);
            Schedulepanel_lbl.Name = "Schedulepanel_lbl";
            Schedulepanel_lbl.Size = new Size(122, 39);
            Schedulepanel_lbl.TabIndex = 3;
            Schedulepanel_lbl.Text = "Schedule";
            Schedulepanel_lbl.Click += AvailabilityButton_Click;
            // 
            // Schedule_pnl_img
            // 
            Schedule_pnl_img.BackColor = Color.CornflowerBlue;
            Schedule_pnl_img.BackgroundImage = Properties.Resources.Iconcalendar;
            Schedule_pnl_img.BackgroundImageLayout = ImageLayout.Stretch;
            Schedule_pnl_img.Location = new Point(3, 3);
            Schedule_pnl_img.Name = "Schedule_pnl_img";
            Schedule_pnl_img.Size = new Size(63, 63);
            Schedule_pnl_img.TabIndex = 2;
            Schedule_pnl_img.Click += AvailabilityButton_Click;
            // 
            // Doctors_pnl
            // 
            Doctors_pnl.BackColor = Color.RoyalBlue;
            Doctors_pnl.Controls.Add(Doctorpanel_lbl);
            Doctors_pnl.Controls.Add(Doctor_pnl_img);
            Doctors_pnl.Cursor = Cursors.Hand;
            Doctors_pnl.Location = new Point(220, 117);
            Doctors_pnl.Name = "Doctors_pnl";
            Doctors_pnl.Size = new Size(194, 69);
            Doctors_pnl.TabIndex = 4;
            Doctors_pnl.Click += DoctorButton_Click;
            // 
            // Doctorpanel_lbl
            // 
            Doctorpanel_lbl.AutoSize = true;
            Doctorpanel_lbl.Font = new Font("Sitka Subheading", 20.2499981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Doctorpanel_lbl.ForeColor = SystemColors.ControlLight;
            Doctorpanel_lbl.Location = new Point(69, 11);
            Doctorpanel_lbl.Name = "Doctorpanel_lbl";
            Doctorpanel_lbl.Size = new Size(108, 39);
            Doctorpanel_lbl.TabIndex = 3;
            Doctorpanel_lbl.Text = "Doctors";
            Doctorpanel_lbl.Click += DoctorButton_Click;
            // 
            // Doctor_pnl_img
            // 
            Doctor_pnl_img.BackColor = Color.CornflowerBlue;
            Doctor_pnl_img.BackgroundImage = Properties.Resources.IconDoctor;
            Doctor_pnl_img.BackgroundImageLayout = ImageLayout.Stretch;
            Doctor_pnl_img.Location = new Point(3, 3);
            Doctor_pnl_img.Name = "Doctor_pnl_img";
            Doctor_pnl_img.Size = new Size(63, 63);
            Doctor_pnl_img.TabIndex = 2;
            Doctor_pnl_img.Click += DoctorButton_Click;
            // 
            // User_Panel
            // 
            User_Panel.BackColor = Color.RoyalBlue;
            User_Panel.Controls.Add(User_lbl);
            User_Panel.Controls.Add(User_pnl_img);
            User_Panel.Cursor = Cursors.Hand;
            User_Panel.Location = new Point(23, 117);
            User_Panel.Name = "User_Panel";
            User_Panel.Size = new Size(194, 69);
            User_Panel.TabIndex = 0;
            User_Panel.Click += PatientButton_Click;
            // 
            // User_lbl
            // 
            User_lbl.AutoSize = true;
            User_lbl.Font = new Font("Sitka Subheading", 20.2499981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            User_lbl.ForeColor = SystemColors.ControlLight;
            User_lbl.Location = new Point(69, 11);
            User_lbl.Name = "User_lbl";
            User_lbl.Size = new Size(85, 39);
            User_lbl.TabIndex = 3;
            User_lbl.Text = "Users";
            User_lbl.Click += PatientButton_Click;
            // 
            // User_pnl_img
            // 
            User_pnl_img.BackColor = Color.CornflowerBlue;
            User_pnl_img.BackgroundImage = Properties.Resources.IconPatient;
            User_pnl_img.BackgroundImageLayout = ImageLayout.Stretch;
            User_pnl_img.Location = new Point(3, 3);
            User_pnl_img.Name = "User_pnl_img";
            User_pnl_img.Size = new Size(63, 63);
            User_pnl_img.TabIndex = 2;
            User_pnl_img.Click += PatientButton_Click;
            // 
            // MainAdminPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 561);
            Controls.Add(Body_pnl);
            Controls.Add(TopHeader);
            Controls.Add(SideBarBackground);
            Name = "MainAdminPage";
            Text = "Main Admin";
            Load += AdminDesignLayout_Load;
            SideBarBackground.ResumeLayout(false);
            Exit_pnl.ResumeLayout(false);
            TopHeader.ResumeLayout(false);
            TopHeader.PerformLayout();
            Body_pnl.ResumeLayout(false);
            ViewReport_pnl.ResumeLayout(false);
            ViewReport_pnl.PerformLayout();
            Services_pnl.ResumeLayout(false);
            Services_pnl.PerformLayout();
            backViewAppointments_pnl.ResumeLayout(false);
            backViewAppointments_pnl.PerformLayout();
            Schedule_pnl.ResumeLayout(false);
            Schedule_pnl.PerformLayout();
            Doctors_pnl.ResumeLayout(false);
            Doctors_pnl.PerformLayout();
            User_Panel.ResumeLayout(false);
            User_Panel.PerformLayout();
            ResumeLayout(false);



        }

        #endregion

        private Panel SideBarBackground;
        private Panel DoctorButton;
        private Panel AppointmentButton;
        private Panel PatientButton;
        private Panel ServicesButton;
        private Panel AvailabilityButton;
        private Panel HomeButton;
        private Panel ReportButton;
        private Panel TopHeader;
        private Label Welcome_lbl;
        private Panel Body_pnl;
        private Panel User_Panel;
        private Label User_lbl;
        private Panel User_pnl_img;
        private Panel Doctors_pnl;
        private Label Doctorpanel_lbl;
        private Panel Doctor_pnl_img;
        private Panel backViewAppointments_pnl;
        private Label ViewAppointment_lbl;
        private Panel Booking_pnl_img;
        private Panel Schedule_pnl;
        private Label Schedulepanel_lbl;
        private Panel Schedule_pnl_img;
        private Panel ViewReport_pnl;
        private Label ViewReport_lbl;
        private Panel Report_pnl_img;
        private Panel Services_pnl;
        private Label Service_lbl;
        private Panel Service_pnl_img;
        private Panel Logout_btn;
        private PictureBox DenicsLogo;
        private Label Top_lbl;
        private Panel Exit_pnl;
    }
}