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
        Tour CreateItem(string name, string description, string startLocation, string endLocation, TransportTypes transportType);
    }
}
