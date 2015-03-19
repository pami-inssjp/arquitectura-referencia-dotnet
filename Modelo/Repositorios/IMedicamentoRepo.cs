using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pami.DotNet.ReferenceArchitecture.Modelo.Repositorios
{
    public interface IMedicamentoRepo : IEntidadAdministrableRepo<Medicamento>
    {
        /// <summary>
        /// Busca diferentes presentaciones comerciales de la misma monodroga
        /// </summary>
        /// <param name="monodroga">La monodroga del medicamento a buscar</param>
        /// <returns>Una lista de las diferentes presentaciones comerciales de la monodroga</returns>
        IList<Medicamento> BuscarPorMonodroga(string monodroga); 
    }
}
