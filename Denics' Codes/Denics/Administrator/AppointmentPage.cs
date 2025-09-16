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
    public partial class AppointmentPage : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Denics Project\\Denics' Database\\Denics_db.mdf\";Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd;

        public AppointmentPage()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void AppointmentPage_Load(object sender, EventArgs e)
        {

        }
    }
}
