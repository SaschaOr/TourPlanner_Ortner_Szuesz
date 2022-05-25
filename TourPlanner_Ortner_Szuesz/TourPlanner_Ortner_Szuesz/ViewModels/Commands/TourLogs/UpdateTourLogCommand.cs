using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Views;

namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands.TourLogs
{
    public class UpdateTourLogCommand : CommandBase
    {
        public TourLogListViewModel TourLogListViewModel { get; }
        public TourLog TourLogItemUpdate { get; set; }
        public Tour TourItem { get; set; }

        public UpdateTourLogCommand(TourLogListViewModel tourLogListVIewModel, Tour tourItem, TourLog tourLogItem)
        {
            TourLogListViewModel = tourLogListVIewModel;
            TourItem = tourItem;
            TourLogItemUpdate = tourLogItem;
        }

        public override bool CanExecute(object parameter)
        {
            bool tourLogsAvailable = false;

            if (TourLogListViewModel.TourLogs.Count > 0)
            {
                tourLogsAvailable = true;
            }

            return tourLogsAvailable && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            var dialog = new TourLogDialog(TourLogListViewModel, TourItem, TourLogItemUpdate);
            dialog.ShowDialog();
        }
    }
}
