using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.DAL.DAO;
using TourPlanner_Ortner_Szuesz.DAL.SqlServer;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.BL.Import_Export
{
    public class ImportExportManagerImplementation : IImportExportManager
    {
        private IImportExportDAO dao = new ImportExportDAO();
        public void DeleteAllTours()
        {
            dao.DeleteAllTours();
        }

        public void ImportAllTours(IEnumerable<Tour> tours)
        {
            foreach (Tour tour in tours)
            {
                TourManagerFactory.GetTourFactoryManager().CreateItemAfterImport(tour);
            }
        }
    }
}
