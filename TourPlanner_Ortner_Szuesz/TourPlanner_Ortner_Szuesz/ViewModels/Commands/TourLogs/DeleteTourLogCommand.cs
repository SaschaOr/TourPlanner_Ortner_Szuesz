using System;
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
            bool tourLogsAvailable = false;

            if (TourLogListViewModel.TourLogs.Count > 0)
            {
                tourLogsAvailable = true;
            }

            return tourLogsAvailable && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            if (!TourLogListViewModel.DeleteSelectedTourLog())
            {
                MessageBox.Show("Something went wrong! Please try again!");
                return;
            }
        }
    }
}
