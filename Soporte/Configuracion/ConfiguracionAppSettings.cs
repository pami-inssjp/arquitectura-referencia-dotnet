using System;
using System.Configuration;
using Pami.DotNet.ReferenceArchitecture.Soporte.DI.AtributosComponentes;

namespace Pami.DotNet.ReferenceArchitecture.Soporte.Configuracion
{
    /// <summary>
    /// Implementación de la configuración del sistema que se base en el AppSettings
    /// del archivo de configuración.
    /// </summary>
    [Componente]
    public class ConfiguracionAppSettings : IConfiguracion
    {
        #region IConfiguracion

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
        public T ObtenerParametro<T>(string clave)
        {
            string valor = ConfigurationManager.AppSettings[clave];
            if (valor == null)
               throw new Exception(clave);
            try
            {
                return (T)Convert.ChangeType(valor, typeof(T));
            }
            catch (Exception e)
            {
                throw new Exception(clave, e);
            }
        }

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
        public T ObtenerParametro<T>(string clave, T valorPorDefecto)
        {
            string valor = ConfigurationManager.AppSettings[clave];
            if (valor == null) 
                return valorPorDefecto;
            try
            {
                return (T)Convert.ChangeType(valor, typeof(T));
            }
            catch (Exception e)
            {
                throw new Exception(clave, e);
            }
        }

        #endregion
    }
}
