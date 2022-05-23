using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner_Ortner_Szuesz.ViewModels.Commands;

namespace TourPlanner_Ortner_Szuesz.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        public ICommand GenerateTourReportCommand { get; }
        public ICommand GenerateSummarizedReportCommand { get; }

        public MenuViewModel(TourListViewModel tourListViewModel, TourLogListViewModel tourLogListViewModel)
        {
            GenerateTourReportCommand = new GenerateTourReportCommand(tourListViewModel, tourLogListViewModel);
        }
    }
}
