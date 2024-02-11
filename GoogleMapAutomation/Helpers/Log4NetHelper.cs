using log4net;
using System;
using System.IO;
using System.Reflection;

namespace GoogleMapAutomation.Helpers
{
    public class Log4NetHelper
    {
        private static ILog? _xmlLogger;

        public static ILog GetXmlLogger(Type type)
        {
            if (_xmlLogger != null)
                return _xmlLogger;

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            log4net.Config.XmlConfigurator.Configure(logRepository, new FileInfo("log4Net.config"));
            _xmlLogger = LogManager.GetLogger(type);

            return _xmlLogger;
        }
    }
}
