using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Windows;
using TourPlanner_Ortner_Szuesz.DAL.Common;
using TourPlanner_Ortner_Szuesz.DAL.Configuration;
using TourPlanner_Ortner_Szuesz.DAL.DAO;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Models.Enums;

namespace TourPlanner_Ortner_Szuesz.DAL.SqlServer
{
    public class TourSqlDAO : ITourDAO
    {
        public ILogger Logger { get; }

        private const string SQL_FIND_BY_ID = "SELECT * FROM public.\"tour\" WHERE \"id\"=@id;";
        private const string SQL_GET_ALL_ITEMS = "SELECT * FROM public.\"tour\";";
        private const string SQL_INSERT_NEW_ITEM = "INSERT INTO public.\"tour\" (\"name\", \"description\", \"startlocation\", \"endlocation\",\"transporttype\", \"distance\", \"estimatedtime\", \"routeimagepath\", \"isfavourite\") VALUES (@name, @description, @startlocation, @endlocation, @transporttype, @distance, @estimatedtime, @routeimagepath, @isfavourite) RETURNING \"id\";";
        private const string SQL_UPDATE_ROUTE_IMAGE_PATH = "UPDATE public.\"tour\" SET \"routeimagepath\"=@routeimagepath WHERE \"id\"=@id;";
        private const string SQL_UPDATE_ITEM = "UPDATE public.\"tour\" SET \"name\"=@name, \"description\"=@description, \"startlocation\"=@startlocation, \"endlocation\"=@endlocation, \"transporttype\"=@transporttype, \"distance\"=@distance, \"estimatedtime\"=@estimatedtime WHERE \"id\"=@id;";
        private const string SQL_UPDATE_FAVOURITE = "UPDATE public.\"tour\" SET \"isfavourite\"=@isfavourite WHERE \"id\"=@id;";
        private const string SQL_DELETE_ITEM = "DELETE FROM public.\"tour\" WHERE \"id\"=@id;";
        private const string SQL_SEARCH_ITEMS = "SELECT DISTINCT tour.* FROM \"tour\" LEFT JOIN \"tourlog\" ON tour.id = tourlog.tourid WHERE name iLIKE @searchString OR description iLIKE @searchString OR startlocation iLIKE @searchString OR endlocation iLIKE @searchString OR comment iLIKE @searchString;";

        private IDatabase database;
        public TourSqlDAO(ILogger logger)
        {
            Logger = logger;
            this.database = DALFactory.GetDatabase();
        }

        public Tour FindById(int tourID)
        {
            DbCommand command = database.CreateCommand(SQL_FIND_BY_ID);
            database.DefineParameter(command, "@id", DbType.Int32, tourID);

            IEnumerable<Tour> tours = QueryToursFromDb(command);
            return tours.FirstOrDefault();
        }

        public Tour AddNewItem(Tour tourItem)
        {
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT_NEW_ITEM);
            database.DefineParameter(insertCommand, "@name", DbType.String, tourItem.Name);
            database.DefineParameter(insertCommand, "@description", DbType.String, tourItem.Description);
            database.DefineParameter(insertCommand, "@startlocation", DbType.String, tourItem.StartLocation);
            database.DefineParameter(insertCommand, "@endlocation", DbType.String, tourItem.EndLocation);
            database.DefineParameter(insertCommand, "@transporttype", DbType.Int32, (int)tourItem.TransportType);
            database.DefineParameter(insertCommand, "@distance", DbType.Int32, tourItem.Distance);
            database.DefineParameter(insertCommand, "@estimatedtime", DbType.Int32, tourItem.EstimatedTime);
            database.DefineParameter(insertCommand, "@routeimagepath", DbType.String, TourPlannerConfigurationManager.GetConfig().DefaultImageLocation);
            database.DefineParameter(insertCommand, "@isfavourite", DbType.Boolean, tourItem.IsFavourite != null ? tourItem.IsFavourite : false);

            return FindById(database.ExecuteScalar(insertCommand));
        }

        public Tour UpdateItem(Tour tourItem)
        {
            DbCommand updateCommand = database.CreateCommand(SQL_UPDATE_ITEM);
            database.DefineParameter(updateCommand, "@name", DbType.String, tourItem.Name);
            database.DefineParameter(updateCommand, "@description", DbType.String, tourItem.Description);
            database.DefineParameter(updateCommand, "@startlocation", DbType.String, tourItem.StartLocation);
            database.DefineParameter(updateCommand, "@endlocation", DbType.String, tourItem.EndLocation);
            database.DefineParameter(updateCommand, "@transporttype", DbType.Int32, (int)tourItem.TransportType);
            database.DefineParameter(updateCommand, "@distance", DbType.Int32, tourItem.Distance);
            database.DefineParameter(updateCommand, "@estimatedtime", DbType.Int32, tourItem.EstimatedTime);
            database.DefineParameter(updateCommand, "@id", DbType.Int32, tourItem.Id);

            int updatedRows = database.ExecuteNonQuery(updateCommand);

            if(updatedRows <= 0)
            {
                MessageBox.Show("Could not update tour! Please try again!");
                Logger.LogError($"{DateTime.Now}: [ERROR] could not update tour item [Id: {tourItem.Id}]");
            }

            return FindById(tourItem.Id);
        }

        public bool DeleteItem(Tour tourItem)
        {
            DbCommand deleteCommand = database.CreateCommand(SQL_DELETE_ITEM);
            database.DefineParameter(deleteCommand, "@id", DbType.Int32, tourItem.Id);

            int deletedRows = database.ExecuteNonQuery(deleteCommand);

            // at least one row has been deleted
            if (deletedRows <= 0)
            {
                MessageBox.Show("Could not delete tour! Please try again!");
                Logger.LogError($"{DateTime.Now}: [ERROR] could not delete tour item [Id: {tourItem.Id}]");
                return false;
            }

            return true;
        }

        public IEnumerable<Tour> GetSearchResults(string searchString)
        {
            DbCommand searchCommand = database.CreateCommand(SQL_SEARCH_ITEMS);
            database.DefineParameter(searchCommand, "@searchString", DbType.String, $"%{searchString}%");

            return QueryToursFromDb(searchCommand);
        }

        public IEnumerable<Tour> GetItems()
        {
            DbCommand command = database.CreateCommand(SQL_GET_ALL_ITEMS);
            return QueryToursFromDb(command);
        }

        private IEnumerable<Tour> QueryToursFromDb(DbCommand command)
        {
            List<Tour> tourList = new List<Tour>();

            using (IDataReader reader = database.ExecuteReader(command))
            {
                try
                {
                    while (reader.Read())
                    {
                        tourList.Add(new Tour(
                            (int)reader["id"],
                            (string)reader["name"],
                            (string)reader["description"],
                            (string)reader["startlocation"],
                            (string)reader["endlocation"],
                            (TransportTypes)Enum.Parse(typeof(TransportTypes), reader["transporttype"].ToString()),
                            (int)reader["distance"],
                            (int)reader["estimatedtime"],
                            (string)reader["routeimagepath"],
                            (bool)reader["isfavourite"]
                        ));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not load tours! Error: {ex.Message}");
                    Logger.LogCritical($"{DateTime.Now}: [FATAL] could not load tours from database. conversion error.");
                }
            }

            return tourList;
        }

        public int SetRouteImagePath(int tourId, string path)
        {
            DbCommand updateCommand = database.CreateCommand(SQL_UPDATE_ROUTE_IMAGE_PATH);
            database.DefineParameter(updateCommand, "@routeimagepath", DbType.String, path);
            database.DefineParameter(updateCommand, "@id", DbType.Int32, tourId);

            return database.ExecuteScalar(updateCommand);
        }

        public bool UpdateFavouriteStatus(int tourId, bool favouriteStatus)
        {
            DbCommand updateCommand = database.CreateCommand(SQL_UPDATE_FAVOURITE);
            database.DefineParameter(updateCommand, "@isfavourite", DbType.Boolean, favouriteStatus);
            database.DefineParameter(updateCommand, "@id", DbType.Int32, tourId);

            int updatedRows = database.ExecuteNonQuery(updateCommand);

            // at least one row has been updated
            if (updatedRows <= 0)
            {
                MessageBox.Show("Could not update tour to favourite! Please try again!");
                Logger.LogError($"{DateTime.Now}: [ERROR] could not update tour item favourite status [Id: {tourId}]");
                return false;
            }

            return true;
        }
    }
}
