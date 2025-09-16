using System;
using System.CodeDom;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Denics.Administrator
{
    public partial class DoctorPage : Form
    {
        // Update the connection string as needed for your environment
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Denics Project\\Denics' Database\\Denics_db.mdf\";Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd;
        public DoctorPage()
        {
            InitializeComponent();
            this.Load += Form1_Load; // Ensure the event is hooked up
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Doctors", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Error: " + ex.Message, "Database Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show("General Error: " + ex.Message, "Error");
            }
            finally
            {
                con.Close();
            }
        }
        private void Save_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("INSERT into Doctors VALUES(@full_name,@email,@phone_number)", con);
            cmd.Parameters.AddWithValue("@full_name", txtfname.Text);
            cmd.Parameters.AddWithValue("@email", txtemail.Text);
            cmd.Parameters.AddWithValue("@phone_number", txtpnum.Text);
            cmd.ExecuteNonQuery();
            con.Close();

            txtfname.Text = " ";
            txtemail.Text = " ";
            txtpnum.Text = " ";
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Doctors", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;

            MessageBox.Show("Information Saved !");
            con.Close();

        }

        private void Edit_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("UPDATE Doctors SET full_name = @full_name, email = @email, phone_number = @phone_number WHERE doctor_id = @doctor_id", con);
            cmd.Parameters.AddWithValue("@doctor_id", Convert.ToInt32(txtDocid.Text));
            cmd.Parameters.AddWithValue("@full_name", txtfname.Text);
            cmd.Parameters.AddWithValue("@email", txtemail.Text);
            cmd.Parameters.AddWithValue("@phone_number", txtpnum.Text);
            cmd.ExecuteNonQuery();
            con.Close();

            txtDocid.Text = " ";
            txtfname.Text = " ";
            txtemail.Text = " ";
            txtpnum.Text = " ";

            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Doctors", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;

            MessageBox.Show("Information Edited !");
        }

        private void Delete_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
          "Are you sure you want to delete this doctor's information?",
          "Confirm Deletion",
          MessageBoxButtons.YesNo,
          MessageBoxIcon.Warning
      );

            if (result == DialogResult.Yes)
            {
                con.Open();
                cmd = new SqlCommand("DELETE FROM Doctors WHERE doctor_id = @doctor_id", con);
                cmd.Parameters.AddWithValue("@doctor_id", Convert.ToInt32(txtDocid.Text));
                cmd.ExecuteNonQuery();
                con.Close();

                txtDocid.Text = "";
                txtfname.Text = "";
                txtemail.Text = "";
                txtpnum.Text = "";

                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Doctors", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();

                MessageBox.Show("Information Deleted!");
            }
            else
            {
                MessageBox.Show("Deletion cancelled.");
            }
        }
        private void MSchedule_Click(object sender, EventArgs e)
        {
            SchedulePage Schedule = new SchedulePage();
            Schedule.Show();
            this.Hide();
        }
    }
}

