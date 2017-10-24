using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Collections;

namespace N5.Core.Configuration
{
    public static class AppConfiguration
    {
        static IConfiguration Configuration;

        static AppConfiguration()
        {
            var currentDirectory = Path.GetDirectoryName(Assembly.GetAssembly(typeof(AppConfiguration)).Locati‌​on);

            var builder = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddJsonFile("./Configuration/appsettings.json");

            
            Configuration = builder.Build();
        }

        public static string GetConnectionString(string name)
        {
            return Configuration.GetConnectionString(name);
        }

        public static Dictionary<string, object> MailSettings { get; private set; }

        public static string GetConfiguration(string key)
        {
            return Configuration[key];
        }

        public static Dictionary<string, object> GetDictionary(string key)
        {
            return Configuration.GetSection(key).GetChildren()
                            .Select(item => new KeyValuePair<string, object>(item.Key, item.Value))
                            .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
