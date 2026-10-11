using CareClinicMVC.Models;
using CareClinicMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace CareClinicMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ClinicDbContext _context;
        // Inject the DbContext via constructor
        public HomeController(ClinicDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            int TotalPatients = _context.Patients.Count();
            int TotalDoctors = _context.Doctors.Count();
            int TotalAppointments = _context.Appointments.Count();
            int TodayAppointments = _context.Appointments.Count(a => a.AppointmentDate.Date == DateTime.Today);

            decimal TodayRevenue = _context.Appointments
                .Where(a => a.Status == AppointmentStatus.Completed && a.Doctor != null)
                .Sum(a => a.Doctor.ConsultationFee);

            var recentapp = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.MedicalRecord)
                .OrderByDescending(a => a.AppointmentDate)
                .Take(5)
                .ToList();


            DashboardViewModel dashboardViewModel = new DashboardViewModel()
            {
                TotalPatients = TotalPatients,
                TotalDoctors = TotalDoctors,
                TotalAppointments = TotalAppointments,
                TodayAppointmentsCount = TodayAppointments,
                RecentAppointments = recentapp,
                TotalRevenue = TodayRevenue

            };
            return View(dashboardViewModel);
        }
    }
}
