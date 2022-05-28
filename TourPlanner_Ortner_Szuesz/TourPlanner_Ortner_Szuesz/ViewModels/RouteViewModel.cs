using Microsoft.Extensions.Logging;

namespace TourPlanner_Ortner_Szuesz.ViewModels
{
    public class RouteViewModel : ViewModelBase
    {
        public TourListViewModel TourListViewModel { get; set; }

        public RouteViewModel(TourListViewModel tourListViewModel, ILogger logger)
        {
            TourListViewModel = tourListViewModel;
        }
    }
}
