namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands.TourLogs
{
    public class AddTourLogCommand : CommandBase
    {
        public TourLogListViewModel TourLogListViewModel { get; }

        public AddTourLogCommand(TourLogListViewModel tourLogListViewModel)
        {
            TourLogListViewModel = tourLogListViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            TourLogListViewModel.OpenAddTourLogDialog();
        }
    }
}
