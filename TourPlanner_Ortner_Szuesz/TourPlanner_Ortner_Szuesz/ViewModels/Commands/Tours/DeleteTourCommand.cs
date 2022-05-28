namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands.Tours
{

	public class DeleteTourCommand : CommandBase
	{
		public TourListViewModel TourListViewModel { get; }

		public DeleteTourCommand(TourListViewModel tourListViewModel)
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
			TourListViewModel.DeleteSelectedTour();
		}
	}
}