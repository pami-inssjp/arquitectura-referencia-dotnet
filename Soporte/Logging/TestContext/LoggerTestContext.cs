using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Profertil.ProLab.Infraestructura.Soporte.Logging.TestContext
{
    /// <summary>
    /// Inicializa el mecanísmo adecuado de logging para los test unitarios.
    /// </summary>
    public class LoggerTestContext
    {
        public LoggerTestContext(ILoggerFactory loggerFactory)
        {
            LoggerManager.LoggerFactory = loggerFactory;
        }
    }
}
