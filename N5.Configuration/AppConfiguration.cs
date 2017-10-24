using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace N5.Configuration
{
    public static class AppConfiguration
    {
        public static string GetConnectionString(string name)
        {
            var currentDirectory =  Path.GetDirectoryName(Assembly.GetExecutingAssembly().Locati‌​on);            

            var builder = new ConfigurationBuilder()              
              .SetBasePath(currentDirectory)
              .AddJsonFile("appsettings.json");

            var connectionStringConfig = builder.Build();

            return connectionStringConfig.GetConnectionString(name);
        }
    }
}