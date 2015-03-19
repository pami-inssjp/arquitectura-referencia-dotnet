using Microsoft.Practices.Unity;
using NHibernate;
using Pami.DotNet.ReferenceArchitecture.DataAccess.Base;
using Pami.DotNet.ReferenceArchitecture.DataAccess.Repositorios;
using Pami.DotNet.ReferenceArchitecture.DataAccess.Repositorios.Base;
using Pami.DotNet.ReferenceArchitecture.Modelo.Repositorios;
using Pami.DotNet.ReferenceArchitecture.Soporte.DI;

namespace Pami.DotNet.ReferenceArchitecture.DataAccess
{
    /// <summary>
    /// Registra los componentes específicos del módulo de acceso a datos en el container de inyección de dependencias.
    /// </summary>
    public class ContainerRegistry : IContainerRegistry
    {
        /// <summary>
        /// Registra los componentes en el containers.
        /// </summary>
        /// <param name="container">Container</param>
        public void Registrar(IUnityContainer container)
        {
            // Registraciones de los principales componentes de NHibernate para el manejo de las sesiones.
            container.RegisterType<ISessionFactoryProvider, SessionFactoryProvider>(new ContainerControlledLifetimeManager());
            IConfiguracionProvider nhconfigurationProvider = new ConfiguracionProvider();
            container.RegisterInstance<IConfiguracionProvider> (nhconfigurationProvider, new ContainerControlledLifetimeManager());
            
            var sessionFactoryProvider = container.Resolve<ISessionFactoryProvider>();
            container.RegisterType<ISessionFactory>(new InjectionFactory(c => sessionFactoryProvider.GetSessionFactory()));
            container.RegisterType<IContextoPersistenciaAsincronico, ContextoPersistenciaAsincronico>();

            // Repositorios genéricos
            container.RegisterType(typeof(IEntidadAdministrableRepo<>), typeof(EntidadRepositorio<>), new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(IEntidadDeReferenciaRepo<>), typeof(EntidadRepositorio<>), new ContainerControlledLifetimeManager());

            //Repositorios específicos
            container.RegisterType<IMedicamentoRepo, MedicamentoRepositorio>();
        }
    }

}
