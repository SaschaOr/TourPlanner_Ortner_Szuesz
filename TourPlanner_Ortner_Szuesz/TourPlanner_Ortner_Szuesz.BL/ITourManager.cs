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
        Task<Tour> CreateItem(Tour tourItem);
        Task<Tour> UpdateItem(Tour tourItem);
    }
}
