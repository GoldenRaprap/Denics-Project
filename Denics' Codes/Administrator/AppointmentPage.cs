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
using Microsoft.VisualBasic;
namespace Denics.Administrator
{
    public partial class AppointmentPage : Form
    {
        // Database connection
        static CallDatabase db = new CallDatabase();
        SqlConnection con = new SqlConnection(db.getDatabaseStringName());
        SqlCommand cmd;

        // Undo tracking variables
        private int? selectedAppointmentId = null;

        public AppointmentPage()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // SideBar Click Events
            ReportButton.Click += ReportButton_Click;
            HomeButton.Click += HomeButton_Click;
            ServicesButton.Click += ServicesButton_Click;
            AvailabilityButton.Click += AvailabilityButton_Click;
            AppointmentButton.Click += AppointmentButton_Click;
            PatientButton.Click += PatientButton_Click;
            DoctorButton.Click += DoctorButton_Click;

            ViewAll_tbl.CellClick += AnyAppointmentTable_CellClick;
            Pending_tbl.CellClick += AnyAppointmentTable_CellClick;
            Reschedule_tbl.CellClick += AnyAppointmentTable_CellClick;
            Approve_tbl.CellClick += AnyAppointmentTable_CellClick;
            NoShow_tbl.CellClick += AnyAppointmentTable_CellClick;

            Search_txtbx.TextChanged += Search_txtbx_TextChanged;

            // ensure TabView name matches your designer TabControl
            TabView_AppointmentTable.SelectedIndexChanged += TabView_AppointmentTable_SelectedIndexChanged;
            UpdateButtonsForCurrentTab();
        }

        private void AppointmentPage_Load(object sender, EventArgs e)
        {
            try
            {

                // Loading the overall appointment table when the form loads
                Load_ViewAllAppointments();
                Load_PendingAppointments();
                Load_RescheduleAppointments();
                Load_ApproveAppointments();
                Load_NoShowAppointments();

                // Make text fields read-only
                MakeFieldsReadOnly();

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
        // Cell click handlers
        // 

        // When Clicking a row in any appointment table, it will remember wich one will be selected
        private void AnyAppointmentTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // If they selected the header, it will return
            var grid = sender as DataGridView; // I will get the name of the table grid from the selected table
            if (grid == null) return; // Check if the grid is empty
            LoadAppointmentFromGrid(grid, e.RowIndex); // Load the appointment from the selected grid row
        }

        // Load appointment details from the table to the text boxes
        private void LoadAppointmentFromGrid(DataGridView grid, int rowIndex)
        {
            if (grid == null) return; // Double check if the grid is empty
            if (rowIndex < 0 || rowIndex >= grid.Rows.Count) return; // Check if the row index is valid

            var row = grid.Rows[rowIndex]; // Get the whole selected row to turn into a variable

            // Store appointment id in the private field
            selectedAppointmentId = null; // Empty the variable first
            var idCell = row.Cells["appointment_id"]?.Value; // Get the appointment id  
            if (idCell != null && int.TryParse(idCell.ToString(), out int id)) // Turn the selected cell id to the selected appointment id vairalble
                selectedAppointmentId = id;

            // Loads the rest of the text fields from the selected row
            Patienttxtbx.Text = row.Cells["Patient"].Value?.ToString() ?? string.Empty; 
            Doctortxtbx.Text = row.Cells["Doctor"].Value?.ToString() ?? string.Empty;
            Servicetxtbx.Text = row.Cells["Service"].Value?.ToString() ?? string.Empty;
            // Try to parse date into DateTimePicker, otherwise leave existing value
            var dateVal = row.Cells["Date"].Value?.ToString();
            if (DateTime.TryParse(dateVal, out DateTime parsedDate))
            {
                Datedtpicker.Value = parsedDate;
            }
            Timetxtbx.Text = row.Cells["Time"].Value?.ToString() ?? string.Empty;
            Statustxtbx.Text = row.Cells["Status"].Value?.ToString() ?? string.Empty;

            // Enable and disable disable Button Functionalities
            UpdateButtonsForCurrentTab();
        }

        //
        // Buttons
        // 

        // Approve button that save the appointment from pending/reschedule to confirmed
        // Sends Emails too to validate appointment status change
        private void Approvebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedAppointmentId == null) // Check if they have selected an appointment, if they click on a cell - it will update the selectedAppointmentId
                {
                    MessageBox.Show("Please select an appointment first.");
                    return;
                }

                // Confirm approval, MessageBox for Yes and No
                DialogResult result = MessageBox.Show("Are you sure you want to approve this appointment? You will also be sending an Email to the Patient.",
                    "Confirm Approval", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) return;

                con.Open();
                int appointment_id = selectedAppointmentId.Value; // Grabs the variable to turn selected id to this method
                string currentStatus = Statustxtbx.Text; // Grabs the text box status into a variable

                if (currentStatus == "pending" || currentStatus == "reschedule") // Double check if the status is pending/reschedule
                {
                    string query = "UPDATE Appointments SET status = 'confirmed', last_updated = GETDATE() WHERE appointment_id = @id"; // Trun new status to confirmed
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", appointment_id);
                        int rowsAffected = cmd.ExecuteNonQuery(); // Check if changes were made
                        if (rowsAffected > 0)
                        {
                            // Capture all values needed by the email when the tables are refreshed/cleared.
                            // This prevents background tasks from reading empty fields after RefreshAllAppointmentTables().
                            string capturedPatientName = Patienttxtbx.Text;
                            string capturedServiceType = Servicetxtbx.Text;
                            DateTime capturedApntDate = Datedtpicker.Value;
                            string capturedApntTime = Timetxtbx.Text;
                            string capturedDoctor = Doctortxtbx.Text;
                            string capturedApntNote = "Your Appointment has been approved. Please attend the alloted time period to not miss your appointment.";

                            con.Close();
                            UpdateLastUpdated(appointment_id);

                            // Refresh all appointment lists and reset selection/state
                            RefreshAllAppointmentTables();
                            MessageBox.Show("Appointment approved and confirmed!");

                            // Fetch patient email and send PDF
                            string patientEmail = null;
                            try
                            {
                                using var emailCon = new SqlConnection(db.getDatabaseStringName());
                                using var cmdEmail = new SqlCommand("SELECT u.email FROM Appointments a INNER JOIN Users u ON a.user_id = u.user_id WHERE a.appointment_id = @id", emailCon);
                                cmdEmail.Parameters.AddWithValue("@id", appointment_id);
                                emailCon.Open();
                                var obj = cmdEmail.ExecuteScalar();
                                patientEmail = obj?.ToString();
                            }
                            catch (Exception ex)
                            {
                                // Do not block approval on email lookup failure
                                MessageBox.Show("Approved but failed to retrieve patient email: " + ex.Message);
                                patientEmail = null;
                            }

                            if (!string.IsNullOrWhiteSpace(patientEmail))
                            {
                                // Fire-and-forget background send so UI remains responsive
                                Task.Run(() =>
                                {
                                    try // Catches if sending an email fails
                                    {
                                        // Use the captured variables (safe copy) so values don't disappear when UI is updated.
                                        AppointmentServices.SendApprovedPdf(recipientEmail: patientEmail,patientName: capturedPatientName,serviceType: capturedServiceType,apntDate: capturedApntDate,apntTime: capturedApntTime,doctor: capturedDoctor,apntNote: capturedApntNote
                                        );

                                        // notify user for successful confirmation
                                        this.Invoke(() => MessageBox.Show("Confirmation email (PDF) sent to patient."));
                                    }
                                    catch (Exception ex)
                                    {
                                        this.Invoke(() => MessageBox.Show("Failed to send confirmation email: " + ex.Message)); // Something went wrong
                                    }
                                });
                            } else { MessageBox.Show("Appointment approved but patient has no email on record."); }
                        } else { MessageBox.Show("Failed to approve appointment."); }
                    }
                } else { MessageBox.Show("Only pending or reschedule appointments can be approved."); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error approving appointment: " + ex.Message);
            }
            finally
            {
                try { if (con.State == ConnectionState.Open) con.Close(); } catch { }
            }
        }

        // Similar to approve button that save the appointment from pending/reschedule to cancelled
        private void CancellationBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedAppointmentId == null) // Checks if they have selected an appointment  
                {
                    MessageBox.Show("Please select an appointment first.");
                    return;
                }

                // Confirm denial
                DialogResult result = MessageBox.Show("Are you sure you want to deny (cancel) this appointment?", "Confirm Deny", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) return;

                // Ask for a reason (simple InputBox like OTPVerification style)
                string reason = Interaction.InputBox("Enter reason for denying this appointment:", "Deny Reason", "");
                if (string.IsNullOrWhiteSpace(reason)) // If they left it blank
                {
                    MessageBox.Show("Deny reason is required. Operation cancelled.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                con.Open();
                int appointment_id = selectedAppointmentId.Value;
                string currentStatus = Statustxtbx.Text;

                if (currentStatus == "pending" || currentStatus == "reschedule") // Double check if the status is pending/reschedule
                {
                    string capturedPatientName = Patienttxtbx.Text;
                    string capturedServiceType = Servicetxtbx.Text;
                    DateTime capturedApntDate = Datedtpicker.Value;
                    string capturedApntTime = Timetxtbx.Text;
                    string capturedDoctor = Doctortxtbx.Text;
                    string capturedReason = reason; // Use the input from the Inputbox

                    string query = "UPDATE Appointments SET status = 'cancelled', last_updated = GETDATE() WHERE appointment_id = @id"; // Turn new status to cancelled
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", appointment_id);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            con.Close();
                            UpdateLastUpdated(appointment_id);

                            // Refresh all appointment lists and clear selection state
                            RefreshAllAppointmentTables();
                            MessageBox.Show("Appointment denied and cancelled.");

                            // Send email notification
                            string patientEmail = null;
                            try
                            {
                                using var emailCon = new SqlConnection(db.getDatabaseStringName());
                                using var cmdEmail = new SqlCommand("SELECT u.email FROM Appointments a INNER JOIN Users u ON a.user_id = u.user_id WHERE a.appointment_id = @id", emailCon);
                                cmdEmail.Parameters.AddWithValue("@id", appointment_id);
                                emailCon.Open();
                                var obj = cmdEmail.ExecuteScalar();
                                patientEmail = obj?.ToString();
                            }
                            catch (Exception ex)
                            {
                                this.Invoke(() => MessageBox.Show("Denied but failed to retrieve patient email: " + ex.Message));
                                patientEmail = null;
                            }

                            if (!string.IsNullOrWhiteSpace(patientEmail))
                            {
                                // Use captured values inside background task
                                Task.Run(() =>
                                {
                                    try
                                    {
                                        AppointmentServices.SendDeniedEmail(
                                            patientEmail,
                                            capturedPatientName,
                                            capturedServiceType,
                                            capturedApntDate,
                                            capturedApntTime,
                                            capturedDoctor,
                                            capturedReason);
                                        this.Invoke(() => MessageBox.Show("Denied email (PDF) sent to patient."));
                                    }
                                    catch (Exception ex)
                                    {
                                        this.Invoke(() => MessageBox.Show("Failed to send denied email: " + ex.Message));
                                    }
                                });
                            }
                            else { MessageBox.Show("Appointment denied (no patient email found)."); }
                        } else { MessageBox.Show("Failed to deny appointment."); }
                    }
                } else { MessageBox.Show("Only pending or reschedule appointments can be denied/cancelled."); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error denying appointment: " + ex.Message);
            }
            finally
            {
                try { if (con.State == ConnectionState.Open) con.Close(); } catch { }
            }
        }

        // Similar to approve button that save the appointment from confirmed to completed
        private void Completebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedAppointmentId == null)
                {
                    MessageBox.Show("Please select an appointment first.");
                    return;
                }

                // Ask whether to send email notification for completion
                DialogResult emailResult = MessageBox.Show("Do you want to send a completion email to the patient?", "Send Email Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (emailResult == DialogResult.No) return;

                con.Open();
                int appointment_id = selectedAppointmentId.Value;
                string currentStatus = Statustxtbx.Text;

                if (currentStatus == "confirmed")
                {
                    string capturedPatientName = Patienttxtbx.Text;
                    string capturedServiceType = Servicetxtbx.Text;
                    DateTime capturedApntDate = Datedtpicker.Value;
                    string capturedApntTime = Timetxtbx.Text;
                    string capturedDoctor = Doctortxtbx.Text;
                    string capturedNote = "Thank you for Visiting Denics! It was a pleasure serving you. We hope to see you again soon!";

                    string query = "UPDATE Appointments SET status = 'completed', last_updated = GETDATE() WHERE appointment_id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", appointment_id);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            con.Close();
                            UpdateLastUpdated(appointment_id);

                            // Refresh all lists
                            RefreshAllAppointmentTables();

                            MessageBox.Show("Appointment marked as completed.");

                            if (emailResult == DialogResult.Yes)
                            {
                                string patientEmail = null;
                                try
                                {
                                    using var emailCon = new SqlConnection(db.getDatabaseStringName());
                                    using var cmdEmail = new SqlCommand("SELECT u.email FROM Appointments a INNER JOIN Users u ON a.user_id = u.user_id WHERE a.appointment_id = @id", emailCon);
                                    cmdEmail.Parameters.AddWithValue("@id", appointment_id);
                                    emailCon.Open();
                                    var obj = cmdEmail.ExecuteScalar();
                                    patientEmail = obj?.ToString();
                                }
                                catch (Exception ex)
                                {
                                    this.Invoke(() => MessageBox.Show("Completed but failed to retrieve patient email: " + ex.Message));
                                    patientEmail = null;
                                }

                                if (!string.IsNullOrWhiteSpace(patientEmail))
                                {
                                    Task.Run(() =>
                                    {
                                        try
                                        {
                                            AppointmentServices.SendCompletedEmail(
                                                patientEmail,
                                                capturedPatientName,
                                                capturedServiceType,
                                                capturedApntDate,
                                                capturedApntTime,
                                                capturedDoctor,
                                                capturedNote);
                                            this.Invoke(() => MessageBox.Show("Completion email (PDF) sent to patient."));
                                        }
                                        catch (Exception ex)
                                        {
                                            this.Invoke(() => MessageBox.Show("Failed to send completion email: " + ex.Message));
                                        }
                                    });
                                } else { MessageBox.Show("Appointment completed (no patient email found)."); }
                            }
                        } else { MessageBox.Show("Failed to mark appointment completed."); }
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
                try { if (con.State == ConnectionState.Open) con.Close(); } catch { }
            }
        }

        // Similar to approve button that save the appointment from confirmed to no-show
        private void NoShowbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedAppointmentId == null)
                {
                    MessageBox.Show("Please select an appointment first.");
                    return;
                }

                // Ask whether to send no-show email
                DialogResult emailResult = MessageBox.Show("Do you want to send a no-show notification to the patient?", "Send Email Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (emailResult == DialogResult.No) return;

                con.Open();
                int appointment_id = selectedAppointmentId.Value;
                string currentStatus = Statustxtbx.Text;

                if (currentStatus == "confirmed")
                {
                    // Capture UI values before any refresh/clear
                    string capturedPatientName = Patienttxtbx.Text;
                    string capturedServiceType = Servicetxtbx.Text;
                    DateTime capturedApntDate = Datedtpicker.Value;
                    string capturedApntTime = Timetxtbx.Text;
                    string capturedDoctor = Doctortxtbx.Text;
                    string capturedNote = capturedPatientName + ", you have missed the alloted time you have appointed.";

                    string query = "UPDATE Appointments SET status = 'no-show', last_updated = GETDATE() WHERE appointment_id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", appointment_id);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            con.Close();
                            UpdateLastUpdated(appointment_id);

                            // Refresh all lists
                            RefreshAllAppointmentTables();

                            MessageBox.Show("Appointment marked as no-show.");

                            if (emailResult == DialogResult.Yes)
                            {
                                string patientEmail = null;
                                try
                                {
                                    using var emailCon = new SqlConnection(db.getDatabaseStringName());
                                    using var cmdEmail = new SqlCommand("SELECT u.email FROM Appointments a INNER JOIN Users u ON a.user_id = u.user_id WHERE a.appointment_id = @id", emailCon);
                                    cmdEmail.Parameters.AddWithValue("@id", appointment_id);
                                    emailCon.Open();
                                    var obj = cmdEmail.ExecuteScalar();
                                    patientEmail = obj?.ToString();
                                }
                                catch (Exception ex)
                                {
                                    this.Invoke(() => MessageBox.Show("No-show updated but failed to retrieve patient email: " + ex.Message));
                                    patientEmail = null;
                                }

                                if (!string.IsNullOrWhiteSpace(patientEmail))
                                {
                                    Task.Run(() =>
                                    {
                                        try
                                        {
                                            // Use captured values
                                            AppointmentServices.SendNoShowEmail(
                                                patientEmail,
                                                capturedPatientName,
                                                capturedServiceType,
                                                capturedApntDate,
                                                capturedApntTime,
                                                capturedDoctor,
                                                capturedNote);
                                            this.Invoke(() => MessageBox.Show("No-show email (PDF) sent to patient."));
                                        }
                                        catch (Exception ex)
                                        {
                                            this.Invoke(() => MessageBox.Show("Failed to send no-show email: " + ex.Message));
                                        }
                                    });
                                } else {MessageBox.Show("Appointment marked as no-show (no patient email found)."); }
                            }
                        } else { MessageBox.Show("Failed to mark appointment no-show."); }
                    }
                } else {MessageBox.Show("Only confirmed appointments can be marked as no-show.");}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating no-show: " + ex.Message);
            }
            finally
            {
                try { if (con.State == ConnectionState.Open) con.Close(); } catch { }
            }
        }

        // Similar to approve button that save the appointment from confirmed status to cancelled
        private void Cancellation_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedAppointmentId == null)
                {
                    MessageBox.Show("Please select an appointment first.");
                    return;
                }

                DialogResult confirm = MessageBox.Show("Are you sure you want to cancel this appointment?", "Confirm Cancellation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.No) return;

                // reason input
                string reason = Interaction.InputBox("Enter reason for cancellation:", "Cancellation Reason", "");
                if (string.IsNullOrWhiteSpace(reason))
                {
                    MessageBox.Show("Cancellation reason is required. Operation cancelled.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Ask whether to send email
                DialogResult emailResult = MessageBox.Show("Do you want to send an email notification to the patient?", "Send Email Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (emailResult == DialogResult.No) return;

                con.Open();
                int appointment_id = selectedAppointmentId.Value;
                string currentStatus = Statustxtbx.Text;

                // allow cancellation for pending/reschedule/confirmed as appropriate
                // Capture UI values BEFORE refreshing/clearing UI
                string capturedPatientName = Patienttxtbx.Text;
                string capturedServiceType = Servicetxtbx.Text;
                DateTime capturedApntDate = Datedtpicker.Value;
                string capturedApntTime = Timetxtbx.Text;
                string capturedDoctor = Doctortxtbx.Text;
                string capturedReason = reason;

                string query = "UPDATE Appointments SET status = 'cancelled', last_updated = GETDATE() WHERE appointment_id = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", appointment_id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        con.Close();
                        UpdateLastUpdated(appointment_id);

                        // Refresh everything via central helper
                        RefreshAllAppointmentTables();

                        Statustxtbx.Text = "cancelled";
                        MessageBox.Show("Appointment cancelled.");

                        if (emailResult == DialogResult.Yes)
                        {
                            string patientEmail = null;
                            try
                            {
                                using var emailCon = new SqlConnection(db.getDatabaseStringName());
                                using var cmdEmail = new SqlCommand("SELECT u.email FROM Appointments a INNER JOIN Users u ON a.user_id = u.user_id WHERE a.appointment_id = @id", emailCon);
                                cmdEmail.Parameters.AddWithValue("@id", appointment_id);
                                emailCon.Open();
                                var obj = cmdEmail.ExecuteScalar();
                                patientEmail = obj?.ToString();
                            }
                            catch (Exception ex)
                            {
                                this.Invoke(() => MessageBox.Show("Cancelled but failed to retrieve patient email: " + ex.Message));
                                patientEmail = null;
                            }

                            if (!string.IsNullOrWhiteSpace(patientEmail))
                            {
                                Task.Run(() =>
                                {
                                    try
                                    {
                                        AppointmentServices.SendCancelledEmail(
                                            patientEmail,
                                            capturedPatientName,
                                            capturedServiceType,
                                            capturedApntDate,
                                            capturedApntTime,
                                            capturedDoctor,
                                            capturedReason);
                                        this.Invoke(() => MessageBox.Show("Cancellation email (PDF) sent to patient."));
                                    }
                                    catch (Exception ex)
                                    {
                                        this.Invoke(() => MessageBox.Show("Failed to send cancellation email: " + ex.Message));
                                    }
                                });
                            } else { MessageBox.Show("Appointment cancelled (no patient email found)."); }
                        }
                    } else { MessageBox.Show("Failed to cancel appointment."); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cancelling appointment: " + ex.Message);
            }
            finally
            {
                try { if (con.State == ConnectionState.Open) con.Close(); } catch { }
            }
        }


        //
        // Search functionality
        // 

        // Search for names and filters the datagridview to show only relevant results
        private void Search_txtbx_TextChanged(object sender, EventArgs e)
        {
            var text = Search_txtbx.Text?.Trim() ?? string.Empty;
            ApplySearchToAllTables(text);
        }

        // Apply the same search text to every appointment table
        private void ApplySearchToAllTables(string searchText)
        {
            // If empty, clear filters
            if (string.IsNullOrWhiteSpace(searchText))
            {
                ClearFilter(ViewAll_tbl);
                ClearFilter(Pending_tbl);
                ClearFilter(Reschedule_tbl);
                ClearFilter(Approve_tbl);
                ClearFilter(NoShow_tbl);
                return;
            }

            // Build DataTable filter expression (escape single quotes)
            var esc = searchText.Replace("'", "''");

            // Wrap column names in brackets and convert non-string columns to string for matching.
            var filter = $"(CONVERT([Patient], 'System.String') LIKE '%{esc}%' OR CONVERT([Doctor], 'System.String') LIKE '%{esc}%' OR CONVERT([Service], 'System.String') LIKE '%{esc}%' OR CONVERT([Status], 'System.String') LIKE '%{esc}%' OR CONVERT([Date], 'System.String') LIKE '%{esc}%' OR CONVERT([Time], 'System.String') LIKE '%{esc}%')";

            ApplyFilterToGrid(ViewAll_tbl, filter);
            ApplyFilterToGrid(Pending_tbl, filter);
            ApplyFilterToGrid(Reschedule_tbl, filter);
            ApplyFilterToGrid(Approve_tbl, filter);
            ApplyFilterToGrid(NoShow_tbl, filter);
        }

        // Apply a DataTable RowFilter to the grid's underlying DataTable or DataView
        private void ApplyFilterToGrid(DataGridView grid, string rowFilter)
        {
            var dt = GetDataTableFromGrid(grid);
            if (dt == null) return; 
            try
            {
                // Apply filter to the DefaultView
                dt.DefaultView.RowFilter = rowFilter;

                // Ensure the grid shows the filtered view. Preserve BindingSource if present.
                if (grid.DataSource is BindingSource bs)
                {
                    bs.DataSource = dt.DefaultView;
                    grid.DataSource = bs;
                } else { grid.DataSource = dt.DefaultView; }
            }
            catch (Exception)
            {
                // If the filter fails (unexpected schema), clear filter as fallback
                try { dt.DefaultView.RowFilter = string.Empty; } catch { }
            }
        }

        // Clear filter on the grid
        private void ClearFilter(DataGridView grid)
        {
            var dt = GetDataTableFromGrid(grid);
            if (dt == null) return;
            dt.DefaultView.RowFilter = string.Empty;

            // Restore grid datasource to DataTable.DefaultView to ensure UI reflects the cleared filter
            if (grid.DataSource is BindingSource bs)
            {
                bs.DataSource = dt.DefaultView;
                grid.DataSource = bs;
            }
            else
            {
                grid.DataSource = dt.DefaultView;
            }
        }

        // Safely obtain a DataTable backing the DataGridView's DataSource
        private DataTable GetDataTableFromGrid(DataGridView grid)
        {
            if (grid == null) return null; // If empty

            // Direct DataTable
            if (grid.DataSource is DataTable table) return table;

            // DataView
            if (grid.DataSource is DataView dv) return dv.Table;

            // BindingSource wrapping DataTable/DataView
            if (grid.DataSource is BindingSource bs)
            {
                if (bs.DataSource is DataTable bdt) return bdt;
                if (bs.DataSource is DataView bdv) return bdv.Table;
                if (bs.DataSource is DataSet ds && bs.DataMember != null && ds.Tables.Contains(bs.DataMember)) return ds.Tables[bs.DataMember];
            }

            // If the grid was assigned a DataTable via DataSource = someDataTable.DefaultView
            if (grid.DataSource is object && grid.DataSource.GetType().Name.Contains("DataView"))
            {
                try
                {
                    var dvs = grid.DataSource as DataView;
                    return dvs?.Table;
                }
                catch { }
            }

            return null;
        }

        //
        // Loading appointment data
        //

        // Load all appointments
        private void Load_ViewAllAppointments()
        {
            try
            {
                con.Open();
                // Making a table for appointments with proper naming conventions
                string query = @"
                    SELECT 
                        a.appointment_id,
                        a.status AS Status,
                        u.firstname + ' ' + u.surname AS Patient,
                        d.full_name AS Doctor,
                        s.service_name AS Service,
                        a.appointment_date AS Date,
                        a.appointment_time AS Time
                    FROM Appointments a
                    INNER JOIN Users u ON a.user_id = u.user_id
                    INNER JOIN Doctors d ON a.doctor_id = d.doctor_id
                    INNER JOIN Services s ON a.service_id = s.service_id
                    ORDER BY a.last_updated DESC";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ViewAll_tbl.DataSource = dt;

                // Hide appointment_id column
                if (ViewAll_tbl.Columns.Contains("appointment_id"))
                    ViewAll_tbl.Columns["appointment_id"].Visible = false;

                ViewAll_tbl.ClearSelection();
                selectedAppointmentId = null;
                Patienttxtbx.Text = "";
                Doctortxtbx.Text = "";
                Servicetxtbx.Text = "";
                Timetxtbx.Text = "";
                Statustxtbx.Text = "";
            } catch (SqlException ex) {MessageBox.Show("SQL Error: " + ex.Message, "Database Error"); 
            } catch (Exception ex) { MessageBox.Show("General Error: " + ex.Message, "Error");
            } finally { con.Close(); }
        }

        // Load only pending appointments
        private void Load_PendingAppointments()
        {
            try
            {
                con.Open();
                // Making a table for appointments with proper naming conventions
                string query = @"
                    SELECT 
                        a.appointment_id,
                        a.status AS Status,
                        u.firstname + ' ' + u.surname AS Patient,
                        d.full_name AS Doctor,
                        s.service_name AS Service,
                        a.appointment_date AS Date,
                        a.appointment_time AS Time
                    FROM Appointments a
                    INNER JOIN Users u ON a.user_id = u.user_id
                    INNER JOIN Doctors d ON a.doctor_id = d.doctor_id
                    INNER JOIN Services s ON a.service_id = s.service_id
                    WHERE a.status IN ('Pending')
                    ORDER BY a.last_updated DESC, a.date_created DESC";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Pending_tbl.DataSource = dt;

                // Hide appointment_id and status columns
                if (Pending_tbl.Columns.Contains("appointment_id"))
                    Pending_tbl.Columns["appointment_id"].Visible = false;
                if (Pending_tbl.Columns.Contains("status"))
                    Pending_tbl.Columns["status"].Visible = false;

            } catch (SqlException ex) { MessageBox.Show("SQL Error: " + ex.Message, "Database Error");
            } catch (Exception ex) { MessageBox.Show("General Error: " + ex.Message, "Error");
            } finally { con.Close(); }
        }

        // Load only pending appointments
        private void Load_RescheduleAppointments()
        {
            try
            {
                con.Open();
                // Making a table for appointments with proper naming conventions
                string query = @"
                    SELECT 
                        a.appointment_id,
                        a.status AS Status,
                        u.firstname + ' ' + u.surname AS Patient,
                        d.full_name AS Doctor,
                        s.service_name AS Service,
                        a.appointment_date AS Date,
                        a.appointment_time AS Time
                    FROM Appointments a
                    INNER JOIN Users u ON a.user_id = u.user_id
                    INNER JOIN Doctors d ON a.doctor_id = d.doctor_id
                    INNER JOIN Services s ON a.service_id = s.service_id
                    WHERE a.status IN ('Reschedule')
                    ORDER BY a.last_updated DESC, a.date_created DESC";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Reschedule_tbl.DataSource = dt;

                // Hide appointment_id column
                if (Reschedule_tbl.Columns.Contains("appointment_id"))
                    Reschedule_tbl.Columns["appointment_id"].Visible = false;
                if (Pending_tbl.Columns.Contains("status"))
                    Pending_tbl.Columns["status"].Visible = false;

            } catch (SqlException ex) { MessageBox.Show("SQL Error: " + ex.Message, "Database Error");
            } catch (Exception ex) { MessageBox.Show("General Error: " + ex.Message, "Error");
            } finally { con.Close(); }
        }

        // Load only confirmed appointments
        private void Load_ApproveAppointments()
        {
            try
            {
                con.Open();
                // Making a table for appointments with proper naming conventions
                string query = @"
                    SELECT 
                        a.appointment_id,
                        a.status AS Status,
                        u.firstname + ' ' + u.surname AS Patient,
                        d.full_name AS Doctor,
                        s.service_name AS Service,
                        a.appointment_date AS Date,
                        a.appointment_time AS Time
                    FROM Appointments a
                    INNER JOIN Users u ON a.user_id = u.user_id
                    INNER JOIN Doctors d ON a.doctor_id = d.doctor_id
                    INNER JOIN Services s ON a.service_id = s.service_id
                    WHERE a.status = 'confirmed'
                    ORDER BY a.last_updated DESC, a.date_created DESC";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Approve_tbl.DataSource = dt;

                // Hide appointment_id column
                if (Approve_tbl.Columns.Contains("appointment_id"))
                    Approve_tbl.Columns["appointment_id"].Visible = false;
                if (Pending_tbl.Columns.Contains("status"))
                    Pending_tbl.Columns["status"].Visible = false;

            } catch (SqlException ex) { MessageBox.Show("SQL Error: " + ex.Message, "Database Error");
            } catch (Exception ex) { MessageBox.Show("General Error: " + ex.Message, "Error");
            } finally { con.Close(); }
        }

        // Load only no-show appointments
        private void Load_NoShowAppointments()
        {
            try
            {
                con.Open();
                // Making a table for appointments with proper naming conventions
                string query = @"
                    SELECT 
                        a.appointment_id,
                        a.status AS Status,
                        u.firstname + ' ' + u.surname AS Patient,
                        d.full_name AS Doctor,
                        s.service_name AS Service,
                        a.appointment_date AS Date,
                        a.appointment_time AS Time
                    FROM Appointments a
                    INNER JOIN Users u ON a.user_id = u.user_id
                    INNER JOIN Doctors d ON a.doctor_id = d.doctor_id
                    INNER JOIN Services s ON a.service_id = s.service_id
                    WHERE a.status = 'no-show'
                    ORDER BY a.last_updated DESC, a.date_created DESC";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                NoShow_tbl.DataSource = dt;

                // Hide appointment_id column
                if (NoShow_tbl.Columns.Contains("appointment_id"))
                    NoShow_tbl.Columns["appointment_id"].Visible = false;
                if (Pending_tbl.Columns.Contains("status"))
                    Pending_tbl.Columns["status"].Visible = false;

            } catch (SqlException ex) { MessageBox.Show("SQL Error: " + ex.Message, "Database Error");
            } catch (Exception ex) { MessageBox.Show("General Error: " + ex.Message, "Error");
            } finally { con.Close(); }
        }

        //
        // Helper Methods
        // 

        // Refresh all appointment grids and reset selection/state, mostly when an update or button was pressed
        private void RefreshAllAppointmentTables()
        {
            try
            {
                // Call each loader so that every DataGridView is bound to latest data.
                Load_ViewAllAppointments();
                Load_PendingAppointments();
                Load_RescheduleAppointments();
                Load_ApproveAppointments();
                Load_NoShowAppointments();

                // Clear selections and reset selectedAppointmentId and input fields
                ViewAll_tbl.ClearSelection();
                Pending_tbl.ClearSelection();
                Reschedule_tbl.ClearSelection();
                Approve_tbl.ClearSelection();
                NoShow_tbl.ClearSelection();

                selectedAppointmentId = null;
                Patienttxtbx.Text = string.Empty;
                Doctortxtbx.Text = string.Empty;
                Servicetxtbx.Text = string.Empty;
                Timetxtbx.Text = string.Empty;
                Statustxtbx.Text = string.Empty;

                // Ensure buttons reflect the new state
                UpdateButtonsForCurrentTab();
            }
            catch (Exception ex)
            {
                // Fail-safe: show error but do not throw
                MessageBox.Show("Error refreshing appointment tables: " + ex.Message, "Refresh Error");
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

        // Make text fields read-only
        private void MakeFieldsReadOnly()
        {
            Patienttxtbx.ReadOnly = true;
            Doctortxtbx.ReadOnly = true;
            Servicetxtbx.ReadOnly = true;
            Timetxtbx.ReadOnly = true;
            Statustxtbx.ReadOnly = true;

            Datedtpicker.Enabled = false;
            Approvebtn.Enabled = false;
            Deny_btn.Enabled = false;
            Completebtn.Enabled = false;
            NoShowbtn.Enabled = false;
            Cancellation_btn.Enabled = false;
        }

        // Check which tab is selected and enable/disable buttons accordingly
        private void TabView_AppointmentTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonsForCurrentTab();
        }

        // Enable and diable buttons based on which tab and index they selected
        private void UpdateButtonsForCurrentTab()
        {
            // If they are in View All tab, it will diable all button and check the status of the appointment to enable the buttons accordingly
            if (TabView_AppointmentTable == null) return;

            // Grab the currently selected info from textboxes and selectedAppointmentId
            bool hasSelection = selectedAppointmentId != null;
            var statusText = Statustxtbx.Text?.Trim() ?? string.Empty;
            // Checks if current status is pending or reschedule
            bool isPendingOrReschedule = string.Equals(statusText, "pending", StringComparison.OrdinalIgnoreCase) || string.Equals(statusText, "reschedule", StringComparison.OrdinalIgnoreCase);
            // Checks if current status is confirmed
            bool isConfirmed = string.Equals(statusText, "confirmed", StringComparison.OrdinalIgnoreCase);

            // Default computed availability
            bool allowApproveOrCancel = hasSelection && isPendingOrReschedule;
            bool allowCompleteOrNoShow = hasSelection && isConfirmed;

            // Default behavior for all tabs 
            Approvebtn.Enabled = allowApproveOrCancel;
            Deny_btn.Enabled = allowApproveOrCancel;
            Completebtn.Enabled = allowCompleteOrNoShow;
            NoShowbtn.Enabled = allowCompleteOrNoShow;
            Cancellation_btn.Enabled = allowCompleteOrNoShow;
        }

        //
        // Navigation Buttons
        // 
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

        private void Add_Appointment_btn_Click(object sender, EventArgs e)
        {
            AddAppoinment addAppointment = new AddAppoinment();
            addAppointment.Show();
            this.Hide();
        }

    }
}