using NHibernate;
using NHibernate.Context;
using NHibernate.Engine;
using Pami.DotNet.ReferenceArchitecture.Soporte.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pami.DotNet.ReferenceArchitecture.DataAccess.Base
{
    /// <summary>
    /// Adaptación del LazySessionContext que se ilustra en el siguiente ejemplo
    /// http://nhibernate.info/blog/2011/03/02/effective-nhibernate-session-management-for-web-apps.html
    /// La diferencia con este último es que esta implementación se extiende a contextos NO http. Es decir es
    /// utilizable en otros contextos que no sean aplicaciones web.
    /// </summary>
    public class LazySessionContext : ICurrentSessionContext
    {

        private readonly ISessionFactoryImplementor factory;
        private const string CurrentSessionContextKey = "NHibernateCurrentSession";

        public LazySessionContext(ISessionFactoryImplementor factory)
        {
            this.factory = factory;
        }

        /// <summary>
        /// Retrieve the current session for the session factory.
        /// </summary>
        /// <returns></returns>
        public ISession CurrentSession()
        {
            Lazy<ISession> initializer;
            var currentSessionFactoryMap = GetCurrentFactoryMap();
            if (currentSessionFactoryMap == null ||
                !currentSessionFactoryMap.TryGetValue(factory, out initializer))
            {
                return null;
            }
            return initializer.Value;
        }

        /// <summary>
        /// Bind a new sessionInitializer to the context of the sessionFactory.
        /// </summary>
        /// <param name="sessionInitializer"></param>
        /// <param name="sessionFactory"></param>
        public static void Bind(Lazy<ISession> sessionInitializer, ISessionFactory sessionFactory)
        {
            var map = GetCurrentFactoryMap();
            map[sessionFactory] = sessionInitializer;
        }

        /// <summary>
        /// Unbind the current session of the session factory.
        /// </summary>
        /// <param name="sessionFactory"></param>
        /// <returns></returns>
        public static ISession UnBind(ISessionFactory sessionFactory)
        {
            var map = GetCurrentFactoryMap();
            var sessionInitializer = map[sessionFactory];
            map[sessionFactory] = null;
            if (sessionInitializer == null || !sessionInitializer.IsValueCreated) return null;
            return sessionInitializer.Value;
        }

        /// <summary>
        /// Provides the CurrentMap of SessionFactories.
        /// If there is no map create/store and return a new one.
        /// </summary>
        /// <returns></returns>
        private static IDictionary<ISessionFactory, Lazy<ISession>> GetCurrentFactoryMap()
        {
            var currentFactoryMap = (IDictionary<ISessionFactory, Lazy<ISession>>)
                                   OperacionContext.Datos[CurrentSessionContextKey];
            if (currentFactoryMap == null)
            {
                currentFactoryMap = new Dictionary<ISessionFactory, Lazy<ISession>>();
                OperacionContext.Datos[CurrentSessionContextKey] = currentFactoryMap;
            }
            return currentFactoryMap;
        }
    }
}
