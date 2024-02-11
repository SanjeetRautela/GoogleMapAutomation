using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace GoogleMapAutomation.Configurations
{
    public class ConfigHelper
    {
        public static IConfiguration AppSettings { get; set; }

        static ConfigHelper()
        {
            AppSettings = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                      .AddJsonFile("appsettings.json").Build();
        }

    }
}
