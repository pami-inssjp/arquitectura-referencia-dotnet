using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace Pami.DotNet.ReferenceArchitecture.Soporte.Logging.NLog
{
    /// <summary>
    /// Logger basado en NLog.
    /// </summary>
    public class NLogLogger : ILogger
    {
        #region Variables de instancia

        private Logger logger;

        #endregion
        
        #region Constructores

        /// <summary>
        /// Crea una instancia del logger a partir de un nombre dado.
        /// </summary>
        /// <param name="name">Nombre del logger</param>
        public NLogLogger(string name)
        {
            this.logger = LogManager.GetLogger(name);
        }

       #endregion

        #region Log

        public bool IsEnabled(LoggerLevel level)
        {
            return logger.IsEnabled(this.GetNLogLevel(level));
        }

        public void Log(LoggerLevel level, object message, Exception exception)
        {
            logger.Log(this.GetNLogLevel(level), this.GetStringMessage(message), exception);
        }

        public void LogFormat(LoggerLevel level, string format, Exception exception, params object[] args)
        {
            logger.Log(this.GetNLogLevel(level), this.FormatMessage(format, args), exception);
        }

        public void LogFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {

        }


        #endregion
        
        #region Trace

        public bool IsTraceEnabled()
        {
            return this.IsEnabled(LoggerLevel.Trace);
        }

        public void Trace(object message)
        {
            this.Trace(message, null);
        }

        public void Trace(object message, Exception exception)
        {
            this.Log(LoggerLevel.Trace, message, exception);
        }

        public void TraceFormat(string format, params object[] args)
        {
            this.TraceFormat(format, null, args);
        }

        public void TraceFormat(string format, Exception exception, params object[] args)
        {
            this.LogFormat(LoggerLevel.Trace, format, exception, args);
        }

        #endregion

        #region Debug

        public bool IsDebugEnabled()
        {
            return this.IsEnabled(LoggerLevel.Debug);
        }

        public void Debug(object message)
        {
            this.Debug(message, null);
        }

        public void Debug(object message, Exception exception)
        {
            this.Log(LoggerLevel.Debug, message, exception);
        }

        public void DebugFormat(string format, params object[] args)
        {
            this.DebugFormat(format, null, args);
        }

        public void DebugFormat(string format, Exception exception, params object[] args)
        {
            this.LogFormat(LoggerLevel.Debug, format, exception, args);
        }

        #endregion

        #region Info

        public bool IsInfoEnabled()
        {
            return this.IsEnabled(LoggerLevel.Info);
        }

        public void Info(object message)
        {
            this.Info(message, null);
        }

        public void Info(object message, Exception exception)
        {
            this.Log(LoggerLevel.Info, message, exception);
        }

        public void InfoFormat(string format, params object[] args)
        {
            this.InfoFormat(format, null, args);
        }

        public void InfoFormat(string format, Exception exception, params object[] args)
        {
            this.LogFormat(LoggerLevel.Info, format, exception, args);
        }

        #endregion

        #region Warn

        public bool IsWarnEnabled()
        {
            return this.IsEnabled(LoggerLevel.Warn);
        }

        public void Warn(object message)
        {
            this.Warn(message, null);
        }

        public void Warn(object message, Exception exception)
        {
            this.Log(LoggerLevel.Warn, message, exception);
        }

        public void WarnFormat(string format, params object[] args)
        {
            this.WarnFormat(format, null, args);
        }

        public void WarnFormat(string format, Exception exception, params object[] args)
        {
            this.LogFormat(LoggerLevel.Warn, format, exception, args);
        }

        #endregion

        #region Error

        public bool IsErrorEnabled()
        {
            return this.IsEnabled(LoggerLevel.Error);
        }

        public void Error(object message)
        {
            this.Error(message, null);
        }

        public void Error(object message, Exception exception)
        {
            this.Log(LoggerLevel.Error, message, exception);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            this.ErrorFormat(format, null, args);
        }

        public void ErrorFormat(string format, Exception exception, params object[] args)
        {
            this.LogFormat(LoggerLevel.Error, format, exception, args);
        }

        #endregion

        #region Fatal

        public bool IsFatalEnabled()
        {
            return this.IsEnabled(LoggerLevel.Fatal);
        }

        public void Fatal(object message)
        {
            this.Fatal(message, null);
        }

        public void Fatal(object message, Exception exception)
        {
            this.Log(LoggerLevel.Fatal, message, exception);
        }

        public void FatalFormat(string format, params object[] args)
        {
            this.FatalFormat(format, null, args);
        }

        public void FatalFormat(string format, Exception exception, params object[] args)
        {
            this.LogFormat(LoggerLevel.Fatal, format, exception, args);
        }

        #endregion

        #region Métodos Auxiliares

        /// <summary>
        /// Traduce un nivel de log al nivel correspondiente en NLog.
        /// </summary>
        private LogLevel GetNLogLevel(LoggerLevel level)
        {
            switch (level)
            {
                case LoggerLevel.Trace:
                    return LogLevel.Trace;
                case LoggerLevel.Debug:
                    return LogLevel.Debug;
                case LoggerLevel.Info:
                    return LogLevel.Info;
                case LoggerLevel.Warn:
                    return LogLevel.Warn;
                case LoggerLevel.Error:
                    return LogLevel.Error;
                case LoggerLevel.Fatal:
                    return LogLevel.Fatal;
                default:
                    throw new ArgumentOutOfRangeException("LoggerLevel", level, "unknown log level"); 
            }
        }

        private string GetStringMessage(object message)
        {
            return message != null ? message.ToString() : null;
        }

        /// <summary>
        /// Retorna un mensaje a partir de un formato de mensaje y sus argumentos.
        /// </summary>
        private string FormatMessage(string format, params object[] args)
        {
            return format != null ? string.Format(format, args) : null;
        }

        #endregion
    }
}
