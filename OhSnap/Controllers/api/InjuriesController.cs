using System.Net;
using System.Web.Mvc;

using Newtonsoft.Json;

using OhSnap.DAL;
using OhSnap.Models;

namespace OhSnap.Controllers
{
    public class InjuriesController : JsonNetController
    {
        // TODO: Centralize this? It's currently replicated for e.g. PatientsController
        private OhSnap.DAL.DbContext db = new OhSnap.DAL.DbContext();

        // GET: /api/Injuries/
        [HttpGet]
        public ActionResult Index()
        {
            return Json (db.Injuries, JsonRequestBehavior.AllowGet);
        }

        // POST: /api/Injuries/
        [HttpPost]
        public ActionResult Index(Injury injury)
        {
            if (ModelState.IsValid)
            {
                db.Injuries.Add(injury);
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
            var injury = new Injury () { ID = id };
            db.Injuries.Attach (injury);
            db.Injuries.Remove (injury);
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
            var injury = db.Injuries.Find (id);
            if (injury == null)
            {
                return HttpNotFound();
            }

            return Json(injury, JsonRequestBehavior.AllowGet);
        }
    }
}
