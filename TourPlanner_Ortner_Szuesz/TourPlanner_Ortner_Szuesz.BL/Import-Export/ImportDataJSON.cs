using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.BL.Import_Export
{
    public class ImportDataJSON
    {
        public ILogger Logger { get; }

        public ImportDataJSON(ILogger logger)
        {
            Logger = logger;
        }

        public ObservableCollection<Tour> Import(string filePath)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show(filePath);
                Logger.LogWarning($"{DateTime.Now}: [WARNING] file path for tour data import does not exist.");
                return new ObservableCollection<Tour>();
            }

            ObservableCollection<Tour> tours = new ObservableCollection<Tour>();
            var serialise = new JsonSerializer();
            
            using (var ns = new StreamReader(filePath)) 
            {
                string json = ns.ReadToEnd();
                tours = JsonConvert.DeserializeObject<ObservableCollection<Tour>>(json);
            }
            
            return tours;
        }
    }
}
