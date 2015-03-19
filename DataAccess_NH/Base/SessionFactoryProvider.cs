using System;
using NHibernate;
using NHibernate.Cfg;

namespace Pami.DotNet.ReferenceArchitecture.DataAccess.Base
{
    /// <summary>
    /// Provee una instancia del SessionFactory de NHibernate
    /// </summary>
    public interface ISessionFactoryProvider : IDisposable
    {
        /// <summary>
        /// Devuelve la instancia del SessionFactory
        /// </summary>
        /// <returns>Instancia del SessionFactory</returns>
        ISessionFactory GetSessionFactory();
    }

    /// <summary>
    /// Implementación por defecto de <code>ISessionFactoryProvider</code> 
    /// </summary>
    public class SessionFactoryProvider : ISessionFactoryProvider
    {

        #region Variables de instancia

        private ISessionFactory sessionFactory;
        private IConfiguracionProvider configProvider;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configuracionProvider">Provider de la configuración de NHibernate</param>
        public SessionFactoryProvider(IConfiguracionProvider configuracionProvider)
        {
            if (configuracionProvider == null)
                throw new ArgumentNullException("configuracionProvider");

            this.configProvider = configuracionProvider;
        }

        #endregion

        #region ISessionFactoryProvider

        /// <summary>
        /// Devuelve la instancia del SessionFactory
        /// </summary>
        /// <returns>Instancia del SessionFactory</returns>
        public ISessionFactory GetSessionFactory()
        {
            Initialize();
            return sessionFactory;
        }

        /// <summary>
        /// Inicializa la instancia del SessionFactory
        /// </summary>
        protected virtual void Initialize()
        {
            if (sessionFactory == null)
            {
                //log.Debug("Initialize a new session factory reading the configuration.");
                Configuration conf = this.configProvider.Obtener();
                if (conf != null)
                {
                    this.sessionFactory = conf.BuildSessionFactory();
                }
                this.configProvider = null; // after built the SessionFactory the configuration is not needed
            }
        }

        #endregion

        #region IDisposable

        private bool disposed;

        /// <summary>
        /// Libera los recursos utilizados.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Libera el SessionFactory si fuera indicado
        /// </summary>
        /// <param name="disposing">Si se libera el SessionFactory o no</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (sessionFactory != null)
                    {
                        sessionFactory.Close();
                        sessionFactory = null;
                    }
                }
                disposed = true;
            }
        }

        #endregion

    }
}
