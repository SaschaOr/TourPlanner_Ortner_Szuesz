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

namespace TourPlanner_Ortner_Szuesz.ViewModels
{
    public class TourListViewModel : ViewModelBase
    {
        //private ITourItemFactory tourItemFactory;
        private ITourManager mediaManager;
        private Tour selectedTour;

        public TourLogListViewModel TourLogListViewModel { get; set; }

        public ObservableCollection<Tour> Tours { get; set; }
        //public bool IsFavourite { get; set; }

        public ICommand AddTourCommand { get; }
        public ICommand UpdateTourCommand { get; set; }
        public ICommand DeleteTourCommand { get; }
        public ICommand SetFavouriteTourCommand { get; set; }

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
                    UpdateTourCommand = new UpdateTourCommand(this, selectedTour);
                    RaisePropertyChangedEvent(nameof(UpdateTourCommand));

                    // update favourite status
                    SetFavouriteTourCommand = new SetFavouriteTourCommand(this, selectedTour);
                    RaisePropertyChangedEvent(nameof(SetFavouriteTourCommand));

                    // load logs from selected tour
                    TourLogListViewModel.LoadDataFromSelectedTour(selectedTour);
                }
            }
        }

        // pass itemFactory over constructor parameter!
        public TourListViewModel(TourLogListViewModel tourLogListViewModel)
        {
            this.mediaManager = TourManagerFactory.GetTourFactoryManager();
            TourLogListViewModel = tourLogListViewModel;

            Tours = new ObservableCollection<Tour>();

            InitTourList();

            AddTourCommand = new RelayCommand((_) =>
            {
                var dialog = new TourDialog(this, null);
                dialog.ShowDialog();
            });

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
                Tours.Add(tour);
            }
        }

        private byte[] GetImageFromFileSystem(String imagePath)
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
        
        public bool DeleteSelectedTour()
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

            return isDeleted;
        }
        
        public bool UpdateFavouriteStatus(Tour tourItem)
        {
            bool isUpdated = this.mediaManager.UpdateFavouriteStatus(tourItem.Id, !tourItem.IsFavourite);

            // change favourite status of selected tour
            if(isUpdated)
            {
                tourItem.IsFavourite = !tourItem.IsFavourite;
            }

            RefillTourList();

            return isUpdated;
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
    }
}
