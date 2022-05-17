using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.DAL.DAO
{
    public interface ITourLogDAO
    {
        TourLog FindById(int tourLogId);
        TourLog AddNewItem(TourLog tourLogItem);
        IEnumerable<TourLog> GetItems(int tourId);
    }
}
