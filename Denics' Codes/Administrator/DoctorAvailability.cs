namespace Denics.Administrator
{
    public class DoctorAvailability
    {
        public int DoctorId { get; set; }
        public TimeSpan HourSlot { get; set; }
        public string Status { get; set; }
    }
}