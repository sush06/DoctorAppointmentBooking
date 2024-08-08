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
    public class DashboardController : ControllerBase
    {
        [Route("HomeDashboard")]
        [HttpGet]
        public Object HomeDashboard()
        {
            DashboardProvider dashboardProvider = new DashboardProvider();
            HomeDashboard homeDashboard = new HomeDashboard();
            homeDashboard.DoctorCount = dashboardProvider.doctorCount();
            homeDashboard.PatientCount = dashboardProvider.patientCount();
            homeDashboard.AppointmentCount = dashboardProvider.appointmentCount();
            return homeDashboard;
        }


        [Route("Dashboard2")]
        [HttpGet]
        public Object Dashboard2()
        {
            HomeDashboard homeDashboard = new HomeDashboard();
            DashboardProvider dashboardProvider = new DashboardProvider();
            return dashboardProvider.AppCount();
        }
    }
}
