using System.IO;
using Microsoft.Extensions.Configuration;


namespace TourPlanner_Ortner_Szuesz.DAL.Configuration
{
    public static class TourPlannerConfigurationManager
    {
        public class TourPlannerConfiguration
        {
            public string DatabaseHost { get; set; }
            public string DatabasePort { get; set; }
            public string DatabaseUsername { get; set; }
            public string DatabasePassword { get; set; }
            public string DatabaseName { get; set; }
            public string ApiKey { get; set; }
            public string ImageLocation { get; set; }
            public string DefaultImageLocation { get; set; }
            public string ReportLocation { get; set; }
            public string ExportLocation { get; set; }
        }

        public static TourPlannerConfiguration GetConfig()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile(Directory.GetCurrentDirectory() + "\\Configuration\\settings.json", false, true)
                .Build();

            return new TourPlannerConfiguration
            {
                ImageLocation = config["image-location"],
                DatabaseHost = config["db:host"],
                DatabasePort = config["db:port"],
                DatabaseUsername = config["db:username"],
                DatabasePassword = config["db:password"],
                DatabaseName = config["db:database"],
                ApiKey = config["api-key"],
                DefaultImageLocation = config["default-image-location"],
                ReportLocation = config["report-location"],
                ExportLocation = config["export-location"]
            };
        }
    }
}
