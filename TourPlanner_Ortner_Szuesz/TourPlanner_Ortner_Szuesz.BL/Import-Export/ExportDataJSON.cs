using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.BL.Import_Export
{
    public class ExportDataJSON
    {
        public ILogger Logger { get; }
        private ITourLogManager mediaManager { get; set; }

        public ExportDataJSON(ILogger logger)
        {
            Logger = logger;
        }

        public void Export(ObservableCollection<Tour> tours, string filePath)
        {
            if(!File.Exists(filePath))
            {
                Logger.LogWarning($"{DateTime.Now}: [WARNING] file path for tour data export does not exist.");
                return;
            }

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
            mediaManager = TourManagerFactory.GetTourLogFactoryManager(Logger);

            foreach (var tour in tours)
            {
                tour.TourLogs = mediaManager.GetItems(tour.Id);
            }
        }
    }
}
