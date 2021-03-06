using System.Collections.Generic;
using TourPlanner_Ortner_Szuesz.Models.Enums;

namespace TourPlanner_Ortner_Szuesz.Models
{
    public class Tour
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public TransportTypes TransportType { get; set; }
        public int Distance { get; set; }
        public int EstimatedTime { get; set; }
        public string RouteImagePath { get; set; }
        public byte[] RouteImage { get; set; }
        public bool IsFavourite { get; set; }
        public IEnumerable<TourLog> TourLogs { get; set; }

        public Tour(int id, string name, string description, string startLocation, string endLocation, TransportTypes transportType, int distance, int estimatedTime, string routeImagePath, bool isFavourite)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.StartLocation = startLocation;
            this.EndLocation = endLocation;
            this.TransportType = transportType;
            this.Distance = distance;
            this.EstimatedTime = estimatedTime;
            this.RouteImagePath = routeImagePath;
            this.IsFavourite = isFavourite;
        }

        public Tour(int id, string name, string description, string startLocation, string endLocation, TransportTypes transportType)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.StartLocation = startLocation;
            this.EndLocation = endLocation;
            this.TransportType = transportType;
        }

        public Tour()
        {

        }
    }
}
