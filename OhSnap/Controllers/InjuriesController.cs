using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using OhSnap.DAL;
using OhSnap.Models;

namespace OhSnap.Controllers
{
    public class InjuriesController : Controller
    {
        private OhSnap.DAL.DbContext db = new OhSnap.DAL.DbContext();

        // GET: Injuries
        public ActionResult Index()
        {
            return View(db.Injuries.ToList());
        }

        // GET: Injuries/Details/5
        public ActionResult Details(int id)
        {
            var injury = db.Injuries.Find(id);
            return View(injury);
        }

        // GET: Injuries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Injuries/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var injury = new Injury()
                    {
                        InjuryDate = DateTime.Parse(collection["InjuryDate"]),
                        InjuryHour = int.Parse(collection["InjuryTime"]),
                        PatientID = int.Parse(collection["PatientID"])
                    };
                    db.Injuries.Add(injury);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {}

            return View();
        }

        // GET: Injuries/Edit/5
        public ActionResult Edit(int id)
        {
            return View(db.Injuries.Find(id));
        }

        // POST: Injuries/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var patient = db.Injuries.Find(id);
                patient.InjuryDate = DateTime.Parse(collection["InjuryDate"]);
                patient.InjuryHour = int.Parse(collection["InjuryTime"]);
                patient.PatientID = int.Parse(collection["PatientID"]);
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
                var patient = new Injury { ID = id };
                db.Injuries.Attach(patient);
                db.Injuries.Remove(patient);
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
