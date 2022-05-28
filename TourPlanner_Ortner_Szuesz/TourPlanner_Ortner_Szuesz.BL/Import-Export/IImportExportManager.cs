using System.Collections.Generic;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.BL.Import_Export
{
    public interface IImportExportManager
    {
        void DeleteAllTours();
        Task<Tour> ImportAllTours(IEnumerable<Tour> tours);
    }
}
