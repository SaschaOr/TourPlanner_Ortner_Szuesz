using System;
using System.Collections.Generic;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Models.Enums;

namespace TourPlanner_Ortner_Szuesz.DAL.DAO
{
    public interface ITourDAO
    {
            Tour FindById(int tourId);
            Tour AddNewItem(string name, string description, string startLocation, string endLocation, TransportTypes transportType);
            IEnumerable<Tour> GetItems();
    }
}
