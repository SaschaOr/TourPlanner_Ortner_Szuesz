using Newtonsoft.Json;
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
        public ObservableCollection<Tour> Import(string filePath)
        {
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
