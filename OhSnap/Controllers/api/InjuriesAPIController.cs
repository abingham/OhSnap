using System.Net;
using System.Linq;
using System.Web.Mvc;

using Newtonsoft.Json;

using OhSnap.DAL;
using OhSnap.Models;

namespace OhSnap.Controllers.API
{
    public class InjuriesAPIController : JsonNetController
    {
        // TODO: Centralize this? It's currently replicated for e.g. PatientsController
        private OhSnap.DAL.DbContext db = new OhSnap.DAL.DbContext();

        // GET: /api/Injuries/
        [HttpGet]
        public ActionResult Index()
        {
            return Json (db.Incidents, JsonRequestBehavior.AllowGet);
        }

        // POST: /api/Injuries/
        [HttpPost]
        public ActionResult Index(Incident injury)
        {
           if (ModelState.IsValid)
            {
                db.Incidents.Add(injury);
                db.SaveChanges();

                return Json (injury);
            }

            // TODO: Is this the right thing to return?
            return new HttpStatusCodeResult(
                HttpStatusCode.InternalServerError);
        }

        // DELETE: /api/Injuries
        [HttpDelete]
        public HttpStatusCode Index(int id)
        {
            var injury = new Incident () { ID = id };
            db.Incidents.Attach (injury);
            db.Incidents.Remove (injury);
            try {
                db.SaveChanges ();
            } catch (System.Data.DataException) {
                return HttpStatusCode.NotFound;
            }

            return HttpStatusCode.OK;
        }

        // GET: /api/Injuries/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var injury = db.Incidents.Find (id);
            if (injury == null)
            {
                return HttpNotFound();
            }

            return Json(injury, JsonRequestBehavior.AllowGet);
        }

        // GET: /api/Injuries/ByUser/:patientid
        public ActionResult ByUser(int id)
        {
            var injuries = db.Incidents.Where(i => i.PatientID == id);
            return Json(injuries, JsonRequestBehavior.AllowGet);
        }
    }
}
