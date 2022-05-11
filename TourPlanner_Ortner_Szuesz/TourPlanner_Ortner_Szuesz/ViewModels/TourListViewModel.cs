using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner_Ortner_Szuesz.BL;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.ViewModels
{
    public class TourListViewModel : ViewModelBase
    {
        //private ITourItemFactory tourItemFactory;
        private ITourManager mediaManager;
        private Tour selectedTour;

        public ObservableCollection<Tour> Tours { get; set; }

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
        public TourListViewModel(ITourManager mediaManager)
        {
            this.mediaManager = mediaManager;

            Tours = new ObservableCollection<Tour>();
            //SelectedTour = new Tour(3, "Bitte", "Funktioniere", "jo", "bro", Models.Enums.TransportTypes.bike, 10, 10, "bro");

            InitTourList();    
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
