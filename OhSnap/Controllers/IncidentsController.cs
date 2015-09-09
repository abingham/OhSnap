using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using OhSnap.DAL;
using OhSnap.Models;

namespace OhSnap.Controllers
{
    public class IncidentsController : Controller
    {
        private OhSnap.DAL.DbContext db = new OhSnap.DAL.DbContext();

        // GET: Incidents
        public ActionResult Index()
        {
            return View(db.Incidents.ToList());
        }        

        // GET: Incidents/Create/:parentID
        public ActionResult Create(string parentID)
        {
            ViewData["patientID"] = parentID;
            return View();
        }

        // POST: Incidents/Create/:parentID
        [HttpPost]
        public ActionResult Create(string parentID, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var injury = new Incident()
                    {
                        PersonalNumber = parentID,
                        InjuryDate = collection["InjuryDate"],
                        InjuryHour = int.Parse(collection["InjuryHour"])
                    };
                    db.Incidents.Add(injury);
                    db.SaveChanges();
                    return RedirectToAction("Details", "Patients", new { id = parentID } );               
                }
            }
            catch
            {}

            return View();
        }

        // GET: Incidents/Edit/:id
        public ActionResult Edit(Guid id)
        {
            return View(db.Incidents.Find(id));
        }

        // POST: Incidents/Edit/:id
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                var patient = db.Incidents.Find(id);
                patient.InjuryDate = collection["InjuryDate"];
                patient.InjuryHour = int.Parse(collection["InjuryHour"]);                
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Incidents/Delete/:id
        public ActionResult Delete(Guid id)
        {
            return View(db.Incidents.Find(id));
        }

        // POST: Incidents/Delete/:id
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                var incident = db.Incidents.Find(id);
                var patientID = incident.PersonalNumber;
                db.Incidents.Remove(incident);
                db.SaveChanges();

                return RedirectToAction("Details", "Patients", new { id = patientID });
            }
            catch
            {
                return View();
            }
        }
    }
}
