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
                FirstName = "Bubba", LastName = "Ho-Tep", Age = 1234, ID = 3
            };
            db.Patients.Add (patient);
            db.SaveChanges ();

            //var injury = new Injury () {
            //    AOCode = "33A3", 
            //    InjuryDate = new DateTime (2010, 3, 4),
            //    InjuryHour = 9,
            //    PatientID=patient.ID
            //};
            //db.Injuries.Add (injury);

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
            var db = new DbContext ();
            addPatients (db);    
            db.SaveChanges ();
        }
    }
}
