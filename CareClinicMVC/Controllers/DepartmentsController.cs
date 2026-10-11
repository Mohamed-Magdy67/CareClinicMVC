using CareClinicMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CareClinicMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ClinicDbContext _context;
        // Inject the DbContext via constructor
        public DepartmentsController(ClinicDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Department> departments = _context.Departments.ToList();

            return View(departments);
        }

        public IActionResult Details(int id)
        {
            
            Department department = _context.Departments.Include(d => d.Doctors).FirstOrDefault(d => d.Id == id);
            if (department == null)
            {
                return NotFound("Department With that id is not found");
            }
            return View(department);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }
            _context.Departments.Add(department);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Department department = _context.Departments.Find(id);
            if (department == null)
            {
                return NotFound("Department is not Found");
            }
            return View(department);
        }
        [HttpPost]
        public IActionResult Edit(int id, Department department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }

            Department department1 = _context.Departments.Find(id);
            if (department1 == null)
            {
                return NotFound("Department is not Found");
            }

            department1.Name = department.Name;
            department1.Location = department.Location;
            department1.Description = department.Description;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Department department = _context.Departments.Find(id);
            if (department == null)
            {
                return NotFound("Department is not found");
            }
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Department department = _context.Departments.Find(id);
            if (department == null)
            {
                return NotFound("Department is not found");
            }
            _context.Departments.Remove(department);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
