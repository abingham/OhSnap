using System.Text;
using System.Web.Mvc;

namespace OhSnap.Controllers.API
{
    /* A controller derivative that uses JSON.NET for JSON en/decoding.
     * 
     * basic idea from http://wingkaiwan.com/2012/12/28/replacing-mvc-javascriptserializer-with-json-net-jsonserializer/
     */
    public abstract class JsonNetController : Controller
    {
        protected override JsonResult Json(object data, string contentType,
            Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
    }
}

