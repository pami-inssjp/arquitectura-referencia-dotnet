using System;

namespace Pami.DotNet.ReferenceArchitecture.Soporte.DI.AtributosComponentes
{
    /// <summary>
    /// Identifica a los mappers entre objetos.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class MapperAttribute : Attribute
    {
    }
}
