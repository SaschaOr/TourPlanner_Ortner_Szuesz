namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands.Tours
{
    public class SetFavouriteTourCommand : CommandBase
    {
        public TourListViewModel TourListViewModel { get; }

        public SetFavouriteTourCommand(TourListViewModel tourListViewModel)
        {
            TourListViewModel = tourListViewModel;
        }

        public override bool CanExecute(object? parameter)
        {
            bool toursAvailable = TourListViewModel.CheckIfToursAvailable();

            return toursAvailable && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            TourListViewModel.UpdateFavouriteStatus();
        }
    }
}
