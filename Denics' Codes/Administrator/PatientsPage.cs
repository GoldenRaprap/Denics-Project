using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Denics.Administrator
{
    public partial class PatientsPage : Form
    {
        // Database connection
        static CallDatabase db = new CallDatabase();
        SqlConnection con = new SqlConnection(db.getDatabaseStringName());
        SqlCommand cmd;

        private int? selectedUserId = null;
        private DataTable usersTable; // cache the original table for filtering

        public PatientsPage()
        {
            InitializeComponent();
            this.Load += PatientsPage_Load;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Sidebar Click Events
            ReportButton.Click += ReportButton_Click;
            HomeButton.Click += HomeButton_Click;
            ServicesButton.Click += ServicesButton_Click;
            AvailabilityButton.Click += AvailabilityButton_Click;
            AppointmentButton.Click += AppointmentButton_Click;
            PatientButton.Click += PatientButton_Click;
            DoctorButton.Click += DoctorButton_Click;

            // Grid and search subscriptions
            ClientsTable.CellClick += ClientsTable_CellClick;
            txtboxsearch.TextChanged += txtboxsearch_TextChanged;
        }

        private void PatientsPage_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Users", con);
            usersTable = new DataTable();
            sda.Fill(usersTable);

            // Bind the full table first
            ClientsTable.DataSource = usersTable;

            // Hide sensitive/unused columns
            string[] hiddenColumns = { "user_id", "middlename", "suffix", "gender","birthdate", "password", "role" };
            foreach (string col in hiddenColumns)
            {
                if (ClientsTable.Columns.Contains(col))
                    ClientsTable.Columns[col].Visible = false;
            }

            ClientsTable.ClearSelection();
            selectedUserId = null;
        }

        private void txtboxsearch_TextChanged(object sender, EventArgs e)
        {
            string searchValue = txtboxsearch.Text.Trim();

            if (usersTable == null) return;

            // Create a view over the cached table to filter
            DataView dv = new DataView(usersTable);

            if (string.IsNullOrEmpty(searchValue))
            {
                dv.RowFilter = string.Empty;
            }
            else
            {
                string escaped = searchValue.Replace("'", "''");
                dv.RowFilter =
                    $"Convert(surname, 'System.String') LIKE '%{escaped}%' OR " +
                    $"Convert(firstname, 'System.String') LIKE '%{escaped}%' OR " +
                    $"Convert(email, 'System.String') LIKE '%{escaped}%' OR " +
                    $"Convert(contact, 'System.String') LIKE '%{escaped}%' OR " +
                    $"Convert(address, 'System.String') LIKE '%{escaped}%'";
            }

            // Bind the filtered view
            ClientsTable.DataSource = dv;
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

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddAppoinment addapp = new AddAppoinment();
            addapp.Show();
            this.Hide();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtboxsurname.Text) ||
                string.IsNullOrWhiteSpace(txtboxfName.Text) ||
                string.IsNullOrWhiteSpace(txtboxemail.Text) ||
                string.IsNullOrWhiteSpace(txtboxcontact.Text) ||
                string.IsNullOrWhiteSpace(txtboxaddress.Text) ||
                string.IsNullOrWhiteSpace(txtboxrole.Text) ||
                string.IsNullOrWhiteSpace(txtboxbday.Text))
            {
                MessageBox.Show("Please fill in all fields before updating.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedUserId == null)
            {
                MessageBox.Show("Please select a user to edit.", "No User Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (IsDuplicateUser(txtboxemail.Text, txtboxcontact.Text, selectedUserId.Value))
            {
                MessageBox.Show("Another user with the same Email or Contact already exists.", "Duplicate Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            con.Open();
            cmd = new SqlCommand(
                "UPDATE Users SET surname = @surname, firstname = @firstname, email = @email, contact = @contact, address = @address, role = @role WHERE user_id = @user_id", con);

            cmd.Parameters.AddWithValue("@surname", txtboxsurname.Text);
            cmd.Parameters.AddWithValue("@firstname", txtboxfName.Text);
            cmd.Parameters.AddWithValue("@email", txtboxemail.Text);
            cmd.Parameters.AddWithValue("@contact", txtboxcontact.Text);
            cmd.Parameters.AddWithValue("@address", txtboxaddress.Text);
            cmd.Parameters.AddWithValue("@role", txtboxrole.Text);
            cmd.Parameters.AddWithValue("@user_id", selectedUserId.Value);
            cmd.ExecuteNonQuery();
            con.Close();

            ClearFields();
            LoadUsers();

            MessageBox.Show("Information Updated!");
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (selectedUserId == null)
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
                cmd.Parameters.AddWithValue("@user_id", selectedUserId.Value);
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

                // Extract and store selected user_id
                selectedUserId = null;
                var idCell = row.Cells["user_id"]?.Value;
                if (idCell != null && int.TryParse(idCell.ToString(), out int id))
                    selectedUserId = id;

                // Populate form fields
                txtboxsurname.Text = row.Cells["surname"].Value?.ToString() ?? "";
                txtboxfName.Text = row.Cells["firstname"].Value?.ToString() ?? "";
                txtboxmName.Text = row.Cells["middlename"].Value?.ToString() ?? "";
                txtboxsuffix.Text = row.Cells["suffix"].Value?.ToString() ?? "";
                txtboxemail.Text = row.Cells["email"].Value?.ToString() ?? "";
                txtboxcontact.Text = row.Cells["contact"].Value?.ToString() ?? "";
                txtboxaddress.Text = row.Cells["address"].Value?.ToString() ?? "";
                txtboxrole.Text = row.Cells["role"].Value?.ToString() ?? "";
                txtboxbday.Text = row.Cells["birthdate"].Value?.ToString() ?? "";
            }
        }

        private void ClearFields()
        {
            selectedUserId = null;
            txtboxsurname.Text = "";
            txtboxfName.Text = "";
            txtboxmName.Text = "";
            txtboxsuffix.Text = "";
            txtboxemail.Text = "";
            txtboxcontact.Text = "";
            txtboxaddress.Text = "";
            txtboxrole.Text = "";
            txtboxbday.Text = "";
        }

        // Navigation handlers
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

        private void bntclear_Click(object sender, EventArgs e)
        {
            txtboxsearch.Text = string.Empty;
            txtboxsearch.Focus();
        }
    }
}
