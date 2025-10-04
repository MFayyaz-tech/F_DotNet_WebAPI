using System;
using NLog;

namespace Logging
{
    public class NLoggingService : Logger, ILogging
    {
        private const string LoggerName = "NLogLogger";

        public new void Debug(Exception exception, string format, params object[] args)
        {
            if (!this.IsDebugEnabled)
                return;
            this.Log(typeof(NLoggingService), this.GetLogEvent(LoggerName, LogLevel.Debug, exception, format, args));
        }

        public new void Error(Exception exception, string format, params object[] args)
        {
            //if (!this.IsErrorEnabled)
            //    return;
            this.Log(typeof(NLoggingService), this.GetLogEvent(LoggerName, LogLevel.Error, exception, format, args));
        }

        public new void Fatal(Exception exception, string format, params object[] args)
        {
            if (!this.IsFatalEnabled)
                return;
            this.Log(typeof(NLoggingService), this.GetLogEvent(LoggerName, LogLevel.Fatal, exception, format, args));
        }

        public new void Info(Exception exception, string format, params object[] args)
        {
            if (!this.IsInfoEnabled)
                return;
            this.Log(typeof(NLoggingService), this.GetLogEvent(LoggerName, LogLevel.Info, exception, format, args));
        }

        public new void Trace(Exception exception, string format, params object[] args)
        {
            if (!this.IsTraceEnabled)
                return;
            this.Log(typeof(NLoggingService), this.GetLogEvent(LoggerName, LogLevel.Trace, exception, format, args));
        }

        public new void Warn(Exception exception, string format, params object[] args)
        {
            if (!this.IsWarnEnabled)
                return;
            this.Log(typeof(NLoggingService), this.GetLogEvent(LoggerName, LogLevel.Warn, exception, format, args));
        }

        public void Log(Exception exception)
        {
            this.Error(exception, string.Empty);
        }

        public void Log(string message)
        {
            // if (!this.IsDebugEnabled)
            //    return;
            message = $"{DateTime.Now}, Message: {message}";
            LogEventInfo logEventInfo = new LogEventInfo(LogLevel.Info, LoggerName, message);
            this.Log(typeof(NLoggingService), logEventInfo);
        }

        public void Debug(Exception exception)
        {
            this.Debug(exception, string.Empty);
        }

        public void Error(Exception exception)
        {
            this.Error(exception, string.Empty);
        }

        public new void Error(string message)
        {
            message = $"{DateTime.Now}, Message: {message}";
            LogEventInfo logEventInfo = new LogEventInfo(LogLevel.Error, LoggerName, message);
            this.Log(typeof(NLoggingService), logEventInfo);
        }

        public void Fatal(Exception exception)
        {
            this.Fatal(exception, string.Empty);
        }

        public new void Fatal(string message)
        {
            message = $"{DateTime.Now}, Message: {message}";
            LogEventInfo logEventInfo = new LogEventInfo(LogLevel.Fatal, LoggerName, message);
            this.Log(typeof(NLoggingService), logEventInfo);
        }

        public void Info(Exception exception)
        {
            this.Info(exception, string.Empty);
        }

        public new void Info(string message)
        {
            message = $"{DateTime.Now}, Message: {message}";
            LogEventInfo logEventInfo = new LogEventInfo(LogLevel.Info, LoggerName, message);
            this.Log(typeof(NLoggingService), logEventInfo);
        }

        public void Trace(Exception exception)
        {
            this.Trace(exception, string.Empty);
        }

        public void Warn(Exception exception)
        {
            this.Warn(exception, string.Empty);
        }

        private LogEventInfo GetLogEvent(
          string loggerName,
          LogLevel level,
          Exception exception,
          string format,
          object[] args)
        {
            string str1 = string.Empty;
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            string str2 = string.Empty;
            string str3 = string.Empty;
            LogEventInfo logEventInfo = new LogEventInfo(level, loggerName, string.Format(format, args));
            if (exception != null)
            {
                str1 = exception.Source;
                logEventInfo.Exception = exception;
                logEventInfo.Message = exception.Message;
                str3 = exception.StackTrace;
                if (exception.InnerException != null)
                    str2 = exception.InnerException.Message;
            }
            logEventInfo.Properties["AssemblyName"] = str1;
            logEventInfo.Properties["ClassName"] = empty1;
            logEventInfo.Properties["MethodName"] = empty2;
            logEventInfo.Properties["StackTrace"] = str3;
            logEventInfo.Properties["InnerMessage"] = str2;
            return logEventInfo;
        }
    }
}
