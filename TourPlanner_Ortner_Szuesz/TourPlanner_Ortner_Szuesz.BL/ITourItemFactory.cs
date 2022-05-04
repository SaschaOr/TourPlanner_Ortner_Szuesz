using System.Collections.Generic;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.BL
{
    public interface ITourItemFactory
    {
        IEnumerable<Tour> GetTours();
        IEnumerable<Tour> Search(string tourName, bool caseSensitive = false);

    }
}
