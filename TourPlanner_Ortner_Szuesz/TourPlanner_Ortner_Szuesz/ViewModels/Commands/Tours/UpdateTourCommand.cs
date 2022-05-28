﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Views;

namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands.Tours
{
    public class UpdateTourCommand : CommandBase
    {
        public TourListViewModel TourListViewModel { get; }

        public UpdateTourCommand(TourListViewModel tourListViewModel)
        {
            TourListViewModel = tourListViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            bool toursAvailable = TourListViewModel.CheckIfToursAvailable();

            return toursAvailable && base.CanExecute(parameter);
        }
        public override void Execute(object parameter)
        {
            TourListViewModel.OpenUpdateTourDialog();
        }
    }
}
