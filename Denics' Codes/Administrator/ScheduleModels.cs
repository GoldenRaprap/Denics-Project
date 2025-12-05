namespace Denics.Administrator
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; }
    }

    public class AvailabilityEntry
    {
        public int DoctorId { get; set; }  
        public TimeSpan HourSlot { get; set; } 
        public string Status { get; set; }   
        public bool IsBlocked { get; set; }
        public DateTime Date { get; set; }
    }
}