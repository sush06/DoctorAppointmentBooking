using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorAppointmentBooking.DataAccess.Models;
using DoctorAppointmentBooking.DataAccess.Providers;
using System.IO;

namespace DoctorAppointmentBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        [Route("InsertDoctor")]
        [HttpPost]
        public APIResponse InsertDoctor([FromForm] DocImage docImage)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    HashingProvider hashingProvider = new HashingProvider();
                    docImage.DoctorPassword = hashingProvider.GetHashedText(docImage.DoctorPassword);
                    docImage.DoctorImage.CopyTo(ms);
                    byte[] vs = ms.ToArray();
                    DoctorProvider doctorProvider = new DoctorProvider();
                    doctorProvider.InsertDoctor(docImage.DoctorEmail, docImage.DoctorPassword, docImage.DoctorName, docImage.Dob.Value, docImage.Specialization, docImage.Experience, docImage.Hospital, docImage.Day, docImage.StartTime.Value, docImage.EndTime.Value , vs);
                }
                return new APIResponse() { Status = "Success" };
            }
            catch
            {
                throw;
            }
        }
        //Working fine

        [Route("LoginDoctor")]
        [HttpPost]
        public IActionResult LoginDoctor(DocImage docImage)
        {
            try
            {
                Doctor doctor = Authenticate(docImage);
                return Ok(doctor);
            }
            catch
            {
                return NotFound("User Not Found");
            }
        }
        //Working Fine

        private Doctor Authenticate(DocImage docImage)
        {
            try
            {
                DoctorProvider doctorProvider = new DoctorProvider();
                HashingProvider hashingProvider = new HashingProvider();
                docImage.DoctorPassword = hashingProvider.GetHashedText(docImage.DoctorPassword);
                var user = doctorProvider.LoginDoctor(docImage.DoctorEmail, docImage.DoctorPassword);
                if (user == null)
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

        [Route("DoctorForgetPassword")]
        [HttpPost]
        public APIResponse ForgetPassword(Doctor doctor)
        {
            try
            {
                using (var dbContext = new DOCTORAPPOINTMENTBOOKINGContext())
                {
                    var user = (from b in dbContext.Doctors
                                where b.DoctorEmail == doctor.DoctorEmail
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
        //Working fine


        [Route("DoctorResetPassword")]
        [HttpPost]
        public APIResponse ResetPassword(Doctor doctor)
        {
            try
            {
                using (var dbContext = new DOCTORAPPOINTMENTBOOKINGContext())
                {
                    var user = (from b in dbContext.Doctors
                                where b.DoctorEmail == doctor.DoctorEmail
                                select b).FirstOrDefault();
                    HashingProvider hashingProvider = new HashingProvider();
                    user.DoctorPassword = hashingProvider.GetHashedText(doctor.DoctorPassword);
                    dbContext.SaveChanges();
                }
                return new APIResponse() { Status = "Success" };
            }
            catch
            {
                throw;
            }
        }
        //Working Fine..

        [Route("ViewDoctor/{doctorEmail}")]
        [HttpGet]
        public Doctor GetDoctor(string doctorEmail)
        {
            try
            {
                DoctorProvider doctorProvider = new DoctorProvider();
                return doctorProvider.GetDoctor(doctorEmail);
            }
            catch
            {
                throw;
            }
        }
        //Working fine


        [Route("DeleteDoctor")]
        [HttpPost]
        public APIResponse DeleteDoctor(Doctor doctor)
        {
            try
            {
                DoctorProvider doctorProvider = new DoctorProvider();
                doctorProvider.DeleteDoctor(doctor.DoctorEmail);
                return new APIResponse() { Status = "Success" };
            }
            catch
            {
                throw;
            }
        }
        //Working fine


        [Route("ListDoctors")]
        [HttpGet]
        public List<Doctor> ListDoctors()
        {
            DoctorProvider doctorProvider = new DoctorProvider();
            List<Doctor> doctors = doctorProvider.ListDoctors();
            return doctors;
        }
        //Working Fine

        [Route("UpdateDoctor")]
        [HttpPost]
        public APIResponse UpdateDoctor([FromForm] DocImage docImage)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    HashingProvider hashingProvider = new HashingProvider();
                    docImage.DoctorPassword = hashingProvider.GetHashedText(docImage.DoctorPassword);
                    docImage.DoctorImage.CopyTo(ms);
                    byte[] vs = ms.ToArray();
                    DoctorProvider doctorProvider = new DoctorProvider();
                    doctorProvider.UpdateDoctor(docImage.DoctorEmail, docImage.DoctorPassword, docImage.DoctorName, docImage.Dob.Value, docImage.Specialization, docImage.Experience, docImage.Hospital, docImage.Day, docImage.StartTime.Value, docImage.EndTime.Value, vs);
                }
                return new APIResponse() { Status = "Success" };
            }
            catch  { throw; }
        }
        //Working fine...


        /*[Route("UpdateDoctor")]
        [HttpPost]
        public APIResponse UpdateDoctor(Doctor doctor)
        {
            try
            {
                DoctorProvider doctorProvider = new DoctorProvider();
                //doctorProvider.GetDoctor(doctor.DoctorEmail);
                doctorProvider.UpdateDoctor(doctor.DoctorEmail, doctor.DoctorPassword, doctor.DoctorName, doctor.Dob.Value, doctor.Specialization, doctor.Experience, doctor.DoctorImage);
                return new APIResponse() { Status = "Success" };
            }
            catch
            {
                throw;
            }
            
        }*/

        [Route("ListHospitals")]
        [HttpGet]
        //public List<Hospital> ListHospitals()
        //{
        //    DoctorProvider doctorProvider = new DoctorProvider();
        //    List<Hospital> hospitals = doctorProvider.ListHospitals();
        //    return hospitals;
        //}
        //Working Fine

        [Route("DoctorHospitalInsert")]
        [HttpPost]
        public APIResponse DoctorHospitalInsert(DocHospital docHospital)
        {
            try
            {
                DoctorProvider doctorProvider = new DoctorProvider();
                doctorProvider.DoctorHospitalInsert(docHospital.DoctorEmail, docHospital.HospitalName);
                return new APIResponse() { Status = "Success" };
            }
            catch
            {
                throw;
            }
        }

        [Route("GetProfilePicture/{doctorEmail}")]
        [HttpGet]
        public APIResponse GetProfilePicture ([FromForm] string doctorEmail)
        {
            try
            {

                using(var ms = new MemoryStream())
                {
                    DocDP docDP = new DocDP();
                    docDP.DoctorImage.CopyTo(ms);
                    byte[] vs = ms.ToArray();
                    DoctorProvider doctorProvider = new DoctorProvider();
                    doctorProvider.GetProfilePicture(docDP.DoctorEmail, vs);
                }
                return new APIResponse() { Status = "Success" };
            }
            catch
            {
                throw;
            }
           
        }


    }
}
