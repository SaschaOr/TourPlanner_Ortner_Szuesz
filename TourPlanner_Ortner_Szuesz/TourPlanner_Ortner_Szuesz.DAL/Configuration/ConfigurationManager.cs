﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;


namespace TourPlanner.DataAccessLayer.Configuration
{
    public static class ConfigurationManager
    {
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
                ApiKey = config["api-key"]
            };
        }

        public class TourPlannerConfiguration
        {
            public string ImageLocation { get; set; }
            public string DatabaseHost { get; set; }
            public string DatabasePort { get; set; }
            public string DatabaseUsername { get; set; }
            public string DatabasePassword { get; set; }
            public string DatabaseName { get; set; }
            public string ApiKey { get; set; }
        }
    }
}
