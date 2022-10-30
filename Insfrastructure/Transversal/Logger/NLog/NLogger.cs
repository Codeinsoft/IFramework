using System;

using NLog;

using IFramework.Infrastructure.Transversal.Logger;

namespace IFramework.Infra.Transversal.Log.NLog
{
    public class NLogger : ILog
    {
        private readonly ILogger _logger;
        public NLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Debug(string message, Exception exception)
        {
            _logger.Debug(message, exception);
        }

        public void Debug(string format, params object[] args)
        {
            _logger.Debug(format, args);
        }

        public void Debug(string format, object arg0)
        {
            _logger.Debug(format, arg0);
        }

        public void Debug(string format, object arg0, object arg1)
        {
            _logger.Debug(format, arg0, arg1);
        }

        public void Debug(string format, object arg0, object arg1, object arg2)
        {
            _logger.Debug(format, arg0, arg1, arg2);
        }

        public void Debug(IFormatProvider provider, string format, params object[] args)
        {
            _logger.Debug(provider, format, args);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Info(string message, Exception exception)
        {
            _logger.Info(message, exception);
        }

        public void Info(string format, params object[] args)
        {
            _logger.Info(format, args);
        }

        public void Info(string format, object arg0)
        {
            _logger.Info(format, arg0);
        }

        public void Info(string format, object arg0, object arg1)
        {
            _logger.Info(format, arg0, arg1);
        }

        public void Info(string format, object arg0, object arg1, object arg2)
        {
            _logger.Info(format, arg0, arg1, arg2);
        }

        public void Info(IFormatProvider provider, string format, params object[] args)
        {
            _logger.Info(provider, format, args);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Warn(string message, Exception exception)
        {
            _logger.Warn(message, exception);
        }

        public void Warn(string format, params object[] args)
        {
            _logger.Warn(format, args);
        }

        public void Warn(string format, object arg0)
        {
            _logger.Warn(format, arg0);
        }

        public void Warn(string format, object arg0, object arg1)
        {
            _logger.Warn(format, arg0, arg1);
        }

        public void Warn(string format, object arg0, object arg1, object arg2)
        {
            _logger.Warn(format, arg0, arg1, arg2);
        }

        public void Warn(IFormatProvider provider, string format, params object[] args)
        {
            _logger.Warn(provider, format, args);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            _logger.Error(message, exception);
        }

        public void Error(string format, params object[] args)
        {
            _logger.Error(format, args);
        }

        public void Error(string format, object arg0)
        {
            _logger.Error(format, arg0);
        }

        public void Error(string format, object arg0, object arg1)
        {
            _logger.Error(format, arg0, arg1);
        }

        public void Error(string format, object arg0, object arg1, object arg2)
        {
            _logger.Error(format, arg0, arg1, arg2);
        }

        public void Error(IFormatProvider provider, string format, params object[] args)
        {
            _logger.Error(provider, format, args);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(string message, Exception exception)
        {
            _logger.Fatal(message, exception);
        }

        public void Fatal(string format, params object[] args)
        {
            _logger.Fatal(format, args);
        }

        public void Fatal(string format, object arg0)
        {
            _logger.Fatal(format, arg0);
        }

        public void Fatal(string format, object arg0, object arg1)
        {
            _logger.Fatal(format, arg0, arg1);
        }

        public void Fatal(string format, object arg0, object arg1, object arg2)
        {
            _logger.Fatal(format, arg0, arg1, arg2);
        }

        public void Fatal(IFormatProvider provider, string format, params object[] args)
        {
            _logger.Fatal(provider, format, args);
        }
    }
}
