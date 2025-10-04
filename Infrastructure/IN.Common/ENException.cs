using Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class ENException : ApplicationException
    {
        private readonly ILogging _logger;
        public ENException(string userId, string message)
            : base(message)
        {
            _logger = NLoggerUtil.GetLoggingService();
            _logger.Log(string.Format("User ID: {0}, Error Message: {1}", userId, message));
        }
        public ENException(string userId, Exception innerException)
            : base(innerException.Message, innerException)
        {
            _logger = NLoggerUtil.GetLoggingService();
            _logger.Log(string.Format("User ID: {0}, Inner Exception: {1}", userId, innerException));
        }
    }
}
