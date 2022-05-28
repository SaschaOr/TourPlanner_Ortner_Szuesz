using System.Collections.Generic;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.BL
{
    public interface ITourLogManager
    {
        IEnumerable<TourLog> GetItems(int tourId);
        TourLog CreateItem(TourLog tourLogItem);
        TourLog UpdateItem(TourLog tourLogItem);
        bool DeleteItem(TourLog tourLogItem);
    }
}
