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

        public ICommand AddTourCommand { get; }
        public ICommand DeleteTourCommand { get; }

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

                    //MessageBox.Show(selectedTour.Description);
                    //MessageBox.Show(SelectedTour.Description);
                    //OnPropertyChanged(nameof(SelectedTour));

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

        public bool emptyList()
        {
            return Tours.Count <= 0;
        }
        public void AddNewTourToList(Tour tourItem)
        {
            Tours.Add(tourItem);
        }

        public void updateTour(Tour tour)
        {
            var previousTour = Tours.FirstOrDefault(previousTour => previousTour.Id == tour.Id);
            Tours[Tours.IndexOf(previousTour)] = tour;
            SelectedTour = tour;
        }

        public void RemoveSelectedTourFromList()
        {
            var remove = SelectedTour;
            SelectedTour = Tours.FirstOrDefault();
            Tours.Remove(remove);
        }
        
        public bool DeleteTour(Tour tourItem)
        {
            return TourManagerFactory.GetTourFactoryManager().DeleteItem(tourItem);
        }
        
    }
}
