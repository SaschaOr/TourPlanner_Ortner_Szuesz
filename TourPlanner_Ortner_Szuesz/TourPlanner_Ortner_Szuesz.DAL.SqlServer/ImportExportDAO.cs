using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner_Ortner_Szuesz.DAL.Common;
using TourPlanner_Ortner_Szuesz.DAL.DAO;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.DAL.SqlServer
{
    public class ImportExportDAO : IImportExportDAO
    {
        public ILogger Logger { get; }

        private const string SQL_DELETE_ALL_TOURS = "DELETE FROM public.\"tour\"";
        
        private IDatabase database;

        public ImportExportDAO(ILogger logger)
        {
            Logger = logger;
            this.database = DALFactory.GetDatabase();
        }
        public void DeleteAllTours()
        {
            DbCommand deleteCommand = database.CreateCommand(SQL_DELETE_ALL_TOURS);

            try
            {
                database.ExecuteNonQuery(deleteCommand);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not delete all tour items! Error: {ex.Message}");
                Logger.LogError($"{DateTime.Now}: [ERROR] database could not delete all tour items.");
            }
        }
    }
}
