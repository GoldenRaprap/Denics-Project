using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Denics.Homepages
{
    public partial class AboutUs : Form
    {
        public AboutUs()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen; 
        }

        private void AboutUs_Load(object sender, EventArgs e)
        {

        }
        private void Home_Click(object sender, EventArgs e)
        {
            Homepage homepage = new Homepage();
            homepage.Show();
            this.Hide();
        }
        private void Contact_Click(object sender, EventArgs e)
        {
            Contactpage contact = new Contactpage();
            contact.Show();
            this.Hide();
        }
        private void About_Us_Click(object sender, EventArgs e)
        {
            AboutUs aboutUs = new AboutUs();
            aboutUs.Show();
            this.Hide();
        }
        private void Services_Click(object sender, EventArgs e)
        {
            Servicespage services = new Servicespage();
            services.Show();
            this.Hide();
        }
    }
}
