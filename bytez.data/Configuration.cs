using Microsoft.Extensions.Configuration;

namespace bytez.data
{
    static class Configuration
    {
        static public string ConnectionString
        {

            get
            {
                ConfigurationManager configurationManager = new();
                try
                {

                    var extension = "../bytez.webui";
                    configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), extension));
                    configurationManager.AddJsonFile("appsettings.json");
                }
                catch
                {
                   configurationManager.AddJsonFile("appsettings.json");
                }
                
                return configurationManager.GetConnectionString("SQLServer");
            }
        }
    }
}