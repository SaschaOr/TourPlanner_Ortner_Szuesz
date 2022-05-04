using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.DAL
{
    public class TourItemDAO
    {
        private IDataAccess dataAccess;

        public TourItemDAO()
        {
            dataAccess = new Database();
        }

        public List<Tour> GetTours()
        {
            return dataAccess.GetTours();
        }
    }
}
