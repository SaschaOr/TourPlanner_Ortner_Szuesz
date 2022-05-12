using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.DAL.Common;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.DAL.REST
{
    public class HttpRequest : IHttpRequest
    {
		private readonly string apiKey = "vX1MVXWPd5rlVGG5x5r3GPGkuYJ8IPfw";
		private HttpClient httpClient;

		public HttpRequest(HttpClient client)
		{
			httpClient = client;
		}

		public async Task<Tour> GetTourFromRequest(Tour tourItem)
		{
			var url = "http://www.mapquestapi.com/directions/v2/route?" +
							$"key={apiKey}&from={tourItem.StartLocation}&to={tourItem.EndLocation}";

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
	}
}
