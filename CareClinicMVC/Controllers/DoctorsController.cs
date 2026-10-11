using CareClinicMVC.Models;
using CareClinicMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CareClinicMVC.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly ClinicDbContext _context;
        // Inject the DbContext via constructor
        public DoctorsController(ClinicDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var doctors = _context.Doctors.Include(p => p.Department).ToList();
            return View(doctors);
        }

        public IActionResult Details(int id)
        {
            Doctor doctor = _context.Doctors.Include(p => p.Department)
                .Include(p => p.Schedules)
                .FirstOrDefault(d => d.Id == id);
            if (doctor == null)
            {
                return NotFound($"Doctor with id = {id} is not found. TRY AGAIN");
            }
            return View(doctor);
        }

        [HttpGet]
        public IActionResult Create()
        {
            DoctorFormViewModel viewModel = new DoctorFormViewModel();
            viewModel.DepartmentList = _context.Departments.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            }).ToList();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(DoctorFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.DepartmentList = _context.Departments.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                }).ToList();
                return View(viewModel);
            }
            Doctor doctor = new Doctor()
            {
                
                FullName = viewModel.FullName,
                Phone = viewModel.Phone,
                ConsultationFee = viewModel.ConsultationFee,
                Specialization = viewModel.Specialization,
                Email = viewModel.Email,
                DepartmentId = viewModel.DepartmentId

            };
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Doctor doctor = _context.Doctors.FirstOrDefault(p => p.Id == id);
            if (doctor == null)
            {
                return NotFound("Doctor is not Found");
            }
            DoctorFormViewModel model = new DoctorFormViewModel();
            model.Id = doctor.Id;
            model.FullName = doctor.FullName;
            model.Phone = doctor.Phone;
            model.Email = doctor.Email;
            model.Specialization = doctor.Specialization;
            model.ConsultationFee = doctor.ConsultationFee;
            model.DepartmentId = doctor.DepartmentId;
            model.DepartmentList = _context.Departments.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            }).ToList(); //  Populate the dropdown!
    

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(int id, DoctorFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.DepartmentList = _context.Departments.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                }).ToList();
                return View(model);
            }
            Doctor doctor = _context.Doctors.FirstOrDefault(d => d.Id == id);
            if (doctor == null)
            {
                return NotFound("Doctor is not found");
            }
            doctor.FullName = model.FullName;
            doctor.Phone = model.Phone;
            doctor.Email = model.Email;
            doctor.Specialization = model.Specialization;
            doctor.ConsultationFee = model.ConsultationFee;
            doctor.DepartmentId = model.DepartmentId;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            Doctor doctor = _context.Doctors.Include(d => d.Department).FirstOrDefault(d => d.Id == id);
            if (doctor == null)
            {
                return NotFound("Doctor is not found");
            }
            return View(doctor);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Doctor doctor = _context.Doctors.Find(id);
            if (doctor == null)
            {
                return NotFound("Doctor is not found");
            }
            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    


    }
}
