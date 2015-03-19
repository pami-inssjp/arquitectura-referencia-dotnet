using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pami.DotNet.ReferenceArchitecture.Soporte.Logging
{
    public interface ILoggerFactory
    {
        /// <summary>
        /// Retorna el logger asociado al nombre dado como parámetro.
        /// </summary>
        /// <param name="name">nombre del logger</param>
        /// <returns>retorna un logger</returns>
        ILogger GetLogger(string name);
    }
}
