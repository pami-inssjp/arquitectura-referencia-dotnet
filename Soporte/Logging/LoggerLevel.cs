using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pami.DotNet.ReferenceArchitecture.Soporte.Logging
{
    /// <summary>
    /// Identifica el nivel de log
    /// </summary>
    public enum LoggerLevel
    {
        /// <summary>
        /// Trace log level.
        /// </summary>
        Trace = 0,
        
        /// <summary>
        /// Debug log level.
        /// </summary>
        Debug = 1,
        
        /// <summary>
        /// Info log level.
        /// </summary>
        Info = 2,
        
        /// <summary>
        /// Warn log level.
        /// </summary>
        Warn = 3,
        
        /// <summary>
        /// Error log level.
        /// </summary>
        Error = 4,
        
        /// <summary>
        /// Fatal log level.
        /// </summary>
        Fatal = 5
    }
}
