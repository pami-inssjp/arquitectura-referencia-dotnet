using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Owin;
using Pami.DotNet.ReferenceArchitecture.Servicios.Common.Security.Utils;
using Pami.DotNet.ReferenceArchitecture.Soporte.DI;
using System;
using System.IdentityModel.Tokens;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Unity.WebApi;
using Pysco68.Owin.Logging.NLogAdapter;

[assembly: OwinStartup(typeof(Pami.DotNet.ReferenceArchitecture.Servicios.Startup))]

namespace Pami.DotNet.ReferenceArchitecture.Servicios
{

    public class Startup
    {
        /// <summary>
        /// Clase de configuración. OWIN la levanta por naming convention (Startup.cs)
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            app.UseErrorPage();

            app.UseNLog();
            #region Security configuration using JWT

            var certData = File.ReadAllBytes(@"C:\Projects\Pami-Reference-Arquitecture\Servicios\internal-public.der");

            RSACryptoServiceProvider rsaCsp = new RSACryptoServiceProvider();
            rsaCsp.LoadPublicKeyDER(certData);


            JwtBearerAuthenticationOptions authOptions = new JwtBearerAuthenticationOptions()
            {
                AllowedAudiences = new[] { "vademecum" },
                TokenHandler = new JwtSecurityTokenHandler(),
                Provider = new CustomOAuthBearerProvider(),
                TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new RsaSecurityKey(rsaCsp),
                    ValidAudience = "vademecum",
                    ValidIssuer = "apimanager"
                }
            };

            app.UseJwtBearerAuthentication(authOptions);

            #endregion


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

        string getTime()
        {
            return DateTime.Now.Millisecond.ToString();
        }
    }
}
