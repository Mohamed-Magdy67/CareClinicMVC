using System.ComponentModel.DataAnnotations.Schema;

namespace CareClinicMVC.Models
{
    public enum AppointmentStatus
    {
        Scheduled,
        Completed,
        Cancelled,
        NoShow
    }
    public class Appointment
    {
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public DateTime AppointmentDate { get; set; }
        public string TimeSlot { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
        public string? ReasonForVisit { get; set; }
        public MedicalRecord? MedicalRecord { get; set; }

    }
}
