using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner_Ortner_Szuesz.BL
{
    public class TourManagerFactory
    {
        private static ITourManager manager;

        public static ITourManager GetFactoryManager()
        {
            if (manager == null)
            {
                manager = new TourManagerImplementation();
            }
            return manager;
        }
    }
}
