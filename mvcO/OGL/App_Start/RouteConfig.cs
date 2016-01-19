using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OGL
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
           name: "UsunKategorie",
           url: "Kategoria/Usun/{id}/{idKat}",
           defaults: new { controller = "Kategoria", action = "Usun", id = UrlParameter.Optional, idKat = UrlParameter.Optional }
       );
        }
    }
}
