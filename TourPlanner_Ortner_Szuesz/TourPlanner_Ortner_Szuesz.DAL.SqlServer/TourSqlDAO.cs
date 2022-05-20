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
        private const string SQL_FIND_BY_ID = "SELECT * FROM public.\"tour\" WHERE \"id\"=@id;";
        private const string SQL_GET_ALL_ITEMS = "SELECT * FROM public.\"tour\";";
        private const string SQL_INSERT_NEW_ITEM = "INSERT INTO public.\"tour\" (\"name\", \"description\", \"startlocation\", \"endlocation\",\"transporttype\", \"distance\", \"estimatedtime\", \"routeimagepath\") VALUES (@name, @description, @startlocation, @endlocation, @transporttype, @distance, @estimatedtime, @routeimagepath) RETURNING \"id\";";
        private const string SQL_UPDATE_ROUTE_IMAGE_PATH = "UPDATE public.\"tour\" SET \"routeimagepath\"=@routeimagepath WHERE \"id\"=@id;";
        private const string SQL_UPDATE_ITEM = "UPDATE public.\"tour\" SET \"name\"=@name, \"description\"=@description, \"startlocation\"=@startlocation, \"endlocation\"=@endlocation, \"transporttype\"=@transporttype, \"distance\"=@distance, \"estimatedtime\"=@estimatedtime WHERE \"id\"=@id;";
        private const string SQL_DELETE_ITEM = "DELETE FROM public.\"tour\" WHERE \"id\"=@id;";

        private IDatabase database;
        public TourSqlDAO()
        {
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

            return FindById(tourItem.Id);
        }

        public bool DeleteItem(Tour tourItem)
        {
            DbCommand deleteCommand = database.CreateCommand(SQL_DELETE_ITEM);
            database.DefineParameter(deleteCommand, "@id", DbType.Int32, tourItem.Id);

            int deletedRows = database.ExecuteNonQuery(deleteCommand);

            // at least one row has been deleted
            if (deletedRows > 0)
            {
                return true;
            }

            return false;
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
                        (string)reader["routeimagepath"]
                    ));
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
    }
}
