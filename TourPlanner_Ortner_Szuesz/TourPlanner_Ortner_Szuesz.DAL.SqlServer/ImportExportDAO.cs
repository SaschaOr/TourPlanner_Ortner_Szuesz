using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.DAL.Common;
using TourPlanner_Ortner_Szuesz.DAL.DAO;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.DAL.SqlServer
{
    public class ImportExportDAO : IImportExportDAO
    {
        private const string SQL_DELETE_ALL_TOURS = "DELETE FROM public.\"tour\"";
        
        private IDatabase database;

        public ImportExportDAO()
        {
            this.database = DALFactory.GetDatabase();
        }
        public void DeleteAllTours()
        {
            DbCommand deleteCommand = database.CreateCommand(SQL_DELETE_ALL_TOURS);

            database.ExecuteNonQuery(deleteCommand);
        }
    }
}
