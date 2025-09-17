using Denics.Models;
using Denics.Services;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;



namespace Denics.Administrator
{
    public partial class SchedulePage : Form
    {
        private readonly ScheduleService _service = new ScheduleService();
        private static readonly string[] Days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        private static readonly string[] Times = { "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00" };

        public SchedulePage()
        {
            InitializeComponent();
            LoadIntoDoctorsComboBox();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void SchedulePage_Load(object sender, EventArgs e)
        {
            ConfigureScheduleGrid();
        }

        private void LoadIntoDoctorsComboBox()
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Denics Project\\Denics' Database\\Denics_db.mdf\";Integrated Security=True;Connect Timeout=30");
            {
                var cmd = new SqlCommand("SELECT doctor_id, full_name FROM Doctors", con);
                var adapter = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                adapter.Fill(dt);
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "full_name";
                comboBox1.ValueMember = "doctor_id";
            }
        }

        private void ConfigureScheduleGrid()
        {
            TableSchedule.Controls.Clear();
            TableSchedule.ColumnCount = 8; // 1 for time labels + 7 days
            TableSchedule.RowCount = 10;   // 1 for day headers + 9 time slots
            TableSchedule.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;



            TableSchedule.ColumnStyles.Clear();
            TableSchedule.RowStyles.Clear();

            for (int i = 0; i < TableSchedule.ColumnCount; i++)
                TableSchedule.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / TableSchedule.ColumnCount));

            for (int i = 0; i < TableSchedule.RowCount; i++)
                TableSchedule.RowStyles.Add(new RowStyle(SizeType.Absolute, 18));

            // Add day headers
            for (int col = 1; col <= 7; col++)
            {
                TableSchedule.Controls.Add(CreateLabel(Days[col - 1], FontStyle.Bold, Color.LightGray), col, 0);
            }

            // Add time labels
            for (int row = 1; row <= 9; row++)
            {
                TableSchedule.Controls.Add(CreateLabel(Times[row - 1], FontStyle.Bold, Color.LightGray), 0, row);
            }

            // Add empty placeholders for schedule cells
            for (int row = 1; row <= 9; row++)
            {
                for (int col = 1; col <= 7; col++)
                {
                    var placeholder = new Label
                    {
                        Text = "", // or "—" if you want a visual marker
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill,
                        Font = new Font("Segoe UI", 8),
                        BackColor = Color.White
                    };
                    TableSchedule.Controls.Add(placeholder, col, row);
                }
            }
        }

        private Label CreateLabel(string text, FontStyle style, Color backColor)
        {
            return new Label
            {
                Text = text,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9, style),
                BackColor = backColor
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null && int.TryParse(comboBox1.SelectedValue.ToString(), out int doctorId))
            {
                ConfigureScheduleGrid();
                var schedule = _service.GetDoctorSchedule(doctorId);
                foreach (var slot in schedule)
                    UpdateGridCell(slot);
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            var availability = new DoctorAvailability
            {
                DoctorId = Convert.ToInt32(comboBox1.SelectedValue),
                DayOfWeek = Day.SelectedItem.ToString(),
                HourSlot = TimeSpan.Parse(Time.SelectedItem.ToString()),
                Status = Status.SelectedItem.ToString()
            };

            _service.SaveAvailability(availability);

            MessageBox.Show(
                $"Doctor availability added:\n\n" +
                $"Day: {availability.DayOfWeek}\n" +
                $"Time: {availability.HourSlot:hh\\:mm}\n" +
                $"Status: {availability.Status}",
                "Schedule Saved",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            // Optional: update the grid visually
            // UpdateGridCell(availability);
        }

        private void UpdateGridCell(DoctorAvailability slot)
        {
            int col = Array.IndexOf(Days, slot.DayOfWeek);
            int row = Array.FindIndex(Times, t => TimeSpan.Parse(t) == slot.HourSlot);

            // Defensive bounds check
            if (col >= 0 && row >= 0 && col + 1 < TableSchedule.ColumnCount && row + 1 < TableSchedule.RowCount)
            {
                // Remove any existing control in that cell to avoid overlap
                var existing = TableSchedule.GetControlFromPosition(col + 1, row + 1);
                if (existing != null)
                    TableSchedule.Controls.Remove(existing);

                // Create and add the new label
                var cell = new Label
                {
                    Text = slot.Status,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 6.5f, FontStyle.Bold), // Slightly smaller for compact layout
                    ForeColor = Color.White,
                    BackColor = GetStatusColor(slot.Status)
                };

                TableSchedule.Controls.Add(cell, col + 1, row + 1);
            }
            else
            {
                // Optional: log or alert if something went wrong
                Debug.WriteLine($"Invalid cell position: Day={slot.DayOfWeek}, Time={slot.HourSlot}");
            }
        }
        private Color GetStatusColor(string status)
        {
            return status.ToLower() switch
            {
                "available" => Color.Green,
                "break" => Color.Blue,
                "not available" => Color.Red,
                _ => Color.Gray
            };
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

        private void NotificationButton_Click(object sender, EventArgs e)
        {
            NotificationPage notificationPage = new NotificationPage();
            notificationPage.Show();
            this.Hide();
        }
    }
}