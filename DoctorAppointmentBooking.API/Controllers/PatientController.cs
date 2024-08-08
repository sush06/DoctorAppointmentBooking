using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorAppointmentBooking.DataAccess.Models;
using DoctorAppointmentBooking.DataAccess.Providers;
using System.IO;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace DoctorAppointmentBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        IConfiguration _config;
        public PatientController(IConfiguration configuration)
        {
            _config = configuration;
        }
        [Route("InsertPatient")]
        [HttpPost]
        public APIResponse InsertPatient([FromForm] PatImage patImage)
        {
            try
            {
                using(var ms = new MemoryStream())
                {
                    HashingProvider hashingProvider = new HashingProvider();
                    patImage.PatientPassword = hashingProvider.GetHashedText(patImage.PatientPassword);
                    patImage.PatientImage.CopyTo(ms);
                    byte[] vs = ms.ToArray();
                    PatientProvider patientProvider = new PatientProvider();
                    patientProvider.InsertPatient(patImage.PatientEmail, patImage.PatientPassword, patImage.PatientName, patImage.Dob, patImage.Disease, vs);
                }
                
                
                return new APIResponse() { Status = "Success" };
            }
            catch
            {
                throw;
            }
        }
        //Working fine



        //public APIResponse LoginPatient(Pa patLogin)
        //{
        //    try
        //    {
        //        PatientProvider patientProvider = new PatientProvider();
        //        patientProvider.LoginPatient(patLogin.PatientEmail, patLogin.PatientPassword);
        //        return new APIResponse() { Status = "Success" };
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //} 


        //[AllowAnonymous]
        [Route("LoginPatient")]
        [HttpPost]
        public IActionResult LoginPatient(PatImage patImage)
        {
            try
            {
                //Authenticate
                Patient patient = Authenticate(patImage);
                //Generate Token
                //string token = GenerateToken(patient);
                return Ok(patient);
            }
            catch
            {
                return NotFound("User not found");
            }
        }
        private Patient Authenticate(PatImage patImage)
        {
            //send email and password to DataAccess and Validate 
            try
            {
                PatientProvider patientProvider = new PatientProvider();
                HashingProvider hashingProvider = new HashingProvider();
                patImage.PatientPassword = hashingProvider.GetHashedText(patImage.PatientPassword);
                var user = patientProvider.LoginPatient(patImage.PatientEmail, patImage.PatientPassword);
                if(user == null)
                {
                    throw new Exception("User Not Found");
                }
                else
                {
                    return user;
                }
                
            }
            catch
            {
                throw new Exception("User Not Found");
            }
        }

        [Route("PatientForgetPassword")]
        [HttpPost]
        public APIResponse ForgetPassword(Patient patient)
        {
            try
            {
                using (var dbContext = new DOCTORAPPOINTMENTBOOKINGContext())
                {
                    var user = (from b in dbContext.Patients
                                where b.PatientEmail == patient.PatientEmail
                                select b).FirstOrDefault();
                    if (user == null)
                    {
                        return new APIResponse() { Status = "Fail" };
                    }
                    else
                    {
                        return new APIResponse() { Status = "Success" };
                    }
                }
            }
            catch
            {
                throw;
            }
        }


        [Route("PatientResetPassword")]
        [HttpPost]
        public APIResponse ResetPassword(Patient patient)
        {
            try
            {
                using (var dbContext = new DOCTORAPPOINTMENTBOOKINGContext())
                {
                    var user = (from b in dbContext.Patients
                                where b.PatientEmail == patient.PatientEmail
                                select b).FirstOrDefault();
                    HashingProvider hashingProvider = new HashingProvider();
                    user.PatientPassword = hashingProvider.GetHashedText(patient.PatientPassword);
                    dbContext.SaveChanges();
                }
                return new APIResponse() { Status = "Success" };
            }
            catch
            {
                throw;
            }
        }
        //Working Fine...

        //private string GenerateToken(Patient patient)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.Email, patient.PatientEmail),
        //        new Claim(ClaimTypes.GivenName, patient.PatientName+""+patient.PatientImage),
        //        new Claim(ClaimTypes.NameIdentifier, patient.PatientPassword),
        //        new Claim(ClaimTypes.DateOfBirth, patient.Dob.ToString()),
        //        new Claim(ClaimTypes.Role, patient.Disease),
        //    };
        //    var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], (IEnumerable<Claim>)claims, null, DateTime.Now.AddMinutes(15), credential);
        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        [Route("DeletePatient")]
        [HttpPost]
        public APIResponse DeletePatient(Patient patient)
        {
            try
            {
                PatientProvider patientProvider = new PatientProvider();
                patientProvider.DeletePatient(patient.PatientEmail);
                return new APIResponse() { Status = "Success" };
            }
            catch
            {
                throw;
            }
        }
        //Working fine



        [Route("UpdatePatient")]
        [HttpPost]
        public APIResponse UpdatePatient([FromForm] PatImage patImage)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    HashingProvider hashingProvider = new HashingProvider();
                    patImage.PatientPassword = hashingProvider.GetHashedText(patImage.PatientPassword);
                    patImage.PatientImage.CopyTo(ms);
                    byte[] vs = ms.ToArray();
                    PatientProvider patientProvider = new PatientProvider();
                    patientProvider.UpdatePatient(patImage.PatientEmail, patImage.PatientPassword, patImage.PatientName, patImage.Dob, patImage.Disease, vs);
                }
                return new APIResponse() { Status = "Success" };
            }
            catch { throw; }
        }
        //Working Fine

        [Route("ViewPatient/{patientEmail}")]
        [HttpGet]
        public Patient GetPatient(string patientEmail)
        {
            try
            {
                PatientProvider patientProvider = new PatientProvider();
                return patientProvider.GetPatient(patientEmail);
            }
            catch
            {
                throw;
            }
        }
        //Working Fine

        [Route("ViewAppointment/{patientEmail}")]
        [HttpGet]
        public Appointment GetAppointment(string patientEmail)
        {
            try
            {
                AppointmentProvider appointmentProvider = new AppointmentProvider();
                return appointmentProvider.GetAppointment(patientEmail);
            }
            catch
            {
                throw;
            }
        }

        [Route("ListPatients")]
        [HttpGet]
        public List<Patient> ListPatients()
        {
            PatientProvider patientProvider = new PatientProvider();
            List<Patient> patients = patientProvider.ListPatients();
            return patients;
        }
        //Working fine


        [Route("BookAppointment")]
        [HttpPost]
        public APIResponse BookAppointment([FromForm] AppointmentImage appointmentImage)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    appointmentImage.MedicalReports.CopyTo(ms);
                    byte[] vs = ms.ToArray();
                    AppointmentProvider appointmentProvider = new AppointmentProvider();
                    appointmentProvider.BookAppointment(appointmentImage.PatientEmail, appointmentImage.DoctorName, appointmentImage.Day, appointmentImage.AppointmentMode, appointmentImage.Time.Value, vs);
                }
                return new APIResponse() { Status = "Success" };
            }
            catch
            {
                throw;
            }
        }
        //Working fine


        [Route("DeleteAppointment")]
        [HttpPost]
        public APIResponse DeleteAppointment(Appointment appointment)
        {
            try
            {
                AppointmentProvider appointmentProvider = new AppointmentProvider();
                appointmentProvider.DeleteAppointment(appointment.PatientEmail);
                return new APIResponse() { Status = "Success" };
            }
            catch
            {
                throw;
            }
        }


        [Route("UpdateAppointment")]
        [HttpPost]
        public APIResponse UpdateAppointment([FromForm] AppointmentImage appointmentImage)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    appointmentImage.MedicalReports.CopyTo(ms);
                    byte[] vs = ms.ToArray();
                    AppointmentProvider appointmentProvider = new AppointmentProvider();
                    appointmentProvider.UpdateAppointment(appointmentImage.PatientEmail, appointmentImage.DoctorName, appointmentImage.Day, appointmentImage.AppointmentMode, appointmentImage.Time.Value, vs);
                }
                return new APIResponse() { Status = "Success" };
            }
            catch
            { 
                throw; 
            }
        }



        [Route("BookAppointmentSuccessful")]
        [HttpPost]
        public APIResponse BookAppointmentSuccessful()
        {
            try
            {
                return new APIResponse() { Status = "Success" };
            }
            catch
            {
                throw;
            }
        }
        //Working Fine



        /*[Route("BookAppointment")]
        [HttpPost]
        public APIResponse BookAppointment(Appointment appointment)
        {
            try
            {
                AppointmentProvider appointmentProvider = new AppointmentProvider();
                appointmentProvider.BookAppointment(appointment.PatientEmail, appointment.DoctorName, appointment.Day, appointment.AppointmentMode, appointment.Time.Value, appointment.MedicalReports);
                return new APIResponse() { Status = "Success" };
            }
            catch
            {
                throw;
            }
           
        }*/
    }
}
