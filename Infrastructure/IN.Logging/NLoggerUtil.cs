using NLog;
using NLog.Config;

namespace Logging
{
    public static class NLoggerUtil
    {
        public static ILogging GetLoggingService(string configPath = null)
        {
            if (!string.IsNullOrWhiteSpace(configPath))
                LogManager.Configuration = new XmlLoggingConfiguration(configPath);
            return (ILogging)LogManager.GetLogger("NLogLogger", typeof(NLoggingService));
        }
    }
}