using Denics.Administrator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Denics.FrontPage
{
    public partial class FAboutUs : Form
    {
        public FAboutUs()
        {
            InitializeComponent();
        }

        private void FAboutUs_Load(object sender, EventArgs e)
        {

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

        private void BookNow_btn_Click(object sender, EventArgs e)
        {
            Registration register = new Registration(); 
            register.Show();
            this.Hide();
        }
    }
}
