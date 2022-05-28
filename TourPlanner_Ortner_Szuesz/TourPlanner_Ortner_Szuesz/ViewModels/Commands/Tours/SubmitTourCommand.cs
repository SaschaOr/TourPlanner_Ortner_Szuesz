using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner_Ortner_Szuesz.BL;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Models.Enums;

namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands.Tours
{
    public class SubmitTourCommand : CommandBase
    {
        public TourDialogViewModel TourDialogViewModel { get; }

        public SubmitTourCommand(TourDialogViewModel tourDialogViewModel)
        {
            TourDialogViewModel = tourDialogViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            bool validateInput = TourDialogViewModel.ValidateInput(); 

            return validateInput && base.CanExecute(parameter);
        }

        public override async void Execute(object parameter)
        {
            if(TourDialogViewModel.TourToAdd)
            {
                TourDialogViewModel.AddNewTourItem();
            }
            else
            {
                TourDialogViewModel.UpdateTourItem();
            }
        }
    }
}
