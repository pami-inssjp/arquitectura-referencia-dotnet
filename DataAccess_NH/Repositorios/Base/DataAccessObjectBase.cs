using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pami.DotNet.ReferenceArchitecture.DataAccess.Repositorios.Base
{
    /// <summary>
    /// Repositorio base sobre NHibernate que factoriza el manejo de la sesión.
    /// </summary>
    public abstract class DataAccessObjectBase
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sessionFactory">Instancia de SessionFactory a utilizar</param>
        /// <exception cref="ArgumentNullException">Si <code>sessionFactory</code> es <code>null</code></exception>
        public DataAccessObjectBase(ISessionFactory sessionFactory)
        {
            if (sessionFactory == null)
                throw new ArgumentNullException("sessionFactory");
            this.sessionFactory = sessionFactory;
        }

        #endregion

        #region Manejo de Sesión de NHibernate

        /// <summary>
        /// Session Factory
        /// </summary>
        private ISessionFactory sessionFactory;

        /// <summary>
        /// Sesión a utilizar
        /// </summary>
        protected virtual ISession Session
        {
            get
            {
                return this.sessionFactory.GetCurrentSession();
            }
        }

        #endregion

    }
}
