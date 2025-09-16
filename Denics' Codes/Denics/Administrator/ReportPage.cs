using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Denics.Administrator
{
    public partial class ReportPage : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Denics Project\\Denics' Database\\Denics_db.mdf\";Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;

        public ReportPage()
        {
            InitializeComponent();
        }

        private void ReportPage_Load(object sender, EventArgs e)
        {
            // Setup filter defaults
            cmbReportType.Items.AddRange(new string[] { "Daily", "Weekly", "Monthly", "Custom" });
            cmbReportType.SelectedIndex = 0;

            dtpStart.Enabled = false;
            dtpEnd.Enabled = false;
        }

        private void cmbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReportType.SelectedItem.ToString() == "Custom")
            {
                dtpStart.Enabled = true;
                dtpEnd.Enabled = true;
            }
            else
            {
                dtpStart.Enabled = false;
                dtpEnd.Enabled = false;
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                string reportType = cmbReportType.SelectedItem.ToString();
                DateTime startDate, endDate;

                if (reportType == "Daily")
                {
                    startDate = DateTime.Today;
                    endDate = DateTime.Today;
                }
                else if (reportType == "Weekly")
                {
                    startDate = DateTime.Today.AddDays(-7);
                    endDate = DateTime.Today;
                }
                else if (reportType == "Monthly")
                {
                    startDate = DateTime.Today.AddMonths(-1);
                    endDate = DateTime.Today;
                }
                else // Custom
                {
                    startDate = dtpStart.Value.Date;
                    endDate = dtpEnd.Value.Date;
                }

                con.Open();

                cmd = new SqlCommand(@"
            SELECT 
                COUNT(*) AS total_appointments,
                SUM(CASE WHEN status = 'completed' THEN 1 ELSE 0 END) AS completed_appointments,
                SUM(CASE WHEN status = 'cancelled' THEN 1 ELSE 0 END) AS cancelled_appointments,
                SUM(CASE WHEN status = 'no-show' THEN 1 ELSE 0 END) AS no_show_count
            FROM Appointments
            WHERE appointment_date BETWEEN @startDate AND @endDate", con);

                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);

                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

                dgvReport.AutoGenerateColumns = true;   // ✅ important
                dgvReport.DataSource = dt;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating report: " + ex.Message);
            }
        }
    }
}
