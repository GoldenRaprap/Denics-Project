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
    public partial class ServicesPage : Form
    {
        // Database connection
        static CallDatabase db = new CallDatabase();
        SqlConnection con = new SqlConnection(db.getDatabaseStringName());
        SqlCommand cmd;

        private int? selectedServiceId = null;

        private string connectionString;

        public ServicesPage()
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

        }

        private void ServicesPage_Load(object sender, EventArgs e)
        {
            // load status options
            cmbStatus.Items.Add("Available");
            cmbStatus.Items.Add("Unavailable");

            // initial load (hides id column and clears selection)
            LoadServices();
            btnRestore.Visible = false;
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtServiceName.Text))
            {
                MessageBox.Show("Please enter a service name to add.");
                return;
            }

            try
            {
                con.Open();
                cmd = new SqlCommand("INSERT INTO Services (service_name) VALUES (@service_name)", con);
                cmd.Parameters.AddWithValue("@service_name", txtServiceName.Text);
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Service Added!");
                }
                else
                {
                    MessageBox.Show("No service was added.");
                }

                // Refresh the data grid via central loader
                LoadServices();

                txtServiceName.Text = "";
                selectedServiceId = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding service: " + ex.Message);
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void editbtn_Click(object sender, EventArgs e)
        {
            if (selectedServiceId == null)
            {
                MessageBox.Show("Please select a service to edit.");
                return;
            }

            try
            {
                con.Open();
                cmd = new SqlCommand("UPDATE Services SET service_name = @service_name, status = @status WHERE service_id = @service_id", con);
                cmd.Parameters.AddWithValue("@service_id", selectedServiceId.Value);
                cmd.Parameters.AddWithValue("@service_name", txtServiceName.Text);
                cmd.Parameters.AddWithValue("@status", cmbStatus.SelectedItem?.ToString() ?? "Available");
                cmd.ExecuteNonQuery();
                con.Close();

                // Clear local selection & UI
                selectedServiceId = null;
                txtServiceName.Text = "";
                cmbStatus.SelectedIndex = -1;

                LoadServices();

                MessageBox.Show("Service Edited!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error editing service: " + ex.Message);
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void removebtn_Click_1(object sender, EventArgs e)
        {
            if (selectedServiceId == null)
            {
                MessageBox.Show("Please select a service to archive.");
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to archive this service?",
                "Archive Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.No)
                return;

            try
            {
                con.Open();
                cmd = new SqlCommand("UPDATE Services SET status = 'Unavailable' WHERE service_id = @service_id", con);
                cmd.Parameters.AddWithValue("@service_id", selectedServiceId.Value);
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                if (rowsAffected > 0)
                    MessageBox.Show("Service archived successfully!");
                else
                    MessageBox.Show("No matching service found.");

                // Refresh the grid
                LoadServices();

                // clear fields
                selectedServiceId = null;
                txtServiceName.Text = "";
                cmbStatus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error archiving service: " + ex.Message);
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void Servicestb_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = Servicestb.Rows[e.RowIndex];

                // store id in private field (do not depend on a textbox)
                selectedServiceId = null;
                var idCell = row.Cells["service_id"]?.Value;
                if (idCell != null && int.TryParse(idCell.ToString(), out int id))
                    selectedServiceId = id;

                // populate other controls
                txtServiceName.Text = row.Cells["service_name"].Value?.ToString() ?? "";
                var statusVal = row.Cells["status"].Value?.ToString();
                if (statusVal != null && cmbStatus.Items.Contains(statusVal))
                    cmbStatus.SelectedItem = statusVal;
                else
                    cmbStatus.SelectedIndex = -1;
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

        private void archivebtn_Click(object sender, EventArgs e)
        {
            if (selectedServiceId == null)
            {
                MessageBox.Show("Please select a service to archive.");
                return;
            }

            int serviceID = selectedServiceId.Value;

            DialogResult result = MessageBox.Show("Are you sure you want to archive this service?",
                                                  "Confirm Archive", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Services SET status = 'Archived' WHERE service_id = @service_id", con);
                cmd.Parameters.AddWithValue("@service_id", serviceID);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Service archived successfully!");

                // refresh data (only active)
                LoadServices("active");

                // clear selection
                selectedServiceId = null;
                txtServiceName.Text = string.Empty;
                cmbStatus.SelectedIndex = -1;
            }
        }

        private void LoadServices(string filter = "active")
        {
            string query = "";

            if (filter == "archived")
            {
                query = "SELECT * FROM Services WHERE status = 'Archived'";
            }
            else
            {
                query = "SELECT * FROM Services WHERE status != 'Archived' OR status IS NULL";
            }

            using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Servicestb.DataSource = dt;

                // hide id column visually but keep it in the underlying data so code can read it
                if (Servicestb.Columns.Contains("service_id"))
                    Servicestb.Columns["service_id"].Visible = false;

                // clear selection and local id state
                Servicestb.ClearSelection();
                selectedServiceId = null;
            }
        }

        private void btnShowArchived_Click(object sender, EventArgs e)
        {
            LoadServices("archived");
            btnRestore.Visible = true;
            btnShowArchived.Enabled = false;
            btnShowActive.Enabled = true;
        }

        private void btnShowActive_Click(object sender, EventArgs e)
        {
            LoadServices("active");
            btnRestore.Visible = false;
            btnShowActive.Enabled = false;
            btnShowArchived.Enabled = true;
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (selectedServiceId == null)
            {
                MessageBox.Show("Please select a service to restore.");
                return;
            }

            int serviceID = selectedServiceId.Value;

            DialogResult result = MessageBox.Show(
                "Are you sure you want to restore this service?",
                "Confirm Restore",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Services SET status = 'Available' WHERE service_id = @service_id",
                    con);
                cmd.Parameters.AddWithValue("@service_id", serviceID);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Service restored successfully!");

                // refresh DataGridView (show archived then active)
                LoadServices("archived");

                // clear fields & selection
                selectedServiceId = null;
                txtServiceName.Text = string.Empty;
                cmbStatus.SelectedIndex = -1;

                LoadServices("active");
                btnRestore.Visible = false;
                btnShowActive.Enabled = false;
                btnShowArchived.Enabled = true;
            }
        }
    }
}

