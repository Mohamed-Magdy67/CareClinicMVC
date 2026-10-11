using System.ComponentModel.DataAnnotations;

namespace CareClinicMVC.Models
{
    public enum Gender{
        Male, Female
    }
    public class Patient
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string? BloodGroup { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
