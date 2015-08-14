using System;
using System.Collections.Generic;

using OhSnap.Models;

namespace OhSnap.DAL
{
    public class DbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DbContext>
    {
        protected override void Seed(DbContext context)
        {
            var patient = new Patient()
            {
                FirstName = "Bubba",
                LastName = "Ho-Tep",
                Age = 1234,
                ID = 3
            };
            context.Patients.Add(patient);
            context.SaveChanges();

            var injury = new Injury()
            {
                AOCode = "33A3",
                InjuryDate = new DateTime(2010, 3, 4),
                InjuryHour = 9,
                PatientID = patient.ID
            };
            context.Injuries.Add(injury);

            patient = new Patient()
            {
                FirstName = "Joe",
                LastName = "Schmoe",
                Age = 35
            };
            context.Patients.Add(patient);
            context.SaveChanges();

            injury = new Injury()
            {
                AOCode = "31B2",
                InjuryDate = new DateTime(2002, 8, 5),
                InjuryHour = 23,
                PatientID = patient.ID
            };
            context.Injuries.Add(injury);
        }
    }
}