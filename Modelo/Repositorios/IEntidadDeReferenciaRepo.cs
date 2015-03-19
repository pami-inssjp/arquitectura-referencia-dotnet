using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pami.DotNet.ReferenceArchitecture.Modelo.Repositorios
{
    /// <summary>
    /// Repositorio genérico que proveen comportamiento de consulta para la entidad en cuestión.
    /// </summary>
    /// <typeparam name="TEntidad">Tipo de la entidad que maneja el repositorio</typeparam>
    public interface IEntidadDeReferenciaRepo<TEntidad> where TEntidad : BaseEntity
    {

        #region Métodos de Consulta

        /// <summary>
        /// Devuelve una entidad a partir de su ID.
        /// </summary>
        /// <typeparam name="TEntidad">Tipo de la entidad</typeparam>
        /// <typeparam name="TId">Tipo del ID de la entidad</typeparam>
        /// <param name="id">Valor del ID de la entidad a recuperar</param>
        /// <returns>Instancia de la entidad buscada. Si no existe, devuelve <code>null</code></returns>
        TEntidad Obtener(int id);

        /// <summary>
        /// Devuelve todas las instancias existentes del tipo de entidad indicado.
        /// </summary>
        /// <typeparam name="TEntidad">Tipo de entidad en cuestión</typeparam>
        /// <typeparam name="TId">Tipo del ID de las entidades a recuperar</typeparam>
        /// <returns>Lista con todas las entidades del tipo indicado.</returns>
        IList<TEntidad> ObtenerTodos();

        /// <summary>
        /// Devuelve la entidades que se corresponden con los IDs indicados
        /// </summary>
        /// <param name="ids">Lista de IDs de las entidades buscadas</param>
        /// <returns>Lista de entidades que se corresponden con los IDs</returns>
        IList<TEntidad> Obtener(int[] ids);

        #endregion

    }
}
