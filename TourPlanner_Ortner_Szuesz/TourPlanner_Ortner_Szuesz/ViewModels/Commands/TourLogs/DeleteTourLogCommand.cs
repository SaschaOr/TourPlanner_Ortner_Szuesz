﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands.TourLogs
{
    public class DeleteTourLogCommand : CommandBase
    {
        public TourLogListViewModel TourLogListViewModel { get; }

        public DeleteTourLogCommand(TourLogListViewModel tourLogListViewModel)
        {
            TourLogListViewModel = tourLogListViewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            bool tourLogsAvailable = TourLogListViewModel.CheckIfTourLogsAvailable(); ;

            return tourLogsAvailable && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            TourLogListViewModel.DeleteSelectedTourLog();
        }
    }
}
