using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using OhSnap.Models;

namespace OhSnap.Controllers
{
    public class ProceduresController : Controller
    {
        private OhSnap.DAL.DbContext db = new OhSnap.DAL.DbContext();

        // GET: Procedures/Create/:parentID
        public ActionResult Create(Guid parentID)
        {
            var fracture = db.Fractures.Find(parentID);
            ViewData["patientID"] = fracture.Incident.PersonalNumber;
            return View();
        }

        // POST: Procedures/Create/:parentID
        [HttpPost]
        public ActionResult Create(Guid parentID, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: We need to also check to see if a consultation ID was passed in, i.e. if we're reusing a consultation.
                    // TODO: timestamp = date + time                
                    var consultation = new Consultation()
                    {
                        Location = collection["Consultation.Location"],
                        Timestamp = DateTime.Parse(collection["Consultation.Timestamp"])
                    };
                    db.Consultations.Add(consultation);

                    var procedure = new Procedure()
                    {
                        Repositioning = collection["Repositioning"],
                        FractureID = parentID,
                        ConsultationID = consultation.ID
                    };
                    db.Procedures.Add(procedure);
                    db.SaveChanges();

                    db.Entry(procedure).Reference(p => p.Fracture).Load();
                    return RedirectToAction("Details", "Patients", new { id = procedure.Fracture.Incident.PersonalNumber });
                }
            }
            catch
            { }

            // TODO: This should actually report some useful error to the user (or developer...maybe breakpoint?)
            return View();
        }

        // GET: Procedures/Edit/:id
        public ActionResult Edit(Guid id)
        {
            var procedure = db.Procedures.Find(id);
            return View(procedure);
        }

        // POST: Procedures/Edit/:id
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: We currently inplace update the consultation as well. This may not be what we want in all cases.
                // TODO: Do we need to lazily load the consultation?
                var procedure = db.Procedures.Find(id);
                procedure.Consultation.Location = collection["Consultation.Location"];
                procedure.Consultation.Timestamp = DateTime.Parse(collection["Consultation.Timestamp"]);
                procedure.Repositioning = collection["Repositioning"];
                db.SaveChanges();

                return RedirectToAction(
                    "Details", "Patients",
                    new { id = procedure.Fracture.Incident.PersonalNumber });
            }
            catch
            {
                return View();
            }
        }

        // GET: Procedures/Delete/:id
        public ActionResult Delete(Guid id)
        {
            var procedure = db.Procedures.Find(id);
            return View(procedure);
        }

        // POST: Procedures/Delete/:id
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                var procedure = db.Procedures.Find(id);
                var patientID = procedure.Fracture.Incident.PersonalNumber;
                db.Procedures.Remove(procedure);
                db.SaveChanges();

                // TODO: If this removes the last procedure for a consultation, remove the consultation.

                return RedirectToAction("Details", "Patients", new { id = patientID });
            }
            catch
            {
                return View();
            }
        }
    }
}
