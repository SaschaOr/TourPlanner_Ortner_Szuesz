using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner_Ortner_Szuesz.BL.PDF_Generation;

namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands
{
    public class GenerateTourReportCommand : CommandBase
    {
        public TourListViewModel TourListViewModel { get; }
        public TourLogListViewModel TourLogListViewModel { get; }

        private TourReportPDF tourReportPDF { get; set; }

        public GenerateTourReportCommand(TourListViewModel tourListViewModel, TourLogListViewModel tourLogListViewModel)
        {
            TourListViewModel = tourListViewModel;
            TourLogListViewModel = tourLogListViewModel;
        }

        public override void Execute(object parameter)
        {
            MessageBox.Show("hello");
            tourReportPDF = new TourReportPDF();
            tourReportPDF.PrintTourReport(TourListViewModel.SelectedTour, TourLogListViewModel.TourLogs);
        }
    }
}
