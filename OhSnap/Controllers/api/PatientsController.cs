﻿using System.Net;
using System.Web.Mvc;

using OhSnap.DAL;
using OhSnap.Models;

namespace OhSnap.Controllers
{
    public class PatientsController : JsonNetController
    {
        private OhSnap.DAL.DbContext db = new OhSnap.DAL.DbContext();

        // GET: /api/Patients/
        [HttpGet]
        public ActionResult Index()
        {
            return Json (db.Patients, JsonRequestBehavior.AllowGet);
        }

        // POST: /api/Patients/
        [HttpPost]
        public ActionResult Index(Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();

                return Json (patient);
            }

            // TODO: Is this the right thing to return?
            return new HttpStatusCodeResult(
                HttpStatusCode.InternalServerError);
        }

        // DELETE: /api/Patients
        [HttpDelete]
        public HttpStatusCode Index(int id)
        {
            var patient = new Patient () { ID = id };
            db.Patients.Attach (patient);
            db.Patients.Remove (patient);
            try {
                db.SaveChanges ();
            } catch (System.Data.DataException) {
                return HttpStatusCode.NotFound;
            }

            return HttpStatusCode.OK;
        }

        // GET: /api/Patients/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var patient = db.Patients.Find (id);
            if (patient == null)
            {
                return HttpNotFound();
            }

            return Json(patient, JsonRequestBehavior.AllowGet);
        }
    }
}
