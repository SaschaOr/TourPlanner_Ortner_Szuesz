using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using TourPlanner_Ortner_Szuesz.ViewModels;


namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands
{

	public class DeleteTourCommand : CommandBase
	{
		public TourListViewModel TourListViewModel { get; }

		public DeleteTourCommand(TourListViewModel tourListViewModel) {
			TourListViewModel = tourListViewModel; 
		}

		public override bool CanExecute(object? parameter) {
			return !TourListViewModel.IsEmpty() &&
				base.CanExecute(parameter);
		}

		public override void Execute(object? parameter) {
			if (!TourListViewModel.DeleteTour()) {
				MessageBox.Show("Could not delete selected tour", "Tour deletion error", MessageBoxButton.OK,
					MessageBoxImage.Error); 
				return;
			}
			TourListViewModel.RemoveSelectedTourFromList;
		}
	}
}