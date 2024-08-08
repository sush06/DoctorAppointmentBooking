using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorAppointmentBooking.DataAccess.Providers;
using DoctorAppointmentBooking.DataAccess.Models;
using DoctorAppointmentBooking.Models;
using Microsoft.AspNetCore.Http;

namespace DoctorAppointmentBooking.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            PatientProvider patientProvider = new PatientProvider();
            List<Patient> patients = patientProvider.ListPatients();
            return View(patients);
        }

        [Route("/Patient/Detail/{patientName}")]
        public IActionResult Detail(string patientName)
        {
            PatientProvider patientProvider = new PatientProvider();
            Patient patient = patientProvider.GetPatient(patientName);
            return View(patient);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            Patient patient = new Patient();
            return View(patient);
        }
        [HttpPost]
        public IActionResult Insert(Patient patient)
        {
            PatientProvider patientProvider = new PatientProvider();
            bool rslt = patientProvider.InsertPatient(patient.PatientEmail, patient.PatientPassword, patient.PatientName, patient.Dob, patient.Disease, patient.PatientImage);
            return View("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            Patient patient = new Patient();
            return View();
        }
        [HttpPost]
        public IActionResult Login(Patient patient)
        {
            PatientProvider patientProvider = new PatientProvider();
            Patient patientoutput = patientProvider.LoginPatient(patient.PatientEmail, patient.PatientPassword);
            if (patientoutput == null)
            {
                return View("Message");
            }
            else
            {
                HttpContext.Session.SetString("PatientName", patientoutput.PatientName);
                HttpContext.Session.SetInt32("PatientID", patientoutput.PatientId);
                return View("Homepage", patientoutput);
            }
        }
        

        [HttpGet]
        [Route("/Patient/Edit/{patientName}")]
        public IActionResult Edit(string patientName)
        {
            PatientProvider patientProvider = new PatientProvider();
            Patient patient = patientProvider.GetPatient(patientName);
            return View(patient);
        }
        //[HttpPost]
        // public IActionResult Edit(Patient patient)
        //{
        //    PatientProvider patientProvider = new PatientProvider();
        //    bool rslt = patientProvider.UpdatePatient(patient.PatientEmail, patient.PatientPassword, patient.PatientName, patient.Dob, patient.Disease, patient.PatientImage);
        //    return View("Message2");
        //}
   
        [HttpGet]
        [Route("/Patient/Delete/{patientName}")]
        public IActionResult Delete(string patientName)
        {
            PatientProvider patientProvider = new PatientProvider();
            bool rslt = patientProvider.DeletePatient(patientName);
            return View("DeletePatient");
        }

        [HttpGet]
        public IActionResult BookAppointment()
        {
            Appointment appointment = new Appointment();
            BookAppointmentViewModel bookAppointmentViewModel = new BookAppointmentViewModel();
            DoctorProvider doctorProvider = new DoctorProvider();
            bookAppointmentViewModel.Doctors = doctorProvider.ListDoctors();
            return View(bookAppointmentViewModel);
        }
        //[HttpPost]
        //public IActionResult BookAppointment(Appointment appointment)
        //{
        //    //Appointment.PatientID == Get PatientID from Session
        //    appointment.PatientId = HttpContext.Session.GetInt32("PatientID").Value;
        //    AppointmentProvider appointmentProvider= new AppointmentProvider();
        //    bool result = appointmentProvider.BookAppointment(appointment);
        //    if(result)
        //    {
        //        return View("SuccessfulMessage");
        //    }
        //    else
        //    {
        //        return View("UnsuccessfulMessage");
        //    }
        //}

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }

        [HttpGet]
        public IActionResult Homepage()
        {
            string patientname = HttpContext.Session.GetString("PatientName");
            Patient patient = new Patient();
            patient.PatientName = patientname;
            return View(patient);
        }

        public IActionResult ViewAppointments()
        {
            Appointment appointment = new Appointment();
            Doctor doctor = new Doctor();
            DoctorProvider doctorProvider = new DoctorProvider();
            BookAppointmentViewModel bookAppointmentViewModel = new BookAppointmentViewModel();
            AppointmentProvider appointmentProvider = new AppointmentProvider();
            bookAppointmentViewModel.Appointments = appointmentProvider.ListAppointments();
            string doctorName = doctor.DoctorName;
            return View(bookAppointmentViewModel);
        }


        //[Route("/Patient/DeleteAppointment/{appointmentId}")]
        //public IActionResult DeleteAppointment(int appointmentId)
        //{
        //    AppointmentProvider appointmentProvider = new AppointmentProvider();
        //    bool rslt = appointmentProvider.DeleteAppointment(appointmentId);
        //    if(rslt)
        //    {
        //        return View("DeleteAppointment");
        //    }
        //    else
        //    {
        //        return View("Message5");
        //    }
            
        //}

        //[HttpGet]
        //[Route("/Patient/EditAppointment/{appointmentId}")]
        //public IActionResult EditAppointment(int appointmentId)
        //{
        //    AppointmentProvider appointmentProvider = new AppointmentProvider();
        //    Appointment appointment = appointmentProvider.GetAppointment(appointmentId);
        //    return View(appointment);
        //}

        //[HttpPost]
        //public IActionResult EditAppointment(Appointment appointment)
        //{
        //    AppointmentProvider appointmentProvider = new AppointmentProvider();
        //    bool rslt = appointmentProvider.UpdateAppointment(appointment.AppointmentId, appointment.DoctorId, appointment.DateTime.Value, appointment.MedicalReports, appointment.AppointmentMode);
        //    if(rslt)
        //    {
        //        return View("Message4");
        //    }
        //    else
        //    {
        //        return View("Message4");
        //    }
            
        //}
    }
}
