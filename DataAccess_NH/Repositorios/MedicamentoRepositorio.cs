using NHibernate;
using Pami.DotNet.ReferenceArchitecture.DataAccess.Repositorios.Base;
using Pami.DotNet.ReferenceArchitecture.Modelo;
using Pami.DotNet.ReferenceArchitecture.Modelo.Repositorios;
using Pami.DotNet.ReferenceArchitecture.Soporte.DI.AtributosComponentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pami.DotNet.ReferenceArchitecture.DataAccess.Repositorios
{
    [Repositorio]
    public class MedicamentoRepositorio : EntidadRepositorio<Medicamento> , IMedicamentoRepo
    {
        public MedicamentoRepositorio(ISessionFactory sessionFactory)
            :base(sessionFactory)
        { 
        }

        public IList<Medicamento> BuscarPorMonodroga(string monodroga)
        {
            return Session
                    .QueryOver<Medicamento>()
                    .WhereRestrictionOn(m => m.Monodroga).IsLike(monodroga)
                    .List();
        }
    }
    
}
