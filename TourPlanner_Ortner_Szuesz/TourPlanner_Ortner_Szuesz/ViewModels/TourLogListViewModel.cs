using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner_Ortner_Szuesz.BL;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.ViewModels.Commands;
using TourPlanner_Ortner_Szuesz.Views;

namespace TourPlanner_Ortner_Szuesz.ViewModels
{
    public class TourLogListViewModel : ViewModelBase
    {
        private ITourLogManager mediaManager;
        public Tour SelectedTour { get; set; }
        public TourLog selectedTourLog { get; set; }

        public ObservableCollection<TourLog> TourLogs { get; set; }

        public ICommand AddTourLogCommand { get; set;  }


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
            }
        }

        public TourLogListViewModel()
        {
            this.mediaManager = TourManagerFactory.GetTourLogFactoryManager();
            TourLogs = new ObservableCollection<TourLog>();
        }

        public void LoadDataFromSelectedTour(Tour tourItem)
        {
            if(tourItem != null)
            {
                SelectedTour = tourItem;

                TourLogs = new ObservableCollection<TourLog>();
                FillTourLogList(tourItem.Id);

                AddTourLogCommand = new RelayCommand((_) =>
                {
                    var dialog = new TourLogDialog(this, tourItem, null);
                    dialog.ShowDialog();
                });

                RaisePropertyChangedEvent(nameof(AddTourLogCommand));
            }
        }

        private void FillTourLogList(int tourId)
        {
            foreach (TourLog tourLog in this.mediaManager.GetItems(tourId))
            {
                TourLogs.Add(tourLog);
            }
        }

        public void AddNewTourLogToList(TourLog tourLogItem)
        {
            TourLogs.Add(tourLogItem);
        }
    }
}
