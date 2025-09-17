namespace Denics.Models
{
    public class DoctorAvailability
    {
        public int DoctorId { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan HourSlot { get; set; }
        public string Status { get; set; }
    }
}