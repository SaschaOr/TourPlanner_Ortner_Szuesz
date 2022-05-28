using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner_Ortner_Szuesz.DAL.Common;
using TourPlanner_Ortner_Szuesz.DAL.DAO;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Models.Enums;

namespace TourPlanner_Ortner_Szuesz.DAL.SqlServer
{
    public class TourLogSqlDAO : ITourLogDAO
    {
        public ILogger Logger { get; }

        private const string SQL_INSERT_NEW_ITEM = "INSERT INTO public.\"tourlog\" (\"date\", \"difficulty\", \"totaltime\", \"rating\",\"comment\",\"tourid\") VALUES (@date, @difficulty, @totaltime, @rating, @comment, @tourid) RETURNING \"id\";";
        private const string SQL_FIND_BY_ID = "SELECT * FROM public.\"tourlog\" WHERE \"id\"=@id;";
        private const string SQL_GET_ALL_ITEMS = "SELECT * FROM public.\"tourlog\" WHERE \"tourid\"=@tourid;";
        private const string SQL_UPDATE_ITEM = "UPDATE public.\"tourlog\" SET \"date\"=@date, \"difficulty\"=@difficulty, \"totaltime\"=@totaltime, \"rating\"=@rating, \"comment\"=@comment WHERE \"id\"=@id;";
        private const string SQL_DELETE_ITEM = "DELETE FROM public.\"tourlog\" WHERE \"id\"=@id;";

        private IDatabase database;
        public TourLogSqlDAO(ILogger logger)
        {
            Logger = logger;
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

        public TourLog UpdateItem(TourLog tourLogItem)
        {
            DbCommand updateCommand = database.CreateCommand(SQL_UPDATE_ITEM);
            database.DefineParameter(updateCommand, "@date", DbType.Date, tourLogItem.Date);
            database.DefineParameter(updateCommand, "@difficulty", DbType.Int32, (int)tourLogItem.Difficulty);
            database.DefineParameter(updateCommand, "@totaltime", DbType.Int32, tourLogItem.TotalTime);
            database.DefineParameter(updateCommand, "@rating", DbType.Int32, tourLogItem.Rating);
            database.DefineParameter(updateCommand, "@comment", DbType.String, tourLogItem.Comment);
            database.DefineParameter(updateCommand, "@id", DbType.Int32, tourLogItem.Id);

            int updatedRows = database.ExecuteNonQuery(updateCommand);

            if(updatedRows <= 0)
            {
                MessageBox.Show("Could not update tour log! Please try again!");
                Logger.LogError($"{DateTime.Now}: [ERROR] could not update tour log item [Id: {tourLogItem.Id}]");
            }

            return FindById(tourLogItem.Id);
        }

        public bool DeleteItem(TourLog tourLogItem)
        {
            DbCommand deleteCommand = database.CreateCommand(SQL_DELETE_ITEM);
            database.DefineParameter(deleteCommand, "@id", DbType.Int32, tourLogItem.Id);

            int deletedRows = database.ExecuteNonQuery(deleteCommand);

            // at least one row has been deleted
            if (deletedRows <= 0)
            {
                MessageBox.Show("Could not delete tour log! Please try again!");
                Logger.LogError($"{DateTime.Now}: [ERROR] could not delete tour log item [Id: {tourLogItem.Id}]");
                return false;
            }
            else

            return true;
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
                try
                {
                    while (reader.Read())
                    {
                        tourLogList.Add(new TourLog(
                            (int)reader["id"],
                            Convert.ToDateTime(reader["date"]),
                            (DifficultyTypes)Enum.Parse(typeof(DifficultyTypes), reader["difficulty"].ToString()),
                            (int)reader["totaltime"],
                            (int)reader["rating"],
                            (string)reader["comment"],
                            (int)reader["tourid"]
                        ));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not load tour logs! Error: {ex.Message}");
                    Logger.LogCritical($"{DateTime.Now}: [FATAL] could not load tour logs from database. conversion error.");
                }
            }

            return tourLogList;
        }
    }
}
