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
            ((System.ComponentModel.ISupportInitialize)OverallAppointmentTable).BeginInit();
            SuspendLayout();
            // 
            // OverallAppointmentTable
            // 
            OverallAppointmentTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            OverallAppointmentTable.Location = new Point(313, 45);
            OverallAppointmentTable.Name = "OverallAppointmentTable";
            OverallAppointmentTable.Size = new Size(559, 497);
            OverallAppointmentTable.TabIndex = 1;
            // 
            // AppointmentIDLabel
            // 
            AppointmentIDLabel.AutoSize = true;
            AppointmentIDLabel.Location = new Point(92, 45);
            AppointmentIDLabel.Name = "AppointmentIDLabel";
            AppointmentIDLabel.Size = new Size(95, 15);
            AppointmentIDLabel.TabIndex = 2;
            AppointmentIDLabel.Text = "Appointment ID:";
            // 
            // PatientNameLabel
            // 
            PatientNameLabel.AutoSize = true;
            PatientNameLabel.Location = new Point(92, 95);
            PatientNameLabel.Name = "PatientNameLabel";
            PatientNameLabel.Size = new Size(50, 15);
            PatientNameLabel.TabIndex = 3;
            PatientNameLabel.Text = "Patient: ";
            // 
            // DoctorLabel
            // 
            DoctorLabel.AutoSize = true;
            DoctorLabel.Location = new Point(92, 140);
            DoctorLabel.Name = "DoctorLabel";
            DoctorLabel.Size = new Size(49, 15);
            DoctorLabel.TabIndex = 4;
            DoctorLabel.Text = "Doctor: ";
            // 
            // serviceLabel
            // 
            serviceLabel.AutoSize = true;
            serviceLabel.Location = new Point(92, 181);
            serviceLabel.Name = "serviceLabel";
            serviceLabel.Size = new Size(47, 15);
            serviceLabel.TabIndex = 5;
            serviceLabel.Text = "Service:";
            // 
            // dateLabel
            // 
            dateLabel.AutoSize = true;
            dateLabel.Location = new Point(92, 218);
            dateLabel.Name = "dateLabel";
            dateLabel.Size = new Size(37, 15);
            dateLabel.TabIndex = 6;
            dateLabel.Text = "Date: ";
            // 
            // TimeLabel
            // 
            TimeLabel.AutoSize = true;
            TimeLabel.Location = new Point(92, 262);
            TimeLabel.Name = "TimeLabel";
            TimeLabel.Size = new Size(39, 15);
            TimeLabel.TabIndex = 7;
            TimeLabel.Text = "Time: ";
            // 
            // ApprovalLabel
            // 
            ApprovalLabel.AutoSize = true;
            ApprovalLabel.Location = new Point(92, 342);
            ApprovalLabel.Name = "ApprovalLabel";
            ApprovalLabel.Size = new Size(52, 15);
            ApprovalLabel.TabIndex = 8;
            ApprovalLabel.Text = "Approve";
            // 
            // AppointmentPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 561);
            Controls.Add(ApprovalLabel);
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
    }
}
