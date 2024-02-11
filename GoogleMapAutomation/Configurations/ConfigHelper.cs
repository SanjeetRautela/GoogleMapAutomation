using Microsoft.Extensions.Configuration;

namespace GoogleMapAutomation.Configurations
{
    public class ConfigHelper
    {
        public static IConfiguration AppSettings { get; set; }

        static ConfigHelper()
        {
            AppSettings = new ConfigurationBuilder()
                      .AddJsonFile("appSettings.json").Build();
        }

    }
}
