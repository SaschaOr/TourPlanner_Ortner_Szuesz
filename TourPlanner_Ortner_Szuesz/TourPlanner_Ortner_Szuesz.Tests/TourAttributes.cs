using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using TourPlanner_Ortner_Szuesz.BL.PDF_Generation;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Models.Enums;

namespace TourPlanner_Ortner_Szuesz.Tests
{
    [TestFixture]
    public class TourAttributes
    {
        private CalculateTourAttributes calculate;
        private TourLog tourLogEasy, tourLogMedium, tourLogHard;
        private ObservableCollection<TourLog> tourLogsEasyMedium, tourLogsMediumHard, emptyTourLogs;
        private Tour tourLong, tourShort;

        private const int POPULARITY_2 = 2;
        private const double TIME_75_MIN = 75 * 60;
        private const double DIFFICULTY_0POINT5 = 0.5;
        private const double RATING_2 = 2;

        [SetUp]
        public void Setup()
        {
            calculate = new CalculateTourAttributes();

            tourLogMedium = new TourLog(1, DateTime.Now, DifficultyTypes.medium, 6000, 1, "Test Tour Log 1", 1);
            tourLogEasy = new TourLog(2, DateTime.Now, DifficultyTypes.easy, 3000, 3, "Test Tour Log 2", 1);
            tourLogHard = new TourLog(3, DateTime.Now, DifficultyTypes.hard, 9000, 3, "Test Tour Log 3", 1);

            tourLogsEasyMedium = new ObservableCollection<TourLog>();
            tourLogsMediumHard = new ObservableCollection<TourLog>();
            emptyTourLogs = new ObservableCollection<TourLog>();

            tourLogsEasyMedium.Add(tourLogEasy);
            tourLogsEasyMedium.Add(tourLogMedium);
            tourLogsMediumHard.Add(tourLogMedium);
            tourLogsMediumHard.Add(tourLogHard);

            tourLong = new Tour(1, "Wien-Hollabrunn", "Das ist eine Test Tour", "Wien", "Hollabrunn", TransportTypes.Car, 56, 2603, "Test", false);
            tourShort = new Tour(1, "Wien-Klosterneuburg", "Das ist eine Test Tour", "Wien", "Klosterneuburg", TransportTypes.Car, 15, 1294, "Test",true);
        }

        [Test]
        public void CalculatePopularity_2TourLogs_2Popularity()
        {
            int popularity = calculate.CalculatePopularity(tourLogsEasyMedium);

            Assert.AreEqual(POPULARITY_2, popularity);
        }

        [Test]
        public void CalculateChildFriedliness_ToLongDistance_NotChildFriedly()
        {
            bool isChildFriendly = calculate.CalculateChildFriendliness(tourLong, tourLogsMediumHard);

            Assert.IsFalse(isChildFriendly);
        }

        [Test]
        public void CalculateChildFriedliness_AverageTimeToHigh_NotChildFriedly()
        {
            bool isChildFriendly = calculate.CalculateChildFriendliness(tourLong, tourLogsMediumHard);

            Assert.IsFalse(isChildFriendly);
        }

        [Test]
        public void CalculateChildFriedliness_AverageDifficultyToHigh_NotChildFriedly()
        {
            bool isChildFriendly = calculate.CalculateChildFriendliness(tourLong, tourLogsMediumHard);

            Assert.IsFalse(isChildFriendly);
        }

        [Test]
        public void CalculateChildFriedliness_EasyTourWithEasyTourLogs_ChildFriedly()
        {
            bool isChildFriendly = calculate.CalculateChildFriendliness(tourShort, tourLogsEasyMedium);

            Assert.IsTrue(isChildFriendly);
        }

        [Test]
        public void CalculateAverageTime_Time50MinAnd100Min_Results75Min()
        {
            double result = calculate.CalculateAverageTime(tourLogsEasyMedium);
            
            Assert.AreEqual(TIME_75_MIN, result);
        }

        [Test]
        public void CalculateAverageDifficulty_EasyAndMedium_Results0Point5()
        {
            double result = calculate.CalculateAverageDifficulty(tourLogsEasyMedium);

            Assert.AreEqual(DIFFICULTY_0POINT5, result);
        }

        [Test]
        public void CalculateAverageRating_1And3_Results2()
        {
            double result = calculate.CalculateAverageRating(tourLogsEasyMedium);

            Assert.AreEqual(RATING_2, result);
        }

        [Test]
        public void CalculateAverageTime_NoTourLogs_ResultsNaN()
        {
            double results = calculate.CalculateAverageTime(emptyTourLogs);

            Assert.IsNaN(results);
        }

        [Test]
        public void CalculateChildFriendliness_NoTourLogs_NotChildFriedly()
        {
            bool isChildFriendly = calculate.CalculateChildFriendliness(tourShort, emptyTourLogs);

            Assert.IsFalse(isChildFriendly);
        }
    }
}