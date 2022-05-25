using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.BL.Import_Export
{
    public interface IImportExportManager
    {
        void DeleteAllTours();
        void ImportAllTours(IEnumerable<Tour> tours);
    }
}
