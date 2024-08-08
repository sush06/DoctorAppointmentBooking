using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorAppointmentBooking.DataAccess.Models;

namespace DoctorAppointmentBooking.Models
{
    public class BookAppointmentViewModel
    {
        public List<Doctor> Doctors { get; set; }
        
        public string doctorName { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
