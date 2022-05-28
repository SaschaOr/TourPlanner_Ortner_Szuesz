using System.Collections.Generic;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.BL
{
    public interface ITourManager
    {
        IEnumerable<Tour> GetItems();
        IEnumerable<Tour> GetSearchResults(string searchString);
        Task<Tour> CreateItem(Tour tourItem);
        Task<Tour> CreateItemAfterImport(Tour tourItem);
        Task<Tour> UpdateItem(Tour tourItem);
        bool DeleteItem(Tour tourItem);
        bool UpdateFavouriteStatus(int tourId, bool favouriteStatus);
    }
}
