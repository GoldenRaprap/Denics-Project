using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Denics.Administrator
{
    public partial class AppointmentPage : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Denics Project\\Denics' Database\\Denics_db.mdf\";Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd;

        public AppointmentPage()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void AppointmentPage_Load(object sender, EventArgs e)
        {
            // Loading the overall appointment table when the form loads
            Load_OverallAppointmentTable();

            // Make text fields read-only
            MakeFieldsReadOnly();

            // Load the saved state of the Automation checkbox
            Automation_checkbox.Checked = Properties.Settings.Default.AutomationEnabled;

            // Run automation if enabled
            if (Automation_checkbox.Checked)
            {
                RunAutomation();
            }
        }

        private void MakeFieldsReadOnly()
        {
            AppointmentIDtxtbx.ReadOnly = true;
            Patienttxtbx.ReadOnly = true;
            Doctortxtbx.ReadOnly = true;
            Servicetxtbx.ReadOnly = true;
            Timetxtbx.ReadOnly = true;
            Statustxtbx.ReadOnly = true;

            // DateTimePicker doesn't have ReadOnly, so disable input
            Datedtpicker.Enabled = false;
        }


        private void OverallAppointmentTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the click is on a valid row (not header)
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = OverallAppointmentTable.Rows[e.RowIndex];

                AppointmentIDtxtbx.Text = row.Cells["appointment_id"].Value?.ToString();
                Patienttxtbx.Text = row.Cells["patient_name"].Value?.ToString();
                Doctortxtbx.Text = row.Cells["doctor_name"].Value?.ToString();
                Servicetxtbx.Text = row.Cells["service_name"].Value?.ToString();
                Datedtpicker.Text = row.Cells["appointment_date"].Value?.ToString();
                Timetxtbx.Text = row.Cells["appointment_time"].Value?.ToString();
                Statustxtbx.Text = row.Cells["status"].Value?.ToString();
            }
        }

        private void Load_OverallAppointmentTable()
        {
            try
            {
                con.Open();
                // Making a table for appointments with proper naming conventions
                string query = @"
                    SELECT 
                        a.appointment_id,
                        a.status,
                        u.firstname + ' ' + u.surname AS patient_name,
                        d.full_name AS doctor_name,
                        s.service_name,
                        a.appointment_date,
                        a.appointment_time
                    FROM Appointments a
                    INNER JOIN Users u ON a.user_id = u.user_id
                    INNER JOIN Doctors d ON a.doctor_id = d.doctor_id
                    INNER JOIN Services s ON a.service_id = s.service_id
                    ORDER BY a.appointment_date, a.appointment_time";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                OverallAppointmentTable.DataSource = dt;
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

        private void Load_PendingAppointments()
        {
            try
            {
                con.Open();
                // Making a table for appointments with proper naming conventions
                string query = @"
                    SELECT 
                        a.appointment_id,
                        a.status,
                        u.firstname + ' ' + u.surname AS patient_name,
                        d.full_name AS doctor_name,
                        s.service_name,
                        a.appointment_date,
                        a.appointment_time
                    FROM Appointments a
                    INNER JOIN Users u ON a.user_id = u.user_id
                    INNER JOIN Doctors d ON a.doctor_id = d.doctor_id
                    INNER JOIN Services s ON a.service_id = s.service_id
                    WHERE a.status IN ('Pending', 'Reschedule')
                    ORDER BY a.appointment_date, a.appointment_time";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                OverallAppointmentTable.DataSource = dt;
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

        private void ViewApprovalbtn_Click(object sender, EventArgs e)
        {
            Load_PendingAppointments();
        }

        private void Refreshbtn_Click(object sender, EventArgs e)
        {
            Load_OverallAppointmentTable();
        }

        private void Approvebtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AppointmentIDtxtbx.Text))
            {
                MessageBox.Show("Please select an appointment first.");
                return;
            }

            try
            {
                con.Open();

                // Step 1: Check the current status of the appointment
                string statusQuery = "SELECT status FROM Appointments WHERE appointment_id = @appointment_id";
                SqlCommand statusCmd = new SqlCommand(statusQuery, con);
                statusCmd.Parameters.AddWithValue("@appointment_id", int.Parse(AppointmentIDtxtbx.Text));
                object result = statusCmd.ExecuteScalar();

                if (result == null)
                {
                    MessageBox.Show("Appointment not found.");
                    return;
                }

                string currentStatus = result.ToString();

                if (currentStatus == "completed" || currentStatus == "confirmed" || currentStatus == "no-show" || currentStatus == "cancelled")
                {
                    MessageBox.Show("This appointment cannot be updated because it is already " + currentStatus + ".");
                    return;
                }

                // Step 2: Check daily capacity (max 15 appointments per day)
                string countQuery = @"
                    SELECT COUNT(*) 
                    FROM Appointments 
                    WHERE appointment_date = @date 
                    AND status IN ('Pending', 'Reschedule')";

                SqlCommand countCmd = new SqlCommand(countQuery, con);
                countCmd.Parameters.AddWithValue("@date", Datedtpicker.Value.Date);
                int appointmentCount = (int)countCmd.ExecuteScalar();

                if (appointmentCount >= 15)
                {
                    MessageBox.Show("The selected date already has 15 appointments. Please choose another date.");
                    return;
                }

                // Step 3: Update status to Confirmed (only if Pending/Reschedule)
                string updateQuery = @"
                    UPDATE Appointments 
                    SET status = 'confirmed' 
                    WHERE appointment_id = @appointment_id 
                    AND status IN ('Pending', 'Reschedule')";

                SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                updateCmd.Parameters.AddWithValue("@appointment_id", int.Parse(AppointmentIDtxtbx.Text));
                int rowsAffected = updateCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    con.Close();
                    MessageBox.Show("Appointment approved and confirmed!");
                    Load_OverallAppointmentTable(); // refresh the table
                    Statustxtbx.Text = "confirmed";
                }
                else
                {
                    MessageBox.Show("This appointment cannot be approved because it's not in a Pending or Reschedule state.");
                }
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

        private void CancellationBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AppointmentIDtxtbx.Text))
            {
                MessageBox.Show("Please select an appointment to cancel.");
                return;
            }

            // Ask for confirmation
            DialogResult confirmResult = MessageBox.Show(
                "Are you sure you want to cancel this appointment?",
                "Confirm Cancellation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult != DialogResult.Yes)
            {
                return;
            }

            try
            {
                con.Open();

                // Step 1: Check the current status
                string statusQuery = "SELECT status FROM Appointments WHERE appointment_id = @appointment_id";
                SqlCommand statusCmd = new SqlCommand(statusQuery, con);
                statusCmd.Parameters.AddWithValue("@appointment_id", int.Parse(AppointmentIDtxtbx.Text));
                object result = statusCmd.ExecuteScalar();

                if (result == null)
                {
                    MessageBox.Show("Appointment not found.");
                    return;
                }

                string currentStatus = result.ToString();

                if (currentStatus != "pending" && currentStatus != "reschedule")
                {
                    MessageBox.Show("Only Pending or Rescheduled appointments can be cancelled. Current status: " + currentStatus);
                    return;
                }

                // Step 2: Update status to Cancelled
                string updateQuery = @"
                    UPDATE Appointments 
                    SET status = 'cancelled' 
                    WHERE appointment_id = @appointment_id";

                SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                updateCmd.Parameters.AddWithValue("@appointment_id", int.Parse(AppointmentIDtxtbx.Text));
                int rowsAffected = updateCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    con.Close();
                    MessageBox.Show("Appointment cancelled successfully.");
                    Load_OverallAppointmentTable();
                    Statustxtbx.Text = "cancelled";
                }
                else
                {
                    MessageBox.Show("Cancellation failed. Please try again.");
                }
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
        private void SaveAutomationbtn_Click(object sender, EventArgs e)
        {
            // Save checkbox state to user settings
            Properties.Settings.Default.AutomationEnabled = Automation_checkbox.Checked;
            Properties.Settings.Default.Save();

            MessageBox.Show("Automation setting saved successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (Automation_checkbox.Checked)
            {
                RunAutomation();
            }
        }

        private void RunAutomation()
        {
            try
            {
                con.Open();

                // Get all pending/reschedule appointments
                string selectQuery = @"
                    SELECT appointment_id, appointment_date
                    FROM Appointments
                    WHERE status IN ('Pending', 'Reschedule');";

                SqlCommand selectCmd = new SqlCommand(selectQuery, con);
                SqlDataReader reader = selectCmd.ExecuteReader();

                List<(int id, DateTime date)> appointments = new List<(int, DateTime)>();

                while (reader.Read())
                {
                    appointments.Add((Convert.ToInt32(reader["appointment_id"]), Convert.ToDateTime(reader["appointment_date"])));
                }
                reader.Close();

                foreach (var appt in appointments)
                {
                    // Count existing confirmed/complete for that day
                    string countQuery = @"
                        SELECT COUNT(*) 
                        FROM Appointments
                        WHERE appointment_date = @date
                        AND status IN ('Confirmed', 'Complete');";

                    SqlCommand countCmd = new SqlCommand(countQuery, con);
                    countCmd.Parameters.AddWithValue("@date", appt.date);
                    int count = (int)countCmd.ExecuteScalar();

                    string updateQuery;
                    if (count < 15)
                    {
                        // Approve
                        updateQuery = "UPDATE Appointments SET status = 'confirmed' WHERE appointment_id = @id";
                    }
                    else
                    {
                        // Cancel
                        updateQuery = "UPDATE Appointments SET status = 'cancelled' WHERE appointment_id = @id";
                    }

                    SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                    updateCmd.Parameters.AddWithValue("@id", appt.id);
                    updateCmd.ExecuteNonQuery();
                }

                con.Close();
                MessageBox.Show("Automation process completed.", "Automation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh table after automation
                Load_OverallAppointmentTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Automation error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }














        private void button1_Click(object sender, EventArgs e)
        {
            MainAdminPage mainAdminPage = new MainAdminPage();
            mainAdminPage.Show();
            this.Hide();
        }

    }
}