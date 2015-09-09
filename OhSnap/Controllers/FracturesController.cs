using System;
using System.Web.Mvc;

using OhSnap.Models;

namespace OhSnap.Controllers
{
    public class FracturesController : Controller
    {
        private OhSnap.DAL.DbContext db = new OhSnap.DAL.DbContext();

        // GET: Fractures/Create/:parentID
        public ActionResult Create(Guid parentID)
        {
            var incident = db.Incidents.Find(parentID);
            ViewData["patientID"] = incident.PersonalNumber;
            return View();
        }
        
        // POST: Fractures/Create/:parentID
        [HttpPost]
        public ActionResult Create(Guid parentID, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var fracture = new Fracture()
                    {
                        AOCode = collection["AOCode"],
                        IncidentID = parentID
                    };
                    db.Fractures.Add(fracture);
                    db.SaveChanges();

                    db.Entry(fracture).Reference(f => f.Incident).Load();
                    return RedirectToAction("Details", "Patients", new { id = fracture.Incident.PersonalNumber });
                }
            }
            catch
            { }

            // TODO: This should actually report some useful error to the user (or developer...maybe breakpoint?)
            return View();
        }

        // GET: Fractures/Edit/:id
        public ActionResult Edit(Guid id)
        {
            var fracture = db.Fractures.Find(id);
            return View(fracture);
        }

        // POST: Fractures/Edit/:id
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                var fracture = db.Fractures.Find(id);
                fracture.AOCode = collection["AOCode"];
                db.SaveChanges();

                return RedirectToAction(
                    "Details", "Patients", 
                    new { id = fracture.Incident.PersonalNumber });                
            }
            catch
            {
                return View();
            }
        }

        // GET: Fractures/Delete/:id
        public ActionResult Delete(Guid id)
        {
            var fracture = db.Fractures.Find(id);
            return View(fracture);
        }

        // POST: Fractures/Delete/:id
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                var fracture = db.Fractures.Find(id);
                var patientID = fracture.Incident.PersonalNumber;
                db.Fractures.Remove(fracture);
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
