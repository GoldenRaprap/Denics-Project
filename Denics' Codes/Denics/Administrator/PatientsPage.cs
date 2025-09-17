using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Denics.Administrator
{
    public partial class PatientsPage : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Denics Project\\Denics' Database\\Denics_db.mdf\";Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd;
        public PatientsPage()
        {
            InitializeComponent();
            this.Load += PatientsPage_Load;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void PatientsPage_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Users", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ClientsTable.DataSource = dt;
        }

        private bool IsDuplicateUser(string email, string contact, int? userId = null)
        {
            con.Open();
            string query;

            if (userId == null) // Add mode
            {
                query = "SELECT COUNT(*) FROM Users WHERE email = @Email OR contact = @Contact";
            }
            else // Edit mode
            {
                query = "SELECT COUNT(*) FROM Users WHERE (email = @Email OR contact = @Contact) AND user_id != @UserId";
            }

            SqlCommand checkCmd = new SqlCommand(query, con);
            checkCmd.Parameters.AddWithValue("@Email", email);
            checkCmd.Parameters.AddWithValue("@Contact", contact);

            if (userId != null)
            {
                checkCmd.Parameters.AddWithValue("@UserId", userId.Value);
            }

            int count = (int)checkCmd.ExecuteScalar();
            con.Close();

            return count > 0;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtboxsurname.Text) ||
                string.IsNullOrWhiteSpace(txtboxfName.Text) ||
                string.IsNullOrWhiteSpace(txtboxemail.Text) ||
                string.IsNullOrWhiteSpace(txtboxpassword.Text) ||
                string.IsNullOrWhiteSpace(txtboxcontact.Text) ||
                string.IsNullOrWhiteSpace(txtboxaddress.Text) ||
                string.IsNullOrWhiteSpace(txtboxrole.Text))
            {
                MessageBox.Show("Please fill in all fields before saving.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (IsDuplicateUser(txtboxemail.Text, txtboxcontact.Text))
            {
                MessageBox.Show("This user already exists (duplicate Email or Contact).", "Duplicate Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            con.Open();
            cmd = new SqlCommand("INSERT INTO Users (surname, firstname, email, password, contact, address, role) VALUES(@surname, @firstname, @email, @password, @contact, @address, @role)", con);
            cmd.Parameters.AddWithValue("@surname", txtboxsurname.Text);
            cmd.Parameters.AddWithValue("@firstname", txtboxfName.Text);
            cmd.Parameters.AddWithValue("@email", txtboxemail.Text);
            cmd.Parameters.AddWithValue("@password", txtboxpassword.Text);
            cmd.Parameters.AddWithValue("@contact", txtboxcontact.Text);
            cmd.Parameters.AddWithValue("@address", txtboxaddress.Text);
            cmd.Parameters.AddWithValue("@role", txtboxrole.Text);
            cmd.ExecuteNonQuery();
            con.Close();

            ClearFields();
            LoadUsers();

            MessageBox.Show("Information Saved!");
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtboxUserID.Text) ||
                string.IsNullOrWhiteSpace(txtboxsurname.Text) ||
                string.IsNullOrWhiteSpace(txtboxfName.Text) ||
                string.IsNullOrWhiteSpace(txtboxemail.Text) ||
                string.IsNullOrWhiteSpace(txtboxpassword.Text) ||
                string.IsNullOrWhiteSpace(txtboxcontact.Text) ||
                string.IsNullOrWhiteSpace(txtboxaddress.Text) ||
                string.IsNullOrWhiteSpace(txtboxrole.Text))
            {
                MessageBox.Show("Please fill in all fields before updating.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int userId = Convert.ToInt32(txtboxUserID.Text);

            if (IsDuplicateUser(txtboxemail.Text, txtboxcontact.Text, userId))
            {
                MessageBox.Show("Another user with the same Email or Contact already exists.", "Duplicate Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            con.Open();
            cmd = new SqlCommand("UPDATE Users SET surname = @surname, firstname = @firstname, email = @email, password = @password, contact = @contact, address = @address, role = @role WHERE user_id = @user_id", con);
            cmd.Parameters.AddWithValue("@surname", txtboxsurname.Text);
            cmd.Parameters.AddWithValue("@firstname", txtboxfName.Text);
            cmd.Parameters.AddWithValue("@email", txtboxemail.Text);
            cmd.Parameters.AddWithValue("@password", txtboxpassword.Text);
            cmd.Parameters.AddWithValue("@contact", txtboxcontact.Text);
            cmd.Parameters.AddWithValue("@address", txtboxaddress.Text);
            cmd.Parameters.AddWithValue("@role", txtboxrole.Text);
            cmd.Parameters.AddWithValue("@user_id", userId);
            cmd.ExecuteNonQuery();
            con.Close();

            ClearFields();
            LoadUsers();

            MessageBox.Show("Information Updated!");
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtboxUserID.Text))
            {
                MessageBox.Show("Please select a user to delete.", "No User Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to delete this user?",
                "Delete Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm == DialogResult.Yes)
            {
                con.Open();
                cmd = new SqlCommand("DELETE FROM Users WHERE user_id = @user_id", con);
                cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(txtboxUserID.Text));
                cmd.ExecuteNonQuery();
                con.Close();

                ClearFields();
                LoadUsers();

                MessageBox.Show("Information Deleted!");
            }
            else
            {
                MessageBox.Show("Deletion cancelled.");
            }
        }

        private void ClientsTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow row = ClientsTable.Rows[e.RowIndex];

                txtboxUserID.Text = row.Cells["user_id"].Value.ToString();
                txtboxsurname.Text = row.Cells["surname"].Value.ToString();
                txtboxfName.Text = row.Cells["firstname"].Value.ToString();
                txtboxemail.Text = row.Cells["email"].Value.ToString();
                txtboxpassword.Text = row.Cells["password"].Value.ToString();
                txtboxcontact.Text = row.Cells["contact"].Value.ToString();
                txtboxaddress.Text = row.Cells["address"].Value.ToString();
                txtboxrole.Text = row.Cells["role"].Value.ToString();
            }
        }

        private void ClearFields()
        {
            txtboxUserID.Text = "";
            txtboxsurname.Text = "";
            txtboxfName.Text = "";
            txtboxemail.Text = "";
            txtboxpassword.Text = "";
            txtboxcontact.Text = "";
            txtboxaddress.Text = "";
            txtboxrole.Text = "";
        }

    
        private void HomeButton_Click(object sender, EventArgs e)
        {
            MainAdminPage homeButton = new MainAdminPage();
            homeButton.Show();
            this.Hide();
        }

        private void PatientButton_Click(object sender, EventArgs e)
        {
            PatientsPage patientsPage = new PatientsPage();
            patientsPage.Show();
            this.Hide();
        }

        private void DoctorButton_Click(object sender, EventArgs e)
        {
            DoctorPage doctorsPage = new DoctorPage();
            doctorsPage.Show();
            this.Hide();
        }

        private void AvailabilityButton_Click(object sender, EventArgs e)
        {
            SchedulePage availabilityPage = new SchedulePage();
            availabilityPage.Show();
            this.Hide();
        }

        private void AppointmentButton_Click(object sender, EventArgs e)
        {
            AppointmentPage appointmentPage = new AppointmentPage();
            appointmentPage.Show();
            this.Hide();
        }

        private void ServicesButton_Click(object sender, EventArgs e)
        {
            ServicesPage servicesPage = new ServicesPage();
            servicesPage.Show();
            this.Hide();
        }

        private void ReportButton_Click(object sender, EventArgs e)
        {
            ReportPage reportsPage = new ReportPage();
            reportsPage.Show();
            this.Hide();
        }

        private void NotificationButton_Click(object sender, EventArgs e)
        {
            NotificationPage notificationPage = new NotificationPage();
            notificationPage.Show();
            this.Hide();
        }


    }

}

