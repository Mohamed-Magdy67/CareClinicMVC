using CareClinicMVC.Models;

namespace CareClinicMVC.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalPatients { get; set; }
        public int TotalDoctors { get; set; }
        public int TotalAppointments { get; set; }
        public int TodayAppointmentsCount { get; set; }

        // Sum of ConsultationFee for all completed appointments
        public decimal TotalRevenue { get; set; }

        // Top 5 most recent appointments
        public List<Appointment> RecentAppointments { get; set; } = new List<Appointment>();
    }
}