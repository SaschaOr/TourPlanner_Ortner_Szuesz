using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner_Ortner_Szuesz.BL;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Models.Enums;

namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands
{
    public class SubmitTourLogCommand : CommandBase
    {
        public TourLogListViewModel TourLogListViewModel { get; }
        public TourLogDialogViewModel TourLogDialogViewModel { get; }
        public bool CreateTourLog { get; set; }

        public SubmitTourLogCommand(TourLogListViewModel tourLogListViewModel, TourLogDialogViewModel tourLogDialogViewModel, bool createTourLog)
        {
            TourLogListViewModel = tourLogListViewModel;
            TourLogDialogViewModel = tourLogDialogViewModel;
            CreateTourLog = createTourLog;
        }

        public override bool CanExecute(object parameter)
        {
            bool fieldsAreNotNull = true;

            if (string.IsNullOrEmpty(TourLogDialogViewModel.TourLogComment)
                || string.IsNullOrEmpty(TourLogDialogViewModel.TourLogTime))
            {
                fieldsAreNotNull = false;
            }

            return fieldsAreNotNull && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            TourLog tourLogItem = new TourLog(TourLogDialogViewModel.TourLogId,
                TourLogDialogViewModel.TourLogDate,
                (DifficultyTypes)TourLogDialogViewModel.TourLogDifficulty,
                Convert.ToInt32(TourLogDialogViewModel.TourLogTime),
                TourLogDialogViewModel.TourLogRating,
                TourLogDialogViewModel.TourLogComment,
                TourLogDialogViewModel.SelectedTour.Id);

            try
            {
                if (CreateTourLog)
                {
                    //create tour
                    tourLogItem = TourManagerFactory.GetTourLogFactoryManager().CreateItem(tourLogItem);
                    TourLogListViewModel.AddNewTourLogToList(tourLogItem);
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Something went wrong. Please check your inputs!");
                throw new NullReferenceException(); // weglassen?
                return;
            }

            TourLogDialogViewModel.Close();
        }
    }
}
