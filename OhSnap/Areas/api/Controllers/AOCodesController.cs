using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AOLoader;

namespace OhSnap.Areas.api.Controllers
{
    public class AOCodesController : Controller
    {
        private IEnumerable<AOLoader.AOLoader.Classification> _classifications;

        private IEnumerable<AOLoader.AOLoader.Classification> classifications
        {
            get
            {
                if (null == _classifications)
                {
                    
                    _classifications = AOLoader.AOLoader.load(
                        new StreamReader(
                            new MemoryStream(OhSnap.Properties.Resources.AOCodes)));
                }

                return _classifications;
            }
        }

        // GET: /api/AOCodeAPI
        [HttpGet]
        public ActionResult Index()
        {
            return Json(classifications, JsonRequestBehavior.AllowGet);
        }
    }
}