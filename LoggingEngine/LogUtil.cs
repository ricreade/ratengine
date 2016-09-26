using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;

namespace LoggingEngine
{
    public class LogUtil
    {
        private static LogUtil _logutil;

        private LogUtil()
        {
            string configPath = @"C:\Users\Developer\Source\Repos\ratengine\LoggingEngine\logging.config";
            FileInfo configFile = new FileInfo(configPath);
            log4net.Config.XmlConfigurator.ConfigureAndWatch(configFile);
        }

        public static LogUtil Instance
        {
            get
            {
                if (_logutil == null)
                    _logutil = new LogUtil();
                return _logutil;
            }
        }

        public RatLogger GetLogger(string Name)
        {
            return new RatLogger(LogManager.GetLogger(Name));
        }

        public RatLogger GetLogger(Type type)
        {
            return new RatLogger(LogManager.GetLogger(type));
        }
    }
}
