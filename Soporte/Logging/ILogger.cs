using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pami.DotNet.ReferenceArchitecture.Soporte.Logging
{
    /// <summary>
    /// Interface que implementan todos los loggers.
    /// Define métodos de logging. 
    /// Permite:
    /// <list>
    /// <item>Loguear un mensaje directo</item>
    /// <item>Loguear excepciones completas</item>
    /// <item>Loguear un mensaje a partir de un patron y sus parámetros</item>
    /// <item>Distintos niveles de severidad: Trace, Debug, Info, Warn, Error, Fatal</item>
    /// </sumary>
    public interface ILogger
    {
        #region Log

        bool IsEnabled(LoggerLevel level);
        void Log(LoggerLevel level, object message, Exception exception);
        void LogFormat(LoggerLevel level, string format, Exception exception, params object[] args);
        
        #endregion

        #region Trace

        bool IsTraceEnabled();
        void Trace(object message);
        void Trace(object message, Exception exception);
        void TraceFormat(string format, params object[] args);
        void TraceFormat(string format, Exception exception, params object[] args);
        
        #endregion

        #region Debug

        bool IsDebugEnabled();
        void Debug(object message);
        void Debug(object message, Exception exception);
        void DebugFormat(string format, params object[] args);
        void DebugFormat(string format, Exception exception, params object[] args);
        
        #endregion

        #region Info

        bool IsInfoEnabled();
        void Info(object message);
        void Info(object message, Exception exception);
        void InfoFormat(string format, params object[] args);
        void InfoFormat(string format, Exception exception, params object[] args);
        
        #endregion

        #region Warn

        bool IsWarnEnabled();
        void Warn(object message);
        void Warn(object message, Exception exception);
        void WarnFormat(string format, params object[] args);
        void WarnFormat(string format, Exception exception, params object[] args);
        
        #endregion

        #region Error

        bool IsErrorEnabled();
        void Error(object message);
        void Error(object message, Exception exception);
        void ErrorFormat(string format, params object[] args);
        void ErrorFormat(string format, Exception exception, params object[] args);
        
        #endregion

        #region Fatal

        bool IsFatalEnabled();
        void Fatal(object message);
        void Fatal(object message, Exception exception);
        void FatalFormat(string format, params object[] args);
        void FatalFormat(string format, Exception exception, params object[] args);
        
        #endregion
    }
}