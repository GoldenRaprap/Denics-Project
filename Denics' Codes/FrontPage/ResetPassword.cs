using System;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Denics.FrontPage
{
    public partial class ResetPassword : Form
    {
        static CallDatabase db = new CallDatabase();
        SqlConnection conn = new SqlConnection(db.getDatabaseStringName());
        SqlCommand cmd;
        private string email;

        public ResetPassword(string userEmail)
        {
            InitializeComponent();
            email = userEmail; // Assign email from constructor
        }

        private void ResetPassword_Load(object sender, EventArgs e)
        {
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private void btnUpdatePass_Click(object sender, EventArgs e)
        {
            string newPassword = txtboxNewPass.Text;
            string confirmPassword = txtboxConfirmPass.Text;
            string hashpassword = HashPassword(newPassword);

            if (newPassword == confirmPassword)
            {
                try
                {
                    {
                        string query = "UPDATE [Users] SET [Password] = @Password WHERE [Email] = @Email";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Password", hashpassword);
                            cmd.Parameters.AddWithValue("@Email", email);

                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            conn.Close();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Password successfully changed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LogInPage log = new LogInPage();
                                log.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("No matching email found. Please check your details.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating password: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Passwords do not match. Please try again.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
