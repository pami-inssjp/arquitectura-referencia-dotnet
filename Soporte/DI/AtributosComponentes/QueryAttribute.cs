using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pami.DotNet.ReferenceArchitecture.Soporte.DI.AtributosComponentes
{
    /// <summary>
    /// Este atributo se ultiliza para representar query objects
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class QueryAttribute : ComponenteAttribute
    {
    }
}
