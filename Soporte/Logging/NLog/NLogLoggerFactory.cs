using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pami.DotNet.ReferenceArchitecture.Soporte.Logging.NLog
{
    /// <summary>
    /// Crea y maneja instancias de loggers.
    /// </summary>
    public class NLogLoggerFactory : ILoggerFactory
    {
        public ILogger GetLogger(string name)
        {
            return new NLogLogger(name);
        }
    }
}
