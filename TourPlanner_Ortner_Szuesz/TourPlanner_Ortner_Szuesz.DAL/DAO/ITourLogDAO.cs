using System.Collections.Generic;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.DAL.DAO
{
    public interface ITourLogDAO
    {
        TourLog FindById(int tourLogId);
        TourLog AddNewItem(TourLog tourLogItem);
        TourLog UpdateItem(TourLog tourLogItem);
        bool DeleteItem(TourLog tourLogItem);
        IEnumerable<TourLog> GetItems(int tourId);
    }
}
