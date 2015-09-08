﻿using System;
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

        // GET: Injuries
        public ActionResult Index()
        {
            return View(db.Incidents.ToList());
        }        

        // GET: Injuries/Create
        public ActionResult Create(string parentID)
        {
            ViewData["parentID"] = parentID;
            return View();
        }

        // POST: Injuries/Create/:parentID
        [HttpPost]
        public ActionResult Create(string parentID, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var injury = new Incident()
                    {
                        PatientID = int.Parse(parentID),
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

        // GET: Injuries/Edit/5
        public ActionResult Edit(int id)
        {
            return View(db.Incidents.Find(id));
        }

        // POST: Injuries/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var patient = db.Incidents.Find(id);
                patient.InjuryDate = collection["InjuryDate"];
                patient.InjuryHour = int.Parse(collection["InjuryHour"]);
                // patient.PatientID = int.Parse(collection["PatientID"]);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Injuries/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Injuries/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var patient = new Incident { ID = id };
                db.Incidents.Attach(patient);
                db.Incidents.Remove(patient);
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
