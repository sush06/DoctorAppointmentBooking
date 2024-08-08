using DoctorAppointmentBooking.DataAccess.Models;
using DoctorAppointmentBooking.DataAccess.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAppointmentBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        //[Route("BookAppointment")]
        //[HttpPost]
        //public APIResponse BookAppointment(Appointment appointment)
        //{
        //    try
        //    {
        //        AppointmentProvider appointmentProvider = new AppointmentProvider();
        //        appointmentProvider.BookAppointment(appointment.PatientEmail,appointment.DoctorName, appointment.Day, appointment.AppointmentMode, appointment.Time.Value, appointment.MedicalReports);
        //        return new APIResponse() { Status = "Success" };
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        //Working fine


        //[Route("UpdateAppointment")]
        //[HttpPost]
        //public APIResponse UpdateAppointment(Appointment appointment)
        //{
        //    try
        //    {

        //        AppointmentProvider appointmentProvider = new AppointmentProvider();
        //        appointmentProvider.UpdateAppointment(appointment.AppointmentId, appointment.DoctorId, appointment.DateTime.Value, appointment.MedicalReports, appointment.AppointmentMode);
        //        return new APIResponse() { Status = "Success" };
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        //Working Fine

        //[Route("DeleteAppointment")]
        //[HttpPost]
        //public APIResponse DeleteAppointment(Appointment appointment)
        //{
        //    try
        //    {
        //        AppointmentProvider appointmentProvider = new AppointmentProvider();
        //        appointmentProvider.DeleteAppointment(appointment.AppointmentId);
        //        return new APIResponse() { Status = "Success" };
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        //Working Fine

        [Route("ListAppointments")]
        [HttpGet]
        public List<Appointment> ListAppointments()
        {
            AppointmentProvider appointmentProvider = new AppointmentProvider();
            List<Appointment> appointments = appointmentProvider.ListAppointments();
            return appointments;
        }
        //Working fine

        //[Route("ViewAppointment/{appointmentId}")]
        //[HttpGet]
        //public APIResponse GetAppointment(int appointmentId)
        //{
        //    AppointmentProvider appointmentProvider = new AppointmentProvider();
        //    appointmentProvider.GetAppointment(appointmentId);
        //    return new APIResponse() { Status = "Success" };
        //}
        //Working Fine
    }
}
