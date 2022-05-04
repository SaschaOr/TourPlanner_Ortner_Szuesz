using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.BL;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.ViewModels
{
    public class TourListViewModel : ViewModelBase
    {
        private ITourItemFactory tourItemFactory;
        private Tour currentTour;

        public ObservableCollection<Tour> Tours { get; set; }

        public Tour CurrentItem
        {
            get
            {
                return currentTour;
            }
            set
            {
                if((currentTour != value) && (value != null))
                {
                    currentTour = value;
                    RaisePropertyChangedEvent(nameof(CurrentItem));
                }
            }
        }

        // pass itemFactory over constructor parameter!
        public TourListViewModel()
        {
            this.tourItemFactory = TourItemFactory.GetInstance();

            InitTourList();    
        }

        private void InitTourList()
        {
            Tours = new ObservableCollection<Tour>();

            FillTourList();
        }

        private void FillTourList()
        {
            foreach (Tour tour in this.tourItemFactory.GetTours())
            {
                Tours.Add(tour);
            }
        }
    }
}
