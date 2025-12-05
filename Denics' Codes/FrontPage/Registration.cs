using Denics.FrontPage;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Denics.FrontPage
{
    public partial class Registration : Form
    {
        static CallDatabase db = new CallDatabase();
        SqlConnection con = new SqlConnection(db.getDatabaseStringName());
        SqlCommand cmd;

        public Registration()
        {
            InitializeComponent();

            // wire Enter-key navigation for registration fields
            boxFName.KeyDown += BoxFName_KeyDown;
            MiddleName_txtbx.KeyDown += MiddleName_txtbx_KeyDown;
            boxLName.KeyDown += BoxLName_KeyDown;
            Suffix_txtbx.KeyDown += Suffix_txtbx_KeyDown;
            Male_rdbtn.KeyDown += Male_rdbtn_KeyDown;
            Female_rdbtn.KeyDown += Female_rdbtn_KeyDown;
            Birthdate_cldr.KeyDown += Birthdate_cldr_KeyDown;
            boxNumber.KeyDown += BoxNumber_KeyDown;
            textBox1.KeyDown += TextBox1_KeyDown;
            boxEmail.KeyDown += BoxEmail_KeyDown;
            boxPassword.KeyDown += BoxPassword_KeyDown;
            ReEnterPassword_txtbx.KeyDown += ReEnterPassword_txtbx_KeyDown;

            // Add click functionality to labels
            Home_lbl.Click += Home_lbl_Click;
            AboutUs_lbl.Click += AboutUs_lbl_Click;
            Contact_lbl.Click += Contact_lbl_Click;
            Services_lbl.Click += Services_lbl_Click;
            Book_lbl.Click += Book_lbl_Click;
            HeaderLogo_img.Click += Home_lbl_Click;

            // Add click function to buttons
            buttonCancel.Click += buttonCancel_Click;
            buttonRegister.Click += buttonRegister_Click;

            // Add click functionality to checkboxes
            checkBoxShow.CheckedChanged += checkBoxShow_CheckedChanged;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;

            // Add text changed functionality to ReEnterPassword_txtbx
            ReEnterPassword_txtbx.TextChanged += ReEnterPassword_txtbx_TextChanged;

        }

        private void Registration_Load(object sender, EventArgs e)
        {
            boxPassword.UseSystemPasswordChar = true;
            ReEnterPassword_txtbx.UseSystemPasswordChar = true;
            Birthdate_cldr.MaxDate = DateTime.Today;
            Birthdate_cldr.ShowCheckBox = true;

        }

        // --- Enter-key navigation handlers ---
        private void MoveFocusOnEnter(object sender, KeyEventArgs e, Control next)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                next?.Focus();
            }
        }

        private void BoxFName_KeyDown(object sender, KeyEventArgs e) =>
            MoveFocusOnEnter(sender, e, MiddleName_txtbx);

        private void MiddleName_txtbx_KeyDown(object sender, KeyEventArgs e) =>
            MoveFocusOnEnter(sender, e, boxLName);

        private void BoxLName_KeyDown(object sender, KeyEventArgs e) =>
            MoveFocusOnEnter(sender, e, Suffix_txtbx);

        private void Suffix_txtbx_KeyDown(object sender, KeyEventArgs e) =>
            MoveFocusOnEnter(sender, e, Male_rdbtn);

        private void Male_rdbtn_KeyDown(object sender, KeyEventArgs e) =>
            MoveFocusOnEnter(sender, e, Female_rdbtn);

        private void Female_rdbtn_KeyDown(object sender, KeyEventArgs e) =>
            MoveFocusOnEnter(sender, e, Birthdate_cldr);

        private void Birthdate_cldr_KeyDown(object sender, KeyEventArgs e) =>
            MoveFocusOnEnter(sender, e, boxNumber);

        private void BoxNumber_KeyDown(object sender, KeyEventArgs e) =>
            MoveFocusOnEnter(sender, e, textBox1);

        private void TextBox1_KeyDown(object sender, KeyEventArgs e) =>
            MoveFocusOnEnter(sender, e, boxEmail);

        private void BoxEmail_KeyDown(object sender, KeyEventArgs e) =>
            MoveFocusOnEnter(sender, e, boxPassword);

        private void BoxPassword_KeyDown(object sender, KeyEventArgs e) =>
            MoveFocusOnEnter(sender, e, ReEnterPassword_txtbx);

        private void ReEnterPassword_txtbx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                // trigger the register button click so existing validation runs
                buttonRegister.PerformClick();
            }
        }
        // --- end Enter-key navigation handlers ---

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


        private bool IsDuplicateUser(string email, string contact, int? userId = null)
        {
            con.Open();
            string query = userId == null
                ? "SELECT COUNT(*) FROM Users WHERE email = @Email OR contact = @Contact"
                : "SELECT COUNT(*) FROM Users WHERE (email = @Email OR contact = @Contact) AND user_id != @UserId";

            SqlCommand checkCmd = new SqlCommand(query, con);
            checkCmd.Parameters.AddWithValue("@Email", email);
            checkCmd.Parameters.AddWithValue("@Contact", contact);
            if (userId != null)
                checkCmd.Parameters.AddWithValue("@UserId", userId.Value);

            int count = (int)checkCmd.ExecuteScalar();
            con.Close();
            return count > 0;
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(boxFName.Text) ||
                string.IsNullOrWhiteSpace(boxLName.Text) ||
                string.IsNullOrWhiteSpace(boxEmail.Text) ||
                string.IsNullOrWhiteSpace(boxNumber.Text) ||
                string.IsNullOrWhiteSpace(boxPassword.Text) ||
                string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(ReEnterPassword_txtbx.Text))
            {
                MessageBox.Show("Please fill in all required fields before saving.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hashedPassword = HashPassword(boxPassword.Text);
            string hashedReEnter = HashPassword(ReEnterPassword_txtbx.Text);

            if (hashedPassword != hashedReEnter)
            {
                MessageBox.Show("Password does not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IsDuplicateUser(boxEmail.Text, boxNumber.Text))
            {
                MessageBox.Show("This user already exists (duplicate Email or Contact).", "Duplicate Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Birthdate_cldr.Checked)
            {
                MessageBox.Show("Please select a birthdate.", "Missing Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Birthdate_cldr.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Birthdate cannot be in the future.", "Invalid Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Determine selected gender from radio buttons
            string gender = null;
            if (Male_rdbtn.Checked) gender = "Male";
            else if (Female_rdbtn.Checked) gender = "Female";

            // Do not insert here. Pass data to OTPVerification which will insert after OTP verification.
            var otpForm = new OTPVerification(
                email: boxEmail.Text,
                surname: boxLName.Text,
                firstname: boxFName.Text,
                middlename: string.IsNullOrWhiteSpace(MiddleName_txtbx.Text) ? null : MiddleName_txtbx.Text,
                suffix: string.IsNullOrWhiteSpace(Suffix_txtbx.Text) ? null : Suffix_txtbx.Text,
                gender: gender,
                hashedPassword: hashedPassword,
                contact: boxNumber.Text,
                birthdate: Birthdate_cldr.Value.Date,
                address: textBox1.Text
            );

            otpForm.Show();
            this.Hide();
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

        private void checkBoxShow_CheckedChanged(object sender, EventArgs e)
        {
            boxPassword.UseSystemPasswordChar = !checkBoxShow.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ReEnterPassword_txtbx.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            new LogInPage().Show();
            this.Hide();
        }

        private void ReEnterPassword_txtbx_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ReEnterPassword_txtbx.Text))
            {
                if (ReEnterPassword_txtbx.Text != boxPassword.Text)
                {
                    ReEnter_lbl.Text = "Passwords do not match!";
                    ReEnter_lbl.ForeColor = Color.White;
                }
                else
                {
                    ReEnter_lbl.Text = "Passwords match!";
                    ReEnter_lbl.ForeColor = Color.White;
                }
            }
            else
            {
                ReEnter_lbl.Text = "";
            }
        }

        private void Male_rdbtn_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
