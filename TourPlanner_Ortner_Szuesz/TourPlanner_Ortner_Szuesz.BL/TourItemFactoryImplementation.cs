using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.DAL;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.BL
{
    class TourItemFactoryImplementation : ITourItemFactory
    {
        private TourItemDAO tourItemDAO = new TourItemDAO();
        public IEnumerable<Tour> GetTours()
        {
            return tourItemDAO.GetTours();
        }

        public IEnumerable<Tour> Search(string tourName, bool caseSensitive = false)
        {
            throw new NotImplementedException();
        }
    }
}
