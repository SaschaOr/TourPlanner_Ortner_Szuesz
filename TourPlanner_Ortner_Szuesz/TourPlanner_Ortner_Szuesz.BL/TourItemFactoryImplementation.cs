using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.BL
{
    class TourItemFactoryImplementation : ITourItemFactory
    {
        public IEnumerable<Tour> GetTours()
        {
            return new List<Tour>() {
                new Tour("Tour1"),
                new Tour("Tour2"),
                new Tour("Tour3"),
                new Tour("Tour4"),
                new Tour("Tour5"),
                new Tour("Tour6")
            };
        }

        public IEnumerable<Tour> Search(string tourName, bool caseSensitive = false)
        {
            throw new NotImplementedException();
        }
    }
}
