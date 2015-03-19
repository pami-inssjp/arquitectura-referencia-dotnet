using System;
using System.Collections.Generic;

namespace Pami.DotNet.ReferenceArchitecture.Soporte.TypeScanner
{
    /// <summary>
    /// Interfaz pública del componete TypeScanner
    /// </summary>
    public interface ITypeScanner
    {

        /// <summary>
        /// Setea un namespace base para filtrar los assemblies.
        /// </summary>
        /// <param name="assemblyPrefix">Prefijo de los assemblies a escanear.</param>
        /// <returns>La instancia del scanner</returns>
        TypeScanner AssemblyPrefix(string assemblyPrefix);

        /// <summary>
        /// Verifica que se hayan cargado todos los assemblies en memoria. Si hay diferencia con
        /// los assemblies que residen en el sistema de archivos, carga los faltantes.
        /// </summary>
        /// <returns>La instancia del scanner</returns>
        TypeScanner ForceAssembliesLoading();

        /// <summary>
        /// Agrega una condición de filtrado de los tipos encontrados
        /// </summary>
        /// <param name="predicate">Predicado con la condición</param>
        /// <returns>La instancia del scanner</returns>
        TypeScanner TypeFilter(Func<Type, bool> predicate);

        /// <summary>
        /// Setea una acción a aplicar a todos los tipos seleccionados.
        /// </summary>
        /// <param name="action">Acción a aplicar</param>
        /// <returns>La instancia del scanner</returns>
        TypeScanner Action(Action<Type> action);

        /// <summary>
        /// Agrega una acción para el conjunto de tipos resultantes de aplicar la condición indicada.
        /// </summary>
        /// <param name="action">Acción a aplicar</param>
        /// <param name="typeSelector">Condición que determina los tipos a los que se le aplicará la acción.</param>
        /// <returns></returns>
        /// <returns>La instancia del scanner</returns>
        TypeScanner AddConditionalAction(Action<Type> action, Func<Type, bool> typeSelector);
        
        /// <summary>
        /// Ejecuta el TypeScanner con la configuración indicada.
        /// </summary>
        /// <returns>Lista de tipos que cumplen con los filtros indicados.</returns>
        IEnumerable<Type> Play();
    }
}
