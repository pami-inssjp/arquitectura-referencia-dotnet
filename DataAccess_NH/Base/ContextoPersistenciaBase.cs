using System;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using Pami.DotNet.ReferenceArchitecture.Soporte.Logging;

namespace Pami.DotNet.ReferenceArchitecture.DataAccess.Base
{
    /// <summary>
    /// Factoriza el inicio/finalización de una sesión de persistencia. En este caso particular, se 
    /// encarga de ligar/desligar un inicializador de sesión de NH tardío (<code>LazySessionContext</code>)
    /// al <code>SessionContext</code> de NHibernate.
    /// </summary>
    public class ContextoPersistenciaBase
    {
        #region Variables de Clase

        protected static ILogger logger = LoggerManager.GetLogger<ContextoPersistenciaBase>();

        #endregion

        #region Propiedades

        /// <summary>
        /// Devuelve la instancia del <code>ISessionFactoryProvider</code>
        /// </summary>
        protected virtual ISessionFactoryProvider SessionFactoryProvider
        {
            get { return ServiceLocator.Current.GetInstance<ISessionFactoryProvider>(); }
        }

        #endregion

        #region Métodos factorizados

        /// <summary>
        /// Inicializa el contexto de persistencia, seteando el inicializador para crear la sesión de NHibernate 
        /// por demanda.
        /// </summary>
        protected virtual void IniciarContexto()
        {
            var sf = this.SessionFactoryProvider.GetSessionFactory();
            LazySessionContext.Bind(new Lazy<ISession>(() => IniciarSesion(sf)), sf);
        }

        /// <summary>
        /// Inicia la sesión de NHibernate y su correspondiente transacción.
        /// </summary>
        /// <param name="sf">SessionFactory</param>
        /// <returns>Instancia de la sesión de NHibernate</returns>
        protected virtual ISession IniciarSesion(ISessionFactory sf)
        {
            var session = sf.OpenSession();
            session.BeginTransaction();
            #region Logging
            if (logger.IsDebugEnabled())
                logger.DebugFormat("Sesion de NH creada: {0}", session.GetSessionImplementation().SessionId);
            #endregion
            return session;
        }

        /// <summary>
        /// Finaliza el contexto, desligando la sesión de NHibernate.
        /// </summary>
        protected virtual void FinalizarContexto()
        {
            ISession session = LazySessionContext.UnBind(this.SessionFactoryProvider.GetSessionFactory());
            if (session != null)
                FinalizarSesion(session);
        }

        /// <summary>
        /// Finaliza la sesión de NHibernate (libera sus recursos)
        /// </summary>
        /// <param name="session">Sesión de NHibernate</param>
        /// <param name="abort">Indica si se aborta la transacción activa, en caso que hubiera una.</param>
        protected virtual void FinalizarSesion(ISession session, bool abort = false)
        {
            if (session.Transaction != null && session.Transaction.IsActive)
            {
                if (abort)
                    session.Transaction.Rollback();
                else
                    session.Transaction.Commit();
                session.Transaction.Dispose();
            }
            session.Dispose();
            #region Logging
            if (logger.IsDebugEnabled())
                logger.DebugFormat("Sesion de NH liberada: {0}", session.GetSessionImplementation().SessionId);
            #endregion
        }

        #endregion
    }
}
