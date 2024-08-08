using Microsoft.VisualStudio.TestTools.UnitTesting;
using DoctorAppointmentBooking.DataAccess.Providers;
using DoctorAppointmentBooking.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;



namespace DoctorAppointmentBooking.DataAccess.Test
{
    [TestClass]
    public class PatientProviderTest
    {
        [TestMethod]
        public void Should_Check_InsertPatientIsWorking()
        {
            
            PatientProvider patientProvider = new PatientProvider();
            List<Patient> list = patientProvider.ListPatients();
            Patient patient = new Patient();
            patient.PatientEmail = "zain@gmail.com";
            patient.PatientName = "Zain";
            patient.PatientPassword = "123456";
            patient.Age = 22;
            patient.Disease = "Headache";
            patient.Dob = new System.DateTime(2000, 1, 1);
            patientProvider.InsertPatient(patient.PatientEmail, patient.PatientPassword,patient.PatientName,patient.Age, patient.Dob, patient.Disease);
            List<Patient> listAfter = patientProvider.ListPatients();
            Assert.IsTrue(listAfter.Where(m => m.PatientName == "Zain").Count() > 0);
        }


        [TestMethod]
        public void Check_Whether_PatientDetails_AreFetched()
        {
            PatientProvider patientProvider = new PatientProvider();
            Assert.IsNotNull(patientProvider.GetPatient("Zain"));
        }
        [TestMethod]
        public void Check_Whether_PatientCanUpdateDetails()
        {
            PatientProvider patientProvider = new PatientProvider();
            Patient patient = new Patient();
            patient.PatientEmail = "zain@gmail.com";
            patient.PatientName = "Zain";
            patient.PatientPassword = "123456";
            patient.Age = 22;
            patient.Disease = "Fever";
            patient.Dob = new System.DateTime(2000, 1, 1);
            Assert.IsTrue(patientProvider.UpdatePatient(patient.PatientEmail, patient.PatientPassword, patient.PatientName, patient.Age, patient.Dob, patient.Disease));
        }

        [TestMethod]
        public void Check_Whether_PatientIsAbleToLogin()
        {
            PatientProvider patientProvider = new PatientProvider();
            Patient patient = new Patient();
            patient.PatientEmail = "zain@gmail.com";
            patient.PatientPassword = "123456";
            Assert.IsTrue(patientProvider.LoginPatient(patient.PatientEmail, patient.PatientPassword) != null) ;
        }

        [TestMethod]
        public void Check_Whether_PatientIsDeleted()
        {
            PatientProvider patientProvider = new PatientProvider();
            Patient patient = new Patient();
            patient.PatientName = "Zain";
            Assert.IsTrue(patientProvider.DeletePatient(patient.PatientName));
        }

        [TestMethod]
        public void Check_Patient_Appointments()
        {
            PatientProvider patientProvider = new PatientProvider();
            Assert.IsNull(patientProvider.ViewAppointments(1));
        }
    }
}
