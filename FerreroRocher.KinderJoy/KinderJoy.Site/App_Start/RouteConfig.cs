using KinderJoy.Site.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KinderJoy.Site
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            /*
            routes.MapRoute(
                name: "Events",
                url: "Event/Word/{action}",
                defaults: new { controller = "Word", action = "Index" }
            );

            routes.MapRoute(
                name: "EventsMobile",
                url: "m/Event/Word/{action}",
                defaults: new { mobile = true, controller = "Word", action = "Index" },
                constraints: new { mobile = new RouteValuePresent() }
            );

            routes.MapRoute(
                name: "EventsTrack",
                url: "Event/Word/Track/{Source}/{Media}",
                defaults: new {  controller = "Word", action = "Track" }
            );

            routes.MapRoute(
               name: "HomeMobile",
               url: "m/{action}",
               defaults: new
               {
                   mobile = true,
                   controller = "Home",
                   action = "Index"
               },
                constraints: new { mobile = new RouteValuePresent() }
            );

            routes.MapRoute(
                name: "HomeWeb",
                url: "{action}",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
               name: "DefaultMobile",
               url: "m/{controller}/{action}",
               defaults: new
               {
                   mobile = true,
                   controller = "Home",
                   action = "Index"
               },
                constraints: new { mobile = new RouteValuePresent() }
            );

            routes.MapRoute(
                name: "DefaultWeb",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            */
        }
    }
}
