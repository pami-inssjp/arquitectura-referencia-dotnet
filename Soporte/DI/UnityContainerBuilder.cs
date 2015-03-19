using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Pami.DotNet.ReferenceArchitecture.Soporte.Configuracion;

namespace Pami.DotNet.ReferenceArchitecture.Soporte.DI
{
    /// <summary>
    /// Construye una instancia del container de inyección de dependencias, 
    /// rastrendo instancias de <code>IContainerRegistry</code> entre los assembies 
    /// de la aplicación, y ejecutándolas para realizar una registración distribuida 
    /// de la registración de componentes.
    /// </summary>
    public class UnityContainerBuilder
    {

        #region Constantes

        /// <summary>
        /// Prefijo de los assemblies del proyecto. Se utiliza para filtrar los assemblies 
        /// a escanear en búsqueda de las registries del container.
        /// </summary>
        public const string AssemblyPrefix = "Pami.DotNet";

        #endregion

        #region Variables de instancia

        /// <summary>
        ///  Lista de registries encontradas.
        /// </summary>
        IEnumerable<IContainerRegistry> registries;
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public UnityContainerBuilder()
        {
            this.registries = new ContainerRegistryCollector(AssemblyPrefix).Collect();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public UnityContainerBuilder(IEnumerable<IContainerRegistry> registries)
        {
            this.registries = registries ?? new List<IContainerRegistry>();
        }

        #endregion

        #region Metodos Publicos

        /// <summary>
        /// Ejecuta la construcción del container.
        /// </summary>
        /// <returns>Instancia del container</returns>
        public virtual IUnityContainer Construir()
        {
            // Crea el container
            IUnityContainer container = new UnityContainer();
            
            // Configura el Service Locator
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));
            
            // Realiza la registración distribuida.
            RegistrarComponentes(container);

            return container;
        }

        #endregion

        #region Métodos privados

        /// <summary>
        /// Registra los componentes del sistema a través de las <code>IContainerRegistry</code> encontradas.
        /// </summary>
        /// <param name="container">Instancia del container</param>
        protected virtual void RegistrarComponentes(IUnityContainer container)
        {
            // Registro el componente de configuración, antes que nada
            container.RegisterType<IConfiguracion, ConfiguracionAppSettings>();

            if (this.registries != null)
            {
                foreach (var registry in this.registries)
                    registry.Registrar(container);
            }
        }

        #endregion

    }
}
