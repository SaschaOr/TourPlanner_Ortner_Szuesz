using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner_Ortner_Szuesz.BL.Import_Export
{
    public class ImportExportFactory
    {
        private static IImportExportManager importExportManager;

        public static IImportExportManager GetImportExportFactoryManager()
        {
            if (importExportManager == null)
            {
                importExportManager = new ImportExportManagerImplementation();
            }
            return importExportManager;
        }
    }
}
