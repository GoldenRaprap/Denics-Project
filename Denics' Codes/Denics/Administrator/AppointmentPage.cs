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
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Denics Project\\Denics' Database\\Denics_db.mdf\";Integrated Security=True;Connect Timeout=30;");
        SqlCommand cmd;

        // Undo tracking variables
        private int? lastAppointmentId = null;
        private string lastOldStatus = null;
        private string lastNewStatus = null;

        public AppointmentPage()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void AppointmentPage_Load(object sender, EventArgs e)
        {
            try
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



        //
        // Buttons
        //

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


        private void ViewApprovalbtn_Click(object sender, EventArgs e)
        {
            Load_PendingAppointments();
        }

        private void Refreshbtn_Click(object sender, EventArgs e)
        {
            Load_OverallAppointmentTable();
        }
        private void ViewCompletionbtn_Click(object sender, EventArgs e)
        {
            Load_ConfirmedAppointments();
        }

        private void Approvebtn_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                int appointment_id = Convert.ToInt32(AppointmentIDtxtbx.Text);
                string currentStatus = Statustxtbx.Text;

                if (currentStatus == "pending" || currentStatus == "reschedule")
                {
                    string query = "UPDATE Appointments SET status = 'confirmed', last_updated = GETDATE() WHERE appointment_id = @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", appointment_id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        con.Close();
                        UpdateLastUpdated(appointment_id);
                        LogLastAction(appointment_id, currentStatus, "confirmed"); // log for undo
                        MessageBox.Show("Appointment approved and confirmed!");
                        Load_OverallAppointmentTable();
                        Statustxtbx.Text = "confirmed";
                        con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Only pending or reschedule appointments can be approved.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error approving appointment: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void CancellationBtn_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure you want to cancel this appointment?", "Confirm Cancellation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    con.Open();
                    int appointment_id = Convert.ToInt32(AppointmentIDtxtbx.Text);
                    string currentStatus = Statustxtbx.Text;

                    if (currentStatus == "pending" || currentStatus == "reschedule")
                    {
                        string query = "UPDATE Appointments SET status = 'cancelled', last_updated = GETDATE() WHERE appointment_id = @id";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@id", appointment_id);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            con.Close();
                            UpdateLastUpdated(appointment_id);
                            LogLastAction(appointment_id, currentStatus, "cancelled"); // log for undo
                            MessageBox.Show("Appointment cancelled.");
                            Load_OverallAppointmentTable();
                            Statustxtbx.Text = "cancelled";
                            con.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Only pending or reschedule appointments can be cancelled.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cancelling appointment: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void Completebtn_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                int appointment_id = Convert.ToInt32(AppointmentIDtxtbx.Text);
                string currentStatus = Statustxtbx.Text;

                if (currentStatus == "confirmed")
                {
                    string query = "UPDATE Appointments SET status = 'completed', last_updated = GETDATE() WHERE appointment_id = @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", appointment_id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        con.Close();
                        UpdateLastUpdated(appointment_id);
                        LogLastAction(appointment_id, currentStatus, "completed"); // log for undo
                        MessageBox.Show("Appointment marked as completed.");
                        Load_OverallAppointmentTable();
                        Statustxtbx.Text = "completed";
                        con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Only confirmed appointments can be marked as completed.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error completing appointment: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        // Similar to approve button that save the appointment from confirmed to no-show
        private void NoShowbtn_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                int appointment_id = Convert.ToInt32(AppointmentIDtxtbx.Text);
                string currentStatus = Statustxtbx.Text;

                if (currentStatus == "confirmed")
                {
                    string query = "UPDATE Appointments SET status = 'no-show', last_updated = GETDATE() WHERE appointment_id = @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", appointment_id);
                    
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        con.Close();
                        UpdateLastUpdated(appointment_id);
                        LogLastAction(appointment_id, currentStatus, "no-show"); // log for undo
                        MessageBox.Show("Appointment marked as no-show.");
                        Load_OverallAppointmentTable();
                        Statustxtbx.Text = "no-show";
                    }
                    
                }
                else
                {
                    MessageBox.Show("Only confirmed appointments can be marked as no-show.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating no-show: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void Undobtn_Click(object sender, EventArgs e)
        {
            if (lastAppointmentId == null || lastOldStatus == null || lastNewStatus == null)
            {
                MessageBox.Show("No action to undo.");
                return;
            }

            try
            {
                con.Open();

                string undoQuery = "UPDATE Appointments SET status = @oldStatus, last_updated = GETDATE() WHERE appointment_id = @id AND status = @newStatus";
                SqlCommand undoCmd = new SqlCommand(undoQuery, con);
                undoCmd.Parameters.AddWithValue("@oldStatus", lastOldStatus);
                undoCmd.Parameters.AddWithValue("@newStatus", lastNewStatus);
                undoCmd.Parameters.AddWithValue("@id", lastAppointmentId.Value);

                int rowsAffected = undoCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    con.Close();
                    MessageBox.Show($"Undo successful. Appointment status reverted to {lastOldStatus}.");
                    Load_OverallAppointmentTable();

                    // Update textbox with reverted status
                    Statustxtbx.Text = lastOldStatus;

                    // Clear history
                    lastAppointmentId = null;
                    lastOldStatus = null;
                    lastNewStatus = null;
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Undo failed. The appointment may have already been modified again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during undo: " + ex.Message);
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


        //
        // Methods
        //

        // Load all appointments
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


        // Load only pending and reschedule appointments
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

        // Load only confirmed appointments
        private void Load_ConfirmedAppointments()
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
                    WHERE a.status = 'confirmed'
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


        // Update last_update on database
        private void UpdateLastUpdated(int appointment_id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE Appointments SET last_updated = SYSDATETIME() WHERE appointment_id = @id", con))
                {
                    cmd.Parameters.AddWithValue("@id", appointment_id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Error while updating last_updated: " + ex.Message, "Database Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show("General Error while updating last_updated: " + ex.Message, "Error");
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        // Automation process to approve/cancel pending appointments
        private void RunAutomation()
        {
            try
            {
                con.Open();

                string selectQuery = @"
                    SELECT appointment_id, appointment_date, status
                    FROM Appointments
                    WHERE status IN ('Pending', 'Reschedule');";

                SqlCommand selectCmd = new SqlCommand(selectQuery, con);
                SqlDataReader reader = selectCmd.ExecuteReader();

                List<(int id, DateTime date, string oldStatus)> appointments = new List<(int, DateTime, string)>();

                while (reader.Read())
                {
                    appointments.Add((
                        Convert.ToInt32(reader["appointment_id"]),
                        Convert.ToDateTime(reader["appointment_date"]),
                        reader["status"].ToString()
                    ));
                }
                reader.Close();

                foreach (var appt in appointments)
                {
                    string countQuery = @"
                        SELECT COUNT(*) 
                        FROM Appointments
                        WHERE appointment_date = @date
                        AND status IN ('Confirmed', 'Complete');";

                    SqlCommand countCmd = new SqlCommand(countQuery, con);
                    countCmd.Parameters.AddWithValue("@date", appt.date);
                    int count = (int)countCmd.ExecuteScalar();

                    string newStatus;
                    if (count < 15)
                        newStatus = "confirmed";
                    else
                        newStatus = "cancelled";

                    string updateQuery = "UPDATE Appointments SET status = @status, last_updated = GETDATE() WHERE appointment_id = @id";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                    updateCmd.Parameters.AddWithValue("@status", newStatus);
                    updateCmd.Parameters.AddWithValue("@id", appt.id);
                    updateCmd.ExecuteNonQuery();

                    LogLastAction(appt.id, appt.oldStatus, newStatus); // log for undo
                }

                con.Close();
                MessageBox.Show("Automation process completed.", "Automation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // Make text fields read-only
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

        // Log the last action for undo functionality
        private void LogLastAction(int appointmentId, string oldStatus, string newStatus)
        {
            lastAppointmentId = appointmentId;
            lastOldStatus = oldStatus;
            lastNewStatus = newStatus;
        }










        //
        // Trash
        //


        private void button1_Click(object sender, EventArgs e)
        {
            MainAdminPage mainAdminPage = new MainAdminPage();
            mainAdminPage.Show();
            this.Hide();
        }




    }
}
