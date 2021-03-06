using Microsoft.Extensions.Logging;

namespace TourPlanner_Ortner_Szuesz.BL
{
    public class TourManagerFactory
    {
        private static ITourManager tourManager;
        private static ITourLogManager tourLogManager;

        public static ITourManager GetTourFactoryManager(ILogger logger)
        {
            if (tourManager == null)
            {
                tourManager = new TourManagerImplementation(logger);
            }
            return tourManager;
        }

        public static ITourLogManager GetTourLogFactoryManager(ILogger logger)
        {
            if (tourLogManager == null)
            {
                tourLogManager = new TourLogManagerImplementation(logger);
            }
            return tourLogManager;
        }
    }
}
