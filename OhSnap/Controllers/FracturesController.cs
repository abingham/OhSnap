using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using OhSnap.DAL;
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

        // GET: Fractures/Details/5
        public ActionResult Details(int id)
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
                        InjuryID = int.Parse(collection["InjuryID"])
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

        // GET: Fractures/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Fractures/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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

        // GET: Fractures/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Fractures/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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
