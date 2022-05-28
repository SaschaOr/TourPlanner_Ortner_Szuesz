namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands.Reports
{
    public class GenerateSummarizedTourReportCommand : CommandBase
    {
        public TourListViewModel TourListViewModel { get; }

        public GenerateSummarizedTourReportCommand(TourListViewModel tourListViewModel)
        {
            TourListViewModel = tourListViewModel;
        }

        public override void Execute(object parameter)
        {
            TourListViewModel.CreateSummarizedTourReport();
        }
    }
}
