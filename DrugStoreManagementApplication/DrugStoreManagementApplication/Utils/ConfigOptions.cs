using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStoreManagementApplication.Utils
{
    public class Config
    {
        // DB
        public static readonly string DatabaseServer;
        public static readonly string DatabaseName;
        public static readonly string DatabaseUserId;
        public static readonly string DatabasePassword;
        public static readonly int MaxPoolSize = 10;

        static Config()
        {
            DatabaseServer = ConfigurationManager.AppSettings["server"];
            DatabaseName = ConfigurationManager.AppSettings["databaseName"];
            DatabaseUserId = ConfigurationManager.AppSettings["userId"];
            DatabasePassword = ConfigurationManager.AppSettings["password"];
            int.TryParse(ConfigurationManager.AppSettings["maxPoolSize"], out MaxPoolSize);
        }
    }
}
