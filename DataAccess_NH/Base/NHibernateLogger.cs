using System;
using System.Collections.Specialized;
using System.Configuration;
using NHibernate;
using Pami.DotNet.ReferenceArchitecture.Soporte.Logging;
using NH = NHibernate;


namespace Pami.DotNet.ReferenceArchitecture.DataAccess.Base
{
    /// <summary>
    /// Implementación de clase Factory de Logger para NHibernate. Para que esta clase sea utilizada como default
    /// por NHibernate, debe incluirse el siguiente elemento en el App.config o Web.config
    ///     <add key="nhibernate-logger" value="Pami.DotNet.ReferenceArchitecture.DataAccess.Base.NHibernateLoggerFactory, Pami.DotNet.ReferenceArchitecture.DataAccess_NH"/>
    /// NHibernate buscará la clave "nhibernate-logger" y utilizará esa factory.
    ///  
    /// <seealso cref="Hibernate.ILoggerFactory"/>
    /// </summary>
    public class NHibernateLoggerFactory : NH.ILoggerFactory
    {
        /// <summary>
        /// Método factory<see cref="NHibernate.ILoggerFactory.LoggerFor"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IInternalLogger LoggerFor(System.Type type)
        {
            return new NHibernateLogger(type);
        }

        /// <summary>
        /// Método factory<see cref="NHibernate.ILoggerFactory.LoggerFor"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IInternalLogger LoggerFor(string keyName)
        {
            return new NHibernateLogger(keyName);
        }
    }

    /// <summary>
    /// Implementación de un NHibernate.IInternalLogger. Esta interfaz permite usar otra librería de logging
    /// (por defecto NHibernate usa Log4net) siempre y cuando se implementen sus métodos. 
    /// </summary>
    class NHibernateLogger : IInternalLogger
    {
        private readonly ILogger Logger;

        private static readonly string ConfigSectionName = "nhibernate_nlog";

        private static readonly string DebugKey = "debug";
        private static readonly string ErrorKey = "error";
        private static readonly string FatalKey = "fatal";
        private static readonly string InfoKey = "info";
        private static readonly string WarnKey = "warn";

        public NHibernateLogger(System.Type type)
        {
            this.Logger=LoggerManager.GetLogger(type);
            InitProperties();
        }

        public NHibernateLogger(string keyName)
        {
            this.Logger = LoggerManager.GetLogger(keyName);
            InitProperties();
        }

        public void Debug(object message, Exception exception)
        {
            Logger.Debug(message, exception);
        }

        public void Debug(object message)
        {
            Logger.Debug(message); 
        }

        public void DebugFormat(string format, params object[] args)
        {
            Logger.DebugFormat(format,args);
        }

        public void Error(object message, Exception exception)
        {
            Logger.Error(message,exception);
        }

        public void Error(object message)
        {
            Logger.Error(message);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            Logger.ErrorFormat(format,args);
        }

        public void Fatal(object message, Exception exception)
        {
            Logger.Fatal(message,exception);
        }

        public void Fatal(object message)
        {
            Logger.Fatal(message);
        }

        public void Info(object message, Exception exception)
        {
            Logger.Info(message,exception);
        }

        public void Info(object message)
        {
            Logger.Info(message);
        }

        public void InfoFormat(string format, params object[] args)
        {
            Logger.InfoFormat(format,args);
        }

        public bool IsDebugEnabled
        {
            get;
            private set;
        }

        public bool IsErrorEnabled
        {
            get;
            private set;
        }

        public bool IsFatalEnabled
        {
            get;
            private set;
        }

        public bool IsInfoEnabled
        {
            get;
            private set;
        }

        public bool IsWarnEnabled
        {
            get;
            private set;
        }

        public void Warn(object message, Exception exception)
        {
            Logger.Warn(message, exception);
        }

        public void Warn(object message)
        {
            Logger.Warn(message);
        }

        public void WarnFormat(string format, params object[] args)
        {
            Logger.WarnFormat(format, args);
        }

        #region Private methods

        /// <summary>
        /// Método para icializar los niveles de logging de Hibernate, estos se definen en una sección 
        /// aparte de App.config o Web.config. En el mismo se debe incluir una nueva sección "nhibernate_log" dónde se definene los niveles de logging deseados, por defecto se loggea
        /// Warn, Error y Fatal.
        ///    <code>
        ///    <configSections>
        ///         <section name="nhibernate_nlog" type="System.Configuration.NameValueSectionHandler"/>
        ///    </configSections>
        ///    <nhibernate_nlog>
        ///         <add key="debug" value="false"/>
        ///         <add key="info" value="false"/>
        ///    </nhibernate_nlog>
        ///    </code>
        /// </summary>
        private void InitProperties()
        {
            IsErrorEnabled = true;
            IsFatalEnabled = true;
            IsWarnEnabled = true;

            //System.Diagnostics.Debug.WriteLine("Finding section");
            var section = ConfigurationManager.GetSection(ConfigSectionName) as NameValueCollection;

            //System.Diagnostics.Debug.WriteLine(section != null ? "Section found" : "Section not found");

            if (section != null)
            {
                bool flag = false;

                if (section[DebugKey] != null && Boolean.TryParse(section[DebugKey], out flag))
                    IsDebugEnabled = flag;
                if (section[ErrorKey] != null && Boolean.TryParse(section[ErrorKey], out flag))
                    IsErrorEnabled = flag;
                if (section[FatalKey] != null && Boolean.TryParse(section[FatalKey], out flag))
                    IsFatalEnabled = flag;
                if (section[InfoKey] != null && Boolean.TryParse(section[InfoKey], out flag))
                    IsInfoEnabled = flag;
                if (section[WarnKey] != null && Boolean.TryParse(section[WarnKey], out flag))
                    IsWarnEnabled = flag;
            }
        }

        #endregion
    }
}
