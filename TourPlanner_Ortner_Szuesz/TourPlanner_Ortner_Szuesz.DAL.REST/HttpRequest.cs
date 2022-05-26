using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.DAL.Common;
using TourPlanner_Ortner_Szuesz.DAL.Configuration;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Models.Enums;

namespace TourPlanner_Ortner_Szuesz.DAL.REST
{
    public class HttpRequest : IHttpRequest
    {
		private readonly string apiKey = TourPlannerConfigurationManager.GetConfig().ApiKey;
		private HttpClient httpClient;

		public HttpRequest(HttpClient client)
		{
			httpClient = client;
		}

		public async Task<Tour> GetTourFromRequest(Tour tourItem)
		{
			// unit = k (Kilometers)
			var url = "http://www.mapquestapi.com/directions/v2/route?" +
							$"key={apiKey}&from={tourItem.StartLocation}&to={tourItem.EndLocation}&routeType={GetTourTypeString(tourItem)}&unit=k";

			var json = JObject.Parse(await httpClient.GetStringAsync(url));
			tourItem.Distance = (int)json["route"]["distance"];
			tourItem.EstimatedTime = (int)json["route"]["time"];

			return tourItem;
		}
		public async Task<byte[]> GetTourImageFromRequest(Tour tourItem)
		{
			var url = "https://open.mapquestapi.com/staticmap/v5/map?" +
					  $"key={apiKey}&start={tourItem.StartLocation}&end={tourItem.EndLocation}";
			return await httpClient.GetByteArrayAsync(url);
		}

		private string GetTourTypeString(Tour tourItem)
        {
			string tourType;

			switch (tourItem.TransportType)
			{
				case TransportTypes.Car:
					tourType = "fastest";
					break;
				case TransportTypes.Bicycle:
					tourType = "bicycle";
					break;
				case TransportTypes.Walking:
					tourType = "pedestrian";
					break;
				default:
					tourType = "fastest";
					break;

			}

			return tourType;
		}
	}
}
