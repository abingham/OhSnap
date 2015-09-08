using System;
using System.Collections.Generic;

using OhSnap.DAL;
using OhSnap.Models;

namespace OhSnap.DbTool
{
    class MainClass
    {
        static void addPatients(DbContext db) {
            var patient = new Patient () {
                FirstName = "Bubba", LastName = "Ho-Tep", Age = 1234, PersonalNumber = "1111111231234"
            };
            db.Patients.Add (patient);
            db.SaveChanges ();

            var incident = new Incident()
            {               
                InjuryDate = "2010-03-04",
                InjuryHour = 9,
                PersonalNumber = patient.PersonalNumber
            };
            db.Incidents.Add(incident);

            //patient = new Patient () {
            //    FirstName = "Joe", LastName = "Schmoe", Age = 35
            //};
            //db.Patients.Add (patient);
            //db.SaveChanges ();

            //injury = new Injury () {
            //    AOCode = "31B2", 
            //    InjuryDate = new DateTime (2002, 8, 5),
            //    InjuryHour = 23,
            //    PatientID=patient.ID
            //};
            //db.Injuries.Add (injury);
        }

        public static void Main (string[] args)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());

            var db = new DbContext ();
            //addPatients (db);    
            //db.SaveChanges ();

            foreach (var patient in db.Patients)
            {
                System.Console.WriteLine(patient.PersonalNumber);

                foreach (var incident in patient.Incidents)
                {
                    System.Console.Write("incident: ");
                    System.Console.Write(incident.Patient.FirstName);
                    System.Console.WriteLine(incident.Patient.PersonalNumber);
                }
            }

            System.Console.ReadLine();
        }
    }
}
