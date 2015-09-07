using System.Net;
using System.Linq;
using System.Web.Mvc;

using Newtonsoft.Json;

using OhSnap.DAL;
using OhSnap.Models;

namespace OhSnap.Controllers.API
{
    public class FracturesAPIController : JsonNetController
    {
        private OhSnap.DAL.DbContext db = new OhSnap.DAL.DbContext();

        // GET: /api/Fractures/
        [HttpGet]
        public ActionResult Index()
        {
            return Json (db.Fractures, JsonRequestBehavior.AllowGet);
        }

        // POST: /api/Fractures/
        [HttpPost]
        public ActionResult Index(Fracture fracture)
        {
            if (ModelState.IsValid)
            {
                db.Fractures.Add(fracture);
                db.SaveChanges();

                return Json (fracture);
            }

            // TODO: Is this the right thing to return?
            return new HttpStatusCodeResult(
                HttpStatusCode.InternalServerError);
        }

        // DELETE: /api/Fractures
        [HttpDelete]
        public HttpStatusCode Index(int id)
        {
            var fracture = new Fracture () { ID = id };
            db.Fractures.Attach (fracture);
            db.Fractures.Remove (fracture);
            try {
                db.SaveChanges ();
            } catch (System.Data.DataException) {
                return HttpStatusCode.NotFound;
            }

            return HttpStatusCode.OK;
        }

        // GET: /api/Fractures/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var injury = db.Fractures.Find (id);
            if (injury == null)
            {
                return HttpNotFound();
            }

            return Json(injury, JsonRequestBehavior.AllowGet);
        }

        // GET: /api/Fractures/ByUser/:patientid
        public ActionResult ByUser(int id)
        {
            var fractures = db.Fractures.Where(f => f.IncidentID == id);
            return Json(fractures, JsonRequestBehavior.AllowGet);
        }
    }
}
