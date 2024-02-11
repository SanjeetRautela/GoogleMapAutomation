using GoogleMapAutomation.Enums;
using System;

namespace GoogleMapAutomation.Configurations
{
    public class AppSettings
    {
        public static string Url = ConfigHelper.AppSettings["Url"];
        public static Browser Browser = Enum.Parse<Browser>(ConfigHelper.AppSettings["Browser"]);
        public static bool IsHeadless = Convert.ToBoolean(ConfigHelper.AppSettings["Headless"]);
        public static string TestReportFolderName = ConfigHelper.AppSettings["TestReportFolderName"];
        public static string OutputReportName = ConfigHelper.AppSettings["OutputReportName"];
        public static string SeleniumVersion = ConfigHelper.AppSettings["SeleniumVersion"];
        public static string ReportName = ConfigHelper.AppSettings["ReportName"];
    }
}
