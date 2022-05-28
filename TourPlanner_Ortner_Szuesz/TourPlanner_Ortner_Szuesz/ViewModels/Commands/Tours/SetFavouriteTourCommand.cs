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

        public SetFavouriteTourCommand(TourListViewModel tourListViewModel)
        {
            TourListViewModel = tourListViewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            bool toursAvailable = TourListViewModel.CheckIfToursAvailable();

            return toursAvailable && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            TourListViewModel.UpdateFavouriteStatus();
        }
    }
}
