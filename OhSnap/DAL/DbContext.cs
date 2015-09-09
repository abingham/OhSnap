using System;
using System.Data.Entity;

using OhSnap.Models;

namespace OhSnap.DAL
{
    public class DbContext : System.Data.Entity.DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Fracture> Fractures { get; set; }
    }
}

