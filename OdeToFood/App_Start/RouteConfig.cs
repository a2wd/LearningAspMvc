using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OdeToFood
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //An http request for /home/index would call the home-controller's action of index
            //A request without an action would default to the index action

            //We could make a specific route:
            //This would override ../cuisine/xxx, if not matched, the url falls through to the next route
            routes.MapRoute(
                name: "cuisine",
                url: "cuisine/{name}",
                defaults: new { controller = "Cuisine", action = "Search", name = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
