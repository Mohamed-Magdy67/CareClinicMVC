using CareClinicMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CareClinicMVC.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ClinicDbContext _context;
        // Inject the DbContext via constructor
        public PatientsController(ClinicDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string? searchTerm)
        {
            var query = _context.Patients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p => p.FullName.Contains(searchTerm) || p.Phone.Contains(searchTerm));
            }

            var patients = query.OrderByDescending(p => p.CreatedAt).ToList();
            ViewBag.CurrentSearch = searchTerm; // Keep search text in the input box
            return View(patients);
        }
        public IActionResult Details(int id)
        {
            Patient patient = _context.Patients
                .Include(p => p.Appointments)
                    .ThenInclude(a => a.Doctor)
                .Include(p => p.Appointments)
                    .ThenInclude(a => a.MedicalRecord)
                .FirstOrDefault(p => p.Id == id);

            if (patient == null)
            {
                return NotFound($"Patient with id = {id} is not found. TRY AGAIN");
            }
            return View(patient);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return View(patient);
            }
            _context.Patients.Add(patient);
            _context.SaveChanges();
            TempData["Success"] = "Patient registered successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Patient patient = _context.Patients.FirstOrDefault(p => p.Id == id);
            if (patient == null)
            {
                return NotFound($"Patient with id = {id} is not found.TRY AGAIN");
            }
            return View(patient);
        }
        [HttpPost]
        public IActionResult Edit(int id, Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return View(patient);
            }
            Patient patient1 = _context.Patients.FirstOrDefault(p => p.Id == id);
            if (patient1 == null)
            {
                return NotFound($"Patient with id = {id} is not found.TRY AGAIN");
            }
            patient1.FullName = patient.FullName;
            patient1.Address = patient.Address;
            patient1.Email = patient.Email;
            patient1.Phone = patient.Phone;
            patient1.DateOfBirth = patient.DateOfBirth;
            patient1.BloodGroup = patient.BloodGroup;
            patient1.Gender = patient.Gender;
            _context.SaveChanges();
            TempData["Success"] = "Patient profile updated successfully!";
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Patient patient = _context.Patients.FirstOrDefault(p=> p.Id == id);
            if (patient == null)
            {
                return NotFound("Patient is not found");
            }
            return View(patient);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Patient patient = _context.Patients.Find(id);
            if (patient == null)
            {
                return NotFound("Patient is not found");
            }
            _context.Patients.Remove(patient);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
