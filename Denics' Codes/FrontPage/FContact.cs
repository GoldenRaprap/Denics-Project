using System;
using System.Threading.Tasks;


namespace Denics.FrontPage
{
    public partial class FContact : Form
    {
        public FContact()
        {
            InitializeComponent();
        }
        private void FContacts_Load(object sender, EventArgs e)
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

    }
}
