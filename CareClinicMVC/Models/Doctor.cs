using System.ComponentModel.DataAnnotations.Schema;

namespace CareClinicMVC.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Specialization { get; set; }

        [Column(TypeName = "decimal(18,2)")]

        public decimal ConsultationFee { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<DoctorSchedule> Schedules { get; set; } = new List<DoctorSchedule>();
    }
}
