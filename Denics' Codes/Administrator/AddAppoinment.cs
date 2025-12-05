using Denics.FrontPage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Data.SqlClient;

namespace Denics.Administrator
{
    public partial class AddAppoinment : Form
    {
        static CallDatabase db = new CallDatabase();
        SqlConnection con = new SqlConnection(db.getDatabaseStringName());
        SqlCommand cmd;

        // currently selected user (from NameTable_grd)
        private int? _selectedUserId;

        public AddAppoinment()
        {
            InitializeComponent();

            // SideBar Click Events
            ReportButton.Click += ReportButton_Click;
            HomeButton.Click += HomeButton_Click;
            ServicesButton.Click += ServicesButton_Click;
            AvailabilityButton.Click += AvailabilityButton_Click;
            AppointmentButton.Click += AppointmentButton_Click;
            PatientButton.Click += PatientButton_Click;
            DoctorButton.Click += DoctorButton_Click;

            // Wire grid click so admin can pick a user row
            NameTable_grd.CellClick += NameTable_grd_CellClick;
            NameTable_grd.SelectionChanged += NameTable_grd_SelectionChanged;

            // Appointment grid action handler
            AppointmentDataGridView.CellContentClick += AppointmentDataGridView_CellContentClick;

            // Ensure password-show checkboxes toggle masking (designer may already wire these; hooking here is safe)
            checkBoxShow.CheckedChanged += checkBoxShow_CheckedChanged;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;

            // Search textbox
            Search_txtbx.TextChanged += Search_txtbx_TextChanged;
        }

        private void AddAppoinment_Load(object sender, EventArgs e)
        {
            LoadNameTable();

            SetupAppointmentPickersAndSlots();
            LoadServices();

            // Ensure password fields are masked by default and single-line so masking works reliably
            if (boxPassword != null)
            {
                boxPassword.UseSystemPasswordChar = true;
                boxPassword.Multiline = false;
            }

            if (ReEnterPassword_txtbx != null)
            {
                ReEnterPassword_txtbx.UseSystemPasswordChar = true;
                ReEnterPassword_txtbx.Multiline = false;
            }
        }

        private void SetupAppointmentPickersAndSlots()
        {
            // Use the designer DateTimePicker named `appointmenrDatePicker` (note: it's declared in the .Designer file).
            if (appointmenrDatePicker != null)
            {
                appointmenrDatePicker.Format = DateTimePickerFormat.Short;
                // Do NOT set Value here - that would override the user's pick. Leave Value untouched.
            }

            // TimeofAppointmentcomboBox is created by the designer; Populate time slots below.
            PopulateHourlyTimeSlots(TimeSpan.FromHours(9), TimeSpan.FromHours(15));
        }
        private void PopulateHourlyTimeSlots(TimeSpan start, TimeSpan end)
        {
            string previousSelection = TimeofAppointmentcomboBox.SelectedItem?.ToString();

            TimeofAppointmentcomboBox.Items.Clear();
            for (TimeSpan t = start; t <= end; t = t.Add(TimeSpan.FromHours(1)))
            {
                TimeofAppointmentcomboBox.Items.Add(t.ToString(@"hh\:mm"));
            }

            if (previousSelection != null && TimeofAppointmentcomboBox.Items.Contains(previousSelection))
            {
                TimeofAppointmentcomboBox.SelectedItem = previousSelection;
            }
            else if (TimeofAppointmentcomboBox.Items.Count > 0)
            {
                TimeofAppointmentcomboBox.SelectedIndex = 0;
            }
        }
        private void LoadServices()
        {
            try
            {
                con.Open();
                string query = "SELECT service_id, service_name FROM Services";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    ServicecomboBox.Items.Clear();
                    while (reader.Read())
                    {
                        ServicecomboBox.Items.Add(new ComboBoxItem
                        {
                            Text = reader["service_name"].ToString(),
                            Value = reader["service_id"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading services: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void LoadNameTable()
        {
            try
            {
                con.Open();

                // load users for selection (user_id hidden)
                string query = @"
                    SELECT 
                        u.user_id,
                        u.firstname + ' ' + u.surname AS Patient,
                        u.email,
                        u.contact,
                        u.address,
                        u.birthdate
                    FROM Users u
                    ORDER BY u.surname, u.firstname";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                NameTable_grd.DataSource = dt;

                if (NameTable_grd.Columns.Contains("user_id"))
                    NameTable_grd.Columns["user_id"].Visible = false;

                // hide less-important columns in the small list
                if (NameTable_grd.Columns.Contains("email"))
                    NameTable_grd.Columns["email"].Visible = false;
                if (NameTable_grd.Columns.Contains("address"))
                    NameTable_grd.Columns["address"].Visible = false;
                if (NameTable_grd.Columns.Contains("birthdate"))
                    NameTable_grd.Columns["birthdate"].Visible = false;
                if (NameTable_grd.Columns.Contains("contact"))
                    NameTable_grd.Columns["contact"].Visible = false;

                NameTable_grd.ClearSelection();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Error: " + ex.Message, "Database Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show("General Error: " + ex.Message, "Error");
            }
            finally
            {
                con.Close();
            }
        }

        // Support click and selection change to load user info / appointments
        private void NameTable_grd_SelectionChanged(object sender, EventArgs e)
        {
            // avoid reentrancy when grid is being populated
            if (NameTable_grd.CurrentCell == null) return;

            var rowIndex = NameTable_grd.CurrentCell.RowIndex;
            var colIndex = NameTable_grd.CurrentCell.ColumnIndex;
            NameTable_grd_CellClick(NameTable_grd, new DataGridViewCellEventArgs(colIndex, rowIndex));
        }

        // When admin clicks a user row - load their info into the info panel and show their appointments
        private void NameTable_grd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                var row = NameTable_grd.Rows[e.RowIndex];
                if (row == null) return;

                if (!NameTable_grd.Columns.Contains("user_id"))
                {
                    _selectedUserId = null;
                    return;
                }

                var cellVal = row.Cells["user_id"].Value;
                if (!int.TryParse(cellVal?.ToString(), out int userId))
                {
                    _selectedUserId = null;
                    return;
                }

                _selectedUserId = userId;

                // Load user details into the info fields
                try
                {
                    using (var qc = new SqlConnection(db.getDatabaseStringName()))
                    using (var qcmd = new SqlCommand("SELECT firstname, surname, birthdate, contact, address, email FROM Users WHERE user_id = @id", qc))
                    {
                        qcmd.Parameters.AddWithValue("@id", userId);
                        qc.Open();
                        using (var reader = qcmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var fname = reader["firstname"]?.ToString() ?? string.Empty;
                                var sname = reader["surname"]?.ToString() ?? string.Empty;
                                var fullname = (fname + " " + sname).Trim();
                                userName_txtbx.Text = fullname;
                                Birthdate_txtbx.Text = reader["birthdate"] != DBNull.Value ? Convert.ToDateTime(reader["birthdate"]).ToString("MMMM dd, yyyy") : string.Empty;
                                Contact_txtbx.Text = reader["contact"]?.ToString() ?? string.Empty;
                                Address_txtbx.Text = reader["address"]?.ToString() ?? string.Empty;
                            }
                        }
                    }
                }
                catch
                {
                    // fallback to values from grid if DB load fails
                    userName_txtbx.Text = row.Cells["Patient"].Value?.ToString() ?? string.Empty;
                    Contact_txtbx.Text = NameTable_grd.Columns.Contains("Contact") ? row.Cells["Contact"].Value?.ToString() ?? string.Empty : string.Empty;
                    Address_txtbx.Text = string.Empty;
                    Birthdate_txtbx.Text = string.Empty;
                }

                // Load appointments for this user
                LoadAppointmentsForUser(userId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading selected user: " + ex.Message, "Error");
            }
        }

        // Loads appointments for a user into AppointmentDataGridView
        private void LoadAppointmentsForUser(int userId)
        {
            try
            {
                using (var qc = new SqlConnection(db.getDatabaseStringName()))
                {
                    qc.Open();
                    string query = @"
                        SELECT 
                            a.appointment_id,
                            s.service_name AS [Service],
                            d.full_name AS [Doctor],
                            a.appointment_date AS [Date],
                            a.appointment_time AS [Time],
                            a.status AS [Status]
                        FROM Appointments a
                        INNER JOIN Doctors d ON a.doctor_id = d.doctor_id
                        INNER JOIN Services s ON a.service_id = s.service_id
                        WHERE a.user_id = @userId AND a.status != 'cancelled'
                        ORDER BY a.appointment_date, a.appointment_time";
                    using (var cmd = new SqlCommand(query, qc))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        var adapter = new SqlDataAdapter(cmd);
                        var table = new DataTable();
                        adapter.Fill(table);
                        AppointmentDataGridView.DataSource = table;

                        if (AppointmentDataGridView.Columns.Contains("appointment_id"))
                            AppointmentDataGridView.Columns["appointment_id"].Visible = false;

                        // Remove existing Cancel column if present, then add fresh Cancel column
                        if (AppointmentDataGridView.Columns.Contains("Cancel"))
                            AppointmentDataGridView.Columns.Remove("Cancel");

                        DataGridViewButtonColumn cancelColumn = new DataGridViewButtonColumn
                        {
                            Name = "Cancel",
                            HeaderText = "Cancel",
                            Text = "Cancel",
                            UseColumnTextForButtonValue = true
                        };
                        AppointmentDataGridView.Columns.Add(cancelColumn);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user appointments: " + ex.Message, "Error");
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(boxFName.Text) ||
                string.IsNullOrWhiteSpace(boxLName.Text) ||
                string.IsNullOrWhiteSpace(boxEmail.Text) ||
                string.IsNullOrWhiteSpace(boxNumber.Text) ||
                string.IsNullOrWhiteSpace(boxPassword.Text) ||
                string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(ReEnterPassword_txtbx.Text))
            {
                MessageBox.Show("Please fill in all required fields before saving.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hashedPassword = HashPassword(boxPassword.Text);
            string hashedReEnter = HashPassword(ReEnterPassword_txtbx.Text);

            if (hashedPassword != hashedReEnter)
            {
                MessageBox.Show("Password does not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IsDuplicateUser(boxEmail.Text, boxNumber.Text))
            {
                MessageBox.Show("This user already exists (duplicate Email or Contact).", "Duplicate Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Birthdate_cldr.Checked)
            {
                MessageBox.Show("Please select a birthdate.", "Missing Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Birthdate_cldr.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Birthdate cannot be in the future.", "Invalid Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Determine selected gender from radio buttons
            string gender = null;
            if (Male_rdbtn.Checked) gender = "Male";
            else if (Female_rdbtn.Checked) gender = "Female";

            // Pass data to OTPVerification which will insert after OTP verification.
            var otpForm = new OTPVerification(
                email: boxEmail.Text,
                surname: boxLName.Text,
                firstname: boxFName.Text,
                middlename: string.IsNullOrWhiteSpace(MiddleName_txtbx.Text) ? null : MiddleName_txtbx.Text,
                suffix: string.IsNullOrWhiteSpace(Suffix_txtbx.Text) ? null : Suffix_txtbx.Text,
                gender: gender,
                hashedPassword: hashedPassword,
                contact: boxNumber.Text,
                birthdate: Birthdate_cldr.Value.Date,
                address: textBox1.Text
            );

            otpForm.Show();
            this.Hide();
        }


        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private bool IsDuplicateUser(string email, string contact)
        {
            // Check database for duplicate email or contact
            try
            {
                using (var connection = new SqlConnection(db.getDatabaseStringName()))
                using (var cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE email = @Email OR contact = @Contact", connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Contact", contact ?? string.Empty);
                    connection.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking duplicate user: " + ex.Message, "Error");
                // conservative behavior: treat as duplicate to avoid creating inconsistent records
                return true;
            }
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

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            AppointmentPage appointmentPage = new AppointmentPage();
            appointmentPage.Show();
            this.Hide();
        }

        /// <summary>
        /// Book button: create appointment for the user selected in NameTable_grd.
        /// Mirrors UserBookingPage.bookbtn_Click behaviour, sends pending email if email present.
        /// </summary>

        private void bookbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedUserId == null)
                {
                    MessageBox.Show("Please select a user from the list first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (AvailableDoctorsComboBox.SelectedItem is not DoctorItem selectedDoctor ||
                    ServicecomboBox.SelectedItem is not ComboBoxItem selectedService)
                {
                    MessageBox.Show("Please select a doctor and a service.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime appointmentDate = appointmenrDatePicker.Value.Date;
                if (!TimeSpan.TryParse(TimeofAppointmentcomboBox.SelectedItem?.ToString() ?? string.Empty, out TimeSpan appointmentTime))
                {
                    MessageBox.Show("Please select a valid time slot.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int doctorId = selectedDoctor.DoctorId;
                string serviceId = selectedService.Value.ToString();

                // Check if slot is already taken
                string checkQuery = @"
                    SELECT COUNT(*) FROM Appointments
                    WHERE doctor_id = @doctorId
                      AND appointment_date = @date
                      AND appointment_time = @time
                      AND status != 'cancelled'";

                using (var checkCmd = new SqlCommand(checkQuery, con))
                {
                    checkCmd.Parameters.AddWithValue("@doctorId", doctorId);
                    checkCmd.Parameters.AddWithValue("@date", appointmentDate);
                    checkCmd.Parameters.Add("@time", SqlDbType.Time).Value = appointmentTime;

                    con.Open();
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    con.Close();

                    if (count > 0)
                    {
                        MessageBox.Show("This doctor is already booked at the selected time.");
                        return;
                    }
                }

                // Insert appointment
                string insertQuery = @"
                    INSERT INTO Appointments 
                    (user_id, doctor_id, service_id, appointment_date, appointment_time, status, date_created, last_updated)
                    VALUES 
                    (@userId, @doctorId, @serviceId, @appointmentDate, @appointmentTime, @status, @created, @updated)";

                using (var insertCmd = new SqlCommand(insertQuery, con))
                {
                    insertCmd.Parameters.AddWithValue("@userId", _selectedUserId.Value);
                    insertCmd.Parameters.AddWithValue("@doctorId", doctorId);
                    insertCmd.Parameters.AddWithValue("@serviceId", serviceId);
                    insertCmd.Parameters.AddWithValue("@appointmentDate", appointmentDate);
                    insertCmd.Parameters.Add("@appointmentTime", SqlDbType.Time).Value = appointmentTime;
                    insertCmd.Parameters.AddWithValue("@status", "Pending");
                    insertCmd.Parameters.AddWithValue("@created", DateTime.Now);
                    insertCmd.Parameters.AddWithValue("@updated", DateTime.Now);

                    con.Open();
                    int rows = insertCmd.ExecuteNonQuery();
                    con.Close();

                    if (rows > 0)
                    {
                        // refresh users' appointments grid
                        LoadAppointmentsForUser(_selectedUserId.Value);

                        // send pending email if user has email
                        string recipientEmail = null;
                        string patientFirstname = null;
                        string patientSurname = null;
                        try
                        {
                            using (var qc = new SqlConnection(db.getDatabaseStringName()))
                            using (var qcmd = new SqlCommand("SELECT email, firstname, surname FROM Users WHERE user_id = @id", qc))
                            {
                                qcmd.Parameters.AddWithValue("@id", _selectedUserId.Value);
                                qc.Open();
                                using (var r = qcmd.ExecuteReader())
                                {
                                    if (r.Read())
                                    {
                                        recipientEmail = r["email"]?.ToString();
                                        patientFirstname = r["firstname"]?.ToString();
                                        patientSurname = r["surname"]?.ToString();
                                    }
                                }
                            }
                        }
                        catch { recipientEmail = null; }

                        if (!string.IsNullOrWhiteSpace(recipientEmail))
                        {
                            string patientName = (patientFirstname?.Trim() + " " + patientSurname?.Trim()).Trim();
                            if (string.IsNullOrWhiteSpace(patientName))
                                patientName = userName_txtbx.Text;

                            string serviceName = selectedService.Text;
                            string doctorName = selectedDoctor.FullName;
                            string apntTime = appointmentTime.ToString(@"hh\:mm");
                            DateTime apntDate = appointmentDate;
                            string apntNote = string.Empty;

                            // Send same way as UserBookingPage: background non-blocking
                            Task.Run(() =>
                            {
                                try
                                {
                                    AppointmentServices.SendPendingEmail(recipientEmail, patientName, serviceName, apntDate, apntTime, doctorName, apntNote);
                                }
                                catch (Exception ex)
                                {
                                    try { this.Invoke(() => MessageBox.Show("Failed to send confirmation email: " + ex.Message, "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)); } catch { }
                                }
                            });
                        }

                        MessageBox.Show("Appointment booked successfully for the selected user.");
                    }
                    else
                    {
                        MessageBox.Show("Booking failed.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving appointment: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        // Handle cancel button clicks within admin AppointmentDataGridView
        private void AppointmentDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var grid = sender as DataGridView;
                if (grid == null) return;

                if (grid.Columns[e.ColumnIndex].Name == "Cancel")
                {
                    var row = grid.Rows[e.RowIndex];
                    if (row == null) return;

                    if (!int.TryParse(row.Cells["appointment_id"]?.Value?.ToString(), out int appointmentId))
                    {
                        MessageBox.Show("Invalid appointment selected.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var confirm = MessageBox.Show("Are you sure you want to cancel this appointment?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm != DialogResult.Yes) return;

                    using (var qc = new SqlConnection(db.getDatabaseStringName()))
                    using (var cmd = new SqlCommand("UPDATE Appointments SET status = 'cancelled', last_updated = GETDATE() WHERE appointment_id = @id", qc))
                    {
                        cmd.Parameters.AddWithValue("@id", appointmentId);
                        qc.Open();
                        int rows = cmd.ExecuteNonQuery();
                        qc.Close();

                        if (rows > 0)
                        {
                            MessageBox.Show("Appointment cancelled.");
                            if (_selectedUserId.HasValue) LoadAppointmentsForUser(_selectedUserId.Value);
                        }
                        else
                        {
                            MessageBox.Show("Failed to cancel appointment.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing appointment action: " + ex.Message, "Error");
            }
        }

        // Local DTOs to match UserBookingPage items — added so casting works here
        public class ComboBoxItem
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public override string ToString() => Text;
        }

        public class DoctorItem
        {
            public int DoctorId { get; set; }
            public string FullName { get; set; }
            public override string ToString() => FullName;
        }

        // Search textbox changed: filter NameTable_grd by Patient (firstname + surname)
        private void Search_txtbx_TextChanged(object sender, EventArgs e)
        {
            var text = Search_txtbx.Text?.Trim() ?? string.Empty;

            var dt = GetDataTableFromGrid(NameTable_grd);
            if (dt == null) return;

            try
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    dt.DefaultView.RowFilter = string.Empty;
                    NameTable_grd.ClearSelection();
                    return;
                }

                // Escape single quotes in user input for the RowFilter expression
                var esc = text.Replace("'", "''");

                // Filter by Patient column (contains). Using CONVERT in case column type isn't string.
                var filter = $"CONVERT([Patient], 'System.String') LIKE '%{esc}%'";

                dt.DefaultView.RowFilter = filter;
                NameTable_grd.ClearSelection();
            }
            catch (Exception)
            {
                // On any filter error fallback to clearing filter
                try { dt.DefaultView.RowFilter = string.Empty; } catch { }
            }
        }

        // Helper: safely obtain a DataTable backing the DataGridView
        private DataTable GetDataTableFromGrid(DataGridView grid)
        {
            if (grid == null) return null;

            if (grid.DataSource is DataTable table) return table;
            if (grid.DataSource is DataView dv) return dv.Table;
            if (grid.DataSource is BindingSource bs)
            {
                if (bs.DataSource is DataTable bdt) return bdt;
                if (bs.DataSource is DataView bdv) return bdv.Table;
                if (bs.DataSource is DataSet ds && bs.DataMember != null && ds.Tables.Contains(bs.DataMember)) return ds.Tables[bs.DataMember];
            }

            // last resort: if grid.DataSource is a DataView-like object
            try
            {
                if (grid.DataSource != null && grid.DataSource.GetType().Name.Contains("DataView"))
                {
                    var dvs = grid.DataSource as DataView;
                    return dvs?.Table;
                }
            }
            catch { }

            return null;
        }

        private void Check_Click(object sender, EventArgs e)
        {
            if (ServicecomboBox.SelectedItem is not ComboBoxItem selectedService)
            {
                MessageBox.Show("Please select a service first.");
                return;
            }

            if (appointmenrDatePicker == null || TimeofAppointmentcomboBox == null)
            {
                MessageBox.Show("Appointment controls are not ready.");
                return;
            }

            if (TimeofAppointmentcomboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a time slot.");
                return;
            }

            DateTime selectedDate = appointmenrDatePicker.Value.Date;
            string selectedTime = TimeofAppointmentcomboBox.SelectedItem.ToString();

            MessageBox.Show($"Checking availability for {selectedDate:yyyy-MM-dd} at {selectedTime}");

            LoadAvailableDoctors(selectedService.Value.ToString(), selectedDate, selectedTime);
        }
        private void LoadAvailableDoctors(string serviceId, DateTime selectedDate, string selectedTime)
        {
            try
            {
                con.Open();

                string query = @"
                SELECT DISTINCT d.doctor_id, d.full_name
                FROM Doctors d
                INNER JOIN DoctorServices ds ON d.doctor_id = ds.doctor_id
                INNER JOIN Availability a ON d.doctor_id = a.doctor_id
                WHERE ds.service_id = @serviceId
                  AND a.date = @date
                  AND a.hour_slot = @hourSlot
                  AND a.status = 'Available'
                  AND a.is_blocked = 0";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@serviceId", serviceId);
                    cmd.Parameters.AddWithValue("@date", selectedDate);
                    cmd.Parameters.AddWithValue("@hourSlot", TimeSpan.Parse(selectedTime));

                    SqlDataReader reader = cmd.ExecuteReader();
                    AvailableDoctorsComboBox.Items.Clear();

                    while (reader.Read())
                    {
                        AvailableDoctorsComboBox.Items.Add(new DoctorItem
                        {
                            DoctorId = Convert.ToInt32(reader["doctor_id"]),
                            FullName = reader["full_name"].ToString()
                        });
                    }

                    if (AvailableDoctorsComboBox.Items.Count == 0)
                    {
                        MessageBox.Show("No doctors are available for the selected service, date, and time.");
                    }
                    else
                    {
                        AvailableDoctorsComboBox.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading available doctors: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        // Toggle visibility (masking) for the primary password textbox
        private void checkBoxShow_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (boxPassword == null) return;

                // Typical UX: checkbox labelled "Show password". When checked -> show plaintext; unchecked -> mask.
                boxPassword.UseSystemPasswordChar = !checkBoxShow.Checked;

                // Ensure single-line so UseSystemPasswordChar works as expected
                boxPassword.Multiline = false;
            }
            catch
            {
                // ignore UI toggle failures
            }
        }

        // Toggle visibility (masking) for the re-enter password textbox
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ReEnterPassword_txtbx == null) return;

                ReEnterPassword_txtbx.UseSystemPasswordChar = !checkBox1.Checked;
                ReEnterPassword_txtbx.Multiline = false;
            }
            catch
            {
                // ignore UI toggle failures
            }
        }
    }
}
