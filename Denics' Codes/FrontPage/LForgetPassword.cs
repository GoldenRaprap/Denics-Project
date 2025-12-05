using Denics.FrontPage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Denics.FrontPage
{
    public partial class Forgot_Password : Form
    {
        string randomcode;
        public static string to;

        public Forgot_Password()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnSendCode_Click(object sender, EventArgs e)
        {
            // generate code
            Random rand = new Random();
            randomcode = (rand.Next(100000, 999999)).ToString();

            // recipient
            var recipientEmail = txtboxEmail.Text?.Trim();

            // get configured sender address
            CallEmail callEmail = new CallEmail();
            string email = callEmail.getEmail();

            if (string.IsNullOrWhiteSpace(recipientEmail))
            {
                MessageBox.Show("Please enter a recipient email.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Keep same simple SMTP approach used in OTPVerification
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(email, "rhrd vytr lxih utoo\r\n")
                };

                MailMessage message = new MailMessage
                {
                    From = new MailAddress(email),
                    Subject = "Password Reset Code - Denics Dental",
                    IsBodyHtml = false,
                    Body =
                        $"Dear User,\n\n" +
                        $"You (or someone using this email) requested a password reset for your Denics Dental account.\n\n" +
                        $"🔐 Your Reset Code: {randomcode}\n\n" +
                        $"This code is valid for 5 minutes. If you did not request a password reset, please ignore this message.\n\n" +
                        $"Best regards,\n" +
                        $"Denics Dental Team"
                };

                message.To.Add(recipientEmail);

                client.Send(message);

                // persist recipient for verification step
                to = recipientEmail;

                MessageBox.Show("Code successfully sent.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (randomcode == (txtboxCode.Text).ToString())
            {
                to = txtboxEmail.Text;

                ResetPassword rp = new ResetPassword(to);
                this.Hide();
                rp.Show();
            }
            else
            {
                MessageBox.Show($"Wrong code: ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Forgot_Password_Load(object sender, EventArgs e)
        {

        }
    }
}
