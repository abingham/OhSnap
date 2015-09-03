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
   
        // GET: Patients/Details/5
        public ActionResult Details(int id)
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

        // GET: Patients/Edit/5
        public ActionResult Edit(int id)
        {
            var patient = db.Patients.Find(id);
            return View(patient);
        }

        // POST: Patients/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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

        // GET: Patients/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Patients/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var patient = new Patient { ID = id };
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
