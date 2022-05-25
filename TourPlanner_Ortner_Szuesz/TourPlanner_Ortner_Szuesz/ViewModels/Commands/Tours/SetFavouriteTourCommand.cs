using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner_Ortner_Szuesz.Models;

namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands.Tours
{
    public class SetFavouriteTourCommand : CommandBase
    {
        public TourListViewModel TourListViewModel { get; }
        public Tour TourItemUpdate { get; }

        public SetFavouriteTourCommand(TourListViewModel tourListViewModel, Tour tourItemUpdate)
        {
            TourListViewModel = tourListViewModel;
            TourItemUpdate = tourItemUpdate;
        }

        public override bool CanExecute(object? parameter)
        {
            bool toursAvailable = false;

            if (TourListViewModel.Tours.Count > 0)
            {
                toursAvailable = true;
            }

            return toursAvailable && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            if (!TourListViewModel.UpdateFavouriteStatus(TourItemUpdate))
            {
                MessageBox.Show("Something went wrong! Please try again!");
                return;
            }
        }
    }
}
