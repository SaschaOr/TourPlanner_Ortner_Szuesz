using Microsoft.Extensions.Logging;

namespace TourPlanner_Ortner_Szuesz.BL.Import_Export
{
    public class ImportExportFactory
    {
        private static IImportExportManager importExportManager;

        public static IImportExportManager GetImportExportFactoryManager(ILogger logger)
        {
            if (importExportManager == null)
            {
                importExportManager = new ImportExportManagerImplementation(logger);
            }
            return importExportManager;
        }
    }
}
