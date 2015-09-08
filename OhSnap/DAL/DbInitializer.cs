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
                PersonalNumber = "1101991231234"
            };
            context.Patients.Add(patient);
            context.SaveChanges();

            var injury = new Incident()
            {
                InjuryDate = "2010-03-04",
                InjuryHour = 9,
                PersonalNumber = patient.PersonalNumber
            };
            context.Incidents.Add(injury);

            var fracture = new Fracture()
            {
                IncidentID = injury.ID,
                AOCode = "33A2"
            };
            context.Fractures.Add(fracture);

            patient = new Patient()
            {
                FirstName = "Joe",
                LastName = "Schmoe",
                Age = 35,
                PersonalNumber = "1212121231234"
            };
            context.Patients.Add(patient);
            context.SaveChanges();

            injury = new Incident()
            {
                InjuryDate = "2002-08-05",
                InjuryHour = 23,
                PersonalNumber = patient.PersonalNumber
            };
            context.Incidents.Add(injury);

            fracture = new Fracture()
            {
                IncidentID = injury.ID,
                AOCode = "22B1"
            };
            context.Fractures.Add(fracture);
        }
    }
}