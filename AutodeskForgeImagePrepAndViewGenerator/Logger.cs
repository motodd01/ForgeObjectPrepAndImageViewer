using System;
using NLog;

namespace AutodeskForgeObjectPrepAndViewGenerator
{
    public interface ILogger
    {
        void Log(LogType messageType, string message);
    }

    public enum LogType
    {
        Debug,
        Info,
        Warning,
        Error,
        Fatal
    }

    public class Logger : ILogger
    {
        protected readonly NLog.Logger Nlog;

        public Logger(string name)
        {
            Nlog = LogManager.GetLogger(name);
        }

        public void Log(LogType messageType, string message)
        {
            LogLevel logLevel;
            switch (messageType)
            {
                case LogType.Debug:
                    logLevel = LogLevel.Debug;
                    break;
                case LogType.Info:
                    logLevel = LogLevel.Info;
                    break;
                case LogType.Warning:
                    logLevel = LogLevel.Warn;
                    break;
                case LogType.Error:
                    logLevel = LogLevel.Error;
                    break;
                case LogType.Fatal:
                    logLevel = LogLevel.Fatal;
                    break;
                default:
                    throw new ArgumentException("Log message type is not supported");
            }

            var logEvent = new LogEventInfo(logLevel, Nlog.Name, message);

            Nlog.Log(typeof(Logger), logEvent);
        }
    }
}