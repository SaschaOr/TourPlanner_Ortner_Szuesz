using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner_Ortner_Szuesz.BL.PDF_Generation;

namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands.Reports
{
    public class GenerateTourReportCommand : CommandBase
    {
        public TourListViewModel TourListViewModel { get; }

        public GenerateTourReportCommand(TourListViewModel tourListViewModel)
        {
            TourListViewModel = tourListViewModel;
        }

        public override void Execute(object parameter)
        {
            TourListViewModel.CreateTourReport();
        }
    }
}
