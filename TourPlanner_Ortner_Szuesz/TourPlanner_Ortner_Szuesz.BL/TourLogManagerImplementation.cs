using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TourPlanner_Ortner_Szuesz.DAL.DAO;
using TourPlanner_Ortner_Szuesz.DAL.SqlServer;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.BL
{
    public class TourLogManagerImplementation : ITourLogManager
    {
        public ILogger Logger { get; }
        private ITourLogDAO tourLogDAO { get; }

        public TourLogManagerImplementation(ILogger logger)
        {
            Logger = logger;
            tourLogDAO = new TourLogSqlDAO(Logger);
        }

        public TourLog CreateItem(TourLog tourLogItem)
        {
            return tourLogDAO.AddNewItem(tourLogItem);
        }

        public IEnumerable<TourLog> GetItems(int tourId)
        {
            return tourLogDAO.GetItems(tourId);
        }

        public TourLog UpdateItem(TourLog tourLogItem)
        {
            return tourLogDAO.UpdateItem(tourLogItem);
        }

        public bool DeleteItem(TourLog tourLogItem)
        {
            return tourLogDAO.DeleteItem(tourLogItem);
        }
    }
}
