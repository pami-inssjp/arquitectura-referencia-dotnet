using Microsoft.Owin;
using Owin;
using Pami.DotNet.ReferenceArchitecture.Servicios.App_Start;
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
            //Use NLog for OWIN logging
            app.UseNLog();
            SecurityConfig.Configure(app);
            WebApiConfig.Configure(app);
        }
    }
}
