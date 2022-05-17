using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner_Ortner_Szuesz.BL
{
    public class TourManagerFactory
    {
        private static ITourManager tourManager;
        private static ITourLogManager tourLogManager;

        public static ITourManager GetTourFactoryManager()
        {
            if (tourManager == null)
            {
                tourManager = new TourManagerImplementation();
            }
            return tourManager;
        }

        public static ITourLogManager GetTourLogFactoryManager()
        {
            if (tourLogManager == null)
            {
                tourLogManager = new TourLogManagerImplementation();
            }
            return tourLogManager;
        }
    }
}
