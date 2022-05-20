using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.DAL.DAO;
using TourPlanner_Ortner_Szuesz.DAL.SqlServer;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.BL
{
    public class TourLogManagerImplementation : ITourLogManager
    {
        private ITourLogDAO tourLogDAO = new TourLogSqlDAO();

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
