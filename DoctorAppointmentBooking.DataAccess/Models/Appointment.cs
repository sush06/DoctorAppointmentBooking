using System;
using System.Collections.Generic;

#nullable disable

namespace DoctorAppointmentBooking.DataAccess.Models
{
    public partial class Appointment
    {
        public int AppointmentId { get; set; }
        public string PatientEmail { get; set; }
        public string DoctorName { get; set; }
        public string Day { get; set; }
        public byte[] MedicalReports { get; set; }
        public string AppointmentMode { get; set; }
        public TimeSpan? Time { get; set; }
    }
}
