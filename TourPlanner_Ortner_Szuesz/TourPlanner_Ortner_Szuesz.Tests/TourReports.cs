using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.BL.PDF_Generation;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.Tests
{
    [TestFixture]
    public class TourReports
    {
        private TourReportPDF tourReport;
        private SummarizedTourReportPDF summarizedReport;

        private ObservableCollection<Tour> emptyTours;
        private ObservableCollection<TourLog> emptyTourLogs;
        private Tour emptyTour;
        private ILogger logger;

        [SetUp]
        public void Setup()
        {
            tourReport = new TourReportPDF(logger);
            summarizedReport = new SummarizedTourReportPDF(logger);

            emptyTours = new ObservableCollection<Tour>();
            emptyTourLogs = new ObservableCollection<TourLog>();
            emptyTour = null;
        }

        [Test]
        public void TourReportGetsEmptyTour_ReturnsFalse()
        {
            bool isPrintable = tourReport.PrintTourReport(emptyTour, emptyTourLogs);
            
            Assert.IsFalse(isPrintable);
        }

        [Test]
        public void SummarizedTourReportGetsEmptyTourList_ReturnsFalse()
        {
            bool isPrintable = summarizedReport.PrintSummarizedTourReport(emptyTours);

            Assert.IsFalse(isPrintable);
        }
    }
}
