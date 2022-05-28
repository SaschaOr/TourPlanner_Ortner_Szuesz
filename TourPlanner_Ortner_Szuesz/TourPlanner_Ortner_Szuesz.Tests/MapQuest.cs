using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.BL;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Models.Enums;

namespace TourPlanner_Ortner_Szuesz.Tests
{
    [TestFixture]
    public class MapQuest
    {
        private TourManagerImplementation manager;
        private const int DISTANCE_WIEN_HOLLABRUNN = 56;
        private const int ESTIMATED_TIME_WIEN_HOLLABRUNN = 2603;
        private Tour tourItem, tourItemWalk, tourItemSameLocation;
        private ILogger logger;

        [SetUp]
        public void Setup()
        {
            manager = new TourManagerImplementation(logger);

            tourItem = new Tour(1, "Wien-Hollabrunn", "Das ist eine Test Tour", "Wien", "Hollabrunn", TransportTypes.Car);
            tourItemWalk = new Tour(2, "Wien-Hollabrunn", "Das ist eine Test Tour", "Wien", "Hollabrunn", TransportTypes.Walking);
            tourItemSameLocation = new Tour(3, "Wien-Wien", "Das ist eine Test Tour", "Wien", "Wien", TransportTypes.Car);
        }

        [Test]
        public async Task GetDistanceAndEstimatedTimeFromAPI_WienHollabrunn_Returns56kmAnd2603Sec()
        {
            Tour tourResult = await manager.GetDistanceAndTimeFromTour(tourItem);

            Assert.AreEqual(DISTANCE_WIEN_HOLLABRUNN, tourResult.Distance);
            Assert.AreEqual(ESTIMATED_TIME_WIEN_HOLLABRUNN, tourResult.EstimatedTime);
        }

        [Test]
        public async Task GetDistanceAndEstimatedTimeFromAPI_WienHollabrunn_Walking_ReturnsDifferentValuesThanCar()
        {
            Tour tourResult = await manager.GetDistanceAndTimeFromTour(tourItemWalk);
            
            Assert.AreNotEqual(DISTANCE_WIEN_HOLLABRUNN, tourResult.Distance);
            Assert.AreNotEqual(ESTIMATED_TIME_WIEN_HOLLABRUNN, tourResult.EstimatedTime);
        }

        [Test]
        public async Task GetDistanceAndEstimatedTimeFromAPI_WienWien_ReturnsHttpRequestException()
        {
            try
            {
                Tour tourResult = await manager.GetDistanceAndTimeFromTour(tourItemSameLocation);
                Assert.Fail();
            }
            catch
            {
                Assert.Throws<HttpRequestException>(delegate { throw new HttpRequestException(); });
            }

        }
    }
}
