using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Pami.DotNet.ReferenceArchitecture.Soporte.Logging.NLog;

namespace Pami.DotNet.ReferenceArchitecture.Soporte.Logging
{
    /// <summary>
    /// Esta clase provee instancias de loggers a través de los métodos <see cref="GetLogger(string)"/> 
    /// y <see cref="GetLogger(System.Type)"/>.
    /// </summary>
    public class LoggerManager
    {
        #region Propiedades Privadas
        
        /// <summary>
        /// Instancias de loggers.
        /// </summary>
        private static Hashtable instances;

        private static ILoggerFactory loggerFactory;

        #endregion

        #region Propiedades Publicas

        /// <summary>
        /// 
        /// </summary>
        public static ILoggerFactory LoggerFactory
        {
            get
            {
                return loggerFactory;
            }

            protected internal set
            {
                loggerFactory = value;
                ClearCache();
            }
        }

        #endregion

        #region Constructor 

        static LoggerManager()
        {
            instances = new Hashtable();
            loggerFactory = new NLogLoggerFactory();
        }

        #endregion

        #region Metodos Publicos

        /// <summary>
        /// Retorna el logger asociado a un nombre dado.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ILogger GetLogger(string name)
        {
            return GetLoggerInternal(name);
        }

        /// <summary>
        /// Retorna el logger asociado a un tipo dado.
        /// </summary>
        /// <param name="type">El tipo de la clase actual.</param>
        /// <returns></returns>
        public static ILogger GetLogger(Type type)
        {
            return GetLoggerInternal(type.FullName);
        }

        /// <summary>
        /// Retorna el logger asociado a una clase dada.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ILogger GetLogger<T>() where T : class
        {
            return GetLoggerInternal(typeof(T).FullName);
        }

        #endregion

        #region Metodos Privados

        /// <summary>
        /// Obtiene o crea una instancia de ILogger a partir de un nombre.
        /// </summary>
        /// <param name="name">Por lo general es Name o FullName de la propiedad <see cref="Type" /></param>
        /// <returns>Una instancia de ILogger</returns>
        private static ILogger GetLoggerInternal(string name)
        {
            ILogger logger = instances[name] as ILogger;
            if (logger == null)
            {
                logger = LoggerFactory.GetLogger(name);
                instances.Add(name, logger);
            }
            return logger;
        }

        /// <summary>
        /// Limpia la cache interna
        /// </summary>
        private static void ClearCache()
        {
            instances.Clear();
        }

        #endregion
    }
}
