using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Denics.Administrator
{
    public partial class LogInPage : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Denics Project\\Denics' Database\\Denics_db.mdf\";Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd;

        public LogInPage()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void LogInPage_Load(object sender, EventArgs e)
        {
            Emailtxtb.Text = "";
            Passwordtxtb.Text = "";
        }

        private void LogInbtn_Click(object sender, EventArgs e)
        {
            try
            {
                string email = Emailtxtb.Text.Trim();
                string password = Passwordtxtb.Text.Trim();

                if (email == "" || password == "")
                {
                    MessageBox.Show("Please enter both Email and Password.");
                    return;
                }

                con.Open();
                string query = "SELECT [user_id], [role] FROM [Users] WHERE [email] = @Email AND [password] = @Password";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string role = reader["role"].ToString();

                    if (role == "admin")
                    {
                        MainAdminPage adminPage = new MainAdminPage();
                        adminPage.Show();
                        this.Hide();
                    }
                    else if (role == "patient")
                    {
                        // TODO: replace with your Patient Dashboard form
                        MessageBox.Show("Patient Page do not Exist yet...");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Email or Password.");
                }

                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
