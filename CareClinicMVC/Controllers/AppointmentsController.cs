using CareClinicMVC.Models;
using CareClinicMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CareClinicMVC.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ClinicDbContext _context;

        public AppointmentsController(ClinicDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var appointments = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                    .ThenInclude(d => d.Department)
                .Include(a => a.MedicalRecord)
                .OrderByDescending(a => a.AppointmentDate)
                .ToList();

            return View(appointments);
        }

        // SHOW  BLANK BOOKING FORM 
        [HttpGet]
        public IActionResult Create(int? patientId)
        {
            var model = new AppointmentFormViewModel();

            if (patientId.HasValue)
            {
                model.PatientId = patientId.Value; // Pre-select the patient!
            }

            PopulateDropdowns(model);
            return View(model);
        }

        //  PROCESS THE BOOKING and save in database
        [HttpPost]
        public IActionResult Create(AppointmentFormViewModel model)
        {
            //  Date cannot be in the past
            if (model.AppointmentDate.Date < DateTime.Today)
            {
                ModelState.AddModelError("AppointmentDate", "Appointment date cannot be in the past.");
            }

            // Business Rule 2: Prevent Double-Booking for the same doctor at the same time
            bool isConflict = _context.Appointments.Any(a =>
                a.DoctorId == model.DoctorId &&
                a.AppointmentDate.Date == model.AppointmentDate.Date &&
                a.TimeSlot == model.TimeSlot &&
                a.Status != AppointmentStatus.Cancelled);

            if (isConflict)
            {
                ModelState.AddModelError("TimeSlot", "This doctor already has a booked appointment at this date and time slot.");
            }

            if (!ModelState.IsValid)
            {
                // Re-populate dropdowns so the form doesn't crash
                PopulateDropdowns(model);
                return View(model);
            }

            var appointment = new Appointment
            {
                PatientId = model.PatientId,
                DoctorId = model.DoctorId,
                AppointmentDate = model.AppointmentDate,
                TimeSlot = model.TimeSlot,
                ReasonForVisit = model.ReasonForVisit,
                Status = AppointmentStatus.Scheduled
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            TempData["Success"] = "Appointment booked successfully!";
            return RedirectToAction(nameof(Index));
        }

        //  CANCEL APPOINTMENT (QUICK ACTION)
        [HttpPost]
        public IActionResult Cancel(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                appointment.Status = AppointmentStatus.Cancelled;
                _context.SaveChanges();
                TempData["Success"] = "Appointment cancelled successfully.";
            }
            return RedirectToAction(nameof(Index));
        }

        //  HELPER METHOD TO POPULATE ALL 3 DROPDOWNS
        private void PopulateDropdowns(AppointmentFormViewModel model)
        {
            // 1. Patients Dropdown
            model.PatientList = _context.Patients
                .OrderBy(p => p.FullName)
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{p.FullName} ({p.Phone})"
                }).ToList();

            // 2. Doctors Dropdown (with Department name)
            model.DoctorList = _context.Doctors
                .Include(d => d.Department)
                .OrderBy(d => d.FullName)
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = $"Dr. {d.FullName} - {d.Specialization} ({d.Department.Name})"
                }).ToList();

            // 3. Clinic 20-Minute Consultation Time Slots
            model.TimeSlotList = new List<SelectListItem>
            {
                new SelectListItem { Value = "09:00 AM - 09:20 AM", Text = "09:00 AM - 09:20 AM" },
                new SelectListItem { Value = "09:20 AM - 09:40 AM", Text = "09:20 AM - 09:40 AM" },
                new SelectListItem { Value = "09:40 AM - 10:00 AM", Text = "09:40 AM - 10:00 AM" },
                new SelectListItem { Value = "10:00 AM - 10:20 AM", Text = "10:00 AM - 10:20 AM" },
                new SelectListItem { Value = "10:20 AM - 10:40 AM", Text = "10:20 AM - 10:40 AM" },
                new SelectListItem { Value = "10:40 AM - 11:00 AM", Text = "10:40 AM - 11:00 AM" },
                new SelectListItem { Value = "11:00 AM - 11:20 AM", Text = "11:00 AM - 11:20 AM" },
                new SelectListItem { Value = "11:20 AM - 11:40 AM", Text = "11:20 AM - 11:40 AM" },
                new SelectListItem { Value = "11:40 AM - 12:00 PM", Text = "11:40 AM - 12:00 PM" },
                new SelectListItem { Value = "01:00 PM - 01:20 PM", Text = "01:00 PM - 01:20 PM" },
                new SelectListItem { Value = "01:20 PM - 01:40 PM", Text = "01:20 PM - 01:40 PM" },
                new SelectListItem { Value = "01:40 PM - 02:00 PM", Text = "01:40 PM - 02:00 PM" },
                new SelectListItem { Value = "02:00 PM - 02:20 PM", Text = "02:00 PM - 02:20 PM" },
                new SelectListItem { Value = "02:20 PM - 02:40 PM", Text = "02:20 PM - 02:40 PM" },
                new SelectListItem { Value = "02:40 PM - 03:00 PM", Text = "02:40 PM - 03:00 PM" }
            };
        }
    }
}