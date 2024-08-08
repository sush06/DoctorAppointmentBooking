using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DoctorAppointmentBooking.DataAccess.Providers;
using DoctorAppointmentBooking.DataAccess.Models;

namespace DoctorAppointmentBooking.DataAccess.Test
{
    [TestClass]
    public class DoctorProviderTest
    {
        [TestMethod]
        public void Should_Check_InsertDoctorIsWorking()
        {

            DoctorProvider doctorProvider = new DoctorProvider();
            List<Doctor> list = doctorProvider.ListDoctors();
            Doctor doctor = new Doctor();
            doctor.DoctorEmail = "zain@gmail.com";
            doctor.DoctorName = "Zain";
            doctor.DoctorPassword = "123456";
            doctor.Age = 22;
            doctor.Specialization = "General Physician";
            doctor.Experience = 10;
            doctor.Day = "Monday";
            doctor.StartTime = new TimeSpan(04,30,00);
            doctor.EndTime = new TimeSpan(08,00,00);
            doctorProvider.InsertDoctor(doctor.DoctorEmail, doctor.DoctorPassword, doctor.DoctorName, doctor.Age, doctor.Specialization, doctor.Experience, doctor.StartTime, doctor.EndTime, doctor.Day);
            List<Doctor> listAfter = doctorProvider.ListDoctors();
            Assert.IsTrue(listAfter.Where(m => m.DoctorName == "Zain").Count() > 0);
        }


        [TestMethod]
        public void Check_Whether_DoctorDetails_AreFetched()
        {
            DoctorProvider doctorProvider = new DoctorProvider();
            Assert.IsNotNull(doctorProvider.GetDoctor("Zain"));
        }
        [TestMethod]
        public void Check_Whether_DoctorCanUpdateDetails()
        {
            DoctorProvider doctorProvider = new DoctorProvider();
            Doctor doctor = new Doctor();
            doctor.DoctorEmail = "zain@gmail.com";
            doctor.DoctorName = "Zain";
            doctor.DoctorPassword = "123456";
            doctor.Age = 22;
            doctor.Specialization = "Cardiologist";
            doctor.Experience = 10;
            doctor.Day = "Friday";
            doctor.StartTime = new TimeSpan(06, 30, 00);
            doctor.EndTime = new TimeSpan(10, 00, 00);
            Assert.IsTrue(doctorProvider.UpdateDoctor(doctor.DoctorEmail, doctor.DoctorPassword, doctor.DoctorName, doctor.Age, doctor.Specialization, doctor.Experience, doctor.StartTime, doctor.EndTime, doctor.Day) != null) ;
        }

        [TestMethod]
        public void Check_Whether_DoctorIsAbleToLogin()
        {
            DoctorProvider doctorProvider = new DoctorProvider();
            Doctor doctor = new Doctor();
            doctor.DoctorEmail = "zain@gmail.com";
            doctor.DoctorPassword = "123456";
            Assert.IsTrue(doctorProvider.LoginDoctor(doctor.DoctorEmail, doctor.DoctorPassword)!=null);
        }

        [TestMethod]
        public void Check_Whether_DoctorIsDeleted()
        {
            DoctorProvider doctorProvider = new DoctorProvider();
            Doctor doctor = new Doctor();
            doctor.DoctorName = "Zain";
            Assert.IsTrue(doctorProvider.DeleteDoctor(doctor.DoctorName)!=null);
        }

        [TestMethod]
        public void Check_Doctor_Appointments()
        {
            DoctorProvider doctorProvider = new DoctorProvider();
            Assert.IsNull(doctorProvider.ViewAppointments(1));
        }
    }
}
