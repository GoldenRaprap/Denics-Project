namespace Denics.Administrator
{
    partial class ReportPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private global::System.ComponentModel.IContainer components = null;

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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            cmbReportType = new ComboBox();
            dtpStart = new DateTimePicker();
            dtpEnd = new DateTimePicker();
            btnGenerate = new Button();
            tabControlReports = new TabControl();
            tabPageAppointments = new TabPage();
            chartAppointments = new System.Windows.Forms.DataVisualization.Charting.Chart();
            txtAppointmentsInterpretation = new TextBox();
            tabPageStatus = new TabPage();
            chartStatus = new System.Windows.Forms.DataVisualization.Charting.Chart();
            txtStatusInterpretation = new TextBox();
            tabPageDoctor = new TabPage();
            chartPatientsPerDoctor = new System.Windows.Forms.DataVisualization.Charting.Chart();
            txtDoctorInterpretation = new TextBox();
            tabPageService = new TabPage();
            chartPatientsPerService = new System.Windows.Forms.DataVisualization.Charting.Chart();
            txtServiceInterpretation = new TextBox();
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
            tabControlReports.SuspendLayout();
            tabPageAppointments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartAppointments).BeginInit();
            tabPageStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartStatus).BeginInit();
            tabPageDoctor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartPatientsPerDoctor).BeginInit();
            tabPageService.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartPatientsPerService).BeginInit();
            SideBarBackground.SuspendLayout();
            TopHeader.SuspendLayout();
            SuspendLayout();
            // 
            // cmbReportType
            // 
            cmbReportType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReportType.Location = new Point(108, 125);
            cmbReportType.Name = "cmbReportType";
            cmbReportType.Size = new Size(151, 23);
            cmbReportType.TabIndex = 0;
            cmbReportType.SelectedIndexChanged += cmbReportType_SelectedIndexChanged;
            // 
            // dtpStart
            // 
            dtpStart.Location = new Point(302, 125);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(189, 23);
            dtpStart.TabIndex = 1;
            // 
            // dtpEnd
            // 
            dtpEnd.Location = new Point(511, 125);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(189, 23);
            dtpEnd.TabIndex = 2;
            // 
            // btnGenerate
            // 
            btnGenerate.Location = new Point(738, 123);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(116, 25);
            btnGenerate.TabIndex = 3;
            btnGenerate.Text = "Generate Report";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerateReport_Click;
            // 
            // tabControlReports
            // 
            tabControlReports.Controls.Add(tabPageAppointments);
            tabControlReports.Controls.Add(tabPageStatus);
            tabControlReports.Controls.Add(tabPageDoctor);
            tabControlReports.Controls.Add(tabPageService);
            tabControlReports.Location = new Point(108, 164);
            tabControlReports.Margin = new Padding(3, 2, 3, 2);
            tabControlReports.Name = "tabControlReports";
            tabControlReports.SelectedIndex = 0;
            tabControlReports.Size = new Size(764, 338);
            tabControlReports.TabIndex = 9;
            // 
            // tabPageAppointments
            // 
            tabPageAppointments.Controls.Add(chartAppointments);
            tabPageAppointments.Controls.Add(txtAppointmentsInterpretation);
            tabPageAppointments.Location = new Point(4, 24);
            tabPageAppointments.Margin = new Padding(3, 2, 3, 2);
            tabPageAppointments.Name = "tabPageAppointments";
            tabPageAppointments.Padding = new Padding(3, 2, 3, 2);
            tabPageAppointments.Size = new Size(756, 310);
            tabPageAppointments.TabIndex = 0;
            tabPageAppointments.Text = "Appointments Trend";
            tabPageAppointments.UseVisualStyleBackColor = true;
            // 
            // chartAppointments
            // 
            chartArea1.Name = "ChartArea1";
            chartAppointments.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartAppointments.Legends.Add(legend1);
            chartAppointments.Location = new Point(6, 15);
            chartAppointments.Margin = new Padding(3, 2, 3, 2);
            chartAppointments.Name = "chartAppointments";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Appointments";
            chartAppointments.Series.Add(series1);
            chartAppointments.Size = new Size(484, 285);
            chartAppointments.TabIndex = 0;
            chartAppointments.Text = "Appointments Chart";
            // 
            // txtAppointmentsInterpretation
            // 
            txtAppointmentsInterpretation.Location = new Point(496, 15);
            txtAppointmentsInterpretation.Margin = new Padding(3, 2, 3, 2);
            txtAppointmentsInterpretation.Multiline = true;
            txtAppointmentsInterpretation.Name = "txtAppointmentsInterpretation";
            txtAppointmentsInterpretation.ReadOnly = true;
            txtAppointmentsInterpretation.Size = new Size(246, 286);
            txtAppointmentsInterpretation.TabIndex = 1;
            txtAppointmentsInterpretation.Text = "Interpretation will be displayed here.";
            // 
            // tabPageStatus
            // 
            tabPageStatus.Controls.Add(chartStatus);
            tabPageStatus.Controls.Add(txtStatusInterpretation);
            tabPageStatus.Location = new Point(4, 24);
            tabPageStatus.Margin = new Padding(3, 2, 3, 2);
            tabPageStatus.Name = "tabPageStatus";
            tabPageStatus.Padding = new Padding(3, 2, 3, 2);
            tabPageStatus.Size = new Size(756, 310);
            tabPageStatus.TabIndex = 1;
            tabPageStatus.Text = "Status Distribution";
            tabPageStatus.UseVisualStyleBackColor = true;
            // 
            // chartStatus
            // 
            chartArea2.Name = "ChartArea1";
            chartStatus.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chartStatus.Legends.Add(legend2);
            chartStatus.Location = new Point(6, 15);
            chartStatus.Margin = new Padding(3, 2, 3, 2);
            chartStatus.Name = "chartStatus";
            chartStatus.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            chartStatus.PaletteCustomColors = new Color[]
    {
    Color.LightSkyBlue,
    Color.SkyBlue,
    Color.PaleTurquoise,
    Color.LightSteelBlue,
    Color.DeepSkyBlue
    };
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Legend = "Legend1";
            series2.Name = "StatusDistribution";
            chartStatus.Series.Add(series2);
            chartStatus.Size = new Size(447, 285);
            chartStatus.TabIndex = 0;
            chartStatus.Text = "Status Chart";
            // 
            // txtStatusInterpretation
            // 
            txtStatusInterpretation.Location = new Point(496, 15);
            txtStatusInterpretation.Margin = new Padding(3, 2, 3, 2);
            txtStatusInterpretation.Multiline = true;
            txtStatusInterpretation.Name = "txtStatusInterpretation";
            txtStatusInterpretation.ReadOnly = true;
            txtStatusInterpretation.Size = new Size(246, 286);
            txtStatusInterpretation.TabIndex = 1;
            txtStatusInterpretation.Text = "Interpretation will be displayed here.";
            // 
            // tabPageDoctor
            // 
            tabPageDoctor.Controls.Add(chartPatientsPerDoctor);
            tabPageDoctor.Controls.Add(txtDoctorInterpretation);
            tabPageDoctor.Location = new Point(4, 24);
            tabPageDoctor.Margin = new Padding(3, 2, 3, 2);
            tabPageDoctor.Name = "tabPageDoctor";
            tabPageDoctor.Padding = new Padding(3, 2, 3, 2);
            tabPageDoctor.Size = new Size(756, 310);
            tabPageDoctor.TabIndex = 2;
            tabPageDoctor.Text = "Patients Per Doctor";
            tabPageDoctor.UseVisualStyleBackColor = true;
            // 
            // chartPatientsPerDoctor
            // 
            chartArea3.Name = "ChartArea1";
            chartPatientsPerDoctor.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            chartPatientsPerDoctor.Legends.Add(legend3);
            chartPatientsPerDoctor.Location = new Point(6, 15);
            chartPatientsPerDoctor.Margin = new Padding(3, 2, 3, 2);
            chartPatientsPerDoctor.Name = "chartPatientsPerDoctor";
            chartPatientsPerDoctor.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            chartPatientsPerDoctor.PaletteCustomColors = new Color[]
    {
    Color.PowderBlue,
    Color.Turquoise,
    Color.PaleTurquoise,
    Color.LightSkyBlue,
    Color.SkyBlue,
    Color.LightSteelBlue,
    Color.CornflowerBlue
    };
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series3.Legend = "Legend1";
            series3.Name = "Patients";
            chartPatientsPerDoctor.Series.Add(series3);
            chartPatientsPerDoctor.Size = new Size(446, 285);
            chartPatientsPerDoctor.TabIndex = 0;
            chartPatientsPerDoctor.Text = "Doctor Chart";
            // 
            // txtDoctorInterpretation
            // 
            txtDoctorInterpretation.Location = new Point(496, 14);
            txtDoctorInterpretation.Margin = new Padding(3, 2, 3, 2);
            txtDoctorInterpretation.Multiline = true;
            txtDoctorInterpretation.Name = "txtDoctorInterpretation";
            txtDoctorInterpretation.ReadOnly = true;
            txtDoctorInterpretation.Size = new Size(246, 286);
            txtDoctorInterpretation.TabIndex = 1;
            txtDoctorInterpretation.Text = "Interpretation will be displayed here.";
            // 
            // tabPageService
            // 
            tabPageService.Controls.Add(chartPatientsPerService);
            tabPageService.Controls.Add(txtServiceInterpretation);
            tabPageService.Location = new Point(4, 24);
            tabPageService.Margin = new Padding(3, 2, 3, 2);
            tabPageService.Name = "tabPageService";
            tabPageService.Padding = new Padding(3, 2, 3, 2);
            tabPageService.Size = new Size(756, 310);
            tabPageService.TabIndex = 3;
            tabPageService.Text = "Patients Per Service";
            tabPageService.UseVisualStyleBackColor = true;
            // 
            // chartPatientsPerService
            // 
            chartArea4.Name = "ChartArea1";
            chartPatientsPerService.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            chartPatientsPerService.Legends.Add(legend4);
            chartPatientsPerService.Location = new Point(6, 15);
            chartPatientsPerService.Margin = new Padding(3, 2, 3, 2);
            chartPatientsPerService.Name = "chartPatientsPerService";
            chartPatientsPerService.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            chartPatientsPerService.PaletteCustomColors = new Color[]
    {
    Color.PaleTurquoise,
    Color.Aqua,
    Color.LightCyan,
    Color.PowderBlue,
    Color.SkyBlue,
    Color.LightSkyBlue,
    Color.LightSteelBlue,
    Color.Lavender,
    Color.AliceBlue,
    Color.Turquoise,
    Color.DeepSkyBlue,
    Color.LightPink,
    Color.Thistle
    };
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series4.Legend = "Legend1";
            series4.Name = "SPatients";
            chartPatientsPerService.Series.Add(series4);
            chartPatientsPerService.Size = new Size(449, 285);
            chartPatientsPerService.TabIndex = 0;
            chartPatientsPerService.Text = "Service Chart";
            // 
            // txtServiceInterpretation
            // 
            txtServiceInterpretation.Location = new Point(496, 15);
            txtServiceInterpretation.Margin = new Padding(3, 2, 3, 2);
            txtServiceInterpretation.Multiline = true;
            txtServiceInterpretation.Name = "txtServiceInterpretation";
            txtServiceInterpretation.ReadOnly = true;
            txtServiceInterpretation.Size = new Size(246, 286);
            txtServiceInterpretation.TabIndex = 1;
            txtServiceInterpretation.Text = "Interpretation will be displayed here.";
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
            SideBarBackground.TabIndex = 35;
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
            TopHeader.TabIndex = 36;
            // 
            // Top_lbl
            // 
            Top_lbl.AutoSize = true;
            Top_lbl.Font = new Font("Sitka Subheading", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Top_lbl.ForeColor = SystemColors.ControlLight;
            Top_lbl.Location = new Point(23, 9);
            Top_lbl.Name = "Top_lbl";
            Top_lbl.Size = new Size(115, 47);
            Top_lbl.TabIndex = 1;
            Top_lbl.Text = "Report";
            // 
            // ReportPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 561);
            Controls.Add(TopHeader);
            Controls.Add(SideBarBackground);
            Controls.Add(tabControlReports);
            Controls.Add(btnGenerate);
            Controls.Add(dtpEnd);
            Controls.Add(dtpStart);
            Controls.Add(cmbReportType);
            Name = "ReportPage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Report";
            Load += ReportPage_Load;
            tabControlReports.ResumeLayout(false);
            tabPageAppointments.ResumeLayout(false);
            tabPageAppointments.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartAppointments).EndInit();
            tabPageStatus.ResumeLayout(false);
            tabPageStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartStatus).EndInit();
            tabPageDoctor.ResumeLayout(false);
            tabPageDoctor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartPatientsPerDoctor).EndInit();
            tabPageService.ResumeLayout(false);
            tabPageService.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartPatientsPerService).EndInit();
            SideBarBackground.ResumeLayout(false);
            TopHeader.ResumeLayout(false);
            TopHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private global::System.Windows.Forms.ComboBox cmbReportType;
        private global::System.Windows.Forms.DateTimePicker dtpStart;
        private global::System.Windows.Forms.DateTimePicker dtpEnd;
        private global::System.Windows.Forms.Button btnGenerate;


        private System.Windows.Forms.DataVisualization.Charting.Chart chartAppointments;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStatus;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPatientsPerDoctor;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPatientsPerService;


        private global::System.Windows.Forms.TabControl tabControlReports;
        private global::System.Windows.Forms.TabPage tabPageAppointments;
        private global::System.Windows.Forms.TabPage tabPageStatus;
        private global::System.Windows.Forms.TabPage tabPageDoctor;
        private global::System.Windows.Forms.TabPage tabPageService;

        private global::System.Windows.Forms.TextBox txtAppointmentsInterpretation;
        private global::System.Windows.Forms.TextBox txtStatusInterpretation;
        private global::System.Windows.Forms.TextBox txtDoctorInterpretation;
        private global::System.Windows.Forms.TextBox txtServiceInterpretation;
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
    }
}