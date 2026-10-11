# 🏥 CareClinic MVC - Healthcare Management System

A full-stack clinic and patient management web application built with **ASP.NET Core (.NET 8)**, **Entity Framework Core**, and **SQL Server**. 

CareClinic streamlines the core operations of outpatient clinics—from patient registration and appointment scheduling to doctor consultations, printable medical prescriptions, and executive revenue analytics.

---

## 🌟 Key Features

### 1. 📊 Executive Operations Dashboard
* Real-time KPI statistics: **Total Patients**, **Active Doctors**, **Today's Visits**, and **Total Clinic Revenue ($)**.
* Dynamic calculation of revenue generated from completed consultations using LINQ aggregations.
* Live feed of recent appointments with one-click consultation and prescription shortcuts.

### 2. 📅 Smart Appointment Booking Engine
* Multi-entity appointment scheduling connecting **Patient**, **Doctor**, and **Department**.
* **Anti-Collision Conflict Detection:** Prevents double-booking doctors for the same date and consultation slot.
* Realistic clinical scheduling with 20-minute consultation intervals.
* Direct **"Book from Patient Profile"** shortcuts to streamline receptionist workflows.

### 3. 🩺 Doctor Consultation & Printable Prescriptions (Rx)
* Dedicated physician consultation workflow to record **Clinical Diagnosis**, **Prescribed Medications (Rx)**, and **Doctor Notes**.
* Automatic state transition: saving a consultation updates the appointment status to `Completed`.
* **Print-Friendly Prescription:** Generates an official prescription report formatted for direct browser printing (`Ctrl+P` / `window.print()`) with physician signature styling.

### 4. 🧑‍🤝‍🧑 Patient Registry & Medical Visit History
* Complete patient directory with real-time search filtering by **Name** or **Phone Number**.
* Comprehensive patient profile displaying contact details, blood group, and an interactive **Appointment History** timeline with direct links to past prescriptions.

### 5. 👨‍⚕️ Medical Staff & Departments Management
* Multi-department organization (Cardiology, Pediatrics, Dermatology, etc.).
* Doctor profiles with consultation fees, specializations, and departmental assignments.
* Strongly-typed ViewModels for foreign key dropdown bindings.

---

## 🛠️ Tech Stack

* **Framework:** ASP.NET Core MVC (.NET 8)
* **ORM:** Entity Framework Core 8 (Code-First)
* **Database:** Microsoft SQL Server
* **Frontend:** Razor Views, Bootstrap 5, ASP.NET Core Tag Helpers
* **Architecture:** MVC Pattern, ViewModel Segregation, Post-Redirect-Get (PRG)

---

## 🗄️ Database Architecture

The domain model contains 5 interconnected entities:

* **Department:** Clinic medical units (`Id`, `Name`).
* **Doctor:** Medical practitioners linked to departments (`FullName`, `Specialization`, `ConsultationFee`, `DepartmentId`).
* **Patient:** Patient demographic records (`FullName`, `Phone`, `Email`, `Gender`, `BloodGroup`, `DateOfBirth`).
* **Appointment:** Visit bookings (`PatientId`, `DoctorId`, `AppointmentDate`, `TimeSlot`, `Status`, `ReasonForVisit`).
* **MedicalRecord:** Consultation findings and prescriptions (`AppointmentId`, `Diagnosis`, `PrescribedMedications`, `DoctorNotes`, `FollowUpDate`).

> **Data Integrity:** Cascading deletes on appointments are configured with `DeleteBehavior.Restrict` via Fluent API to prevent accidental data loss.

---

## 🚀 Getting Started

### Prerequisites
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [SQL Server](https://www.microsoft.com/sql-server) (LocalDB, Express, or Developer Edition)
* Visual Studio 2022 or VS Code

### Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/Mohamed-Magdy67/CareClinicMVC.git
   cd CareClinicMVC
   ```

2. **Configure Database Connection:**
   Open `CareClinicMVC/appsettings.json` and adjust the connection string to match your SQL Server instance:
   ```json
   "ConnectionStrings": {
     "ClinicConnection": "Server=YOUR_SERVER_NAME;Database=CareClinicMVC;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

3. **Apply Database Migrations:**
   Run the following command from the project directory to create the database schema:
   ```bash
   dotnet ef database update --project CareClinicMVC
   ```

4. **Run the Application:**
   ```bash
   dotnet run --project CareClinicMVC
   ```
   Open your browser and navigate to `https://localhost:7148` (or the port specified in the terminal).

---

## 🔮 Upcoming Improvements

* [ ] Authentication & Authorization (ASP.NET Core Identity).
* [ ] Role-Based Access Control (Admin, Doctor, Receptionist).
* [ ] Advanced Reporting & PDF Exporting.
* [ ] Email & SMS Appointment Reminders.

---

## 👨‍💻 Author

**Mohamed Magdy**
* GitHub: [@Mohamed-Magdy67](https://github.com/Mohamed-Magdy67)
