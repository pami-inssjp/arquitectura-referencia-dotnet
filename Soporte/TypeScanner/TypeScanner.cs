using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Pami.DotNet.ReferenceArchitecture.Soporte.TypeScanner
{
    /// <summary>
    /// Se encarga de escanear los assemblies de la aplicación en busca de determinados tipos,
    /// de manera tal de poder aplicar una acción a dichos tipos.
    /// </summary>
    public class TypeScanner : ITypeScanner
    {
        #region Variables de instancia

        private string assemblyPrefix;
        private bool forceAssembliesLoading;
        private Func<Type, bool> typeCondition;
        private Action<Type> globalAction;
        private IDictionary<Func<Type, bool>, Action<Type>> actions;

        #endregion

        #region Factory Method

        /// <summary>
        /// Factory method para crear una instancia del type scanner. 
        /// </summary>
        /// <returns>Instancia de <code>ITypeScanner</code></returns>
        public static ITypeScanner Crear()
        {
            return new TypeScanner();
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private TypeScanner()
        {
            this.actions = new Dictionary<Func<Type, bool>, Action<Type>>();
        }

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Setea un namespace base para filtrar los assemblies.
        /// </summary>
        /// <param name="assemblyPrefix">Prefijo de los assemblies a escanear.</param>
        /// <returns>La instancia del scanner</returns>
        public TypeScanner AssemblyPrefix(string assemblyPrefix)
        {
            if (string.IsNullOrWhiteSpace(assemblyPrefix))
                throw new ArgumentNullException("assemblyPrefix");
            
            this.assemblyPrefix = assemblyPrefix;

            return this;
        }

        /// <summary>
        /// Verifica que se hayan cargado todos los assemblies en memoria. Si hay diferencia con
        /// los assemblies que residen en el sistema de archivos, carga los faltantes.
        /// </summary>
        /// <returns>La instancia del scanner</returns>
        public TypeScanner ForceAssembliesLoading()
        {
            this.forceAssembliesLoading = true;

            return this;
        }

        /// <summary>
        /// Agrega una condición de filtrado de los tipos encontrados
        /// </summary>
        /// <param name="predicate">Predicado con la condición</param>
        /// <returns>La instancia del scanner</returns>
        public TypeScanner TypeFilter(Func<Type, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            this.typeCondition = predicate;

            return this;
        }

        /// <summary>
        /// Setea una acción a aplicar a todos los tipos seleccionados.
        /// </summary>
        /// <param name="action">Acción a aplicar</param>
        /// <returns>La instancia del scanner</returns>
        public TypeScanner Action(Action<Type> action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            this.globalAction = action;
            
            return this;
        }

        /// <summary>
        /// Agrega una acción para el conjunto de tipos resultantes de aplicar la condición indicada.
        /// </summary>
        /// <param name="action">Acción a aplicar</param>
        /// <param name="typeSelector">Condición que determina los tipos a los que se le aplicará la acción.</param>
        /// <returns></returns>
        /// <returns>La instancia del scanner</returns>
        public TypeScanner AddConditionalAction(Action<Type> action, Func<Type, bool> typeSelector)
        {
            this.actions[typeSelector] = action;

            return this;
        }

        /// <summary>
        /// Ejecuta el TypeScanner con la configuración indicada.
        /// </summary>
        /// <returns>Lista de tipos que cumplen con los filtros indicados.</returns>
        public IEnumerable<Type> Play()
        {
            // Verifica que se hayan cargado todos los assemblies.
            if (this.forceAssembliesLoading)
                AssemblyHelper.VerifyLoadedAssemblies(this.assemblyPrefix);

            // Obtiene los assemblies.
            IEnumerable<Assembly> assemblies = AssemblyHelper.GetAssemblies(this.assemblyPrefix);

            // Obtengo los tipos
            IEnumerable<Type> types = GetTypes(assemblies);

            // Aplico la acción configurada para los tipos seleccionados
            ApplyActions(types);

            return types;
        }

        #endregion

        #region Métodos auxiliares
        
        private IEnumerable<Type> GetTypes(IEnumerable<Assembly> assemblies)
        {
            IEnumerable<Type> types;
            if (this.typeCondition == null)
                types = assemblies.SelectMany(a => a.GetTypes());
            else
                types = assemblies.SelectMany(a => a.GetTypes()).Where(this.typeCondition);
            return types;
        }

        private void ApplyActions(IEnumerable<Type> types)
        {
            if (this.globalAction != null || this.actions.Count > 0)
            {
                foreach (var aType in types)
                {
                    // Aplico las acciones para determinados tipos.
                    foreach (var typeSelector in this.actions.Keys)
                    {
                        if (typeSelector.Invoke(aType))
                            this.actions[typeSelector].Invoke(aType);
                    }

                    // Aplico la acción general.
                    if (this.globalAction != null)
                        this.globalAction.Invoke(aType);
                }
            }
        }

        #endregion
    }
}
