using System;

namespace Pami.DotNet.ReferenceArchitecture.Soporte.DI.AtributosComponentes
{
    /// <summary>
    /// Se utiliza para identificar servicios del dominio
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ServicioDominioAttribute : ComponenteAttribute
    {
    }
}
