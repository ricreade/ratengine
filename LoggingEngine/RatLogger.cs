using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using log4net.Core;

namespace LoggingEngine
{
    public class RatLogger
    {
        private readonly ILog _ilog;

        public RatLogger(ILog LogInterface)
        {
            _ilog = LogInterface;
        }
        public bool IsDebugEnabled
        {
            get
            {
                return _ilog.IsDebugEnabled;
            }
        }

        public bool IsErrorEnabled
        {
            get
            {
                return _ilog.IsErrorEnabled;
            }
        }

        public bool IsFatalEnabled
        {
            get
            {
                return _ilog.IsFatalEnabled;
            }
        }

        public bool IsInfoEnabled
        {
            get
            {
                return _ilog.IsInfoEnabled;
            }
        }

        public bool IsWarnEnabled
        {
            get
            {
                return _ilog.IsWarnEnabled;
            }
        }

        public void Debug(object message)
        {
            _ilog.Debug(message);
        }

        public void Debug(object message, Exception exception)
        {
            _ilog.Debug(message, exception);
        }

        public void DebugFormat(string format, object arg0)
        {
            _ilog.DebugFormat(format, arg0);
        }

        public void DebugFormat(string format, params object[] args)
        {
            _ilog.DebugFormat(format, args);
        }

        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            _ilog.DebugFormat(provider, format, args);
        }

        public void DebugFormat(string format, object arg0, object arg1)
        {
            _ilog.DebugFormat(format, arg0, arg1);
        }

        public void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            _ilog.DebugFormat(format, arg0, arg1, arg2);
        }

        public void Error(object message)
        {
            _ilog.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            _ilog.Error(message, exception);
        }

        public void ErrorFormat(string format, object arg0)
        {
            _ilog.ErrorFormat(format, arg0);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            _ilog.ErrorFormat(format, args);
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            _ilog.ErrorFormat(provider, format, args);
        }

        public void ErrorFormat(string format, object arg0, object arg1)
        {
            _ilog.ErrorFormat(format, arg0, arg1);
        }

        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            _ilog.ErrorFormat(format, arg0, arg1, arg2);
        }

        public void Fatal(object message)
        {
            _ilog.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            _ilog.Fatal(message, exception);
        }

        public void FatalFormat(string format, object arg0)
        {
            _ilog.FatalFormat(format, arg0);
        }

        public void FatalFormat(string format, params object[] args)
        {
            _ilog.FatalFormat(format, args);
        }

        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            _ilog.FatalFormat(provider, format, args);
        }

        public void FatalFormat(string format, object arg0, object arg1)
        {
            _ilog.FatalFormat(format, arg0, arg1);
        }

        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            _ilog.FatalFormat(format, arg0, arg1, arg2);
        }

        public void Info(object message)
        {
            _ilog.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            _ilog.Info(message, exception);
        }

        public void InfoFormat(string format, object arg0)
        {
            _ilog.InfoFormat(format, arg0);
        }

        public void InfoFormat(string format, params object[] args)
        {
            _ilog.InfoFormat(format, args);
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            _ilog.InfoFormat(provider, format, args);
        }

        public void InfoFormat(string format, object arg0, object arg1)
        {
            _ilog.InfoFormat(format, arg0, arg1);
        }

        public void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            _ilog.InfoFormat(format, arg0, arg1, arg2);
        }

        public void Warn(object message)
        {
            _ilog.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            _ilog.Warn(message, exception);
        }

        public void WarnFormat(string format, object arg0)
        {
            _ilog.WarnFormat(format, arg0);
        }

        public void WarnFormat(string format, params object[] args)
        {
            _ilog.WarnFormat(format, args);
        }

        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            _ilog.WarnFormat(provider, format, args);
        }

        public void WarnFormat(string format, object arg0, object arg1)
        {
            _ilog.WarnFormat(format, arg0, arg1);
        }

        public void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            _ilog.WarnFormat(format, arg0, arg1, arg2);
        }
    }
}
