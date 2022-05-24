using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Models.Enums;

namespace TourPlanner_Ortner_Szuesz.BL.Import_Export
{
    public class ImportDataCSV
    {
		public List<KeyValuePair<string, string>> Mappings = new List<KeyValuePair<string, string>>{
		new KeyValuePair<string, string>("Id","Id"),
		new KeyValuePair<string, string>("Name","Name"),
		new KeyValuePair<string, string>("Description","Description"),
		new KeyValuePair<string, string>("StartLocation","StartLocation"),
		new KeyValuePair<string, string>("EndLocation","EndLocation"),
		new KeyValuePair<string, string>("TransportType","TransportType"),
		new KeyValuePair<string, string>("Distance","Distance"),
		new KeyValuePair<string, string>("EstimatedTime","EstimatedTime"),
		new KeyValuePair<string, string>("RouteImagePath","RouteImagePath"),
		new KeyValuePair<string, string>("RouteImage","RouteImage"),
		new KeyValuePair<string, string>("IsFavourite","IsFavourite"),
	};

	public ObservableCollection<Tour> Import(string file)
		{
			ObservableCollection<Tour> tourList = new ObservableCollection<Tour>();
			List<string> lines = File.ReadAllLines(file).ToList();
			string headerLine = lines[0];
			var headerInfo = headerLine.Split(';').ToList().Select((v, i) => new { ColName = v, ColIndex = i });

			Type type = typeof(Tour);
			var properties = type.GetProperties();

			var dataLines = lines.Skip(1);
			dataLines.ToList().ForEach(line => {
				var values = line.Split(';');
				Tour obj = (Tour)Activator.CreateInstance(type);

				//set values to obj properties from csv columns
				foreach (var prop in properties)
				{
					//find mapping for the prop
					var mapping = Mappings.SingleOrDefault(m => m.Value == prop.Name);
					var colName = mapping.Key;
					var colIndex = headerInfo.SingleOrDefault(s => s.ColName == colName).ColIndex;
					var value = values[colIndex];
					var propType = prop.PropertyType;

					MessageBox.
					//if(propType == TransportTypes)
					prop.SetValue(obj, Convert.ChangeType(value, propType));
				}

				tourList.Add(obj);
			});

			return tourList;
		}
	}
}
