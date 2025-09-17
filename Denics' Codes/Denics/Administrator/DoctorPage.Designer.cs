namespace Denics.Administrator
{
    partial class DoctorPage
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
            dgvDoctortable = new DataGridView();
            label1 = new Label();
            Save = new Button();
            Edit = new Button();
            Delete = new Button();
            txtfname = new TextBox();
            txtemail = new TextBox();
            txtpnum = new TextBox();
            txtDocid = new TextBox();
            MSchedule = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            SideBarBackground = new Panel();
            NotificationButton = new Panel();
            ReportButton = new Panel();
            HomeButton = new Panel();
            ServicesButton = new Panel();
            AvailabilityButton = new Panel();
            AppointmentButton = new Panel();
            PatientButton = new Panel();
            DoctorButton = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvDoctortable).BeginInit();
            SideBarBackground.SuspendLayout();
            SuspendLayout();
            // 
            // dgvDoctortable
            // 
            dgvDoctortable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDoctortable.Location = new Point(164, 318);
            dgvDoctortable.Name = "dgvDoctortable";
            dgvDoctortable.RowHeadersWidth = 51;
            dgvDoctortable.Size = new Size(453, 186);
            dgvDoctortable.TabIndex = 0;
            dgvDoctortable.CellClick += dgvDoctortable_CellClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(164, 29);
            label1.Name = "label1";
            label1.Size = new Size(123, 15);
            label1.TabIndex = 1;
            label1.Text = "Calling table : Doctors";
            // 
            // Save
            // 
            Save.Location = new Point(164, 234);
            Save.Margin = new Padding(3, 2, 3, 2);
            Save.Name = "Save";
            Save.Size = new Size(82, 22);
            Save.TabIndex = 2;
            Save.Text = "Save";
            Save.UseVisualStyleBackColor = true;
            Save.Click += Save_Click;
            // 
            // Edit
            // 
            Edit.Location = new Point(252, 234);
            Edit.Margin = new Padding(3, 2, 3, 2);
            Edit.Name = "Edit";
            Edit.Size = new Size(82, 22);
            Edit.TabIndex = 3;
            Edit.Text = "Edit";
            Edit.UseVisualStyleBackColor = true;
            Edit.Click += Edit_Click;
            // 
            // Delete
            // 
            Delete.Location = new Point(340, 234);
            Delete.Margin = new Padding(3, 2, 3, 2);
            Delete.Name = "Delete";
            Delete.Size = new Size(82, 22);
            Delete.TabIndex = 4;
            Delete.Text = "Delete";
            Delete.UseVisualStyleBackColor = true;
            Delete.Click += Delete_Click_1;
            // 
            // txtfname
            // 
            txtfname.Location = new Point(275, 97);
            txtfname.Margin = new Padding(3, 2, 3, 2);
            txtfname.Name = "txtfname";
            txtfname.Size = new Size(242, 23);
            txtfname.TabIndex = 6;
            // 
            // txtemail
            // 
            txtemail.Location = new Point(275, 134);
            txtemail.Margin = new Padding(3, 2, 3, 2);
            txtemail.Name = "txtemail";
            txtemail.Size = new Size(242, 23);
            txtemail.TabIndex = 7;
            // 
            // txtpnum
            // 
            txtpnum.Location = new Point(275, 176);
            txtpnum.Margin = new Padding(3, 2, 3, 2);
            txtpnum.Name = "txtpnum";
            txtpnum.Size = new Size(242, 23);
            txtpnum.TabIndex = 8;
            // 
            // txtDocid
            // 
            txtDocid.Location = new Point(308, 64);
            txtDocid.Margin = new Padding(3, 2, 3, 2);
            txtDocid.Name = "txtDocid";
            txtDocid.Size = new Size(36, 23);
            txtDocid.TabIndex = 9;
            // 
            // MSchedule
            // 
            MSchedule.Location = new Point(495, 291);
            MSchedule.Margin = new Padding(3, 2, 3, 2);
            MSchedule.Name = "MSchedule";
            MSchedule.Size = new Size(122, 22);
            MSchedule.TabIndex = 10;
            MSchedule.Text = "Manage Schedule";
            MSchedule.UseVisualStyleBackColor = true;
            MSchedule.Click += MSchedule_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(210, 64);
            label2.Name = "label2";
            label2.Size = new Size(20, 15);
            label2.TabIndex = 11;
            label2.Text = "Id:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(186, 99);
            label3.Name = "label3";
            label3.Size = new Size(64, 15);
            label3.TabIndex = 12;
            label3.Text = "Full Name:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(198, 140);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 13;
            label4.Text = "Email:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(164, 178);
            label5.Name = "label5";
            label5.Size = new Size(99, 15);
            label5.TabIndex = 14;
            label5.Text = "Contact Number:";
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
            SideBarBackground.TabIndex = 15;
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
            // DoctorPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(734, 561);
            Controls.Add(SideBarBackground);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(MSchedule);
            Controls.Add(txtDocid);
            Controls.Add(txtpnum);
            Controls.Add(txtemail);
            Controls.Add(txtfname);
            Controls.Add(Delete);
            Controls.Add(Edit);
            Controls.Add(Save);
            Controls.Add(label1);
            Controls.Add(dgvDoctortable);
            Name = "DoctorPage";
            Text = "Doctor Management";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDoctortable).EndInit();
            SideBarBackground.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvDoctortable;
        private Label label1;
        private Button Save;
        private Button Edit;
        private Button Delete;
        private TextBox txtfname;
        private TextBox txtemail;
        private TextBox txtpnum;
        private TextBox txtDocid;
        private Button MSchedule;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
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