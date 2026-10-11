using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CareClinicMVC.ViewModels
{
    public class DoctorFormViewModel
    {
        public int Id { get; set; } // Needed for Edit

        [Required(ErrorMessage = "Doctor's name is required")]
        [Display(Name = "Full Name")]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Specialization is required")]
        public string Specialization { get; set; } = string.Empty;

        [Required(ErrorMessage = "Consultation fee is required")]
        [Range(0, 10000, ErrorMessage = "Fee must be between 0 and 10,000")]
        [Display(Name = "Consultation Fee (£)")]
        public decimal ConsultationFee { get; set; }

        [Required(ErrorMessage = "Please select a department")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        // This list will populate the <select> dropdown in the Razor View
        public IEnumerable<SelectListItem>? DepartmentList { get; set; }
    }
}