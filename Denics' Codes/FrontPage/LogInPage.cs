using Denics.Administrator;
using Denics.FrontPage;
using Denics.UserInterface;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Denics.FrontPage
{
    public partial class LogInPage : Form
    {
        static CallDatabase db = new CallDatabase();
        SqlConnection con = new SqlConnection(db.getDatabaseStringName());
        SqlCommand cmd;

        public LogInPage()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // wire Enter key behavior for quicker keyboard navigation
            Emailtxtb.KeyDown += Emailtxtb_KeyDown;
            Passwordtxtb.KeyDown += Passwordtxtb_KeyDown;

            // Add click functionality to labels
            Home_lbl.Click += Home_lbl_Click;
            AboutUs_lbl.Click += AboutUs_lbl_Click;
            Contact_lbl.Click += Contact_lbl_Click;
            Services_lbl.Click += Services_lbl_Click;
            Book_lbl.Click += Book_lbl_Click;
            HeaderLogo_img.Click += Home_lbl_Click;
        }

        private void LogInPage_Load(object sender, EventArgs e)
        {
            Emailtxtb.Text = "";
            Passwordtxtb.Text = "";

            // Ensure single-line mode (masking does not work with Multiline = true)
            Passwordtxtb.Multiline = false;
            Passwordtxtb.UseSystemPasswordChar = true;
        }

        // Move focus from Email to Password when Enter is pressed
        private void Emailtxtb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent the form AcceptButton from also firing
                Passwordtxtb.Focus();
            }
        }

        // Trigger the login button when Enter is pressed on Password
        private void Passwordtxtb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                LogInbtn.PerformClick();
            }
        }

        private void LogInbtn_Click(object sender, EventArgs e)
        {
            try
            {
                string email = Emailtxtb.Text.Trim();
                string password = Passwordtxtb.Text.Trim();

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter both Email and Password.");
                    return;
                }

                con.Open();
                string query = "SELECT [user_id], [role], [password] FROM [Users] WHERE [email] = @Email";
                using (var cmdLocal = new SqlCommand(query, con))
                {
                    cmdLocal.Parameters.AddWithValue("@Email", email);

                    using (var reader = cmdLocal.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            MessageBox.Show("Invalid Email or Password. Please try again.");
                            return;
                        }

                        int userId = Convert.ToInt32(reader["user_id"]);
                        string role = reader["role"]?.ToString() ?? "";
                        string storedHash = reader["password"]?.ToString() ?? "";

                        string enteredHash = HashPassword(password);

                        if (!string.Equals(enteredHash, storedHash, StringComparison.Ordinal))
                        {
                            MessageBox.Show("Invalid Email or Password. Please try again.");
                            return;
                        }


                        Denics.UserAccount.SetUserId(userId); // set global current user id

                        if (role == "admin")
                        {
                            var adminPage = new MainAdminPage();
                            adminPage.Show();
                            this.Hide();
                        }
                        else if (role == "patient")
                        {
                            var homepage = new HomePage();
                            homepage.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Unknown user role. Contact administrator.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        // helper - must match Registration's hashing exactly
        private static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private void Registerbtn_Click(object sender, EventArgs e)
        {
            // gose to registration page
            Registration registrationPage = new Registration();
            registrationPage.Show();
            this.Hide();
        }

        // Checkbox to show/hide password
        private void ShowPassword_chkbx_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowPassword_chkbx.Checked)
            {
                Passwordtxtb.UseSystemPasswordChar = false;
            }
            else
            {
                Passwordtxtb.UseSystemPasswordChar = true;
            }
        }


        private void ForgotPassword_btn_Click(object sender, EventArgs e)
        {
            Forgot_Password fpassword = new Forgot_Password();
            fpassword.Show();
            this.Hide();
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
