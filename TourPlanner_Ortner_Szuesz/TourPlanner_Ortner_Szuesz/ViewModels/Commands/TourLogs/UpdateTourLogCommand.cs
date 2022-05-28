namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands.TourLogs
{
    public class UpdateTourLogCommand : CommandBase
    {
        public TourLogListViewModel TourLogListViewModel { get; }

        public UpdateTourLogCommand(TourLogListViewModel tourLogListViewModel)
        {
            TourLogListViewModel = tourLogListViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            bool tourLogsAvailable = TourLogListViewModel.CheckIfTourLogsAvailable();

            return tourLogsAvailable && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            TourLogListViewModel.OpenUpdateTourLogDialog();
        }
    }
}
