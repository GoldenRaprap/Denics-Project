using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Denics.Administrator
{
    public partial class ServicesPage : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Denics Project\\Denics' Database\\Denics_db.mdf\";Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd;
        private string connectionString;

        public ServicesPage()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void ServicesPage_Load(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Services", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Servicestb.DataSource = dt;
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
                    MessageBox.Show("No service found with the specified name.");
                }

                // Refresh the data grid
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Services", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Servicestb.DataSource = dt;

                txtServiceName.Text = "";

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
            con.Open();
            cmd = new SqlCommand("UPDATE Services Set service_name = @service_name WHERE service_id =@service_id", con);
            cmd.Parameters.AddWithValue("@service_id", Convert.ToInt32(txtServiceID.Text));
            cmd.Parameters.AddWithValue("@service_name", txtServiceName.Text);
            cmd.ExecuteNonQuery();
            con.Close();

            txtServiceID.Text = " ";
            txtServiceName.Text = " ";

            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Services", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Servicestb.DataSource = dt;

            MessageBox.Show("Service Edited !");
        }

        private void removebtn_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtServiceID.Text) && string.IsNullOrWhiteSpace(txtServiceName.Text))
            {
                MessageBox.Show("Please enter a service ID or name to remove.");
                return;
            }

            // Ask for confirmation
            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to delete this service?",
                "Delete Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm == DialogResult.No)
            {
                MessageBox.Show("Deletion cancelled.");
                return;
            }

            try
            {
                con.Open();
                int rowsAffected = 0;

                // Determine whether to delete by ID or name
                if (!string.IsNullOrWhiteSpace(txtServiceID.Text))
                {
                    // Delete by primary key
                    cmd = new SqlCommand("DELETE FROM Services WHERE service_id = @service_id", con);
                    cmd.Parameters.AddWithValue("@service_id", Convert.ToInt32(txtServiceID.Text.Trim()));
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                else
                {
                    // Delete by name
                    cmd = new SqlCommand("DELETE FROM Services WHERE service_name = @service_name", con);
                    cmd.Parameters.AddWithValue("@service_name", txtServiceName.Text.Trim());
                    rowsAffected = cmd.ExecuteNonQuery();
                }

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Service removed!");
                }
                else
                {
                    MessageBox.Show("No service found with the specified ID or name.");
                }

                // Refresh the data grid
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Services", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Servicestb.DataSource = dt;

                txtServiceID.Text = "";
                txtServiceName.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing service: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void Servicestb_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow row = Servicestb.Rows[e.RowIndex];
                txtServiceID.Text = row.Cells["service_id"].Value.ToString();
                txtServiceName.Text = row.Cells["service_name"].Value.ToString();
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

        private void NotificationButton_Click(object sender, EventArgs e)
        {
            NotificationPage notificationPage = new NotificationPage();
            notificationPage.Show();
            this.Hide();
        }

    }
}
