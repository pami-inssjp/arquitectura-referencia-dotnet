using System.Collections;
using System.Web;

namespace Pami.DotNet.ReferenceArchitecture.Soporte.Utils
{
    /// <summary>
    /// Contexto de la operación actual.
    /// </summary>
    public static class OperacionContext
    {
        /// <summary>
        /// Almacenamiento del contexto de la operación. Permite almacenar datos que se mantendrán
        /// durante el ciclo de vide la operación.
        /// </summary>
        public static IDictionary Datos
        {
            get
            {
                if (HttpContext.Current != null)
                    return HttpContext.Current.Items;
                else
                    return ThreadContext.Current.Items;
            }
        }

    }
}
