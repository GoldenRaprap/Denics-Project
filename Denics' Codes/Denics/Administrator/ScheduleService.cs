using Denics.Models;
using Denics.Repositories;

namespace Denics.Services
{
    public class ScheduleService
    {
        private readonly ScheduleRepository _repository = new ScheduleRepository();

        public List<DoctorAvailability> GetDoctorSchedule(int doctorId)
        {
            return _repository.GetSchedule(doctorId);
        }

        public void SaveAvailability(DoctorAvailability availability)
        {
            if (_repository.Exists(availability))
                _repository.Update(availability);
            else
                _repository.Insert(availability);
        }
    }
}
