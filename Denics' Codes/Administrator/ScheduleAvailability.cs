using System;
using System.Collections.Generic;
using System.Linq;


namespace Denics.Administrator
{
    public class ScheduleAvailability
    {
        private readonly AvailabilityRepository _repo;
        public ScheduleAvailability(AvailabilityRepository repo) => _repo = repo;

        public Dictionary<(string day, TimeSpan hour), AvailabilityEntry> BuildLookup(int doctorId, DateTime weekStart)
        {
            var list = _repo.GetByDoctorAndWeek(doctorId, weekStart);
            var dict = new Dictionary<(string, TimeSpan), AvailabilityEntry>();

            foreach (var e in list)
            {
                var day = e.Date.DayOfWeek.ToString();
                dict[(day, e.HourSlot)] = new AvailabilityEntry
                {
                    DoctorId = doctorId,
                    Date = e.Date,
                    HourSlot = e.HourSlot,
                    Status = e.Status,
                    IsBlocked = e.IsBlocked
                };
            }

            return dict;
        }

        public void SaveForDoctor(int doctorId, IEnumerable<AvailabilityEntry> rows) => _repo.ReplaceForDoctor(doctorId, rows);

        private static string NormalizeDay(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return null;
            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "sun", "Sunday" }, { "sunday", "Sunday" },
                { "mon", "Monday" }, { "monday", "Monday" },
                { "tue", "Tuesday" }, { "tues", "Tuesday" }, { "tuesday", "Tuesday" },
                { "wed", "Wednesday" }, { "wednesday", "Wednesday" },
                { "thu", "Thursday" }, { "thur", "Thursday" }, { "thursday", "Thursday" },
                { "fri", "Friday" }, { "friday", "Friday" },
                { "sat", "Saturday" }, { "saturday", "Saturday" }
            };
            return map.TryGetValue(raw.Trim(), out var t) ? t : null;
        }
    }
}