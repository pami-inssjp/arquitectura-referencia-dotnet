
namespace Pami.DotNet.ReferenceArchitecture.Soporte.Configuracion
{
    /// <summary>
    /// Componente que resuelve la obtención de los parámetros de configuración 
    /// del sistema.
    /// </summary>
    public interface IConfiguracion
    {
        /// <summary>
        /// Devuelve el valor del parámetro de configuración indicado. 
        /// </summary>
        /// <typeparam name="T">Tipo del valor del parámetro</typeparam>
        /// <param name="clave">Nombre del valor del parámetros</param>
        /// <returns>Valor del parámetro de configuración</returns>
        /// <exception cref="NoExisteParametroConfiguracionException">Si el parámetro con el 
        /// nombre indicado no existe en la configuración del sistema.</exception>
        /// <exception cref="ConfiguracionException">Si el valor del parámetro 
        /// no se corresponde con el tipo esperado.</exception>
        T ObtenerParametro<T>(string clave);

        /// <summary>
        /// Devuelve el valor del parámetro de configuración indicado. Si no se especificó
        /// el parámetro en la configuración, devuelve el valor por defecto.
        /// </summary>
        /// <typeparam name="T">Tipo del valor del parámetro</typeparam>
        /// <param name="clave">Nombre del valor del parámetros</param>
        /// <param name="valorPorDefecto">Valor por defecto</param>
        /// <returns>Valor del parámetro de configuración</returns>
        /// <exception cref="ConfiguracionException">Si el valor del parámetro 
        /// no se corresponde con el tipo esperado.</exception>
        T ObtenerParametro<T>(string clave, T valorPorDefecto);

    }
}
