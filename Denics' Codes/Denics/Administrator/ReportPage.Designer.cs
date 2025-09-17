namespace Denics.Administrator
{
    partial class ReportPage
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
            cmbReportType = new ComboBox();
            dtpStart = new DateTimePicker();
            dtpEnd = new DateTimePicker();
            btnGenerate = new Button();
            dgvReport = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvReport).BeginInit();
            SuspendLayout();
            // 
            // cmbReportType
            // 
            cmbReportType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReportType.Location = new Point(165, 49);
            cmbReportType.Name = "cmbReportType";
            cmbReportType.Size = new Size(200, 23);
            cmbReportType.TabIndex = 0;
            cmbReportType.SelectedIndexChanged += cmbReportType_SelectedIndexChanged;
            // 
            // dtpStart
            // 
            dtpStart.Location = new Point(385, 49);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(200, 23);
            dtpStart.TabIndex = 1;
            // 
            // dtpEnd
            // 
            dtpEnd.Location = new Point(605, 49);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(200, 23);
            dtpEnd.TabIndex = 2;
            // 
            // btnGenerate
            // 
            btnGenerate.Location = new Point(825, 49);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(120, 25);
            btnGenerate.TabIndex = 3;
            btnGenerate.Text = "Generate Report";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerateReport_Click;
            // 
            // dgvReport
            // 
            dgvReport.AccessibleRole = AccessibleRole.IpAddress;
            dgvReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReport.Location = new Point(129, 93);
            dgvReport.Name = "dgvReport";
            dgvReport.Size = new Size(843, 500);
            dgvReport.TabIndex = 4;
            // 
            // ReportPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 661);
            Controls.Add(dgvReport);
            Controls.Add(btnGenerate);
            Controls.Add(dtpEnd);
            Controls.Add(dtpStart);
            Controls.Add(cmbReportType);
            Name = "ReportPage";
            Text = "Report";
            Load += ReportPage_Load;
            ((System.ComponentModel.ISupportInitialize)dgvReport).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cmbReportType;
        private DateTimePicker dtpStart;
        private DateTimePicker dtpEnd;
        private Button btnGenerate;
        private DataGridView dgvReport;
        private Label lblSummary;
    }
}
