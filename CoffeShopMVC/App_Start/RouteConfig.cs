using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CoffeShopMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "",
                url: "",
                defaults: new { controller = "Users", action = "Login", id = UrlParameter.Optional }
            );

/*            routes.MapRoute(
                name: "a",
                url: "home",
                defaults: new { controller = "Home", action = "index", id = UrlParameter.Optional }
            ); */

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "index", id = UrlParameter.Optional }
            );
        }
    }
}
