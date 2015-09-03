
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Newtonsoft.Json;

namespace OhSnap
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes (RouteCollection routes)
        {
            routes.IgnoreRoute ("{resource}.axd/{*pathInfo}");

            //routes.MapRoute (
            //    "API",
            //    "api/{controller}/{action}/{id}",
            //    namespaces: new string[] { "OhSnap.Controllers.API" }
            //);

            //routes.MapRoute (
            //    "APIIndex",
            //    "api/{controller}/{id}",
            //    new { action = "Index", id = "" },
            //    namespaces: new string[] { "OhSnap.Controllers.API" }
            //);

            // TODO: How do we limit this to non-api stuff without having to explicitly list each controller that should match the default pattern?
            routes.MapRoute(
                "CreateChildren",
                "{controller}/Create/{parentID}",
                new { action = "Create" }
                );

            routes.MapRoute (
                "Default",
                "{controller}/{action}/{id}",
                new { action = "Index", controller = "Home", id = ""},
                namespaces: new string[] { "OhSnap.Controllers" }
            );

        }

        public static void RegisterGlobalFilters (GlobalFilterCollection filters)
        {
            filters.Add (new HandleErrorAttribute ());
        }

        protected void Application_Start ()
        {
            AreaRegistration.RegisterAllAreas ();
            RegisterGlobalFilters (GlobalFilters.Filters);
            RegisterRoutes (RouteTable.Routes);

            // var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            // json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
        }
    }
}
