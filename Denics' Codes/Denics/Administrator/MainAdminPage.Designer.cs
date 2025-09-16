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
            NotificationButton = new Panel();
            ReportButton = new Panel();
            HomeButton = new Panel();
            ServicesButton = new Panel();
            AvailabilityButton = new Panel();
            AppointmentButton = new Panel();
            PatientButton = new Panel();
            DoctorButton = new Panel();
            SideBarBackground.SuspendLayout();
            SuspendLayout();
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
            SideBarBackground.Size = new Size(75, 661);
            SideBarBackground.TabIndex = 0;
            // 
            // NotificationButton
            // 
            NotificationButton.BackgroundImage = Properties.Resources.Bell;
            NotificationButton.BackgroundImageLayout = ImageLayout.Stretch;
            NotificationButton.Location = new Point(12, 471);
            NotificationButton.Name = "NotificationButton";
            NotificationButton.Size = new Size(50, 50);
            NotificationButton.TabIndex = 4;
            NotificationButton.Click += NotificationButton_Click;
            // 
            // ReportButton
            // 
            ReportButton.BackgroundImage = Properties.Resources.report;
            ReportButton.BackgroundImageLayout = ImageLayout.Stretch;
            ReportButton.Location = new Point(12, 415);
            ReportButton.Name = "ReportButton";
            ReportButton.Size = new Size(50, 50);
            ReportButton.TabIndex = 3;
            ReportButton.Click += ReportButton_Click;
            // 
            // HomeButton
            // 
            HomeButton.BackgroundImage = Properties.Resources.white_home;
            HomeButton.BackgroundImageLayout = ImageLayout.Stretch;
            HomeButton.Location = new Point(12, 79);
            HomeButton.Name = "HomeButton";
            HomeButton.Size = new Size(50, 50);
            HomeButton.TabIndex = 2;
            HomeButton.Click += HomeButton_Click;
            // 
            // ServicesButton
            // 
            ServicesButton.BackgroundImage = Properties.Resources.service;
            ServicesButton.BackgroundImageLayout = ImageLayout.Stretch;
            ServicesButton.Location = new Point(12, 359);
            ServicesButton.Name = "ServicesButton";
            ServicesButton.Size = new Size(50, 50);
            ServicesButton.TabIndex = 2;
            ServicesButton.Click += ServicesButton_Click;
            // 
            // AvailabilityButton
            // 
            AvailabilityButton.BackgroundImage = Properties.Resources.calendar;
            AvailabilityButton.BackgroundImageLayout = ImageLayout.Stretch;
            AvailabilityButton.Location = new Point(12, 247);
            AvailabilityButton.Name = "AvailabilityButton";
            AvailabilityButton.Size = new Size(50, 50);
            AvailabilityButton.TabIndex = 2;
            AvailabilityButton.Click += AvailabilityButton_Click;
            // 
            // AppointmentButton
            // 
            AppointmentButton.BackgroundImage = Properties.Resources.Book;
            AppointmentButton.BackgroundImageLayout = ImageLayout.Stretch;
            AppointmentButton.Location = new Point(12, 303);
            AppointmentButton.Name = "AppointmentButton";
            AppointmentButton.Size = new Size(50, 50);
            AppointmentButton.TabIndex = 1;
            AppointmentButton.Click += AppointmentButton_Click;
            // 
            // PatientButton
            // 
            PatientButton.BackgroundImage = Properties.Resources.patient;
            PatientButton.BackgroundImageLayout = ImageLayout.Stretch;
            PatientButton.Location = new Point(12, 135);
            PatientButton.Name = "PatientButton";
            PatientButton.Size = new Size(50, 50);
            PatientButton.TabIndex = 1;
            PatientButton.Click += PatientButton_Click;
            // 
            // DoctorButton
            // 
            DoctorButton.BackgroundImage = Properties.Resources.doctor;
            DoctorButton.BackgroundImageLayout = ImageLayout.Stretch;
            DoctorButton.Location = new Point(12, 191);
            DoctorButton.Name = "DoctorButton";
            DoctorButton.Size = new Size(50, 50);
            DoctorButton.TabIndex = 0;
            DoctorButton.Click += DoctorButton_Click;
            // 
            // MainAdminPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 661);
            Controls.Add(SideBarBackground);
            Name = "MainAdminPage";
            Text = "AdminDesignLayout";
            Load += AdminDesignLayout_Load;
            SideBarBackground.ResumeLayout(false);
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
        private Panel NotificationButton;
        private Panel ReportButton;
    }
}