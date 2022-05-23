using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner_Ortner_Szuesz.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
		public TourListViewModel TourListViewModel { get; set; }
		public RouteViewModel RouteViewModel { get; set; }
		public TourLogListViewModel TourLogListViewModel { get; set; }
		public MenuViewModel MenuViewModel { get; set; }

		public MainWindowViewModel(
			TourListViewModel tourListViewModel,
			RouteViewModel routeViewModel,
			MenuViewModel menuViewModel)
		{
			TourListViewModel = tourListViewModel;
			RouteViewModel = routeViewModel;
			MenuViewModel = menuViewModel;
		}
	}
}
