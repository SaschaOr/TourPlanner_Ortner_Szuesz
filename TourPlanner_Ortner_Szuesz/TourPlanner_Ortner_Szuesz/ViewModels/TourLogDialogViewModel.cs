using Microsoft.Extensions.Logging;
using System;
using System.Windows;
using System.Windows.Input;
using TourPlanner_Ortner_Szuesz.BL;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Models.Enums;
using TourPlanner_Ortner_Szuesz.ViewModels.Commands.TourLogs;

namespace TourPlanner_Ortner_Szuesz.ViewModels
{
    public class TourLogDialogViewModel : ViewModelBase
    {
        public int TourLogId { get; set; }
        public DateTime TourLogDate { get; set; }
        public string TourLogTime { get; set; }
        public string TourLogDifficulty { get; set; }
        public string TourLogRating { get; set; }
        public string TourLogComment { get; set; }

        public string DialogHeading { get; set; }
        public bool TourLogToAdd { get; set; }
        public int ComboDifficultyTypeIndex { get; set; }
        public int ComboRatingIndex { get; set; }

        public Action CloseDialog { get; }
        public ICommand SubmitTourLogCommand { get; }
        public TourLogListViewModel TourLogListViewModel { get; set; }
        public ILogger Logger { get; }
        private const int MULTIPLIER_MINUTES_TO_SECONDS = 60;
        private const int DIVIDER_SECONDS_TO_MINUTES = 60;

        public TourLogDialogViewModel(TourLogListViewModel tourLogListViewModel, bool tourLogToAdd, Action closeDialog, ILogger logger)
        {
            DialogHeading = "Create Tour Log";
            TourLogId = 0;
            ComboDifficultyTypeIndex = (int)DifficultyTypes.medium;
            ComboRatingIndex = 2;
            TourLogDate = DateTime.Now;
            TourLogListViewModel = tourLogListViewModel;
            Logger = logger;
            TourLogToAdd = tourLogToAdd;

            // updating existing tour log item
            if(!tourLogToAdd)
            {
                DialogHeading = "Update Log";

                // fill in values
                FillTourLogValuesInTextBox();
            }

            SubmitTourLogCommand = new SubmitTourLogCommand(this);
            CloseDialog = closeDialog;
        }

        private void FillTourLogValuesInTextBox()
        {
            TourLog tourLogItem = TourLogListViewModel.SelectedTourLog;
            TourLogId = tourLogItem.Id;
            TourLogDate = tourLogItem.Date;
            TourLogTime = tourLogItem.TotalTime.ToString();
            TourLogDifficulty = tourLogItem.Difficulty.ToString();
            TourLogRating = tourLogItem.Rating.ToString();
            TourLogComment = tourLogItem.Comment;

            ComboDifficultyTypeIndex = (int)tourLogItem.Difficulty;
            
            // subtract -1 because of ComboBox index
            ComboRatingIndex = tourLogItem.Rating - 1;
        }

        public bool ValidateInput()
        {
            int time = 0;

            try
            {
                 time = Convert.ToInt32(TourLogTime);
            }
            catch
            {
                return false;
            }

            if(TourLogDate == null
                || time <= 0
                || string.IsNullOrEmpty(TourLogDifficulty)
                || string.IsNullOrEmpty(TourLogRating)
                || string.IsNullOrEmpty(TourLogComment))
            {
                return false;
            }

            return true;
        }

        public void AddNewTourLogItem()
        {
            TourLog tourLogItem = GetTourLogItemFromTextFields();

            if(tourLogItem == null)
            {
                MessageBox.Show("Error while adding tour log. Please try again!");
                return;
            }

            try
            {
                tourLogItem = TourManagerFactory.GetTourLogFactoryManager(Logger).CreateItem(tourLogItem);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error while saving tour log. Error: {ex.Message}");
            }

            // change seconds to minutes
            tourLogItem.TotalTime /= DIVIDER_SECONDS_TO_MINUTES;

            // show new tour in UI
            TourLogListViewModel.AddNewTourLogToList(tourLogItem);

            // close dialog
            CloseDialog();
        }

        public void UpdateTourLogItem()
        {
            TourLog tourLogItem = GetTourLogItemFromTextFields();

            if(tourLogItem == null)
            {
                MessageBox.Show("Error while updating tour log. Please try again!");
                return;
            }

            try
            {
                tourLogItem = TourManagerFactory.GetTourLogFactoryManager(Logger).UpdateItem(tourLogItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while saving tour log update. Error: {ex.Message}");
            }

            // change seconds to minutes
            tourLogItem.TotalTime /= DIVIDER_SECONDS_TO_MINUTES;

            // update UI
            TourLogListViewModel.UpdateTourLogList(tourLogItem);

            // close dialog
            CloseDialog();
        }

        private TourLog GetTourLogItemFromTextFields()
        {
            TourLog tourLogItem;
            try
            {
                tourLogItem = new TourLog(TourLogId,
                    TourLogDate,
                    (DifficultyTypes)Enum.Parse(typeof(DifficultyTypes), TourLogDifficulty),
                    Convert.ToInt32(TourLogTime) * MULTIPLIER_MINUTES_TO_SECONDS,
                    Convert.ToInt32(TourLogRating.Substring(0, 1)),
                    TourLogComment,
                    TourLogListViewModel.SelectedTour.Id);
            }
            catch
            {
                MessageBox.Show("Some inputs may not be in the right format! Please check your inputs!");
                return null;
            }

            return tourLogItem;
        }
    }
}
