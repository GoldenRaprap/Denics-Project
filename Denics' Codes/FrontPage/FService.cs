using System;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Denics.FrontPage
{


    public partial class FService : Form
    {
        // Calling database
        static CallDatabase db = new CallDatabase();
        SqlConnection con = new SqlConnection(db.getDatabaseStringName());
        SqlCommand cmd;

        public FService()
        {
            InitializeComponent();
        }

        private void FService_Load(object sender, EventArgs e)
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


        private void Home_lbl_Click(object sender, EventArgs e)
        {
            FHomepage fhome = new FHomepage();
            fhome.Show();
            this.Hide();
        }

        private void AboutUs_lbl_Click(object sender, EventArgs e)
        {
            FAboutUs fabout = new FAboutUs();
            fabout.Show();
            this.Hide();
        }

        private void Contact_lbl_Click(object sender, EventArgs e)
        {
            FContact fcontact = new FContact();
            fcontact.Show();
            this.Hide();
        }
        private void Services_lbl_Click(object sender, EventArgs e)
        {
            FService fservice = new FService();
            fservice.Show();
            this.Hide();
        }
        private void Book_lbl_Click(object sender, EventArgs e)
        {
            LogInPage flogin = new LogInPage();
            flogin.Show();
            this.Hide();
        }

    }
}
