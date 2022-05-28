using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
		
		public ILogger Logger { get; }

		public HttpRequest(HttpClient client, ILogger logger)
		{
			httpClient = client;
			Logger = logger;
		}

		public async Task<Tour> GetTourFromRequest(Tour tourItem)
		{
			
			try
			{
				// unit = k (Kilometers)
				var url = "http://www.mapquestapi.com/directions/v2/route?" +
								$"key={apiKey}&from={tourItem.StartLocation}&to={tourItem.EndLocation}&routeType={GetTourTypeString(tourItem)}&unit=k";

				var json = JObject.Parse(await httpClient.GetStringAsync(url));
				tourItem.Distance = (int)json["route"]["distance"];
				tourItem.EstimatedTime = (int)json["route"]["time"];
			}
			catch
            {
				MessageBox.Show("Could not get tour distance and time! Please try again!");
				Logger.LogCritical($"{DateTime.Now}: [FATAL] could not get tour distance and estimated time from MapQuest API.");

			}

			return tourItem;
		}
		public async Task<byte[]> GetTourImageFromRequest(Tour tourItem)
		{
			byte[] image = null;

			try
			{
				var url = "https://open.mapquestapi.com/staticmap/v5/map?" +
						 $"key={apiKey}&start={tourItem.StartLocation}&end={tourItem.EndLocation}";
				image = await httpClient.GetByteArrayAsync(url);
			}
			catch
            {
				MessageBox.Show("Could not get tour image! Please try again!");
				Logger.LogCritical($"{DateTime.Now}: [FATAL] could not get tour image from MapQuest API.");
			}

			return image;
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
