using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using OhSnap.DAL;
using OhSnap.Models;

namespace OhSnap.Controllers
{
    public class PatientsController : Controller
    {
        private OhSnap.DAL.DbContext db = new OhSnap.DAL.DbContext();

        // GET: Patients
        public ActionResult Index()
        {
            return View(db.Patients.ToList());
        }
   
        // GET: Patients/Details/:personalNumber
        public ActionResult Details(string id)
        {
            var patient = db.Patients.Find(id);
            return View(patient);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var patient = new Patient()
                    {
                        PersonalNumber = collection["PersonalNumber"],
                        FirstName = collection["FirstName"],
                        LastName = collection["LastName"],
                        Age = int.Parse(collection["Age"])
                    };
                    db.Patients.Add(patient);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                } 
            }
            catch
            {}

            return View();
        }

        // GET: Patients/Edit/:personalNumber
        public ActionResult Edit(string id)
        {
            var patient = db.Patients.Find(id);
            return View(patient);
        }

        // POST: Patients/Edit/:personalNumber
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                var patient = db.Patients.Find(id);
                patient.FirstName = collection["FirstName"];
                patient.LastName = collection["LastName"];
                patient.Age = int.Parse(collection["Age"]);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Patients/Delete/:personalNumber
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: Patients/Delete/:personalNumber
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var patient = new Patient { PersonalNumber = id };
                db.Patients.Attach(patient);
                db.Patients.Remove(patient);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
