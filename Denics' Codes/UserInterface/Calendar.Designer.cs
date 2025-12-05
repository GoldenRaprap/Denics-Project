namespace Denics.UserInterface
{
    partial class Calendar
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
            HomeButton = new Panel();
            ServicesButton = new Panel();
            AvailabilityButton = new Panel();
            AppointmentButton = new Panel();
            PatientButton = new Panel();
            DoctorButton = new Panel();
            CalendarView = new FlowLayoutPanel();
            TimeAvailability = new TableLayoutPanel();
            MonthPicker = new DateTimePicker();
            ServiceType_cmbbx = new ComboBox();
            DoctorAvailable_list = new ListView();
            ServiceType_lbl = new Label();
            Doctor_lbl = new Label();
            Month_lbl = new Label();
            TimeSlot_lbl = new Label();
            Top_lbl = new Label();
            TopHeader = new Panel();
            bookbtn = new Button();
            SideBarBackground.SuspendLayout();
            TopHeader.SuspendLayout();
            SuspendLayout();
            // 
            // SideBarBackground
            // 
            SideBarBackground.BackColor = Color.CornflowerBlue;
            SideBarBackground.Controls.Add(HomeButton);
            SideBarBackground.Controls.Add(ServicesButton);
            SideBarBackground.Controls.Add(AvailabilityButton);
            SideBarBackground.Controls.Add(AppointmentButton);
            SideBarBackground.Controls.Add(PatientButton);
            SideBarBackground.Controls.Add(DoctorButton);
            SideBarBackground.Dock = DockStyle.Left;
            SideBarBackground.Location = new Point(0, 0);
            SideBarBackground.Name = "SideBarBackground";
            SideBarBackground.Size = new Size(75, 661);
            SideBarBackground.TabIndex = 26;
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
            AppointmentButton.Location = new Point(16, 165);
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
            DoctorButton.Location = new Point(16, 257);
            DoctorButton.Name = "DoctorButton";
            DoctorButton.Size = new Size(40, 40);
            DoctorButton.TabIndex = 0;
            // 
            // CalendarView
            // 
            CalendarView.AllowDrop = true;
            CalendarView.AutoScroll = true;
            CalendarView.AutoSize = true;
            CalendarView.BackColor = Color.CornflowerBlue;
            CalendarView.Location = new Point(330, 119);
            CalendarView.Margin = new Padding(3, 2, 3, 2);
            CalendarView.Name = "CalendarView";
            CalendarView.Size = new Size(642, 520);
            CalendarView.TabIndex = 105;
            // 
            // TimeAvailability
            // 
            TimeAvailability.BackColor = SystemColors.GradientInactiveCaption;
            TimeAvailability.ColumnCount = 2;
            TimeAvailability.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TimeAvailability.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TimeAvailability.Location = new Point(110, 290);
            TimeAvailability.Name = "TimeAvailability";
            TimeAvailability.RowCount = 2;
            TimeAvailability.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            TimeAvailability.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            TimeAvailability.Size = new Size(200, 273);
            TimeAvailability.TabIndex = 106;
            // 
            // MonthPicker
            // 
            MonthPicker.Location = new Point(427, 95);
            MonthPicker.Name = "MonthPicker";
            MonthPicker.Size = new Size(200, 23);
            MonthPicker.TabIndex = 107;
            // 
            // ServiceType_cmbbx
            // 
            ServiceType_cmbbx.FormattingEnabled = true;
            ServiceType_cmbbx.Location = new Point(111, 119);
            ServiceType_cmbbx.Name = "ServiceType_cmbbx";
            ServiceType_cmbbx.Size = new Size(200, 23);
            ServiceType_cmbbx.TabIndex = 108;
            // 
            // DoctorAvailable_list
            // 
            DoctorAvailable_list.Location = new Point(111, 170);
            DoctorAvailable_list.Name = "DoctorAvailable_list";
            DoctorAvailable_list.Size = new Size(200, 86);
            DoctorAvailable_list.TabIndex = 109;
            DoctorAvailable_list.UseCompatibleStateImageBehavior = false;
            // 
            // ServiceType_lbl
            // 
            ServiceType_lbl.AutoSize = true;
            ServiceType_lbl.Location = new Point(111, 101);
            ServiceType_lbl.Name = "ServiceType_lbl";
            ServiceType_lbl.Size = new Size(90, 15);
            ServiceType_lbl.TabIndex = 110;
            ServiceType_lbl.Text = "Select a Service:";
            // 
            // Doctor_lbl
            // 
            Doctor_lbl.AutoSize = true;
            Doctor_lbl.Location = new Point(111, 152);
            Doctor_lbl.Name = "Doctor_lbl";
            Doctor_lbl.Size = new Size(89, 15);
            Doctor_lbl.TabIndex = 111;
            Doctor_lbl.Text = "Select a Doctor:";
            // 
            // Month_lbl
            // 
            Month_lbl.AutoSize = true;
            Month_lbl.Location = new Point(330, 101);
            Month_lbl.Name = "Month_lbl";
            Month_lbl.Size = new Size(90, 15);
            Month_lbl.TabIndex = 112;
            Month_lbl.Text = "Change Month:";
            // 
            // TimeSlot_lbl
            // 
            TimeSlot_lbl.AutoSize = true;
            TimeSlot_lbl.Location = new Point(111, 269);
            TimeSlot_lbl.Name = "TimeSlot_lbl";
            TimeSlot_lbl.Size = new Size(120, 15);
            TimeSlot_lbl.TabIndex = 113;
            TimeSlot_lbl.Text = "Select Available Slots:";
            // 
            // Top_lbl
            // 
            Top_lbl.AutoSize = true;
            Top_lbl.Font = new Font("Sitka Subheading", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Top_lbl.ForeColor = SystemColors.ControlLight;
            Top_lbl.Location = new Point(20, 9);
            Top_lbl.Name = "Top_lbl";
            Top_lbl.Size = new Size(145, 47);
            Top_lbl.TabIndex = 0;
            Top_lbl.Text = "Calendar";
            // 
            // TopHeader
            // 
            TopHeader.BackColor = Color.RoyalBlue;
            TopHeader.Controls.Add(Top_lbl);
            TopHeader.Dock = DockStyle.Top;
            TopHeader.Location = new Point(75, 0);
            TopHeader.Name = "TopHeader";
            TopHeader.Size = new Size(909, 65);
            TopHeader.TabIndex = 27;
            // 
            // bookbtn
            // 
            bookbtn.BackColor = Color.CornflowerBlue;
            bookbtn.Cursor = Cursors.Hand;
            bookbtn.Font = new Font("Sitka Text", 11.249999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            bookbtn.ForeColor = Color.White;
            bookbtn.Location = new Point(110, 591);
            bookbtn.Margin = new Padding(3, 2, 3, 2);
            bookbtn.Name = "bookbtn";
            bookbtn.Size = new Size(200, 48);
            bookbtn.TabIndex = 114;
            bookbtn.Text = "Book an Appointment";
            bookbtn.UseVisualStyleBackColor = false;
            bookbtn.Click += bookbtn_Click;
            // 
            // Calendar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 661);
            Controls.Add(bookbtn);
            Controls.Add(TimeSlot_lbl);
            Controls.Add(Month_lbl);
            Controls.Add(Doctor_lbl);
            Controls.Add(ServiceType_lbl);
            Controls.Add(DoctorAvailable_list);
            Controls.Add(ServiceType_cmbbx);
            Controls.Add(MonthPicker);
            Controls.Add(TimeAvailability);
            Controls.Add(CalendarView);
            Controls.Add(TopHeader);
            Controls.Add(SideBarBackground);
            Name = "Calendar";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Calendar";
            Load += Calendar_Load;
            SideBarBackground.ResumeLayout(false);
            TopHeader.ResumeLayout(false);
            TopHeader.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel SideBarBackground;
        private Panel HomeButton;
        private Panel ServicesButton;
        private Panel AvailabilityButton;
        private Panel AppointmentButton;
        private Panel PatientButton;
        private Panel DoctorButton;
        private FlowLayoutPanel CalendarView;
        private TableLayoutPanel TimeAvailability;
        private DateTimePicker MonthPicker;
        private ComboBox ServiceType_cmbbx;
        private ListView DoctorAvailable_list;
        private Label ServiceType_lbl;
        private Label Doctor_lbl;
        private Label Month_lbl;
        private Label TimeSlot_lbl;
        private Label Top_lbl;
        private Panel TopHeader;
        private Button bookbtn;
    }
}