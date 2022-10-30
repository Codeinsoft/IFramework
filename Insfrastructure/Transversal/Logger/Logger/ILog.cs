using System;

namespace IFramework.Infrastructure.Transversal.Logger
{
    public interface ILog
    {
        void Debug(string message);

        void Debug(string message, Exception exception);

        void Debug(string format, params object[] args);

        void Debug(string format, object arg0);

        void Debug(string format, object arg0, object arg1);

        void Debug(string format, object arg0, object arg1, object arg2);

        void Debug(IFormatProvider provider, string format, params object[] args);

        void Info(string message);

        void Info(string message, Exception exception);

        void Info(string format, params object[] args);

        void Info(string format, object arg0);

        void Info(string format, object arg0, object arg1);

        void Info(string format, object arg0, object arg1, object arg2);

        void Info(IFormatProvider provider, string format, params object[] args);

        void Warn(string message);

        void Warn(string message, Exception exception);

        void Warn(string format, params object[] args);

        void Warn(string format, object arg0);

        void Warn(string format, object arg0, object arg1);

        void Warn(string format, object arg0, object arg1, object arg2);

        void Warn(IFormatProvider provider, string format, params object[] args);

        void Error(string message);

        void Error(string message, Exception exception);

        void Error(string format, params object[] args);

        void Error(string format, object arg0);

        void Error(string format, object arg0, object arg1);

        void Error(string format, object arg0, object arg1, object arg2);

        void Error(IFormatProvider provider, string format, params object[] args);

        void Fatal(string message);

        void Fatal(string message, Exception exception);

        void Fatal(string format, params object[] args);

        void Fatal(string format, object arg0);

        void Fatal(string format, object arg0, object arg1);

        void Fatal(string format, object arg0, object arg1, object arg2);

        void Fatal(IFormatProvider provider, string format, params object[] args);

    }
}
