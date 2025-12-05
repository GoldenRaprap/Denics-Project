using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Denics.Administrator
{
    public class DbConfig
    {
        public string ConnectionString { get; }
        public DbConfig(string connectionString) => ConnectionString = connectionString;
    }

    public class DoctorRepository
    {
        private readonly DbConfig _cfg;
        public DoctorRepository(DbConfig cfg) => _cfg = cfg;

        public List<Doctor> GetAll()
        {
            var list = new List<Doctor>();
            using (var conn = new SqlConnection(_cfg.ConnectionString))
            using (var cmd = new SqlCommand("SELECT doctor_id, full_name FROM Doctors ORDER BY full_name", conn))
            {
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                        list.Add(new Doctor { DoctorId = r.GetInt32(0), FullName = r.GetString(1) });
                }
            }
            return list;
        }
    }

    public class AvailabilityRepository
    {
        private readonly DbConfig _cfg;
        public AvailabilityRepository(DbConfig cfg) => _cfg = cfg;

        public List<AvailabilityEntry> GetByDoctorAndWeek(int doctorId, DateTime weekStart)
        {
            var list = new List<AvailabilityEntry>();
            DateTime weekEnd = weekStart.AddDays(6);

            using (var conn = new SqlConnection(_cfg.ConnectionString))
            using (var cmd = new SqlCommand(
                "SELECT date, hour_slot, status, is_blocked FROM Availability " +
                "WHERE doctor_id = @doc AND date BETWEEN @start AND @end ORDER BY date, hour_slot", conn))
            {
                cmd.Parameters.Add("@doc", SqlDbType.Int).Value = doctorId;
                cmd.Parameters.Add("@start", SqlDbType.Date).Value = weekStart;
                cmd.Parameters.Add("@end", SqlDbType.Date).Value = weekEnd;

                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        DateTime date = r.GetDateTime(0);
                        TimeSpan ts = r.IsDBNull(1) ? TimeSpan.Zero : r.GetTimeSpan(1);
                        string status = r.IsDBNull(2) ? "" : r.GetString(2);
                        bool isBlocked = !r.IsDBNull(3) && r.GetBoolean(3);

                        list.Add(new AvailabilityEntry
                        {
                            Date = date,
                            HourSlot = ts,
                            Status = status,
                            IsBlocked = isBlocked
                        });
                    }
                }
            }
            return list;
        }

        // Replace all availability rows for a doctor with the provided list
        public void ReplaceForDoctor(int doctorId, IEnumerable<AvailabilityEntry> rows)
        {
            using (var conn = new SqlConnection(_cfg.ConnectionString))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (var del = new SqlCommand("DELETE FROM Availability WHERE doctor_id = @doc", conn, tran))
                        {
                            del.Parameters.Add("@doc", SqlDbType.Int).Value = doctorId;
                            del.ExecuteNonQuery();
                        }

                        using (var ins = new SqlCommand(
                            "INSERT INTO Availability (doctor_id, date, hour_slot, status, is_blocked) VALUES (@doc,@date,@hour,@status,@isBlocked)",
                            conn, tran))
                        {
                            ins.Parameters.Add(new SqlParameter("@doc", SqlDbType.Int));
                            ins.Parameters.Add(new SqlParameter("@date", SqlDbType.Date));
                            ins.Parameters.Add(new SqlParameter("@hour", SqlDbType.Time));
                            ins.Parameters.Add(new SqlParameter("@status", SqlDbType.NVarChar, 200));
                            ins.Parameters.Add(new SqlParameter("@isBlocked", SqlDbType.Bit));

                            foreach (var r in rows)
                            {
                                if (string.IsNullOrWhiteSpace(r.Status) && !r.IsBlocked) continue;
                                ins.Parameters["@doc"].Value = doctorId;
                                ins.Parameters["@date"].Value = r.Date;
                                ins.Parameters["@hour"].Value = r.HourSlot;
                                ins.Parameters["@status"].Value = r.Status ?? "";
                                ins.Parameters["@isBlocked"].Value = r.IsBlocked ? 1 : 0;
                                ins.ExecuteNonQuery();
                            }
                        }

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}