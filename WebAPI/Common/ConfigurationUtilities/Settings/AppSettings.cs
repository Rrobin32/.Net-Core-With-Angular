using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationUtilities.Settings
{
    public class AppSettings
    {
        public static string ContentRootPath { get; set; }
        public static string EnvironmentName { get; set; }
        public static string DateFormat { get; set; }
        public static string JsonPath { get; set; }
        public static List<RequestJson> RequestJson { get; set; }
    }

    public class JWT
    {
        public static string ValidAudience { get; set; }
        public static string ValidIssuer { get; set; }
        public static string Secret { get; set; }
        public static string ExpireTime { get; set; }
    }
    public class RequestJson
    {
        public string ActionName { get; set; }
    }

    public class ConnectionStrings
    {
        public static string DBConnect { get; set; }
        public static string Database { get; set; }
    }
}
