using CareClinicMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CareClinicMVC.Controllers
{
    public class MedicalRecordsController : Controller
    {
        private readonly ClinicDbContext _context;
        // Inject the DbContext via constructor
        public MedicalRecordsController(ClinicDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create(int appointmentId)
        {
            // 1. Fetch appointment with Patient, Doctor, and existing MedicalRecord
            var appointment = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.MedicalRecord)
                .FirstOrDefault(a => a.Id == appointmentId);

            // 2. Guard: Check if appointment exists
            if (appointment == null)
            {
                return NotFound($"Appointment with id = {appointmentId} is not found.");
            }

            // 3. Guard: If consultation already completed, redirect to the existing record
            if (appointment.MedicalRecord != null)
            {
                TempData["Error"] = "This appointment already has a completed medical record.";
                return RedirectToAction(nameof(Details), new { id = appointment.MedicalRecord.Id });
            }

            // 4. Create blank record tied to this appointment
            var medicalRecord = new MedicalRecord
            {
                AppointmentId = appointmentId,
                Appointment = appointment
            };

            return View(medicalRecord);
        }
        [HttpPost]
        public IActionResult Create(MedicalRecord medicalRecord)
        {

            if (!ModelState.IsValid)
            {
                medicalRecord.Appointment = _context.Appointments
                    .Include(a => a.Patient)
                    .Include(a => a.Doctor)
                    .FirstOrDefault(a => a.Id == medicalRecord.AppointmentId);

                return View(medicalRecord);
            }
            //MedicalRecord medicalRecord1 = new MedicalRecord()
            //{
            //    Diagnosis = medicalRecord.Diagnosis,
            //    FollowUpDate= medicalRecord.FollowUpDate,
            //    Appointment = medicalRecord.Appointment,
            //    DoctorNotes = medicalRecord.DoctorNotes,
            //    PrescribedMedications = medicalRecord.PrescribedMedications,
            //    AppointmentId = medicalRecord.AppointmentId

            //};
            _context.MedicalRecords.Add(medicalRecord);
            Appointment appointment = _context.Appointments.Find(medicalRecord.AppointmentId);
            appointment.Status = AppointmentStatus.Completed;
            _context.SaveChanges();
             TempData["Success"] = "Consultation completed and medical record saved!";
            return RedirectToAction(nameof(Details), new { id = medicalRecord.Id });

        }
        [HttpGet]
        public IActionResult Details (int id)
        {
            MedicalRecord medicalRecord = _context.MedicalRecords.Include(a => a.Appointment).ThenInclude(a => a.Patient)
                .Include(a => a.Appointment).ThenInclude(a => a.Doctor).FirstOrDefault( a => a.Id ==id);

            return View(medicalRecord);

        }
    }
}
