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
            label1 = new Label();
            cmbServices = new ComboBox();
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
            Add_btn = new Button();
            Remove_btn = new Button();
            DoctorService_grdpnl = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvDoctortable).BeginInit();
            SideBarBackground.SuspendLayout();
            TopHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DoctorService_grdpnl).BeginInit();
            SuspendLayout();
            // 
            // dgvDoctortable
            // 
            dgvDoctortable.AllowUserToAddRows = false;
            dgvDoctortable.AllowUserToDeleteRows = false;
            dgvDoctortable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDoctortable.Location = new Point(115, 331);
            dgvDoctortable.Name = "dgvDoctortable";
            dgvDoctortable.ReadOnly = true;
            dgvDoctortable.RowHeadersWidth = 51;
            dgvDoctortable.Size = new Size(471, 186);
            dgvDoctortable.TabIndex = 0;
            dgvDoctortable.CellClick += dgvDoctortable_CellClick;
            // 
            // Save
            // 
            Save.Location = new Point(144, 250);
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
            Edit.Location = new Point(231, 250);
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
            Delete.Location = new Point(319, 250);
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
            txtfname.Location = new Point(226, 123);
            txtfname.Margin = new Padding(3, 2, 3, 2);
            txtfname.Name = "txtfname";
            txtfname.Size = new Size(308, 23);
            txtfname.TabIndex = 6;
            // 
            // txtemail
            // 
            txtemail.Location = new Point(226, 156);
            txtemail.Margin = new Padding(3, 2, 3, 2);
            txtemail.Name = "txtemail";
            txtemail.Size = new Size(308, 23);
            txtemail.TabIndex = 7;
            // 
            // txtpnum
            // 
            txtpnum.Location = new Point(226, 188);
            txtpnum.Margin = new Padding(3, 2, 3, 2);
            txtpnum.Name = "txtpnum";
            txtpnum.Size = new Size(308, 23);
            txtpnum.TabIndex = 8;
            // 
            // txtDocid
            // 
            txtDocid.Location = new Point(226, 92);
            txtDocid.Margin = new Padding(3, 2, 3, 2);
            txtDocid.Name = "txtDocid";
            txtDocid.Size = new Size(36, 23);
            txtDocid.TabIndex = 9;
            txtDocid.Visible = false;
            // 
            // MSchedule
            // 
            MSchedule.Location = new Point(446, 304);
            MSchedule.Margin = new Padding(3, 2, 3, 2);
            MSchedule.Name = "MSchedule";
            MSchedule.Size = new Size(140, 22);
            MSchedule.TabIndex = 10;
            MSchedule.Text = "Manage Schedule";
            MSchedule.UseVisualStyleBackColor = true;
            MSchedule.Click += MSchedule_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(194, 94);
            label2.Name = "label2";
            label2.Size = new Size(20, 15);
            label2.TabIndex = 11;
            label2.Text = "Id:";
            label2.Visible = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(150, 126);
            label3.Name = "label3";
            label3.Size = new Size(64, 15);
            label3.TabIndex = 12;
            label3.Text = "Full Name:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(175, 159);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 13;
            label4.Text = "Email:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(115, 190);
            label5.Name = "label5";
            label5.Size = new Size(99, 15);
            label5.TabIndex = 14;
            label5.Text = "Contact Number:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(606, 129);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 32;
            label1.Text = "Services:";
            // 
            // cmbServices
            // 
            cmbServices.FormattingEnabled = true;
            cmbServices.Location = new Point(606, 146);
            cmbServices.Margin = new Padding(3, 2, 3, 2);
            cmbServices.Name = "cmbServices";
            cmbServices.Size = new Size(229, 23);
            cmbServices.TabIndex = 33;
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
            SideBarBackground.TabIndex = 36;
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
            TopHeader.TabIndex = 37;
            // 
            // Top_lbl
            // 
            Top_lbl.AutoSize = true;
            Top_lbl.Font = new Font("Sitka Subheading", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Top_lbl.ForeColor = SystemColors.ControlLight;
            Top_lbl.Location = new Point(20, 9);
            Top_lbl.Name = "Top_lbl";
            Top_lbl.Size = new Size(303, 47);
            Top_lbl.TabIndex = 0;
            Top_lbl.Text = "Doctor Management";
            // 
            // Add_btn
            // 
            Add_btn.Location = new Point(606, 188);
            Add_btn.Margin = new Padding(3, 2, 3, 2);
            Add_btn.Name = "Add_btn";
            Add_btn.Size = new Size(109, 23);
            Add_btn.TabIndex = 38;
            Add_btn.Text = "Add";
            Add_btn.UseVisualStyleBackColor = true;
            Add_btn.Click += Add_btn_Click;
            // 
            // Remove_btn
            // 
            Remove_btn.Location = new Point(726, 188);
            Remove_btn.Margin = new Padding(3, 2, 3, 2);
            Remove_btn.Name = "Remove_btn";
            Remove_btn.Size = new Size(109, 23);
            Remove_btn.TabIndex = 39;
            Remove_btn.Text = "Remove";
            Remove_btn.UseVisualStyleBackColor = true;
            Remove_btn.Click += Remove_btn_Click;
            // 
            // DoctorService_grdpnl
            // 
            DoctorService_grdpnl.AllowUserToAddRows = false;
            DoctorService_grdpnl.AllowUserToDeleteRows = false;
            DoctorService_grdpnl.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DoctorService_grdpnl.Location = new Point(606, 250);
            DoctorService_grdpnl.Name = "DoctorService_grdpnl";
            DoctorService_grdpnl.ReadOnly = true;
            DoctorService_grdpnl.RowHeadersWidth = 51;
            DoctorService_grdpnl.Size = new Size(229, 267);
            DoctorService_grdpnl.TabIndex = 40;
            // 
            // DoctorPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 561);
            Controls.Add(DoctorService_grdpnl);
            Controls.Add(Remove_btn);
            Controls.Add(Add_btn);
            Controls.Add(TopHeader);
            Controls.Add(SideBarBackground);
            Controls.Add(cmbServices);
            Controls.Add(label1);
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
            Controls.Add(dgvDoctortable);
            Name = "DoctorPage";
            Text = "Doctor Management";
            Load += DoctorPageForm1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDoctortable).EndInit();
            SideBarBackground.ResumeLayout(false);
            TopHeader.ResumeLayout(false);
            TopHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DoctorService_grdpnl).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvDoctortable;
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
        private Label label1;
        private ComboBox cmbServices;
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
        private Button Add_btn;
        private Button Remove_btn;
        private DataGridView DoctorService_grdpnl;
    }
}