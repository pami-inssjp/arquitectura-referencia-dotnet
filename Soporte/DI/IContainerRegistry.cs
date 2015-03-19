using Microsoft.Practices.Unity;

namespace Pami.DotNet.ReferenceArchitecture.Soporte.DI
{
    /// <summary>
    /// Interfaz que deben implementar aquellas clases que tienen que registrar tipos o instancias en el 
    /// container de inyección de dependencias.
    /// </summary>
    public interface IContainerRegistry
    {
        /// <summary>
        /// Permite hacer las registraciones sobre el container indicado.
        /// </summary>
        /// <param name="container">Instancia del container</param>
        void Registrar(IUnityContainer container);
    }
}
