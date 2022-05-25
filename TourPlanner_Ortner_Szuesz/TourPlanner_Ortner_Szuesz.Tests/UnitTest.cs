using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using TourPlanner_Ortner_Szuesz.BL.PDF_Generation;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Models.Enums;

namespace TourPlanner_Ortner_Szuesz.Tests
{
    [TestFixture]
    public class Tests
    {
        private CalculateTourAttributes calculate;
        private TourLog tourLogEasy, tourLogMedium, tourLogHard;
        private ObservableCollection<TourLog> tourLogsEasyMedium;
        private ObservableCollection<TourLog> tourLogsMediumHard;
        Tour tourLong, tourShort;

        private const int POPULARITY_2 = 2;
        private const bool NOT_CHILD_FRIENDLY = false;
        private const bool CHILD_FRIEDLY = true;

        [SetUp]
        public void Setup()
        {
            calculate = new CalculateTourAttributes();

            tourLogMedium = new TourLog(1, DateTime.Now, DifficultyTypes.medium, 6000, 1, "Test Tour Log 1", 1);
            tourLogEasy = new TourLog(2, DateTime.Now, DifficultyTypes.easy, 3000, 3, "Test Tour Log 2", 1);
            tourLogHard = new TourLog(3, DateTime.Now, DifficultyTypes.hard, 9000, 3, "Test Tour Log 3", 1);

            tourLogsEasyMedium = new ObservableCollection<TourLog>();
            tourLogsMediumHard = new ObservableCollection<TourLog>();

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
            bool isChildFriedly = calculate.CalculateChildFriedliness(tourLong, tourLogsMediumHard);

            Assert.IsFalse(isChildFriedly);
        }

        [Test]
        public void CalculateChildFriedliness_AverageTimeToHigh_NotChildFriedly()
        {
            bool isChildFriedly = calculate.CalculateChildFriedliness(tourLong, tourLogsMediumHard);

            Assert.IsFalse(isChildFriedly);
        }

        [Test]
        public void CalculateChildFriedliness_AverageDifficultyToHigh_NotChildFriedly()
        {
            bool isChildFriedly = calculate.CalculateChildFriedliness(tourLong, tourLogsMediumHard);

            Assert.IsFalse(isChildFriedly);
        }

        [Test]
        public void CalculateChildFriedliness_EasyTourWithEasyTourLogs_ChildFriedly()
        {
            bool isChildFriedly = calculate.CalculateChildFriedliness(tourShort, tourLogsEasyMedium);

            Assert.IsTrue(isChildFriedly);
        }
    }
}