using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner_Ortner_Szuesz.BL;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.ViewModels.Commands;
using TourPlanner_Ortner_Szuesz.ViewModels.Commands.TourLogs;
using TourPlanner_Ortner_Szuesz.Views;

namespace TourPlanner_Ortner_Szuesz.ViewModels
{
    public class TourLogListViewModel : ViewModelBase
    {
        private ITourLogManager mediaManager;
        public Tour SelectedTour { get; set; }
        private TourLog selectedTourLog { get; set; }

        public ObservableCollection<TourLog> TourLogs { get; set; }

        public ICommand AddTourLogCommand { get; set; }
        public ICommand UpdateTourLogCommand { get; set; }
        public ICommand DeleteTourLogCommand { get; set; }

        public ILogger Logger { get; }
        private const int DIVIDER_SECONDS_TO_MINUTES = 60;

        public TourLog SelectedTourLog
        {
            get
            {
                return selectedTourLog;
            }
            set
            {
                selectedTourLog = value;
                RaisePropertyChangedEvent(nameof(SelectedTourLog));

                UpdateTourLogCommand = new UpdateTourLogCommand(this);
                RaisePropertyChangedEvent(nameof(UpdateTourLogCommand));

                DeleteTourLogCommand = new DeleteTourLogCommand(this);
                RaisePropertyChangedEvent(nameof(DeleteTourLogCommand));
            }
        }

        public TourLogListViewModel(ILogger logger)
        {
            Logger = logger;
            this.mediaManager = TourManagerFactory.GetTourLogFactoryManager(logger);
            TourLogs = new ObservableCollection<TourLog>();
        }

        public void LoadDataFromSelectedTour(Tour tourItem)
        {
            if (tourItem != null)
            {
                SelectedTour = tourItem;

                TourLogs = new ObservableCollection<TourLog>();
                FillTourLogList(tourItem.Id);
                RaisePropertyChangedEvent(nameof(TourLogs));

                AddTourLogCommand = new AddTourLogCommand(this);

                RaisePropertyChangedEvent(nameof(AddTourLogCommand));
            }
        }

        private void FillTourLogList(int tourId)
        {
            foreach (TourLog tourLog in this.mediaManager.GetItems(tourId))
            {
                // change time to minutes
                tourLog.TotalTime /= DIVIDER_SECONDS_TO_MINUTES;
                TourLogs.Add(tourLog);
            }
        }

        public void AddNewTourLogToList(TourLog tourLogItem)
        {
            TourLogs.Add(tourLogItem);
        }

        public void UpdateTourLogList(TourLog tourLogItem)
        {
            var tourLog = TourLogs.FirstOrDefault(tourLog => tourLog.Id == tourLogItem.Id);
            int index = TourLogs.IndexOf(tourLog);

            // replace old tour log with updated one
            TourLogs[index] = tourLogItem;
            SelectedTourLog = tourLogItem;
        }

        public void DeleteSelectedTourLog()
        {
            bool isDeleted = this.mediaManager.DeleteItem(SelectedTourLog);

            if (isDeleted)
            {
                // remove from list
                TourLogs.Remove(SelectedTourLog);
            }
            else
            {
                MessageBox.Show("Error while deleting the selected tour log! Please try again!");
            }
        }

        public void OpenAddTourLogDialog()
        {
            var dialog = new TourLogDialog(this, true, Logger);
            dialog.ShowDialog();
        }

        public void OpenUpdateTourLogDialog()
        {
            var dialog = new TourLogDialog(this, false, Logger);
            dialog.ShowDialog();
        }

        public bool CheckIfTourLogsAvailable()
        {
            if(TourLogs.Count > 0)
            {
                return true;
            }

            return false;
        }
    }
}
