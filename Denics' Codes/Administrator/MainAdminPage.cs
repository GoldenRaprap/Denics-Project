using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Denics.Administrator
{
    public partial class MainAdminPage : Form
    {
        public MainAdminPage()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void AdminDesignLayout_Load(object sender, EventArgs e)
        {
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
