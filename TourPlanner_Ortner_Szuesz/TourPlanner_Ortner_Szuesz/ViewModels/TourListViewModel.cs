using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourPlanner_Ortner_Szuesz.BL;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.ViewModels.Commands;
using TourPlanner_Ortner_Szuesz.Views;
using System.Drawing;
using TourPlanner_Ortner_Szuesz.ViewModels.Commands.Tours;
using Microsoft.Extensions.Logging;
using TourPlanner_Ortner_Szuesz.BL.PDF_Generation;

namespace TourPlanner_Ortner_Szuesz.ViewModels
{
    public class TourListViewModel : ViewModelBase
    {
        //private ITourItemFactory tourItemFactory;
        private ITourManager mediaManager;
        private Tour selectedTour;

        public TourLogListViewModel TourLogListViewModel { get; set; }

        public ObservableCollection<Tour> Tours { get; set; }

        public ICommand AddTourCommand { get; }
        public ICommand UpdateTourCommand { get; set; }
        public ICommand DeleteTourCommand { get; }
        public ICommand SetFavouriteTourCommand { get; set; }

        public ILogger Logger { get; }
        private const int DIVIDER_SECONDS_TO_MINUTES = 60;

        public Tour SelectedTour
        {
            get
            {
                return selectedTour;
            }
            set
            {
                if((selectedTour != value) && (value != null))
                {
                    selectedTour = value;
                    RaisePropertyChangedEvent(nameof(SelectedTour));

                    // display image
                    if(selectedTour.RouteImagePath != null)
                    {
                        string currentPath = Path.Combine(Directory.GetCurrentDirectory(), selectedTour.RouteImagePath);
                        selectedTour.RouteImage = GetImageFromFileSystem(currentPath);
                        RaisePropertyChangedEvent(nameof(SelectedTour));
                    }

                    // update selected tour
                    UpdateTourCommand = new UpdateTourCommand(this);
                    RaisePropertyChangedEvent(nameof(UpdateTourCommand));

                    // update favourite status
                    SetFavouriteTourCommand = new SetFavouriteTourCommand(this);
                    RaisePropertyChangedEvent(nameof(SetFavouriteTourCommand));

                    // load logs from selected tour
                    TourLogListViewModel.LoadDataFromSelectedTour(selectedTour);
                }
            }
        }

        // pass itemFactory over constructor parameter!
        public TourListViewModel(TourLogListViewModel tourLogListViewModel, ILogger logger)
        {
            Logger = logger;
            this.mediaManager = TourManagerFactory.GetTourFactoryManager(logger);
            TourLogListViewModel = tourLogListViewModel;

            Tours = new ObservableCollection<Tour>();

            InitTourList();

            AddTourCommand = new AddTourCommand(this);
            DeleteTourCommand = new DeleteTourCommand(this);
        }

        private void InitTourList()
        {
            Tours = new ObservableCollection<Tour>();

            FillTourList();
        }

        private void FillTourList()
        {
            foreach (Tour tour in this.mediaManager.GetItems())
            {
                // change seconds to minutes
                tour.EstimatedTime /= DIVIDER_SECONDS_TO_MINUTES;
                Tours.Add(tour);
            }
        }

        public byte[] GetImageFromFileSystem(String imagePath)
        {
            byte[] image;

            try
            {
                // load file metadata
                FileInfo fileInfo = new FileInfo(imagePath);

                image = new byte[fileInfo.Length];

                using (FileStream fs = fileInfo.OpenRead())
                {
                    fs.Read(image, 0, image.Length);
                }
            }
            catch
            {
                image = null;
            }

            return image;
        }

        public void AddNewTourToList(Tour tourItem)
        {
            Tours.Add(tourItem);
        }

        public void UpdateTourList(Tour tourItem)
        {
            var tour = Tours.FirstOrDefault(tour => tour.Id == tourItem.Id);
            int index = Tours.IndexOf(tour);

            // replace old tour with updated one
            Tours[index] = tourItem;
            SelectedTour = tourItem;
        }
        
        public void DeleteSelectedTour()
        {
            bool isDeleted = this.mediaManager.DeleteItem(SelectedTour);

            if(isDeleted)
            {
                // remove from list
                Tours.Remove(SelectedTour);

                // delete image
                string path = Path.Combine(Directory.GetCurrentDirectory(), SelectedTour.RouteImagePath);
                
                if(File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            else
            {
                MessageBox.Show("Error while deleting the selected tour! Please try again!");
            }
        }
        
        public void UpdateFavouriteStatus()
        {
            bool isUpdated = this.mediaManager.UpdateFavouriteStatus(SelectedTour.Id, !SelectedTour.IsFavourite);

            // change favourite status of selected tour
            if(isUpdated)
            {
                SelectedTour.IsFavourite = !SelectedTour.IsFavourite;
            }
            else
            {
                MessageBox.Show("Error while updating favourite status of the selected tour! Please try again!");
            }

            RefillTourList();
        }

        private void RefillTourList()
        {
            List<Tour> tmpList = new List<Tour>(Tours);
            Tours.Clear();

            foreach (Tour tour in tmpList)
            {
                Tours.Add(tour);
            }

        }

        public void UpdateUIAfterImport()
        {
            RaisePropertyChangedEvent(nameof(Tours));
        }

        public void CreateSummarizedTourReport()
        {
            SummarizedTourReportPDF summarizedTourReportPDF = new SummarizedTourReportPDF(Logger);
            summarizedTourReportPDF.PrintSummarizedTourReport(Tours);
        }

        public void CreateTourReport()
        {
            TourReportPDF tourReportPDF = new TourReportPDF(Logger);
            tourReportPDF.PrintTourReport(SelectedTour, TourLogListViewModel.TourLogs);
        }

        public void OpenAddTourDialog()
        {
            var dialog = new TourDialog(this, true, Logger);
            dialog.ShowDialog();
        }

        public void OpenUpdateTourDialog()
        {
            var dialog = new TourDialog(this, false, Logger);
            dialog.ShowDialog();
        }

        public bool CheckIfToursAvailable()
        {
            if (Tours.Count > 0)
            {
                return true;
            }

            return false;
        }
    }
}
