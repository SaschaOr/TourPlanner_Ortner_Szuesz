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
            // load file metadata
            FileInfo fileInfo = new FileInfo(imagePath);

            byte[] image = new byte[fileInfo.Length];

            using(FileStream fs = fileInfo.OpenRead())
            {
                fs.Read(image, 0, image.Length);
            }

            return image;
        }

        public void AddNewTourToList(Tour tourItem)
        {
            Tours.Add(tourItem);
        }
    }
}
