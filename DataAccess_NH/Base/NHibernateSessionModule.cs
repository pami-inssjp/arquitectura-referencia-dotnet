using System;
using System.Web;

namespace Pami.DotNet.ReferenceArchitecture.DataAccess.Base
{
    /// <summary>
    /// Modulo HTTP que se encarga de hacer el bind/unbind del contexto de la sesión de NHibernate en el 
    /// inicio/finalización del request HTTP.
    /// Extraído de http://nhibernate.info/blog/2011/03/02/effective-nhibernate-session-management-for-web-apps.html
    /// La diferencia con este último es que esta implementación se extiende a contextos NO http. Es decir es
    /// utilizable en otros contextos que no sean aplicaciones web.
    /// </summary>
    public class NHibernateSessionModule : ContextoPersistenciaBase, IHttpModule
    {
        #region Variables de Instancia

        /// <summary>
        /// HTTP Application
        /// </summary>
        private HttpApplication app;

        #endregion

        #region IHttpModule

        /// <summary>
        /// Inicializa el módulo. Se encarga de setear handlers para los eventos dél módulo.
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            this.app = context;
            this.app.BeginRequest += ContextBeginRequest;
            this.app.EndRequest += ContextEndRequest;
            this.app.Error += ContextError;
        }

        /// <summary>
        /// Libera los handlers de los eventos.
        /// </summary>
        public void Dispose()
        {
            //this.app.BeginRequest -= ContextBeginRequest;
            //this.app.EndRequest -= ContextEndRequest;
            //this.app.Error -= ContextError;
        }

        #endregion

        #region Metodos Auxiliares

        /// <summary>
        /// Hace el lazy bind de sesión de NHibernate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextBeginRequest(object sender, EventArgs e)
        {
            IniciarContexto();
        }

        /// <summary>
        /// Hace el unbind de la sesión de NHibernate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextEndRequest(object sender, EventArgs e)
        {
            FinalizarContexto();
        }

        /// <summary>
        /// Hace el unbind de la sesión de NHibernate. Se ejecuta cuando ocurre un error inesperado en la aplicación.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextError(object sender, EventArgs e)
        {
            FinalizarContexto();
        }

        #endregion
    }
}
