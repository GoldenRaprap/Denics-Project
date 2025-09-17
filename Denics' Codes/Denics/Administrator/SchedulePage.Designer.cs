namespace Denics.Administrator
{
    partial class SchedulePage
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
            Save = new Button();
            comboBox1 = new ComboBox();
            Status = new ComboBox();
            Day = new ComboBox();
            Time = new ComboBox();
            Display = new Button();
            TableSchedule = new TableLayoutPanel();
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
            // Save
            // 
            Save.Location = new Point(578, 152);
            Save.Margin = new Padding(3, 2, 3, 2);
            Save.Name = "Save";
            Save.Size = new Size(82, 22);
            Save.TabIndex = 4;
            Save.Text = "Save";
            Save.UseVisualStyleBackColor = true;
            Save.Click += Save_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(150, 73);
            comboBox1.Margin = new Padding(3, 2, 3, 2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(218, 23);
            comboBox1.TabIndex = 5;
            comboBox1.Text = "Doctors";
            // 
            // Status
            // 
            Status.FormattingEnabled = true;
            Status.Items.AddRange(new object[] { "Available", "Not Available", "Break" });
            Status.Location = new Point(427, 152);
            Status.Margin = new Padding(3, 2, 3, 2);
            Status.Name = "Status";
            Status.Size = new Size(133, 23);
            Status.TabIndex = 8;
            Status.Text = "Status";
            // 
            // Day
            // 
            Day.FormattingEnabled = true;
            Day.Items.AddRange(new object[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" });
            Day.Location = new Point(149, 153);
            Day.Margin = new Padding(3, 2, 3, 2);
            Day.Name = "Day";
            Day.Size = new Size(133, 23);
            Day.TabIndex = 9;
            Day.Text = "Day";
            // 
            // Time
            // 
            Time.FormattingEnabled = true;
            Time.Items.AddRange(new object[] { "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00 " });
            Time.Location = new Point(288, 152);
            Time.Margin = new Padding(3, 2, 3, 2);
            Time.Name = "Time";
            Time.Size = new Size(133, 23);
            Time.TabIndex = 10;
            Time.Text = "Time";
            // 
            // Display
            // 
            Display.Location = new Point(402, 72);
            Display.Margin = new Padding(3, 2, 3, 2);
            Display.Name = "Display";
            Display.Size = new Size(82, 22);
            Display.TabIndex = 11;
            Display.Text = "Display";
            Display.UseVisualStyleBackColor = true;
            Display.Click += button1_Click;
            // 
            // TableSchedule
            // 
            TableSchedule.ColumnCount = 8;
            TableSchedule.ColumnStyles.Add(new ColumnStyle());
            TableSchedule.ColumnStyles.Add(new ColumnStyle());
            TableSchedule.ColumnStyles.Add(new ColumnStyle());
            TableSchedule.ColumnStyles.Add(new ColumnStyle());
            TableSchedule.ColumnStyles.Add(new ColumnStyle());
            TableSchedule.ColumnStyles.Add(new ColumnStyle());
            TableSchedule.ColumnStyles.Add(new ColumnStyle());
            TableSchedule.ColumnStyles.Add(new ColumnStyle());
            TableSchedule.Location = new Point(150, 228);
            TableSchedule.Margin = new Padding(3, 2, 3, 2);
            TableSchedule.Name = "TableSchedule";
            TableSchedule.RowCount = 10;
            TableSchedule.RowStyles.Add(new RowStyle());
            TableSchedule.RowStyles.Add(new RowStyle());
            TableSchedule.RowStyles.Add(new RowStyle());
            TableSchedule.RowStyles.Add(new RowStyle());
            TableSchedule.RowStyles.Add(new RowStyle());
            TableSchedule.RowStyles.Add(new RowStyle());
            TableSchedule.RowStyles.Add(new RowStyle());
            TableSchedule.RowStyles.Add(new RowStyle());
            TableSchedule.RowStyles.Add(new RowStyle());
            TableSchedule.RowStyles.Add(new RowStyle());
            TableSchedule.Size = new Size(521, 188);
            TableSchedule.TabIndex = 12;
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
            SideBarBackground.TabIndex = 13;
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
            // SchedulePage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(734, 561);
            Controls.Add(SideBarBackground);
            Controls.Add(TableSchedule);
            Controls.Add(Display);
            Controls.Add(Time);
            Controls.Add(Day);
            Controls.Add(Status);
            Controls.Add(comboBox1);
            Controls.Add(Save);
            Margin = new Padding(3, 2, 3, 2);
            Name = "SchedulePage";
            Text = "Schedule Management";
            Load += SchedulePage_Load;
            SideBarBackground.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button Save;
        private ComboBox comboBox1;
        private ComboBox Status;
        private ComboBox Day;
        private ComboBox Time;
        private Button Display;
        private TableLayoutPanel TableSchedule;
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