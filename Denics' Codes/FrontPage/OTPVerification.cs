using Denics.UserInterface;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Windows.Forms;
using System.Linq;

namespace Denics.FrontPage
{
    public partial class OTPVerification : Form
    {
        private readonly string userEmail;
        private readonly string surname;
        private readonly string firstname;
        private readonly string middlename;
        private readonly string suffix;
        private readonly string gender;
        private readonly string hashedPassword;
        private readonly string contact;
        private readonly DateTime birthdate;
        private readonly string address;

        private string generatedOTP;
        private int countdown = 30;

        static CallDatabase db = new CallDatabase();

        // helper array for easier navigation
        private TextBox[] otpBoxes;

        public OTPVerification(
            string email,
            string surname,
            string firstname,
            string middlename,
            string suffix,
            string gender,
            string hashedPassword,
            string contact,
            DateTime birthdate,
            string address)
        {
            InitializeComponent();

            this.userEmail = email;
            this.surname = surname;
            this.firstname = firstname;
            this.middlename = middlename;
            this.suffix = suffix;
            this.gender = gender;
            this.hashedPassword = hashedPassword;
            this.contact = contact;
            this.birthdate = birthdate;
            this.address = address;

            // wire up OTP textbox behavior
            SetupOtpBoxes();

            GenerateAndSendOTP();
            StartCountDown();
        }

        private void SetupOtpBoxes()
        {
            otpBoxes = new[] { boxOTP1, boxOTP2, boxOTP3, boxOTP4, boxOTP5, boxOTP6 };

            foreach (var box in otpBoxes)
            {
                // enforce single character visually; logic handles pastes
                box.MaxLength = 6; // allow paste of multiple digits which will be distributed
                box.TextChanged += OtpBox_TextChanged;
                box.KeyPress += OtpBox_KeyPress;
                box.KeyDown += OtpBox_KeyDown;
                box.Enter += OtpBox_Enter;
                box.MouseClick += OtpBox_MouseClick;
            }
        }

        private void OtpBox_MouseClick(object? sender, MouseEventArgs e)
        {
            if (sender is TextBox tb)
                tb.SelectAll();
        }

        private void OtpBox_Enter(object? sender, EventArgs e)
        {
            if (sender is TextBox tb)
                tb.SelectAll();
        }

        private void OtpBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // allow control keys (e.g., backspace), digits only for typed input
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void OtpBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (sender is not TextBox tb) return;

            int idx = Array.IndexOf(otpBoxes, tb);
            if (e.KeyCode == Keys.Back)
            {
                if (string.IsNullOrEmpty(tb.Text) && idx > 0)
                {
                    // move to previous and clear it so user can continue deleting
                    var prev = otpBoxes[idx - 1];
                    prev.Focus();
                    prev.SelectAll();
                    e.SuppressKeyPress = true;
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (idx > 0)
                {
                    otpBoxes[idx - 1].Focus();
                    otpBoxes[idx - 1].SelectAll();
                    e.SuppressKeyPress = true;
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (idx < otpBoxes.Length - 1)
                {
                    otpBoxes[idx + 1].Focus();
                    otpBoxes[idx + 1].SelectAll();
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void OtpBox_TextChanged(object? sender, EventArgs e)
        {
            if (sender is not TextBox tb) return;
            int idx = Array.IndexOf(otpBoxes, tb);
            if (idx < 0) return;

            string raw = tb.Text ?? string.Empty;
            // keep only digits
            string digits = new string(raw.Where(char.IsDigit).ToArray());

            if (string.IsNullOrEmpty(digits))
            {
                // nothing valid to show
                tb.Text = string.Empty;
                return;
            }

            // If user pasted more than one digit, distribute across boxes starting here
            if (digits.Length > 1)
            {
                for (int i = 0; i < digits.Length && (idx + i) < otpBoxes.Length; i++)
                {
                    // assign single char to each subsequent box
                    otpBoxes[idx + i].Text = digits[i].ToString();
                }

                int nextIndex = Math.Min(otpBoxes.Length - 1, idx + digits.Length);
                // if we filled exactly to the end, focus last; otherwise focus next empty
                int focusIndex = nextIndex < otpBoxes.Length ? nextIndex : otpBoxes.Length - 1;
                otpBoxes[focusIndex].Focus();
                otpBoxes[focusIndex].SelectAll();
                return;
            }

            // Single digit typed/pasted: ensure textbox contains only that digit
            if (tb.Text != digits)
            {
                tb.Text = digits;
                // caret will be moved by SelectAll below
            }

            // move focus to next box automatically
            if (idx < otpBoxes.Length - 1)
            {
                otpBoxes[idx + 1].Focus();
                otpBoxes[idx + 1].SelectAll();
            }
            else
            {
                // last box: keep selected so user can replace easily
                tb.SelectAll();
            }
        }

        private void GenerateAndSendOTP()
        {
            Random rnd = new Random();
            generatedOTP = rnd.Next(100000, 999999).ToString();

            try
            {
                SendOTPEmail(userEmail, generatedOTP);
                MessageBox.Show("OTP sent to your email.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send OTP: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SendOTPEmail(string recipientEmail, string OTP)
        {
            CallEmail callEmail = new CallEmail();
            string email = callEmail.getEmail();
            if (string.IsNullOrWhiteSpace(recipientEmail))
                throw new ArgumentException("Recipient email cannot be empty.", nameof(recipientEmail));

            // Keep same simple SMTP approach as before.
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential(email, "rhrd vytr lxih utoo\r\n")
            };

            MailMessage message = new MailMessage
            {
                From = new MailAddress(email),
                Subject = "Email Verification - Denics Dental",
                IsBodyHtml = false,
                Body =
                    $"Dear User,\n\n" +
                    $"Thank you for registering with Denics Dental.\n\n" +
                    $"To complete your registration, please verify your email address using the One-Time Password (OTP) below:\n\n" +
                    $"🔐 Your OTP: {OTP}\n\n" +
                    $"This OTP is valid for 5 minutes. If you did not initiate this request, please ignore this message.\n\n" +
                    $"Best regards,\n" +
                    $"Denics Dental Team"
            };

            message.To.Add(recipientEmail);
            client.Send(message);
        }

        private void StartCountDown()
        {
            lblResendOTP.Enabled = false;
            countdown = 30;
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (countdown > 0)
            {
                lbltime.Text = $"Resend OTP in: {countdown} seconds";
                countdown--;
            }
            else
            {
                timer1.Stop();
                lblResendOTP.Enabled = true;
                lbltime.Text = "Resend OTP";
            }
        }

        private void lblResendOTP_Click(object sender, EventArgs e)
        {
            GenerateAndSendOTP();
            StartCountDown();
        }

        private string GetEnteredOTP()
        {
            return boxOTP1.Text + boxOTP2.Text + boxOTP3.Text + boxOTP4.Text + boxOTP5.Text + boxOTP6.Text;
        }

        private void buttonVerify_Click(object sender, EventArgs e)
        {
            string enteredOTP = GetEnteredOTP();
            if (enteredOTP == generatedOTP)
            {
                // Insert user into database now that OTP is verified
                try
                {
                    using (var con = new SqlConnection(db.getDatabaseStringName()))
                    {
                        con.Open();

                        // Check duplicate again (defensive)
                        using (var checkCmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE email = @Email OR contact = @Contact", con))
                        {
                            checkCmd.Parameters.AddWithValue("@Email", userEmail);
                            checkCmd.Parameters.AddWithValue("@Contact", contact ?? string.Empty);
                            int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                            if (count > 0)
                            {
                                MessageBox.Show("This email or contact already exists. Registration cancelled.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        using (var cmd = new SqlCommand("INSERT INTO Users (surname, firstname, middlename, suffix, gender, email, password, contact, birthdate, address) VALUES(@surname, @firstname, @middlename, @suffix, @gender, @email, @password, @contact, @birthdate, @address)", con))
                        {
                            cmd.Parameters.AddWithValue("@surname", surname ?? string.Empty);
                            cmd.Parameters.AddWithValue("@firstname", firstname ?? string.Empty);
                            cmd.Parameters.AddWithValue("@middlename", string.IsNullOrWhiteSpace(middlename) ? (object)DBNull.Value : middlename);
                            cmd.Parameters.AddWithValue("@suffix", string.IsNullOrWhiteSpace(suffix) ? (object)DBNull.Value : suffix);
                            cmd.Parameters.AddWithValue("@gender", string.IsNullOrWhiteSpace(gender) ? (object)DBNull.Value : gender);
                            cmd.Parameters.AddWithValue("@email", userEmail ?? string.Empty);
                            cmd.Parameters.AddWithValue("@password", hashedPassword ?? string.Empty);
                            cmd.Parameters.AddWithValue("@contact", contact ?? string.Empty);
                            cmd.Parameters.AddWithValue("@birthdate", birthdate.Date);
                            cmd.Parameters.AddWithValue("@address", address ?? string.Empty);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("OTP verification successful. Registration completed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Set userId to UserAccount
                    int userId = GetUserIdByEmail(userEmail);
                    Denics.UserAccount.SetUserId(userId); // set global current user id


                    // Goes to UserInterface Homepage after successful registration
                    HomePage homepage = new HomePage();
                    homepage.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to complete registration: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Incorrect OTP. Please try again.", "Verification Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OTPVerification_Load(object sender, EventArgs e)
        {
        }

        // Method to get User ID after registration
        private int GetUserIdByEmail(string email)
        {
            int userId = -1;
            using (var con = new SqlConnection(db.getDatabaseStringName()))
            {
                con.Open();
                using (var cmd = new SqlCommand("SELECT user_id FROM Users WHERE email = @Email", con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        userId = Convert.ToInt32(result);
                    }
                }
            }
            return userId;
        }
    }
}
