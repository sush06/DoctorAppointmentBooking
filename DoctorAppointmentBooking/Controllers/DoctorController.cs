using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorAppointmentBooking.DataAccess.Models;
using DoctorAppointmentBooking.DataAccess.Providers;
using Microsoft.AspNetCore.Http;

namespace DoctorAppointmentBooking.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            DoctorProvider doctorProvider = new DoctorProvider();
            List<Doctor> doctors = doctorProvider.ListDoctors();
            return View(doctors);
        }

        [Route("/Doctor/Detail/{doctorName}")]
        public IActionResult Detail(string doctorName)
        {
            DoctorProvider doctorProvider = new DoctorProvider();
            Doctor doctor = doctorProvider.GetDoctor(doctorName);
            return View(doctor);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            Doctor doctor = new Doctor();
            return View(doctor);
        }
        //[HttpPost]
        //public IActionResult Insert(Doctor doctor)
        //{
        //    DoctorProvider doctorProvider = new DoctorProvider();
        //    bool rslt = doctorProvider.InsertDoctor(doctor.DoctorEmail, doctor.DoctorPassword, doctor.DoctorName, doctor.Dob.Value , doctor.Specialization, doctor.Experience, doctor.DoctorImage);
        //    return View("Login");
        //}

        [HttpGet]
        public IActionResult Login()
        {
            Doctor doctor = new Doctor();
            return View(doctor);
        }
        [HttpPost]
        public IActionResult Login(Doctor doctor)
        { 
            DoctorProvider doctorProvider = new DoctorProvider();
            Doctor doctoroutput = doctorProvider.LoginDoctor(doctor.DoctorEmail, doctor.DoctorPassword);
            if (doctoroutput == null)
            {
                return View("Message");
            }
            else
            {
                HttpContext.Session.SetString("DoctorName", doctoroutput.DoctorName);
                return View("Homepage", doctoroutput);
            }
        }

        [HttpGet]
        [Route("/Doctor/Edit/{doctorName}")]
        public IActionResult Edit(string doctorName)
        {
            DoctorProvider doctorProvider = new DoctorProvider();
            Doctor doctor = doctorProvider.GetDoctor(doctorName);
            return View(doctor);
        }
        //[HttpPost]
        //public IActionResult Edit(Doctor doctor)
        //{
        //    DoctorProvider doctorProvider = new DoctorProvider();
        //    Doctor doctoroutput = doctorProvider.UpdateDoctor(doctor.DoctorEmail, doctor.DoctorPassword, doctor.DoctorName, doctor.Dob.Value, doctor.Specialization, doctor.Experience, doctor.DoctorImage);
        //    HttpContext.Session.SetString("DoctorName", doctoroutput.DoctorName);
        //    return View("Message2");
        //}

        [HttpGet]
        [Route("/Doctor/Delete/{doctorName}")]
        public IActionResult Delete(string doctorName)
        {
            DoctorProvider doctorProvider = new DoctorProvider();
            Doctor doctoroutput = doctorProvider.DeleteDoctor(doctorName);
            HttpContext.Session.SetString("DoctorName", doctoroutput.DoctorName);
            return View("DeleteDoctor");
        }

        [HttpGet]
        public IActionResult ViewAppointments(Appointment appointment)
        {
            DoctorProvider doctorProvider=new DoctorProvider();
            //bool rslt=doctorProvider.ViewAppointments(0).Value();
            return View();
        }

        [HttpGet]
        public IActionResult Homepage()
        {
            string doctorname = HttpContext.Session.GetString("DoctorName");
            Doctor doctor = new Doctor();
            doctor.DoctorName = doctorname;
            return View(doctor);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }
    }
}
