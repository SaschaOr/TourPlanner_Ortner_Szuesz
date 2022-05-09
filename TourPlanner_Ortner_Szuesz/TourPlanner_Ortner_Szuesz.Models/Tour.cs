using TourPlanner_Ortner_Szuesz.Models.Enums;

namespace TourPlanner_Ortner_Szuesz.Models
{
    public class Tour
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string Start { get; set; }
        public string Destination { get; set; }
        public TransportTypes TransportType { get; set; }
        public int Distance { get; set; }
        public int EstimatedTime { get; set; }
        public string RouteInformation { get; set; }

        public Tour(int id, string name, string description, string start, string destination, TransportTypes transportType, int distance, int estimatedTime, string routeInformation)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Start = start;
            this.Destination = destination;
            this.TransportType = transportType;
            this.Distance = distance;
            this.EstimatedTime = estimatedTime;
            this.RouteInformation = routeInformation;
        }

        // for testing purposes
        public Tour(string name)
        {
            this.Name = name;
        }
    }
}
