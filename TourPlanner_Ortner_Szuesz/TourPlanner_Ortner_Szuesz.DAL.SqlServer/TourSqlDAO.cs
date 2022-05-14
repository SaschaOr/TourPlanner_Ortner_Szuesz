using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using TourPlanner_Ortner_Szuesz.DAL.Common;
using TourPlanner_Ortner_Szuesz.DAL.DAO;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Models.Enums;

namespace TourPlanner_Ortner_Szuesz.DAL.SqlServer
{
    public class TourSqlDAO : ITourDAO
    {
        private const string SQL_FIND_BY_ID = "SELECT * FROM public.\"tour\" WHERE \"id\"=@id;";
        private const string SQL_GET_ALL_ITEMS = "SELECT * FROM public.\"tour\";";
        private const string SQL_INSERT_NEW_ITEM = "INSERT INTO public.\"tour\" (\"name\", \"description\", \"startlocation\", \"endlocation\",\"transporttype\") VALUES (@name, @description, @startlocation, @endlocation, @transporttype) RETURNING \"id\";";
        private const string SQL_UPDATE_ROUTE_IMAGE_PATH = "UPDATE public.\"tour\" SET ;";

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
        public Tour AddNewItem(string name, string description, string startLocation, string endLocation, TransportTypes transportType)
        {
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT_NEW_ITEM);
            database.DefineParameter(insertCommand, "@name", DbType.String, name);
            database.DefineParameter(insertCommand, "@description", DbType.String, description);
            database.DefineParameter(insertCommand, "@startlocation", DbType.String, startLocation);
            database.DefineParameter(insertCommand, "@endlocation", DbType.String, endLocation);
            database.DefineParameter(insertCommand, "@transporttype", DbType.Int32, transportType);

            return FindById(database.ExecuteScalar(insertCommand));
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
                        (string)reader["routeinformation"]
                    ));
                }
            }

            return tourList;
        }

        public int SetRouteImagePath(int tourId, string path)
        {
            DbCommand updateCommand = database.CreateCommand(SQL_INSERT_NEW_ITEM);
            database.DefineParameter(insertCommand, "@name", DbType.String, name);
            database.DefineParameter(insertCommand, "@description", DbType.String, description);
            database.DefineParameter(insertCommand, "@startlocation", DbType.String, startLocation);
            database.DefineParameter(insertCommand, "@endlocation", DbType.String, endLocation);
            database.DefineParameter(insertCommand, "@transporttype", DbType.Int32, transportType);

            return FindById(database.ExecuteScalar(insertCommand));
        }
    }
}
