using Microsoft.VisualBasic.ApplicationServices;
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

namespace Denics.UserInterface
{
    public partial class AvailableServices : Form
    {
        // Calling database
        static CallDatabase db = new CallDatabase();
        SqlConnection con = new SqlConnection(db.getDatabaseStringName());
        SqlCommand cmd;

        public AvailableServices()
        {
            InitializeComponent();

            // Sidebar click functions
            HomeButton.Click += HomeButton_Click;
            PatientButton.Click += PatientButton_Click;
            DoctorButton.Click += DoctorButton_Click;
            AvailabilityButton.Click += AvailabilityButton_Click;
            AppointmentButton.Click += AppointmentButton_Click;
            ServicesButton.Click += ServicesButton_Click;

        }

        private void AvailableServices_Load(object sender, EventArgs e)
        {
            LoadAvailabilityLabels();
        }
        private void LoadAvailabilityLabels()
        {
            try
            {
                con.Open();
                string query = "SELECT service_name, status FROM Services";
                cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string serviceName = reader["service_name"].ToString();
                    string serviceStatus = reader["status"].ToString();
                    switch (serviceName)
                    {
                        case "Check-Up & Cleaning":
                            Available1_lbl.Text = serviceStatus == "Available" ? "Available" : "Not Available";
                            break;
                        case "Tooth Filing":
                            Available2_lbl.Text = serviceStatus == "Available" ? "Available" : "Not Available";
                            break;
                        case "Tooth Extraction":
                            Available3_lbl.Text = serviceStatus == "Available" ? "Available" : "Not Available";
                            break;
                        case "Root Canal Treatment":
                            Available4_lbl.Text = serviceStatus == "Available" ? "Available" : "Not Available";
                            break;
                        case "Teeth Whitening":
                            Available5_lbl.Text = serviceStatus == "Available" ? "Available" : "Not Available";
                            break;
                        case "Orthodontic Consultation":
                            Available6_lbl.Text = serviceStatus == "Available" ? "Available" : "Not Available";
                            break;
                        case "Pediatric Dental Visit":
                            Available7_lbl.Text = serviceStatus == "Available" ? "Available" : "Not Available";
                            break;
                        case "Crown / Bridge / Veneer":
                            Available8_lbl.Text = serviceStatus == "Available" ? "Available" : "Not Available";
                            break;
                        case "Emergency":
                            Available9_lbl.Text = serviceStatus == "Available" ? "Available" : "Not Available";
                            break;
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading service availability: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Show();
            this.Hide();
        }

        private void PatientButton_Click(object sender, EventArgs e)
        {
            UserProfile userProfile = new UserProfile();
            userProfile.Show();
            this.Hide();
        }

        private void DoctorButton_Click(object sender, EventArgs e)
        {
            Doctors doctors = new Doctors();
            doctors.Show();
            this.Hide();
        }

        private void AvailabilityButton_Click(object sender, EventArgs e)
        {
            Calendar calendar = new Calendar();
            calendar.Show();
            this.Hide();
        }

        private void AppointmentButton_Click(object sender, EventArgs e)
        {
            UserBookingPage userBookingPage = new UserBookingPage();
            userBookingPage.Show();
            this.Hide();
        }

        private void ServicesButton_Click(object sender, EventArgs e)
        {
            AvailableServices availableServices = new AvailableServices();
            availableServices.Show();
            this.Hide();
        }


    }
}
