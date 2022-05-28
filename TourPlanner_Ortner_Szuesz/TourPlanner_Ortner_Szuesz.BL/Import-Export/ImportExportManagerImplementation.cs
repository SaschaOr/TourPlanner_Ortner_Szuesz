using Microsoft.Extensions.Logging;
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
        public ILogger Logger { get; }
        private IImportExportDAO importExportDAO { get; }

        public ImportExportManagerImplementation(ILogger logger)
        {
            Logger = logger;
            importExportDAO = new ImportExportDAO(Logger);
        }

        public void DeleteAllTours()
        {
            importExportDAO.DeleteAllTours();
        }

        public void ImportAllTours(IEnumerable<Tour> tours)
        {
            foreach (Tour tour in tours)
            {
                TourManagerFactory.GetTourFactoryManager(Logger).CreateItemAfterImport(tour);
            }
        }
    }
}
