using System;
using System.CodeDom;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Denics.Administrator
{
    public partial class DoctorPage : Form
    {
        // Database connection
        static CallDatabase db = new CallDatabase();
        SqlConnection con = new SqlConnection(db.getDatabaseStringName());
        SqlCommand cmd; 

        public DoctorPage()
        {
            InitializeComponent();
            this.Load += DoctorPageForm1_Load; // Ensure the event is hooked up
            this.StartPosition = FormStartPosition.CenterScreen;
            // SideBar Click Events
            ReportButton.Click += ReportButton_Click;
            HomeButton.Click += HomeButton_Click;
            ServicesButton.Click += ServicesButton_Click;
            AvailabilityButton.Click += AvailabilityButton_Click;
            AppointmentButton.Click += AppointmentButton_Click;
            PatientButton.Click += PatientButton_Click;
            DoctorButton.Click += DoctorButton_Click;
        }

        private void DoctorPageForm1_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Doctors WHERE is_active = 1", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dgvDoctortable.DataSource = dt;
                HideDoctorIdColumn();

                SqlDataAdapter serviceAdapter = new SqlDataAdapter("SELECT service_id, service_name FROM Services", con);
                DataTable serviceTable = new DataTable();
                serviceAdapter.Fill(serviceTable);

                cmbServices.DataSource = serviceTable;
                cmbServices.DisplayMember = "service_name";
                cmbServices.ValueMember = "service_id";
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
        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                // Insert doctor
                cmd = new SqlCommand("INSERT INTO Doctors (full_name, email, phone_number) OUTPUT INSERTED.doctor_id VALUES (@full_name, @email, @phone_number)", con);
                cmd.Parameters.AddWithValue("@full_name", txtfname.Text);
                cmd.Parameters.AddWithValue("@email", txtemail.Text);
                cmd.Parameters.AddWithValue("@phone_number", txtpnum.Text);
                int newDoctorId = (int)cmd.ExecuteScalar();

                // Assign selected service
                int serviceId = Convert.ToInt32(cmbServices.SelectedValue);
                SqlCommand serviceCmd = new SqlCommand("INSERT INTO DoctorServices (doctor_id, service_id) VALUES (@doctorId, @serviceId)", con);
                serviceCmd.Parameters.AddWithValue("@doctorId", newDoctorId);
                serviceCmd.Parameters.AddWithValue("@serviceId", serviceId);
                serviceCmd.ExecuteNonQuery();

                MessageBox.Show("Doctor and service saved!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
                RefreshDoctorTable();
                ClearFields();
            }
        }


        private void Edit_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                // Update doctor info
                cmd = new SqlCommand("UPDATE Doctors SET full_name = @full_name, email = @email, phone_number = @phone_number WHERE doctor_id = @doctor_id", con);
                cmd.Parameters.AddWithValue("@doctor_id", Convert.ToInt32(txtDocid.Text));
                cmd.Parameters.AddWithValue("@full_name", txtfname.Text);
                cmd.Parameters.AddWithValue("@email", txtemail.Text);
                cmd.Parameters.AddWithValue("@phone_number", txtpnum.Text);
                cmd.ExecuteNonQuery();

                // Reassign service (optional: delete old first)
                int serviceId = Convert.ToInt32(cmbServices.SelectedValue);
                SqlCommand serviceCmd = new SqlCommand("IF NOT EXISTS (SELECT * FROM DoctorServices WHERE doctor_id = @doctorId AND service_id = @serviceId) INSERT INTO DoctorServices (doctor_id, service_id) VALUES (@doctorId, @serviceId)", con);
                serviceCmd.Parameters.AddWithValue("@doctorId", Convert.ToInt32(txtDocid.Text));
                serviceCmd.Parameters.AddWithValue("@serviceId", serviceId);
                serviceCmd.ExecuteNonQuery();

                MessageBox.Show("Doctor info and service updated!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
                RefreshDoctorTable();
                ClearFields();
            }
        }
        private void RefreshDoctorTable()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Doctors WHERE is_active = 1", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvDoctortable.DataSource = dt;
            HideDoctorIdColumn();
            con.Close();
        }

        private void ClearFields()
        {
            txtDocid.Text = "";
            txtfname.Text = "";
            txtemail.Text = "";
            txtpnum.Text = "";
        }
        private void Delete_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to archive this doctor's information?",
                "Confirm Archive",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("UPDATE Doctors SET is_active = 0 WHERE doctor_id = @doctor_id", con);
                    cmd.Parameters.AddWithValue("@doctor_id", Convert.ToInt32(txtDocid.Text));
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error archiving doctor: " + ex.Message);
                }
                finally
                {
                    con.Close();
                    RefreshDoctorTable();
                    ClearFields();
                    MessageBox.Show("Doctor archived successfully.");
                }
            }
            else
            {
                MessageBox.Show("Archiving cancelled.");
            }
        }
        private void dgvDoctortable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDoctortable.Rows[e.RowIndex];
                // doctor_id remains available in the data row even when the column is hidden,
                // so we can safely populate txtDocid for editing while keeping the grid column hidden.
                txtDocid.Text = row.Cells["doctor_id"].Value.ToString();
                txtfname.Text = row.Cells["full_name"].Value.ToString();
                txtemail.Text = row.Cells["email"].Value.ToString();
                txtpnum.Text = row.Cells["phone_number"].Value.ToString();

                // Load services assigned to the selected doctor
                if (int.TryParse(txtDocid.Text, out int doctorId))
                {
                    LoadDoctorServices(doctorId);
                }
            }
        }

        private void MSchedule_Click(object sender, EventArgs e)
        {
            SchedulePage Schedule = new SchedulePage();
            Schedule.Show();
            this.Hide();
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

        /// <summary>
        /// Hide the 'doctor_id' column in the DataGridView if it exists.
        /// The data is still available in the row cells for internal use.
        /// </summary>
        private void HideDoctorIdColumn()
        {
            if (dgvDoctortable.Columns != null && dgvDoctortable.Columns.Contains("doctor_id"))
            {
                dgvDoctortable.Columns["doctor_id"].Visible = false;
            }
        }

        /// <summary>
        /// Load DoctorServices for a given doctor id and display service names in the
        /// DoctorService_grdpnl (assumed to be a DataGridView).
        /// </summary>
        private void LoadDoctorServices(int doctorId)
        {
            try
            {
                using (var conn = new SqlConnection(db.getDatabaseStringName()))
                {
                    conn.Open();
                    // Join DoctorServices with Services to get the service name
                    string sql = @"SELECT ds.doctor_id, ds.service_id, s.service_name
                                   FROM DoctorServices ds
                                   INNER JOIN Services s ON ds.service_id = s.service_id
                                   WHERE ds.doctor_id = @DoctorId";

                    using (var cmdLoad = new SqlCommand(sql, conn))
                    {
                        cmdLoad.Parameters.AddWithValue("@DoctorId", doctorId);
                        using (var adapter = new SqlDataAdapter(cmdLoad))
                        {
                            DataTable dtServices = new DataTable();
                            adapter.Fill(dtServices);

                            // Assign to grid
                            // Ensure the control exists and is a DataGridView; if different control type adjust accordingly.
                            DoctorService_grdpnl.DataSource = dtServices;

                            // Hide technical id columns if present so UX shows only meaningful service name
                            if (DoctorService_grdpnl.Columns != null)
                            {
                                if (DoctorService_grdpnl.Columns.Contains("doctor_id"))
                                    DoctorService_grdpnl.Columns["doctor_id"].Visible = false;
                                if (DoctorService_grdpnl.Columns.Contains("service_id"))
                                    DoctorService_grdpnl.Columns["service_id"].Visible = false;

                                // Optionally set service_name column header and autosize
                                if (DoctorService_grdpnl.Columns.Contains("service_name"))
                                {
                                    DoctorService_grdpnl.Columns["service_name"].HeaderText = "Service";
                                    DoctorService_grdpnl.Columns["service_name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading doctor services: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Add_btn_Click(object sender, EventArgs e)
        {
            // Validate doctor id
            if (string.IsNullOrWhiteSpace(txtDocid.Text))
            {
                MessageBox.Show("Select a doctor from the table (click a row) before adding a service.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtDocid.Text, out int doctorId))
            {
                MessageBox.Show("Doctor ID is invalid.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbServices.SelectedValue == null || !int.TryParse(cmbServices.SelectedValue.ToString(), out int serviceId))
            {
                MessageBox.Show("Select a valid service to add.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                con.Open();

                // Check if assignment already exists
                using (var checkCmd = new SqlCommand("SELECT COUNT(*) FROM DoctorServices WHERE doctor_id = @doctorId AND service_id = @serviceId", con))
                {
                    checkCmd.Parameters.AddWithValue("@doctorId", doctorId);
                    checkCmd.Parameters.AddWithValue("@serviceId", serviceId);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("This service is already assigned to the doctor.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                // Insert assignment
                using (var insertCmd = new SqlCommand("INSERT INTO DoctorServices (doctor_id, service_id) VALUES (@doctorId, @serviceId)", con))
                {
                    insertCmd.Parameters.AddWithValue("@doctorId", doctorId);
                    insertCmd.Parameters.AddWithValue("@serviceId", serviceId);
                    insertCmd.ExecuteNonQuery();
                }

                MessageBox.Show("Service successfully assigned to doctor.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error assigning service: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
                RefreshDoctorTable();
                try
                {
                    // refresh services grid for the same doctor
                    LoadDoctorServices(doctorId);
                }
                catch
                {
                    // ignore refresh errors
                }
            }
        }

        private void Remove_btn_Click(object sender, EventArgs e)
        {
            // Validate doctor id
            if (string.IsNullOrWhiteSpace(txtDocid.Text))
            {
                MessageBox.Show("Select a doctor from the table (click a row) before removing a service.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtDocid.Text, out int doctorId))
            {
                MessageBox.Show("Doctor ID is invalid.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbServices.SelectedValue == null || !int.TryParse(cmbServices.SelectedValue.ToString(), out int serviceId))
            {
                MessageBox.Show("Select a valid service to remove.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to remove the selected service from this doctor?", "Confirm Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                con.Open();

                using (var deleteCmd = new SqlCommand("DELETE FROM DoctorServices WHERE doctor_id = @doctorId AND service_id = @serviceId", con))
                {
                    deleteCmd.Parameters.AddWithValue("@doctorId", doctorId);
                    deleteCmd.Parameters.AddWithValue("@serviceId", serviceId);
                    int rows = deleteCmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Service removed from doctor.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("The selected service was not assigned to this doctor.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing service: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
                RefreshDoctorTable();
                try
                {
                    // refresh services grid for the same doctor
                    LoadDoctorServices(doctorId);
                }
                catch
                {
                    // ignore refresh errors
                }
            }
        }
    }
}

