using System;

namespace Pami.DotNet.ReferenceArchitecture.Soporte.DI.AtributosComponentes
{
    /// <summary>
    /// Este atributo se ultiliza para representar repositorios
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class RepositorioAttribute : ComponenteAttribute
    {
    }
}
