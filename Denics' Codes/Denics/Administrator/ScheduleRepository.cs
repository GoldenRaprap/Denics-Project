using Denics.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Denics.Repositories
{
    public class ScheduleRepository
    {
        private readonly string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Denics Project\\Denics' Database\\Denics_db.mdf\";Integrated Security=True;Connect Timeout=30";

        public List<DoctorAvailability> GetSchedule(int doctorId)
        {
            var result = new List<DoctorAvailability>();
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "SELECT day_of_week, hour_slot, status FROM Availability WHERE doctor_id = @doctor_id";
                var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@doctor_id", doctorId);
                con.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["hour_slot"] != DBNull.Value)
                        {
                            result.Add(new DoctorAvailability
                            {
                                DoctorId = doctorId,
                                DayOfWeek = reader["day_of_week"].ToString(),
                                HourSlot = (TimeSpan)reader["hour_slot"],
                                Status = reader["status"].ToString()
                            });
                        }
                        else
                        {
                            // Optional: skip or log the null entry
                            Debug.WriteLine($"Skipped null hour_slot for doctor {doctorId} on {reader["day_of_week"]}");
                        }
                    }
                }
            }
            return result;
        }

        public bool Exists(DoctorAvailability availability)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM Availability WHERE doctor_id = @doctor_id AND day_of_week = @day AND hour_slot = @time";
                var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@doctor_id", availability.DoctorId);
                cmd.Parameters.AddWithValue("@day", availability.DayOfWeek);
                cmd.Parameters.AddWithValue("@time", availability.HourSlot);
                con.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public void Insert(DoctorAvailability availability)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Availability (doctor_id, day_of_week, hour_slot, status) VALUES (@doctor_id, @day, @time, @status)";
                var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@doctor_id", availability.DoctorId);
                cmd.Parameters.AddWithValue("@day", availability.DayOfWeek);
                cmd.Parameters.AddWithValue("@time", availability.HourSlot);
                cmd.Parameters.AddWithValue("@status", availability.Status);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(DoctorAvailability availability)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Availability SET status = @status WHERE doctor_id = @doctor_id AND day_of_week = @day AND hour_slot = @time";
                var cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@doctor_id", availability.DoctorId);
                cmd.Parameters.AddWithValue("@day", availability.DayOfWeek);
                cmd.Parameters.AddWithValue("@time", availability.HourSlot);
                cmd.Parameters.AddWithValue("@status", availability.Status);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}