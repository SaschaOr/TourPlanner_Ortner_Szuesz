﻿using System;
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
			bool toursAvailable = false;

			if(TourListViewModel.Tours.Count > 0)
            {
				toursAvailable = true;
            }

			return toursAvailable && base.CanExecute(parameter);
		}

		public override void Execute(object? parameter) {
			if (!TourListViewModel.DeleteSelectedTour()) {
				MessageBox.Show("Something went wrong! Please try again!"); 
				return;
			}
		}
	}
}