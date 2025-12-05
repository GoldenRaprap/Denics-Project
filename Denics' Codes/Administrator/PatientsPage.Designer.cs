namespace Denics.Administrator
{
    partial class PatientsPage
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
            ClientsTable = new DataGridView();
            txtboxfName = new TextBox();
            txtboxsurname = new TextBox();
            txtboxemail = new TextBox();
            txtboxcontact = new TextBox();
            txtboxaddress = new TextBox();
            labelfName = new Label();
            labellName = new Label();
            labelemail = new Label();
            labelnum = new Label();
            labeladdress = new Label();
            buttonAdd = new Button();
            buttonEdit = new Button();
            buttonDelete = new Button();
            Role = new Label();
            Rolelabel = new Label();
            txtboxrole = new ComboBox();
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
            labelsuffix = new Label();
            labelmName = new Label();
            txtboxsuffix = new TextBox();
            txtboxmName = new TextBox();
            labelbday = new Label();
            txtboxbday = new TextBox();
            txtboxsearch = new TextBox();
            Searchlb = new Label();
            bntclear = new Button();
            ((System.ComponentModel.ISupportInitialize)ClientsTable).BeginInit();
            SideBarBackground.SuspendLayout();
            TopHeader.SuspendLayout();
            SuspendLayout();
            // 
            // ClientsTable
            // 
            ClientsTable.AllowUserToAddRows = false;
            ClientsTable.AllowUserToDeleteRows = false;
            ClientsTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ClientsTable.Location = new Point(292, 106);
            ClientsTable.Name = "ClientsTable";
            ClientsTable.ReadOnly = true;
            ClientsTable.Size = new Size(580, 443);
            ClientsTable.TabIndex = 0;
            ClientsTable.CellClick += ClientsTable_CellClick;
            // 
            // txtboxfName
            // 
            txtboxfName.Location = new Point(93, 98);
            txtboxfName.Name = "txtboxfName";
            txtboxfName.Size = new Size(182, 23);
            txtboxfName.TabIndex = 1;
            // 
            // txtboxsurname
            // 
            txtboxsurname.Location = new Point(93, 142);
            txtboxsurname.Name = "txtboxsurname";
            txtboxsurname.Size = new Size(182, 23);
            txtboxsurname.TabIndex = 2;
            // 
            // txtboxemail
            // 
            txtboxemail.Location = new Point(93, 279);
            txtboxemail.Name = "txtboxemail";
            txtboxemail.Size = new Size(182, 23);
            txtboxemail.TabIndex = 3;
            // 
            // txtboxcontact
            // 
            txtboxcontact.Location = new Point(93, 324);
            txtboxcontact.Name = "txtboxcontact";
            txtboxcontact.Size = new Size(182, 23);
            txtboxcontact.TabIndex = 4;
            // 
            // txtboxaddress
            // 
            txtboxaddress.Location = new Point(93, 368);
            txtboxaddress.Name = "txtboxaddress";
            txtboxaddress.Size = new Size(182, 23);
            txtboxaddress.TabIndex = 5;
            // 
            // labelfName
            // 
            labelfName.AutoSize = true;
            labelfName.Location = new Point(94, 80);
            labelfName.Name = "labelfName";
            labelfName.Size = new Size(67, 15);
            labelfName.TabIndex = 6;
            labelfName.Text = "First Name:";
            // 
            // labellName
            // 
            labellName.AutoSize = true;
            labellName.Location = new Point(95, 124);
            labellName.Name = "labellName";
            labellName.Size = new Size(66, 15);
            labellName.TabIndex = 7;
            labellName.Text = "Last Name:";
            // 
            // labelemail
            // 
            labelemail.AutoSize = true;
            labelemail.Location = new Point(95, 261);
            labelemail.Name = "labelemail";
            labelemail.Size = new Size(39, 15);
            labelemail.TabIndex = 8;
            labelemail.Text = "Email:";
            // 
            // labelnum
            // 
            labelnum.AutoSize = true;
            labelnum.Location = new Point(94, 306);
            labelnum.Name = "labelnum";
            labelnum.Size = new Size(99, 15);
            labelnum.TabIndex = 9;
            labelnum.Text = "Contact Number:";
            // 
            // labeladdress
            // 
            labeladdress.AutoSize = true;
            labeladdress.Location = new Point(93, 350);
            labeladdress.Name = "labeladdress";
            labeladdress.Size = new Size(52, 15);
            labeladdress.TabIndex = 10;
            labeladdress.Text = "Address:";
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(93, 495);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(80, 21);
            buttonAdd.TabIndex = 11;
            buttonAdd.Text = "ADD";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonEdit
            // 
            buttonEdit.Location = new Point(194, 495);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(81, 21);
            buttonEdit.TabIndex = 12;
            buttonEdit.Text = "UPDATE";
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += buttonEdit_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(135, 522);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(95, 21);
            buttonDelete.TabIndex = 13;
            buttonDelete.Text = "REMOVE";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // Role
            // 
            Role.AutoSize = true;
            Role.Location = new Point(94, 394);
            Role.Name = "Role";
            Role.Size = new Size(33, 15);
            Role.TabIndex = 15;
            Role.Text = "Role:";
            // 
            // Rolelabel
            // 
            Rolelabel.AutoSize = true;
            Rolelabel.Location = new Point(94, 463);
            Rolelabel.Name = "Rolelabel";
            Rolelabel.Size = new Size(0, 15);
            Rolelabel.TabIndex = 17;
            // 
            // txtboxrole
            // 
            txtboxrole.FormattingEnabled = true;
            txtboxrole.Items.AddRange(new object[] { "patient", "admin" });
            txtboxrole.Location = new Point(94, 412);
            txtboxrole.Name = "txtboxrole";
            txtboxrole.Size = new Size(182, 23);
            txtboxrole.TabIndex = 18;
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
            SideBarBackground.TabIndex = 32;
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
            TopHeader.TabIndex = 33;
            // 
            // Top_lbl
            // 
            Top_lbl.AutoSize = true;
            Top_lbl.Font = new Font("Sitka Subheading", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Top_lbl.ForeColor = SystemColors.ControlLight;
            Top_lbl.Location = new Point(20, 9);
            Top_lbl.Name = "Top_lbl";
            Top_lbl.Size = new Size(272, 47);
            Top_lbl.TabIndex = 0;
            Top_lbl.Text = "User Management";
            // 
            // labelsuffix
            // 
            labelsuffix.AutoSize = true;
            labelsuffix.Location = new Point(95, 212);
            labelsuffix.Name = "labelsuffix";
            labelsuffix.Size = new Size(40, 15);
            labelsuffix.TabIndex = 37;
            labelsuffix.Text = "Suffix:";
            // 
            // labelmName
            // 
            labelmName.AutoSize = true;
            labelmName.Location = new Point(94, 168);
            labelmName.Name = "labelmName";
            labelmName.Size = new Size(82, 15);
            labelmName.TabIndex = 36;
            labelmName.Text = "Middle Name:";
            // 
            // txtboxsuffix
            // 
            txtboxsuffix.Location = new Point(93, 230);
            txtboxsuffix.Name = "txtboxsuffix";
            txtboxsuffix.Size = new Size(182, 23);
            txtboxsuffix.TabIndex = 35;
            // 
            // txtboxmName
            // 
            txtboxmName.Location = new Point(93, 186);
            txtboxmName.Name = "txtboxmName";
            txtboxmName.Size = new Size(182, 23);
            txtboxmName.TabIndex = 34;
            // 
            // labelbday
            // 
            labelbday.AutoSize = true;
            labelbday.Location = new Point(93, 438);
            labelbday.Name = "labelbday";
            labelbday.Size = new Size(58, 15);
            labelbday.TabIndex = 39;
            labelbday.Text = "Birth Day:";
            // 
            // txtboxbday
            // 
            txtboxbday.Location = new Point(93, 456);
            txtboxbday.Name = "txtboxbday";
            txtboxbday.Size = new Size(182, 23);
            txtboxbday.TabIndex = 38;
            // 
            // txtboxsearch
            // 
            txtboxsearch.Location = new Point(526, 80);
            txtboxsearch.Name = "txtboxsearch";
            txtboxsearch.Size = new Size(265, 23);
            txtboxsearch.TabIndex = 41;
            txtboxsearch.TextChanged += txtboxsearch_TextChanged;
            // 
            // Searchlb
            // 
            Searchlb.AutoSize = true;
            Searchlb.Location = new Point(472, 83);
            Searchlb.Name = "Searchlb";
            Searchlb.Size = new Size(48, 15);
            Searchlb.TabIndex = 40;
            Searchlb.Text = "Search: ";
            // 
            // bntclear
            // 
            bntclear.Location = new Point(797, 80);
            bntclear.Name = "bntclear";
            bntclear.Size = new Size(75, 23);
            bntclear.TabIndex = 43;
            bntclear.Text = "Clear";
            bntclear.UseVisualStyleBackColor = true;
            bntclear.Click += bntclear_Click;
            // 
            // PatientsPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 561);
            Controls.Add(bntclear);
            Controls.Add(txtboxsearch);
            Controls.Add(Searchlb);
            Controls.Add(labelbday);
            Controls.Add(txtboxbday);
            Controls.Add(labelsuffix);
            Controls.Add(labelmName);
            Controls.Add(txtboxsuffix);
            Controls.Add(txtboxmName);
            Controls.Add(TopHeader);
            Controls.Add(txtboxrole);
            Controls.Add(Rolelabel);
            Controls.Add(Role);
            Controls.Add(buttonDelete);
            Controls.Add(buttonEdit);
            Controls.Add(buttonAdd);
            Controls.Add(labeladdress);
            Controls.Add(labelnum);
            Controls.Add(labelemail);
            Controls.Add(labellName);
            Controls.Add(labelfName);
            Controls.Add(txtboxaddress);
            Controls.Add(txtboxcontact);
            Controls.Add(txtboxemail);
            Controls.Add(txtboxsurname);
            Controls.Add(txtboxfName);
            Controls.Add(ClientsTable);
            Controls.Add(SideBarBackground);
            Name = "PatientsPage";
            Text = "Patient Mangement";
            Load += PatientsPage_Load;
            ((System.ComponentModel.ISupportInitialize)ClientsTable).EndInit();
            SideBarBackground.ResumeLayout(false);
            TopHeader.ResumeLayout(false);
            TopHeader.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView ClientsTable;
        private TextBox txtboxfName;
        private TextBox txtboxsurname;
        private TextBox txtboxemail;
        private TextBox txtboxcontact;
        private TextBox txtboxaddress;
        private Label labelfName;
        private Label labellName;
        private Label labelemail;
        private Label labelnum;
        private Label labeladdress;
        private Button buttonAdd;
        private Button buttonEdit;
        private Button buttonDelete;
        private Label Role;
        private Label Rolelabel;
        private ComboBox txtboxrole;
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
        private Label labelsuffix;
        private Label labelmName;
        private TextBox txtboxsuffix;
        private TextBox txtboxmName;
        private Label labelbday;
        private TextBox txtboxbday;
        private TextBox txtboxsearch;
        private Label Searchlb;
        private Button bntclear;
    }
}