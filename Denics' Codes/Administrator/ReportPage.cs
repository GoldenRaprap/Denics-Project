using Denics.FrontPage;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; 

namespace Denics.Administrator
{
    public partial class ReportPage : Form
    {
        
        static CallDatabase db = new CallDatabase();

        SqlConnection con = new SqlConnection(db.getDatabaseStringName());

        public ReportPage()
        {
            InitializeComponent();
            ReportButton.Click += ReportButton_Click;
            HomeButton.Click += HomeButton_Click;
            ServicesButton.Click += ServicesButton_Click;
            AvailabilityButton.Click += AvailabilityButton_Click;
            AppointmentButton.Click += AppointmentButton_Click;
            PatientButton.Click += PatientButton_Click;
            DoctorButton.Click += DoctorButton_Click;
        }
        private void AdminDesignLayout_Load(object sender, EventArgs e)
        {
        }
        private void HomeButton_Click(object sender, EventArgs e)
        {
            MainAdminPage homeButton = new MainAdminPage();
            homeButton.Show();
            this.Hide();
        }

        private void PatientButton_Click(object sender, EventArgs e)
        {
            PatientsPage patientsPage = new PatientsPage();
            patientsPage.Show();
            this.Hide();
        }

        private void DoctorButton_Click(object sender, EventArgs e)
        {
            DoctorPage doctorsPage = new DoctorPage();
            doctorsPage.Show();
            this.Hide();
        }

        private void AvailabilityButton_Click(object sender, EventArgs e)
        {
            SchedulePage availabilityPage = new SchedulePage();
            availabilityPage.Show();
            this.Hide();
        }

        private void AppointmentButton_Click(object sender, EventArgs e)
        {
            AppointmentPage appointmentPage = new AppointmentPage();
            appointmentPage.Show();
            this.Hide();
        }

        private void ServicesButton_Click(object sender, EventArgs e)
        {
            ServicesPage servicesPage = new ServicesPage();
            servicesPage.Show();
            this.Hide();
        }

        private void ReportButton_Click(object sender, EventArgs e)
        {
            ReportPage reportsPage = new ReportPage();
            reportsPage.Show();
            this.Hide();
        }



        private void ReportPage_Load(object sender, EventArgs e)
        {
            cmbReportType.Items.AddRange(new string[] { "Daily", "Weekly", "Monthly", "Custom" });
            cmbReportType.SelectedIndex = 0;
            if (dtpStart != null) dtpStart.Enabled = false;
            if (dtpEnd != null) dtpEnd.Enabled = false;
        }

        private void cmbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReportType.SelectedItem == null) return;
            bool isCustom = cmbReportType.SelectedItem.ToString() == "Custom";
            if (dtpStart != null) dtpStart.Enabled = isCustom;
            if (dtpEnd != null) dtpEnd.Enabled = isCustom;
        }

        private void GetDateRange(out DateTime startDate, out DateTime endDate)
        {
            string reportType = cmbReportType.SelectedItem.ToString();
            DateTime today = DateTime.Today;
            startDate = today;
            endDate = today.AddDays(1);

            switch (reportType)
            {
                case "Daily":
                    startDate = today;
                    endDate = today.AddDays(1);
                    break;
                case "Weekly":
                    int diff = (7 + (today.DayOfWeek - DayOfWeek.Sunday)) % 7;
                    startDate = today.AddDays(-1 * diff).Date;
                    endDate = startDate.AddDays(7);
                    break;
                case "Monthly":
                    startDate = new DateTime(today.Year, today.Month, 1);
                    endDate = startDate.AddMonths(1);
                    break;
                case "Custom":
                    
                    startDate = dtpStart.Value.Date;
                    endDate = dtpEnd.Value.Date.AddDays(1);
                    if (startDate >= endDate)
                    {
                        MessageBox.Show("Start date must be before end date. Showing last 7 days instead.",
                            "Date Range Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        startDate = today.AddDays(-7);
                        endDate = today.AddDays(1);
                    }
                    break;
            }
        }


        void LoadAppointments(out DataTable dtAppointments)
        {
            dtAppointments = new DataTable();
            try
            {
                GetDateRange(out DateTime startDate, out DateTime endDate);

                string sql = @"SELECT appointment_id, user_id, doctor_id, service_id, appointment_date, appointment_time, status
                                 FROM dbo.Appointments
                                 WHERE appointment_date >= @StartDate AND appointment_date < @EndDate
                                 ORDER BY appointment_date";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dtAppointments);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading appointments: " + ex.Message);
            }
        }

        void LoadAppointmentsGraph(out DataTable dtChart)
        {
            dtChart = new DataTable();
            try
            {
                
                if (chartAppointments == null)
                {
                    return;
                }

                GetDateRange(out DateTime startDate, out DateTime endDate);

                string sql = @"
                    SELECT CONVERT(date, appointment_date) AS AppointmentDateOnly,
                            COUNT(*) AS Total
                    FROM dbo.Appointments
                    WHERE appointment_date >= @StartDate AND appointment_date < @EndDate
                    GROUP BY CONVERT(date, appointment_date)
                    ORDER BY AppointmentDateOnly";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dtChart);

                    chartAppointments.Series.Clear();
                    var series = new Series("Appointments")
                    {
                        XValueMember = "AppointmentDateOnly",
                        YValueMembers = "Total",
                        ChartType = SeriesChartType.Column
                    };

                    chartAppointments.Series.Add(series);
                    
                    chartAppointments.ChartAreas[0].AxisX.Title = "Date";
                    chartAppointments.ChartAreas[0].AxisY.Title = "Total Appointments";
                    chartAppointments.ChartAreas[0].AxisX.LabelStyle.Format = "MM/dd";

                    if (dtChart.Rows.Count > 0)
                    {
                        
                        int maxTotal = dtChart.AsEnumerable().Max(r => r.Field<int>("Total"));
                        chartAppointments.ChartAreas[0].AxisY.Maximum = maxTotal > 0 ? maxTotal + 1 : 2;
                        chartAppointments.ChartAreas[0].AxisY.Interval = maxTotal < 10 ? 1 : Math.Ceiling((double)maxTotal / 5);
                    }
                    else
                    {
                        chartAppointments.ChartAreas[0].AxisY.Maximum = 2;
                        chartAppointments.ChartAreas[0].AxisY.Interval = 1;
                    }

                    chartAppointments.DataSource = dtChart;
                    chartAppointments.DataBind();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading chart: " + ex.Message);
            }
        }

       
        void LoadStatusPieChart(out DataTable dtChart)
        {
            dtChart = new DataTable();
            try
            {
                if (chartStatus == null)
                {
                    return;
                }

                GetDateRange(out DateTime startDate, out DateTime endDate);

                string sql = @"
                 SELECT status, COUNT(*) AS Total
                 FROM dbo.Appointments
                 WHERE appointment_date >= @StartDate AND appointment_date < @EndDate
                 GROUP BY status
                 ORDER BY status ASC";


                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dtChart);

                    chartStatus.Series.Clear();
                    Series series = new Series("StatusDistribution")
                    {
                        ChartType = SeriesChartType.Pie
                    };

                    chartStatus.Series.Add(series);
                    foreach (DataRow row in dtChart.Rows)
                    {
                        double value = Convert.ToDouble(row["Total"]);
                        string status = row["status"].ToString();
                        series.Points.AddXY(status, value);
                    }

                    series.IsValueShownAsLabel = true;
                    series.Label = "#PERCENT{P0}";
                    series["PieLabelStyle"] = "Inside";
                    series.LegendText = "#VALX";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Pie Chart: " + ex.Message);
            }
        }


        private void LoadPatientsPerDoctorChart(out DataTable dtChart)
        {
            dtChart = new DataTable();
            try
            {

                if (chartPatientsPerDoctor == null)
                {
                    return;
                }

                GetDateRange(out DateTime startDate, out DateTime endDate);

                using (SqlConnection con = new SqlConnection(db.getDatabaseStringName()))
                {
                    con.Open();
                    string query = @"
                     SELECT d.full_name AS DoctorName, COUNT(a.appointment_id) AS TotalPatients
                     FROM Appointments a
                     JOIN Doctors d ON a.doctor_id = d.doctor_id
                     WHERE a.appointment_date >= @StartDate AND a.appointment_date < @EndDate
                     GROUP BY d.full_name";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dtChart);

                    if (chartPatientsPerDoctor.Series.Count > 0 && chartPatientsPerDoctor.Series.Any(s => s.Name == "Patients"))
                    {
                        chartPatientsPerDoctor.Series["Patients"].Points.Clear();

                        foreach (DataRow row in dtChart.Rows)
                        {
                            string doctorName = row["DoctorName"].ToString();
                            int totalPatients = Convert.ToInt32(row["TotalPatients"]);

                            int pointIndex = chartPatientsPerDoctor.Series["Patients"].Points.AddXY(doctorName, totalPatients);
                            var point = chartPatientsPerDoctor.Series["Patients"].Points[pointIndex];

                            point.LegendText = $"{doctorName} ({totalPatients})";
                            point.Label = totalPatients.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Doctor Chart: " + ex.Message);
            }
        }


        private void LoadPatientsPerServiceChart(out DataTable dtChart)
        {
            dtChart = new DataTable();
            try
            {

                if (chartPatientsPerService == null)
                {
                    return;
                }

                GetDateRange(out DateTime startDate, out DateTime endDate);

                using (SqlConnection con = new SqlConnection(db.getDatabaseStringName()))
                {
                    con.Open();

                    string query = @"
                    SELECT 
                        s.service_name AS ServiceName, 
                        COUNT(a.appointment_id) AS TotalPatients
                    FROM Appointments a
                    JOIN Services s ON a.service_id = s.service_id
                    WHERE a.appointment_date >= @StartDate AND a.appointment_date < @EndDate
                    GROUP BY s.service_name";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtChart);


                        if (chartPatientsPerService.Series.Count > 0 && chartPatientsPerService.Series.Any(s => s.Name == "SPatients"))
                        {
                             chartPatientsPerService.Palette = ChartColorPalette.None;
                             chartPatientsPerService.Series["SPatients"].Points.Clear();

                            foreach (DataRow row in dtChart.Rows)
                            {
                                string serviceName = row["ServiceName"].ToString();
                                int totalPatients = Convert.ToInt32(row["TotalPatients"]);

                                int pointIndex = chartPatientsPerService.Series["SPatients"].Points.AddXY(serviceName, totalPatients);
                                var point = chartPatientsPerService.Series["SPatients"].Points[pointIndex];


                                point.LegendText = $"{serviceName} ({totalPatients})";
                                point.Label = totalPatients.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Service Chart: " + ex.Message);
            }
        }
        private void InterpretAppointmentsGraph(DataTable dt)
        {
          
            if (txtAppointmentsInterpretation == null) return;

            if (dt.Rows.Count == 0)
            {
                txtAppointmentsInterpretation.Text = "No appointment data available for the selected date range.";
                return;
            }

            DataRow maxRow = dt.AsEnumerable()
                               .OrderByDescending(r => r.Field<int>("Total"))
                               .FirstOrDefault();

            DataRow minRow = dt.AsEnumerable()
                               .OrderBy(r => r.Field<int>("Total"))
                               .FirstOrDefault();

            string peakDate = maxRow.Field<DateTime>("AppointmentDateOnly").ToShortDateString();
            int peakTotal = maxRow.Field<int>("Total");
            string minDate = minRow.Field<DateTime>("AppointmentDateOnly").ToShortDateString();
            int minTotal = minRow.Field<int>("Total");
            int totalAppointments = dt.AsEnumerable().Sum(r => r.Field<int>("Total"));

            txtAppointmentsInterpretation.Text =
            $"--- APPOINTMENT TREND ANALYSIS ---\r\n\r\n" +
            $"Total Appointments: {totalAppointments}\r\n\r\n" +
            $"PEAK DAY: {peakDate} with {peakTotal} appointments. This was your busiest day, showing strong patient interest. Try to find what worked well on this day so you can do the same on other days.\r\n\r\n" +
            $" SLOWEST DAY: {minDate} with {minTotal} appointment(s). This was your quietest day. It’s a good time for staff training, cleaning, or special promotions to bring in more patients.";
        }

        private void InterpretStatusPieChart(DataTable dt)
        {
            // Essential Null Check for TextBox
            if (txtStatusInterpretation == null) return;

            if (dt.Rows.Count == 0)
            {
                txtStatusInterpretation.Text = "No status data available for interpretation.";
                return;
            }

            int total = dt.AsEnumerable().Sum(r => r.Field<int>("Total"));
            if (total == 0)
            {
                txtStatusInterpretation.Text = "Total appointments for this period is zero.";
                return;
            }


            int cancelled = dt.AsEnumerable()
                                 .Where(r => r.Field<string>("status").Equals("cancelled", StringComparison.OrdinalIgnoreCase))
                                 .Select(r => r.Field<int>("Total")).FirstOrDefault();

            int noShow = dt.AsEnumerable()
                           .Where(r => r.Field<string>("status").Equals("no-show", StringComparison.OrdinalIgnoreCase))
                           .Select(r => r.Field<int>("Total")).FirstOrDefault();

            int reschedule = dt.AsEnumerable()
                           .Where(r => r.Field<string>("status").Equals("reschedule", StringComparison.OrdinalIgnoreCase))
                           .Select(r => r.Field<int>("Total")).FirstOrDefault();


            int wastedAppts = cancelled + noShow + reschedule;
            double wastedPercent = (double)wastedAppts / total * 100;

            txtStatusInterpretation.Text =
                $"--- APPOINTMENT STATUS ANALYSIS ---\r\n\r\n" +
                $"Total Scheduled: {total} appointments.\r\n\r\n" +
                $"- Cancellations: {cancelled} ({((double)cancelled / total * 100):N1}%)\r\n" +
                $"- No-Shows: {noShow} ({((double)noShow / total * 100):N1}%)\r\n" +
                $"- Rescheduled: {reschedule} ({((double)reschedule / total * 100):N1}%)\r\n\r\n" +
                $" ACTION: Many missed or cancelled appointments can waste time and reduce income. Send reminder texts or calls 24 hours before appointments, and ask for small deposits to reduce no-shows.";
        }

        private void InterpretPatientsPerDoctorChart(DataTable dt)
        {
            
            if (txtDoctorInterpretation == null) return;

            if (dt.Rows.Count == 0)
            {
                txtDoctorInterpretation.Text = "No patient-per-doctor data available for interpretation.";
                return;
            }

            DataRow maxRow = dt.AsEnumerable()
                               .OrderByDescending(r => r.Field<int>("TotalPatients"))
                               .FirstOrDefault();

            DataRow minRow = dt.AsEnumerable()
                               .OrderBy(r => r.Field<int>("TotalPatients"))
                               .FirstOrDefault();

            string busiestDoctor = maxRow.Field<string>("DoctorName");
            int maxPatients = maxRow.Field<int>("TotalPatients");
            string lightestDoctor = minRow.Field<string>("DoctorName");
            int minPatients = minRow.Field<int>("TotalPatients");
            int totalDoctors = dt.Rows.Count;
            double averagePatients = dt.AsEnumerable().Average(r => r.Field<int>("TotalPatients"));

            txtDoctorInterpretation.Text =
                $"--- DOCTOR WORKLOAD ANALYSIS ---\r\n\r\n" +
                $"Total Doctors Reporting: {totalDoctors}\r\n" +
                $"Average Patient Load: {averagePatients:N1} patients per doctor.\r\n\r\n" +
                $"BUSIEST DOCTOR: {busiestDoctor} with {maxPatients} patients. This doctor has the heaviest workload—check their schedule to prevent burnout or consider offering support or incentives.\r\n\r\n" +
                $" LIGHTEST DOCTOR: {lightestDoctor} with {minPatients} patients. This doctor has fewer patients—try giving them more appointments, promoting their services, or assigning them to walk-in patients.";
        }

        private void InterpretPatientsPerServiceChart(DataTable dt)
        {
            
            if (txtServiceInterpretation == null) return;

            if (dt.Rows.Count == 0)
            {
                txtServiceInterpretation.Text = "No patient-per-service data available for interpretation.";
                return;
            }

            DataRow maxRow = dt.AsEnumerable()
                               .OrderByDescending(r => r.Field<int>("TotalPatients"))
                               .FirstOrDefault();

            string topService = maxRow.Field<string>("ServiceName");
            int maxPatients = maxRow.Field<int>("TotalPatients");
            int totalPatients = dt.AsEnumerable().Sum(r => r.Field<int>("TotalPatients"));
            double topServicePercent = (double)maxPatients / totalPatients * 100;


            txtServiceInterpretation.Text =
                $"--- SERVICE POPULARITY ANALYSIS ---\r\n\r\n" +
                $"Total Patients Across All Services: {totalPatients}\r\n\r\n" +
                $" TOP SERVICE: '{topService}' makes up {topServicePercent:N1}% of all appointments. This is your main revenue source.\r\n\r\n" +
                $"ACTION: Make sure there are enough supplies and trained staff for this service. Use its popularity in marketing, promotions, or package deals to attract more patients.";
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            
            if (chartAppointments != null) chartAppointments.Series.Clear();
            if (chartStatus != null) chartStatus.Series.Clear();

            
            if (chartPatientsPerDoctor != null && chartPatientsPerDoctor.Series.Any(s => s.Name == "Patients"))
                chartPatientsPerDoctor.Series["Patients"].Points.Clear();

            if (chartPatientsPerService != null && chartPatientsPerService.Series.Any(s => s.Name == "SPatients"))
                chartPatientsPerService.Series["SPatients"].Points.Clear();



            DataTable dtAppointments, dtAppointmentsGraph, dtStatus, dtDoctor, dtService;

            LoadAppointments(out dtAppointments);
            LoadAppointmentsGraph(out dtAppointmentsGraph);
            LoadStatusPieChart(out dtStatus);
            LoadPatientsPerDoctorChart(out dtDoctor);
            LoadPatientsPerServiceChart(out dtService);


            InterpretAppointmentsGraph(dtAppointmentsGraph);
            InterpretStatusPieChart(dtStatus);
            InterpretPatientsPerDoctorChart(dtDoctor);
            InterpretPatientsPerServiceChart(dtService);


            if (tabControlReports != null)
            {
                tabControlReports.SelectedIndex = 0;
            }
        }
    }
}