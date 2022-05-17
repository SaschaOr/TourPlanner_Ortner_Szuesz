using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.DAL.Common;
using TourPlanner_Ortner_Szuesz.DAL.DAO;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Models.Enums;

namespace TourPlanner_Ortner_Szuesz.DAL.SqlServer
{
    public class TourLogSqlDAO : ITourLogDAO
    {
        private const string SQL_INSERT_NEW_ITEM = "INSERT INTO public.\"tourlog\" (\"date\", \"difficulty\", \"totaltime\", \"rating\",\"comment\",\"tourid\") VALUES (@date, @difficulty, @totaltime, @rating, @comment, @tourid) RETURNING \"id\";";
        private const string SQL_FIND_BY_ID = "SELECT * FROM public.\"tourlog\" WHERE \"id\"=@id;";
        private const string SQL_GET_ALL_ITEMS = "SELECT * FROM public.\"tourlog\" WHERE \"tourid\"=@tourid;";

        private IDatabase database;
        public TourLogSqlDAO()
        {
            this.database = DALFactory.GetDatabase();
        }

        public TourLog AddNewItem(TourLog tourLogItem)
        {
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT_NEW_ITEM);
            database.DefineParameter(insertCommand, "@date", DbType.Date, tourLogItem.Date);
            database.DefineParameter(insertCommand, "@difficulty", DbType.Int32, (int)tourLogItem.Difficulty);
            database.DefineParameter(insertCommand, "@totaltime", DbType.Int32, tourLogItem.TotalTime);
            database.DefineParameter(insertCommand, "@rating", DbType.Int32, tourLogItem.Rating);
            database.DefineParameter(insertCommand, "@comment", DbType.String, tourLogItem.Comment);
            database.DefineParameter(insertCommand, "@tourid", DbType.Int32, tourLogItem.TourId);

            return FindById(database.ExecuteScalar(insertCommand));
        }

        public TourLog FindById(int tourLogId)
        {
            DbCommand command = database.CreateCommand(SQL_FIND_BY_ID);
            database.DefineParameter(command, "@id", DbType.Int32, tourLogId);

            IEnumerable<TourLog> tourLogs = QueryTourLogsFromDb(command);
            return tourLogs.FirstOrDefault();
        }

        public IEnumerable<TourLog> GetItems(int tourId)
        {
            DbCommand command = database.CreateCommand(SQL_GET_ALL_ITEMS);
            database.DefineParameter(command, "@tourid", DbType.Int32, tourId);
            return QueryTourLogsFromDb(command);
        }

        private IEnumerable<TourLog> QueryTourLogsFromDb(DbCommand command)
        {
            List<TourLog> tourLogList = new List<TourLog>();

            using (IDataReader reader = database.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    tourLogList.Add(new TourLog(
                        (int)reader["id"],
                        DateOnly.Parse(Convert.ToDateTime(reader["date"].ToString())),
                        (DifficultyTypes)Enum.Parse(typeof(DifficultyTypes), reader["difficulty"].ToString()),
                        (int)reader["totaltime"],
                        (int)reader["rating"],
                        (string)reader["comment"],
                        (int)reader["tourid"]
                    ));
                }
            }

            return tourLogList;
        }
    }
}
