namespace TourPlanner_Ortner_Szuesz.BL
{
    public static class TourItemFactory
    {
        private static ITourItemFactory instance;

        public static ITourItemFactory GetInstance()
        {
            if(instance == null)
            {
                instance = new TourItemFactoryImplementation();
            }

            return instance;
        }
    }
}
