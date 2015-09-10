using System;
using System.Web.Mvc;

using Newtonsoft.Json;

// TODO: This should probably be moved into a more generic, less area-specific location.
namespace OhSnap.Areas.api.Controllers
{
    /* A JsonResult subclass which uses JSON.NET (newtonsoft) for en/decoding.
     * 
     * Basic idea from http://stackoverflow.com/a/7150912
     */
    public class JsonNetResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var response = context.HttpContext.Response;

            response.ContentType = !String.IsNullOrEmpty(ContentType) 
                ? ContentType 
                : "application/json";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            // If you need special handling, you can call another form of SerializeObject below
            var serializedObject = JsonConvert.SerializeObject(Data, Formatting.Indented);
            response.Write(serializedObject);
        }
    }
}

