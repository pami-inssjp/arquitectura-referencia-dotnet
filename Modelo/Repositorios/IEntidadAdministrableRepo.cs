using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pami.DotNet.ReferenceArchitecture.Modelo.Repositorios
{
    /// <summary>
    /// Repositorio genérico que proveen comportamiento de administración para la entidad en cuestión.
    /// </summary>
    /// <typeparam name="TEntidad">Tipo de la entidad que maneja el repositorio</typeparam>
    public interface IEntidadAdministrableRepo<TEntidad> : IEntidadDeReferenciaRepo<TEntidad>
            where TEntidad : BaseEntity
    {

        /// <summary>
        /// Agrega la entidad al repositorio.
        /// </summary>
        /// <param name="entidad">Instancia de la entidad</param>
        /// <returns>Instancia de la entidad agregada</returns>
        TEntidad Agregar(TEntidad entidad);

        /// <summary>
        /// Actualiza la entidad modificada.
        /// </summary>
        /// <param name="entidad">Instancia de la entidad</param>
        void Actualizar(TEntidad entidad);

        ///// <summary>
        ///// Elimina la entidad del repositorio.
        ///// </summary>
        ///// <param name="entidad">Instancia de la entidad</param>
        void Eliminar(TEntidad entidad);

    }
}
