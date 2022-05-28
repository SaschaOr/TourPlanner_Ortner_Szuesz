using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.BL.Import_Export
{
    public class ExportDataCSV
    {
		public ILogger Logger { get; }

        public ExportDataCSV(ILogger logger)
        {
			Logger = logger;
        }

		public void Export(ObservableCollection<Tour> tours, string file)
		{
			var lines = GetLines(tours);
			File.WriteAllLines(file, lines);
		}

		private List<string> GetLines(ObservableCollection<Tour> tours)
		{
			var tourProperties = typeof(Tour).GetProperties();

			string header = "";
			var firstDone = false;

			foreach (var prop in tourProperties)
			{
				if (!firstDone)
				{
					header += prop.Name;
					firstDone = true;
				}
				else
				{
					header += ";" + prop.Name;
				}
			}

			List<string> lines = new List<string>();
			lines.Add(header);
			foreach (var obj in tours)
			{
				firstDone = false;
				string line = "";
				foreach (var prop in tourProperties)
				{
					var value = prop.GetValue(obj, null)?.ToString();

					if (typeof(string) == prop.PropertyType)
					{
						value = "\"" + value + "\"";
					}

					if (!firstDone)
					{
						line += value;
						firstDone = true;
					}
					else
					{
						line += ";" + value;
					}
				}
				lines.Add(line);
			}

			return lines;
		}
	}
}
