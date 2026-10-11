using System.ComponentModel.DataAnnotations.Schema;

namespace CareClinicMVC.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        [ForeignKey("Appointment")]
        public int AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }
        public string Diagnosis { get; set; }
        public string? PrescribedMedications { get; set; }
        public string? DoctorNotes { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
