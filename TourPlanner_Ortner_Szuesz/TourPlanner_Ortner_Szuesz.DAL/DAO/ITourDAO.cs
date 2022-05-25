using System;
using System.Collections.Generic;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Models.Enums;

namespace TourPlanner_Ortner_Szuesz.DAL.DAO
{
    public interface ITourDAO
    {
        Tour FindById(int tourId);
        Tour AddNewItem(Tour tourItem);
        Tour UpdateItem(Tour tourItem);
        bool DeleteItem(Tour tourItem);
        IEnumerable<Tour> GetSearchResults(string searchString);
        IEnumerable<Tour> GetItems();
        int SetRouteImagePath(int tourId, string path);
        bool UpdateFavouriteStatus(int tourId, bool favouriteStatus);
    }
}
