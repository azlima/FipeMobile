using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FipeMobile
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                //defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                defaults: new { controller = "Marca", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Veiculo",
                url: "{controller}/{action}/{tipo}/{marcaName}/{marcaId}/{fipeName}"
            );

            routes.MapRoute(
                name: "Modelo",
                url: "{controller}/{action}/{veiculoName}/{veiculoId}"
            );

            routes.MapRoute(
                name: "ModeloAno",
                url: "{controller}/{action}/{modeloName}/{modeloId}"
            );
        }
    }
}