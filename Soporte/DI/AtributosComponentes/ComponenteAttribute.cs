using System;

namespace Pami.DotNet.ReferenceArchitecture.Soporte.DI.AtributosComponentes
{
    /// <summary>
    /// Representa un componente del sistema
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ComponenteAttribute : Attribute
    {
        public ComponenteAttribute()
            : base()
        {

        }
    }
}
