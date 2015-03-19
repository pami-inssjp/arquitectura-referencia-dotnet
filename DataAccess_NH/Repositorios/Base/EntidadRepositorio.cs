using NHibernate;
using Pami.DotNet.ReferenceArchitecture.Modelo;
using Pami.DotNet.ReferenceArchitecture.Modelo.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pami.DotNet.ReferenceArchitecture.DataAccess.Repositorios.Base
{
    public class EntidadRepositorio<TEntidad> : DataAccessObjectBase, IEntidadAdministrableRepo<TEntidad>
        where TEntidad : BaseEntity
    {
        public EntidadRepositorio(ISessionFactory sessionFactory)
            :base(sessionFactory)
        {
        }


        #region IEntidadAdminsitrableRepo

        public virtual TEntidad Agregar(TEntidad entidad)
        {
            Guardar(entidad);
            return entidad;
        }
        
        public void Actualizar(TEntidad entidad)
        {
            Guardar(entidad);
        }

        public void Eliminar(TEntidad entidad)
        {
            Session.Delete(entidad);
        }

        public TEntidad Obtener(int id)
        {
            return Session.Get<TEntidad>(id);
        }

        public IList<TEntidad> ObtenerTodos()
        {
            return Session.QueryOver<TEntidad>().List();
        }

        public IList<TEntidad> Obtener(int[] ids)
        {
            if (ids == null || ids.Length == 0)
                return new List<TEntidad>();

            return Session
                    .QueryOver<TEntidad>()
                    .WhereRestrictionOn(e => e.Id).IsIn(ids)
                    .List();
        }

        #endregion

        #region Metodos privados

        private void Guardar(TEntidad entidad)
        {
            Session.SaveOrUpdate(entidad);
        }
        
        #endregion
    }
}
