using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Views;

namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands
{
    public class UpdateTourCommand : CommandBase
    {
        public TourListViewModel TourListViewModel { get; }
        public Tour TourItemUpdate { get; set; }

        public UpdateTourCommand(TourListViewModel tourListViewModel, Tour tourItemUpdate)
        {
            TourListViewModel = tourListViewModel;
            TourItemUpdate = tourItemUpdate;
        }

        public override bool CanExecute(object parameter)
        {
            bool toursAvailable = false;

            if (TourListViewModel.Tours.Count > 0)
            {
                toursAvailable = true;
            }

            return toursAvailable && base.CanExecute(parameter);
        }
        public override void Execute(object parameter)
        {
            var dialog = new TourDialog(TourListViewModel, TourItemUpdate);
            dialog.Show();
        }
    }
}
