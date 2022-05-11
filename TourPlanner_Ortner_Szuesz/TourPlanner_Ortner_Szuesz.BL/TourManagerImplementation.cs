using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.DAL.Common;
using TourPlanner_Ortner_Szuesz.DAL.DAO;
using TourPlanner_Ortner_Szuesz.DAL.SqlServer;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Models.Enums;

namespace TourPlanner_Ortner_Szuesz.BL
{
    public class TourManagerImplementation : ITourManager
    {
        public Tour CreateItem(string name, string description, string startLocation, string endLocation, TransportTypes transportType)
        {
            //ITourDAO tourDAO = DALFactory.CreateTourDAO();
            ITourDAO tourDAO = new TourSqlDAO();
            return tourDAO.AddNewItem(name, description, startLocation, endLocation, transportType);
        }

        public IEnumerable<Tour> GetItems()
        {
            //ITourDAO tourDAO = DALFactory.CreateTourDAO();
            ITourDAO tourDAO = new TourSqlDAO();
            return tourDAO.GetItems();
        }
    }
}
