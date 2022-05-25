using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.BL.Import_Export
{
    public class ExportDataJSON
    {
        ITourLogManager mediaManager { get; set; }

        public void Export(ObservableCollection<Tour> tours, string filePath)
        {
            FillTourLogsInTours(tours);

            var serialise = new JsonSerializer();
            using (var ns = new StreamWriter(filePath))
            using (JsonWriter writer = new JsonTextWriter(ns))
            {
                serialise.Serialize(writer, tours);
            }
        }

        private void FillTourLogsInTours(ObservableCollection<Tour> tours)
        {
            mediaManager = TourManagerFactory.GetTourLogFactoryManager();

            foreach (var tour in tours)
            {
                tour.TourLogs = mediaManager.GetItems(tour.Id);
            }
        }
    }
}
