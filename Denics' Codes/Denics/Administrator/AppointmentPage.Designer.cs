namespace Denics.Administrator
{
    partial class AppointmentPage
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
            OverallAppointmentTable = new DataGridView();
            AppointmentIDLabel = new Label();
            PatientNameLabel = new Label();
            DoctorLabel = new Label();
            serviceLabel = new Label();
            dateLabel = new Label();
            TimeLabel = new Label();
            ApprovalLabel = new Label();
            AppointmentIDtxtbx = new TextBox();
            Patienttxtbx = new TextBox();
            Doctortxtbx = new TextBox();
            Servicetxtbx = new TextBox();
            Datedtpicker = new DateTimePicker();
            Timetxtbx = new TextBox();
            ViewApprovalbtn = new Button();
            Statustxtbx = new TextBox();
            StatusLabel = new Label();
            Refreshbtn = new Button();
            Approvebtn = new Button();
            CancellationBtn = new Button();
            Automation_checkbox = new CheckBox();
            SaveAutomationbtn = new Button();
            button1 = new Button();
            ApprovalPanel = new Panel();
            ViewCompletionbtn = new Button();
            CompletionLabel = new Label();
            NoShowbtn = new Button();
            Completebtn = new Button();
            ((System.ComponentModel.ISupportInitialize)OverallAppointmentTable).BeginInit();
            ApprovalPanel.SuspendLayout();
            SuspendLayout();
            // 
            // OverallAppointmentTable
            // 
            OverallAppointmentTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            OverallAppointmentTable.Location = new Point(294, 42);
            OverallAppointmentTable.Name = "OverallAppointmentTable";
            OverallAppointmentTable.Size = new Size(560, 488);
            OverallAppointmentTable.TabIndex = 1;
            OverallAppointmentTable.CellClick += OverallAppointmentTable_CellClick;
            // 
            // AppointmentIDLabel
            // 
            AppointmentIDLabel.AutoSize = true;
            AppointmentIDLabel.Location = new Point(90, 24);
            AppointmentIDLabel.Name = "AppointmentIDLabel";
            AppointmentIDLabel.Size = new Size(95, 15);
            AppointmentIDLabel.TabIndex = 2;
            AppointmentIDLabel.Text = "Appointment ID:";
            // 
            // PatientNameLabel
            // 
            PatientNameLabel.AutoSize = true;
            PatientNameLabel.Location = new Point(90, 68);
            PatientNameLabel.Name = "PatientNameLabel";
            PatientNameLabel.Size = new Size(50, 15);
            PatientNameLabel.TabIndex = 3;
            PatientNameLabel.Text = "Patient: ";
            // 
            // DoctorLabel
            // 
            DoctorLabel.AutoSize = true;
            DoctorLabel.Location = new Point(90, 112);
            DoctorLabel.Name = "DoctorLabel";
            DoctorLabel.Size = new Size(49, 15);
            DoctorLabel.TabIndex = 4;
            DoctorLabel.Text = "Doctor: ";
            // 
            // serviceLabel
            // 
            serviceLabel.AutoSize = true;
            serviceLabel.Location = new Point(90, 156);
            serviceLabel.Name = "serviceLabel";
            serviceLabel.Size = new Size(47, 15);
            serviceLabel.TabIndex = 5;
            serviceLabel.Text = "Service:";
            // 
            // dateLabel
            // 
            dateLabel.AutoSize = true;
            dateLabel.Location = new Point(90, 200);
            dateLabel.Name = "dateLabel";
            dateLabel.Size = new Size(37, 15);
            dateLabel.TabIndex = 6;
            dateLabel.Text = "Date: ";
            // 
            // TimeLabel
            // 
            TimeLabel.AutoSize = true;
            TimeLabel.Location = new Point(90, 244);
            TimeLabel.Name = "TimeLabel";
            TimeLabel.Size = new Size(39, 15);
            TimeLabel.TabIndex = 7;
            TimeLabel.Text = "Time: ";
            // 
            // ApprovalLabel
            // 
            ApprovalLabel.AutoSize = true;
            ApprovalLabel.Location = new Point(13, 7);
            ApprovalLabel.Name = "ApprovalLabel";
            ApprovalLabel.Size = new Size(55, 15);
            ApprovalLabel.TabIndex = 8;
            ApprovalLabel.Text = "Approval";
            // 
            // AppointmentIDtxtbx
            // 
            AppointmentIDtxtbx.Location = new Point(90, 42);
            AppointmentIDtxtbx.Name = "AppointmentIDtxtbx";
            AppointmentIDtxtbx.Size = new Size(187, 23);
            AppointmentIDtxtbx.TabIndex = 9;
            // 
            // Patienttxtbx
            // 
            Patienttxtbx.Location = new Point(90, 86);
            Patienttxtbx.Name = "Patienttxtbx";
            Patienttxtbx.Size = new Size(187, 23);
            Patienttxtbx.TabIndex = 10;
            // 
            // Doctortxtbx
            // 
            Doctortxtbx.Location = new Point(90, 130);
            Doctortxtbx.Name = "Doctortxtbx";
            Doctortxtbx.Size = new Size(187, 23);
            Doctortxtbx.TabIndex = 11;
            // 
            // Servicetxtbx
            // 
            Servicetxtbx.Location = new Point(90, 174);
            Servicetxtbx.Name = "Servicetxtbx";
            Servicetxtbx.Size = new Size(187, 23);
            Servicetxtbx.TabIndex = 12;
            // 
            // Datedtpicker
            // 
            Datedtpicker.Location = new Point(90, 218);
            Datedtpicker.Name = "Datedtpicker";
            Datedtpicker.Size = new Size(187, 23);
            Datedtpicker.TabIndex = 13;
            // 
            // Timetxtbx
            // 
            Timetxtbx.Location = new Point(90, 262);
            Timetxtbx.Name = "Timetxtbx";
            Timetxtbx.Size = new Size(187, 23);
            Timetxtbx.TabIndex = 14;
            // 
            // ViewApprovalbtn
            // 
            ViewApprovalbtn.Location = new Point(121, 3);
            ViewApprovalbtn.Name = "ViewApprovalbtn";
            ViewApprovalbtn.Size = new Size(75, 23);
            ViewApprovalbtn.TabIndex = 15;
            ViewApprovalbtn.Text = "View";
            ViewApprovalbtn.UseVisualStyleBackColor = true;
            ViewApprovalbtn.Click += ViewApprovalbtn_Click;
            // 
            // Statustxtbx
            // 
            Statustxtbx.Location = new Point(13, 46);
            Statustxtbx.Name = "Statustxtbx";
            Statustxtbx.Size = new Size(183, 23);
            Statustxtbx.TabIndex = 16;
            // 
            // StatusLabel
            // 
            StatusLabel.AutoSize = true;
            StatusLabel.Location = new Point(13, 28);
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new Size(42, 15);
            StatusLabel.TabIndex = 17;
            StatusLabel.Text = "Status:";
            // 
            // Refreshbtn
            // 
            Refreshbtn.Location = new Point(294, 13);
            Refreshbtn.Name = "Refreshbtn";
            Refreshbtn.Size = new Size(75, 23);
            Refreshbtn.TabIndex = 18;
            Refreshbtn.Text = "Refresh";
            Refreshbtn.UseVisualStyleBackColor = true;
            Refreshbtn.Click += Refreshbtn_Click;
            // 
            // Approvebtn
            // 
            Approvebtn.Location = new Point(13, 73);
            Approvebtn.Name = "Approvebtn";
            Approvebtn.Size = new Size(87, 23);
            Approvebtn.TabIndex = 19;
            Approvebtn.Text = "Approve";
            Approvebtn.UseVisualStyleBackColor = true;
            Approvebtn.Click += Approvebtn_Click;
            // 
            // CancellationBtn
            // 
            CancellationBtn.Location = new Point(109, 73);
            CancellationBtn.Name = "CancellationBtn";
            CancellationBtn.Size = new Size(87, 23);
            CancellationBtn.TabIndex = 20;
            CancellationBtn.Text = "Deny";
            CancellationBtn.UseVisualStyleBackColor = true;
            CancellationBtn.Click += CancellationBtn_Click;
            // 
            // Automation_checkbox
            // 
            Automation_checkbox.AutoSize = true;
            Automation_checkbox.Location = new Point(15, 107);
            Automation_checkbox.Name = "Automation_checkbox";
            Automation_checkbox.Size = new Size(79, 19);
            Automation_checkbox.TabIndex = 21;
            Automation_checkbox.Text = "Automate";
            Automation_checkbox.UseVisualStyleBackColor = true;
            // 
            // SaveAutomationbtn
            // 
            SaveAutomationbtn.Location = new Point(121, 103);
            SaveAutomationbtn.Name = "SaveAutomationbtn";
            SaveAutomationbtn.Size = new Size(75, 23);
            SaveAutomationbtn.TabIndex = 22;
            SaveAutomationbtn.Text = "Save";
            SaveAutomationbtn.UseVisualStyleBackColor = true;
            SaveAutomationbtn.Click += SaveAutomationbtn_Click;
            // 
            // button1
            // 
            button1.Location = new Point(12, 16);
            button1.Name = "button1";
            button1.Size = new Size(50, 23);
            button1.TabIndex = 23;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // ApprovalPanel
            // 
            ApprovalPanel.BackColor = Color.LightGray;
            ApprovalPanel.Controls.Add(NoShowbtn);
            ApprovalPanel.Controls.Add(Completebtn);
            ApprovalPanel.Controls.Add(ViewCompletionbtn);
            ApprovalPanel.Controls.Add(CompletionLabel);
            ApprovalPanel.Controls.Add(SaveAutomationbtn);
            ApprovalPanel.Controls.Add(Automation_checkbox);
            ApprovalPanel.Controls.Add(CancellationBtn);
            ApprovalPanel.Controls.Add(Approvebtn);
            ApprovalPanel.Controls.Add(StatusLabel);
            ApprovalPanel.Controls.Add(Statustxtbx);
            ApprovalPanel.Controls.Add(ViewApprovalbtn);
            ApprovalPanel.Controls.Add(ApprovalLabel);
            ApprovalPanel.Location = new Point(81, 301);
            ApprovalPanel.Name = "ApprovalPanel";
            ApprovalPanel.Size = new Size(205, 229);
            ApprovalPanel.TabIndex = 24;
            // 
            // ViewCompletionbtn
            // 
            ViewCompletionbtn.Location = new Point(121, 154);
            ViewCompletionbtn.Name = "ViewCompletionbtn";
            ViewCompletionbtn.Size = new Size(75, 23);
            ViewCompletionbtn.TabIndex = 24;
            ViewCompletionbtn.Text = "View";
            ViewCompletionbtn.UseVisualStyleBackColor = true;
            // 
            // CompletionLabel
            // 
            CompletionLabel.AutoSize = true;
            CompletionLabel.Location = new Point(13, 158);
            CompletionLabel.Name = "CompletionLabel";
            CompletionLabel.Size = new Size(70, 15);
            CompletionLabel.TabIndex = 23;
            CompletionLabel.Text = "Completion";
            // 
            // NoShowbtn
            // 
            NoShowbtn.Location = new Point(109, 183);
            NoShowbtn.Name = "NoShowbtn";
            NoShowbtn.Size = new Size(87, 23);
            NoShowbtn.TabIndex = 26;
            NoShowbtn.Text = "No-Show";
            NoShowbtn.UseVisualStyleBackColor = true;
            // 
            // Completebtn
            // 
            Completebtn.Location = new Point(13, 183);
            Completebtn.Name = "Completebtn";
            Completebtn.Size = new Size(87, 23);
            Completebtn.TabIndex = 25;
            Completebtn.Text = "Complete";
            Completebtn.UseVisualStyleBackColor = true;
            // 
            // AppointmentPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 561);
            Controls.Add(ApprovalPanel);
            Controls.Add(button1);
            Controls.Add(Refreshbtn);
            Controls.Add(Timetxtbx);
            Controls.Add(Datedtpicker);
            Controls.Add(Servicetxtbx);
            Controls.Add(Doctortxtbx);
            Controls.Add(Patienttxtbx);
            Controls.Add(AppointmentIDtxtbx);
            Controls.Add(TimeLabel);
            Controls.Add(dateLabel);
            Controls.Add(serviceLabel);
            Controls.Add(DoctorLabel);
            Controls.Add(PatientNameLabel);
            Controls.Add(AppointmentIDLabel);
            Controls.Add(OverallAppointmentTable);
            Name = "AppointmentPage";
            Text = "Appointment Management";
            Load += AppointmentPage_Load;
            ((System.ComponentModel.ISupportInitialize)OverallAppointmentTable).EndInit();
            ApprovalPanel.ResumeLayout(false);
            ApprovalPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private DataGridView OverallAppointmentTable;
        private Label AppointmentIDLabel;
        private Label PatientNameLabel;
        private Label DoctorLabel;
        private Label serviceLabel;
        private Label dateLabel;
        private Label TimeLabel;
        private Label ApprovalLabel;
        private TextBox AppointmentIDtxtbx;
        private TextBox Patienttxtbx;
        private TextBox Doctortxtbx;
        private TextBox Servicetxtbx;
        private DateTimePicker Datedtpicker;
        private TextBox Timetxtbx;
        private Button ViewApprovalbtn;
        private TextBox Statustxtbx;
        private Label StatusLabel;
        private Button Refreshbtn;
        private Button Approvebtn;
        private Button CancellationBtn;
        private CheckBox Automation_checkbox;
        private Button SaveAutomationbtn;
        private Button button1;
        private Panel ApprovalPanel;
        private Button ViewCompletionbtn;
        private Label CompletionLabel;
        private Button NoShowbtn;
        private Button Completebtn;
        private Button button2;
        private CheckBox checkBox1;
        private TextBox textBox1;
    }
}