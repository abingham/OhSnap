using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using OhSnap.Models;

namespace OhSnap.Controllers
{
    public class ConsultationsController : Controller
    {
        private OhSnap.DAL.DbContext db = new OhSnap.DAL.DbContext();

        // GET: Consultations/Create/:parentID
    //    public ActionResult Create(Guid parentID)
    //    {
    //        var fracture = db.Fractures.Find(parentID);
    //        ViewData["patientID"] = fracture.Incident.PersonalNumber;
    //        return View();
    //    }

    //    // POST: Fractures/Create/:parentID
    //    [HttpPost]
    //    public ActionResult Create(Guid parentID, FormCollection collection)
    //    {
    //        try
    //        {
    //            if (ModelState.IsValid)
    //            {
    //                var fracture = db.Fractures.Find(parentID);

    //                var consultation = new Consultation()
    //                {
    //                    Location = collection["Location"]
    //                };
    //                consultation.Fractures.Add(fracture);
    //                fracture.Consultations.Add(consultation);

    //                db.Consultations.Add(consultation);
    //                db.SaveChanges();

    //                // TODO: This feels very messy, and a violation of Demeter's Law. Should the Consultation
    //                // itself be able to report a PersonalNumber? Should it take care of doing the lazy 
    //                // loading when fetching the personal number?
    //                db.Entry(consultation).Reference(c => c.Fractures.First().Incident).Load();
    //                return RedirectToAction("Details", "Patients", new { id = consultation.Fractures.First().Incident.PersonalNumber });
    //            }
    //        }
    //        catch
    //        { }

    //        // TODO: This should actually report some useful error to the user (or developer...maybe breakpoint?)
    //        return View();
    //    }

    //    // GET: Consultations/Edit/:id
    //    public ActionResult Edit(Guid id)
    //    {
    //        var consultation = db.Consultations.Find(id);
    //        return View(consultation);
    //    }

    //    // POST: Consultations/Edit/:id
    //    [HttpPost]
    //    public ActionResult Edit(Guid id, FormCollection collection)
    //    {
    //        try
    //        {
    //            var consultation = db.Consultations.Find(id);
    //            consultation.Location = collection["Location"];
    //            // TODO: Changes to fractures collection
    //            db.SaveChanges();

    //            return RedirectToAction(
    //                "Details", "Patients",
    //                new { id = consultation.Fractures.First().Incident.PersonalNumber });
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }

    //    // GET: Consultations/Delete/:id
    //    public ActionResult Delete(Guid id)
    //    {
    //        var consultation = db.Consultations.Find(id);
    //        return View(consultation);
    //    }

    //    // POST: Consultations/Delete/:id
    //    [HttpPost]
    //    public ActionResult Delete(Guid id, FormCollection collection)
    //    {
    //        try
    //        {
    //            var consultation = db.Consultations.Find(id);
    //            var patientID = consultation.Fractures.First().Incident.PersonalNumber;
    //            db.Consultations.Remove(consultation);
    //            db.SaveChanges();

    //            return RedirectToAction("Details", "Patients", new { id = patientID });
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }
    }
}