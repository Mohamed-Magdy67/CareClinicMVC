using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using CareClinicMVC.Models;

namespace CareClinicMVC.ViewModels
{
    public class AppointmentFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select a patient")]
        [Display(Name = "Patient")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Please select a doctor")]
        [Display(Name = "Doctor")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Please choose an appointment date")]
        [DataType(DataType.Date)]
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; } = DateTime.Today.AddDays(1);

        [Required(ErrorMessage = "Please select a time slot")]
        [Display(Name = "Time Slot")]
        public string TimeSlot { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

        [MaxLength(250)]
        [Display(Name = "Reason for Visit")]
        public string? ReasonForVisit { get; set; }

        // Dropdowns data
        public IEnumerable<SelectListItem>? PatientList { get; set; }
        public IEnumerable<SelectListItem>? DoctorList { get; set; }
        public IEnumerable<SelectListItem>? TimeSlotList { get; set; }
    }
}