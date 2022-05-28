namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands.Tours
{
    public class AddTourCommand : CommandBase
    {
        public TourListViewModel TourListViewModel { get; }

        public AddTourCommand(TourListViewModel tourListViewModel)
        {
            TourListViewModel = tourListViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            TourListViewModel.OpenAddTourDialog();
        }
    }
}
