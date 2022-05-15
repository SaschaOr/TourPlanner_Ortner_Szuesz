using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner_Ortner_Szuesz.BL;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.ViewModels.Commands;
using TourPlanner_Ortner_Szuesz.Views;

namespace TourPlanner_Ortner_Szuesz.ViewModels
{
    public class TourListViewModel : ViewModelBase
    {
        //private ITourItemFactory tourItemFactory;
        private ITourManager mediaManager;
        private Tour selectedTour;

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
                    //MessageBox.Show(selectedTour.Description);
                    //MessageBox.Show(SelectedTour.Description);
                    //OnPropertyChanged(nameof(SelectedTour));
                }
            }
        }

        // pass itemFactory over constructor parameter!
        public TourListViewModel()
        {
            this.mediaManager = TourManagerFactory.GetFactoryManager();

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
    }
}
