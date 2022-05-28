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
