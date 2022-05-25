using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Models.Enums;

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
