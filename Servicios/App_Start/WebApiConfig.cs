using Pami.DotNet.ReferenceArchitecture.Soporte.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Unity.WebApi;
using Owin;

namespace Pami.DotNet.ReferenceArchitecture.Servicios.App_Start
{
    /*
     * Clase que encapsula con configuración de WebAPI 2
     */
    public class WebApiConfig
    {
        public static void Configure(Owin.IAppBuilder app)
        {
            #region WebApi configuration

            HttpConfiguration config = new HttpConfiguration();
            var container = new UnityContainerBuilder().Construir();
            config.DependencyResolver = new UnityDependencyResolver(container);
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultMVC",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            app.UseWebApi(config);

            #endregion
        }
    }
}