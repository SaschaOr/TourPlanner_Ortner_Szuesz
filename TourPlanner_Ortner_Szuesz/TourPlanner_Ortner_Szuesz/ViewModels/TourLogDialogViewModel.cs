using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.ViewModels.Commands;

namespace TourPlanner_Ortner_Szuesz.ViewModels
{
    public class TourLogDialogViewModel : ViewModelBase
    {
        public int TourLogId { get; set; }
        public DateTime TourLogDate { get; set; }
        public string TourLogTime { get; set; }
        public int TourLogDifficulty { get; set; }
        public int TourLogRating { get; set; }
        public string TourLogComment { get; set; }
        public string DialogHeading { get; set; }
        private bool createTourLog { get; set; }
        public Tour SelectedTour { get; set; }

        public Action Close { get; }
        public ICommand SubmitTourLogCommand { get; }

        public TourLogDialogViewModel(TourLogListViewModel tourLogListViewModel, Tour tourItem, TourLog tourLogItem, Action close)
        {
            createTourLog = true;
            DialogHeading = "Create Tour Log";
            TourLogId = 0;
            SelectedTour = tourItem;
            TourLogDate = DateTime.Now;

            // updating existing tour log item
            if(tourLogItem != null)
            {
                DialogHeading = "Update Log";
                createTourLog = false;
                FillTourLogValuesInTextBox(tourLogItem);
            }

            SubmitTourLogCommand = new SubmitTourLogCommand(tourLogListViewModel, this, createTourLog);
            Close = close;
        }

        private void FillTourLogValuesInTextBox(TourLog tourLogItem)
        {
            TourLogId = tourLogItem.Id;
            TourLogDate = tourLogItem.Date;
            TourLogTime = tourLogItem.TotalTime.ToString();
            TourLogDifficulty = (int)tourLogItem.Difficulty;
            TourLogRating = (int)tourLogItem.Rating;
            TourLogComment = tourLogItem.Comment;
        }
    }
}
