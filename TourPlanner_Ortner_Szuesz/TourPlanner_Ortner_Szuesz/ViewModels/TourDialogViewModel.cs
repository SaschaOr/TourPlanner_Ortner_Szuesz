using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.ViewModels.Commands;

namespace TourPlanner_Ortner_Szuesz.ViewModels
{
    public class TourDialogViewModel
    {
        public int TourId { get; set; }
        public string TourName { get; set; }
        public string TourDescription { get; set; }
        public string TourStartLocation { get; set; }
        public string TourEndLocation { get; set; }
        public int TourTransportType { get; set; }
        public string DialogHeading { get; set; }
        private bool createTour { get; set; }

        public Action Close { get; }
        public ICommand SubmitTourCommand { get; }

        public TourDialogViewModel(TourListViewModel tourListViewModel, Tour tourItem, Action close)
        {
            createTour = true;
            DialogHeading = "Create Tour";
            TourId = 0;
            
            // updating existing tour item
            if(tourItem != null)
            {
                DialogHeading = $"Update {tourItem.Name}";
                createTour = false;

                // fill in values
                FillTourValuesInTextBox(tourItem);
            }

            SubmitTourCommand = new SubmitTourCommand(tourListViewModel, this, createTour);
            Close = close;
        }

        private void FillTourValuesInTextBox(Tour tourItem)
        {
            TourId = tourItem.Id;
            TourName = tourItem.Name;
            TourDescription = tourItem.Description;
            TourStartLocation = tourItem.StartLocation;
            TourEndLocation = tourItem.EndLocation;
        }
    }
}
