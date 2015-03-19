using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Pami.DotNet.ReferenceArchitecture.Soporte.DI
{
    /// <summary>
    /// Recolecta las instancias de <code>IContainerRegistry</code> que se encuentren entre los assemblies de la aplicación.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Escanea los assemblies que cumplen con el prefijo de nombre de assembly, buscando clases que implementen la interfaz 
    /// <code>IContainerRegistry</code>. Luego crea las instancias de dichas clases para que el ContainerManager 
    /// las utilice en el proceso de registración.
    /// </para>
    /// <para>
    /// Utiliza TypeScanner para escanear los assemblies.
    /// </para>
    /// </remarks>
    /// TODO: Reemplazar el Trace por el mecanismo de logging.
    public class ContainerRegistryCollector
    {

        #region Variables de Instancia

        /// <summary>
        /// Prefijo del nombre del assembly utilizado para identificar a los assemblies en los que se rastreen instancias de 
        /// <code>IContainerRegistry</code>.
        /// </summary>
        private string assemblyPrefix;

        /// <summary>
        /// Lista de registries.
        /// </summary>
        private Collection<IContainerRegistry> registries;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="assemblyPrefix">Base del namespace que se utiliza para identificar a los assemblies en los 
        /// que se rastreen instancias de <code>IContainerRegistry</code>.
        /// </param>
        public ContainerRegistryCollector(string assemblyPrefix)
        {
            this.assemblyPrefix = assemblyPrefix;
        }

        #endregion

        #region Metodos Publicos

        /// <summary>
        /// Escanea los assemblies que respetan el BaseNamespace, identifica las clases que implementan la interfaz
        /// <code>IContainerRegistry</code>, crea las instancias correspondientes a dichas clases y las devuelve.
        /// </summary>
        /// <returns>Conjunto de instancias de <code>IContainerRegistry</code> encontradas</returns>
        public IEnumerable<IContainerRegistry> Collect()
        {
            this.registries = new Collection<IContainerRegistry>();

            Trace.TraceInformation("-------------------------------");
            Trace.TraceInformation("Recolectando IContainerRegistry");
            Trace.TraceInformation("-------------------------------");

            TypeScanner.TypeScanner.Crear()
                .AssemblyPrefix(this.assemblyPrefix)
                .ForceAssembliesLoading()
                .TypeFilter(t => !t.IsInterface && typeof(IContainerRegistry).IsAssignableFrom(t))
                .Action(t => AgregarContainerRegistry(t))
                .Play();
 
            Trace.TraceInformation("-------------------------------");
            Trace.TraceInformation("IContainerRegistry encontradas: " + this.registries.Count);
            Trace.TraceInformation("-------------------------------");

            return registries;
        }

        #endregion

        #region Metodos Privados

        private void AgregarContainerRegistry(Type containerRegistryType)
        {
            Trace.TraceInformation("IContainerRegistry: " + containerRegistryType.FullName);
            this.registries.Add(System.Activator.CreateInstance(containerRegistryType) as IContainerRegistry);
        }

        #endregion

    }
}
