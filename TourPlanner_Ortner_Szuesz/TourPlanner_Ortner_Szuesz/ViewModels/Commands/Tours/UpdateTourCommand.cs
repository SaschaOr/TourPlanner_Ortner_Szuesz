namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands.Tours
{
    public class UpdateTourCommand : CommandBase
    {
        public TourListViewModel TourListViewModel { get; }

        public UpdateTourCommand(TourListViewModel tourListViewModel)
        {
            TourListViewModel = tourListViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            bool toursAvailable = TourListViewModel.CheckIfToursAvailable();

            return toursAvailable && base.CanExecute(parameter);
        }
        public override void Execute(object parameter)
        {
            TourListViewModel.OpenUpdateTourDialog();
        }
    }
}
