using System;
using System.Collections.Generic;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.DAL.DAO
{
    public interface ITourDAO
    {
            Tour FindById(int itemId);
            Tour AddNewItem(string name, string annotation, string url, DateTime creationTime);
            IEnumerable<Tour> GetItems();
    }
}
