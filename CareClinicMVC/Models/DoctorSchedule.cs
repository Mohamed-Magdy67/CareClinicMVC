using System.ComponentModel.DataAnnotations.Schema;

namespace CareClinicMVC.Models
{
    public class DoctorSchedule
    {
        public int Id { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public int MaxPatientsPerDay { get; set; } = 20;
    }
}
