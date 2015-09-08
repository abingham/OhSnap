using System;
using System.Web.Mvc;

using OhSnap.Models;

namespace OhSnap.Controllers
{
    public class FracturesController : Controller
    {
        private OhSnap.DAL.DbContext db = new OhSnap.DAL.DbContext();

        // GET: Fractures
        public ActionResult Index()
        {
            return View(db.Fractures);
        }

        // GET: Fractures/Details/:id
        public ActionResult Details(Guid id)
        {
            var fracture = db.Fractures.Find(id);
            return View(fracture);
        }

        // GET: Fractures/Create
        public ActionResult Create()
        {
            return View();
        }
        
        // POST: Fractures/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var fracture = new Fracture()
                    {
                        AOCode = collection["AOCode"],
                        IncidentID = System.Guid.Parse(collection["InjuryID"])
                    };
                    db.Fractures.Add(fracture);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            { }

            return View();
        }

        // GET: Fractures/Edit/:id
        public ActionResult Edit(Guid id)
        {
            return View();
        }

        // POST: Fractures/Edit/:id
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Fractures/Delete/:id
        public ActionResult Delete(Guid id)
        {
            return View();
        }

        // POST: Fractures/Delete/:id
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
