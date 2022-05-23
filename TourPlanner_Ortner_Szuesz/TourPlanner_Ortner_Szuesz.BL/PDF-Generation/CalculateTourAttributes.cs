using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.BL.PDF_Generation
{
    public class CalculateTourAttributes
    {
        private const int DIFFICULTY_MAX_CHILD = 1;
        private const int TOTAL_TIME_MAX_CHILD = 90;
        private const int DISTANCE_MAX_CHILD = 25;

        public int CalculatePopularity(ObservableCollection<TourLog> tourLogs)
        {
            return tourLogs.Count;
        }

        public bool CalculateChildFriedliness(Tour tourItem, ObservableCollection<TourLog> tourLogs)
        {
            // no recorded tour logs -> can not know, if child-friedly
            if (tourLogs.Count == 0)
            {
                return false;
            }

            if (DIFFICULTY_MAX_CHILD < CalculateAverageDifficulty(tourLogs))
            {
                return false;
            }

            if (TOTAL_TIME_MAX_CHILD < CalculateAverageTime(tourLogs))
            {
                return false;
            }

            if (DISTANCE_MAX_CHILD < tourItem.Distance)
            {
                return false;
            }

            return true;
        }

        public double CalculateAverageTime(ObservableCollection<TourLog> tourLogs)
        {
            double sum = 0;

            foreach(TourLog tourLog in tourLogs)
            {
                sum += tourLog.TotalTime;
            }

            return sum/ tourLogs.Count;
        }

        public double CalculateAverageDifficulty(ObservableCollection<TourLog> tourLogs)
        {
            double sum = 0;

            foreach (TourLog tourLog in tourLogs)
            {
                sum += (int)tourLog.Difficulty;
            }

            return sum / tourLogs.Count;
        }

        public double CalculateAverageRating(ObservableCollection<TourLog> tourLogs)
        {
            double sum = 0;

            foreach (TourLog tourLog in tourLogs)
            {
                sum += tourLog.Rating;
            }

            return sum / tourLogs.Count;
        }
    }
}
