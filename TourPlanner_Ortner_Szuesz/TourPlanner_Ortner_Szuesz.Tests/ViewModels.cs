using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Models.Enums;
using TourPlanner_Ortner_Szuesz.ViewModels;
using TourPlanner_Ortner_Szuesz.Views;

namespace TourPlanner_Ortner_Szuesz.Tests
{
    [TestFixture]
    public class ViewModels
    {
        private TourListViewModel tourListViewModel;
        private TourLogListViewModel tourLogListViewModel, tourLogListViewModelEmpty;
        private MenuViewModel menuViewModel;

        private TourDialogViewModel tourDialogViewModel;
        private TourLogDialogViewModel tourLogDialogViewModel, tourLogDialogViewModelEmptyTourLog;

        private const string FALSE_IMAGE_PATH = "D:\\test\\1_Image.png";
        private const string UPDATE_TOUR_HEADING = "Update Wien-Hollabrunn";
        private const string UPDATE_TOUR_LOG_HEADING = "Update Log";

        private Tour tourItem;
        private TourLog tourLogItem, emptyTourLogItem;
        private ILogger logger;

        [SetUp]
        public void Setup()
        {
            
            tourLogListViewModel = new TourLogListViewModel(logger);
            tourLogListViewModelEmpty = new TourLogListViewModel(logger);
            tourListViewModel = new TourListViewModel(tourLogListViewModel, logger);
            
            menuViewModel = new MenuViewModel(tourListViewModel, tourLogListViewModel, logger);

            tourItem = new Tour(1, "Wien-Hollabrunn", "Das ist eine Test Tour", "Wien", "Hollabrunn", TransportTypes.Car);
            tourListViewModel.SelectedTour = tourItem;
            tourLogItem = new TourLog(1, DateTime.Now, DifficultyTypes.easy, 6000, 1, "Das ist ein Test Tour Log", 1);
            emptyTourLogItem = null;

            tourLogListViewModel.SelectedTour = tourItem;
            tourLogListViewModel.SelectedTourLog = tourLogItem;
            tourLogListViewModelEmpty.SelectedTour = tourItem;
            tourLogListViewModelEmpty.SelectedTourLog = emptyTourLogItem;

            tourDialogViewModel = new TourDialogViewModel(tourListViewModel, false, null, logger);
            tourLogDialogViewModel = new TourLogDialogViewModel(tourLogListViewModel, false, null, logger);
            tourLogDialogViewModelEmptyTourLog = new TourLogDialogViewModel(tourLogListViewModel, true, null, logger);

        }

        [Test]
        public void GetImageFromTour_FalsePath_ReturnsNull()
        {
            var result = tourListViewModel.GetImageFromFileSystem(FALSE_IMAGE_PATH);

            Assert.IsNull(result);
        }

        [Test]
        public void UpdateTourItem_GetDataFromViewModelToDialog_ReturnsTourId()
        {
            int id = tourDialogViewModel.TourId;

            Assert.IsNotNull(id);
            Assert.AreEqual(tourItem.Id, id);
        }

        [Test]
        public void UpdateTourItem_GetDataFromViewModelToDialog_ReturnsCorrectDialogHeading()
        {
            string headingText = tourDialogViewModel.DialogHeading;

            Assert.IsNotEmpty(headingText);
            Assert.AreEqual(UPDATE_TOUR_HEADING, headingText);
        }

        [Test]
        public void CheckIfToursAreAvailable_ForImportAndExport_ReturnsTrue()
        {
            tourListViewModel.Tours.Add(tourItem);

            bool toursAvailable = menuViewModel.ToursAvailable();

            Assert.IsTrue(toursAvailable);
        }

        [Test]
        public void UpdateTourLogItem_GetDataFromViewModelToDialog_ReturnsTourLogId()
        {
            int id = tourLogDialogViewModel.TourLogId;

            Assert.IsNotNull(id);
            Assert.AreEqual(tourLogItem.Id, id);
        }

        [Test]
        public void CreateTourLogItem_GetNotDataFromViewModelToDialog_ReturnsFalseHeading()
        {
            string headingText = tourLogDialogViewModelEmptyTourLog.DialogHeading;

            Assert.IsNotEmpty(headingText);
            Assert.AreNotEqual(UPDATE_TOUR_LOG_HEADING, headingText);
        }
    }
}
