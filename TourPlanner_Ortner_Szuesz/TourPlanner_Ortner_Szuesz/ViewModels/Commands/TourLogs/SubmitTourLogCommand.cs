using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner_Ortner_Szuesz.BL;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Models.Enums;

namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands.TourLogs
{
    public class SubmitTourLogCommand : CommandBase
    {
        public TourLogDialogViewModel TourLogDialogViewModel { get; }

        public SubmitTourLogCommand(TourLogDialogViewModel tourLogDialogViewModel)
        {
            TourLogDialogViewModel = tourLogDialogViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            bool validateInput = TourLogDialogViewModel.ValidateInput();

            return validateInput && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            if(TourLogDialogViewModel.TourLogToAdd)
            {
                TourLogDialogViewModel.AddNewTourLogItem();
            }
            else
            {
                TourLogDialogViewModel.UpdateTourLogItem();
            }
        }
    }
}
