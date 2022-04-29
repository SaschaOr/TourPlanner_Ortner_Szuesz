namespace TourPlanner_Ortner_Szuesz.Models
{
    public class Tour
    {
        public string Name { get; set; }
        public string TourDescription { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public TransportTypes TransportType { get; set; }
        public int TourDistance { get; set; }
        public int EstimatedTime { get; set; }
        public string RouteInformation { get; set; }

        public Tour(string name, string tourDescription, string to, string from, TransportTypes transportType, int tourDistance, int estimatedTime, string routeInformation)
        {
            this.Name = name;
            this.TourDescription = tourDescription;
            this.To = to;
            this.From = from;
            this.TransportType = transportType;
            this.TourDistance = tourDistance;
            this.EstimatedTime = estimatedTime;
            this.RouteInformation = routeInformation;
        }
    }
}
