using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner_Ortner_Szuesz.ViewModels
{
    public class RouteViewModel : ViewModelBase
    {
        public TourListViewModel TourListViewModel { get; set; }

        public RouteViewModel(TourListViewModel tourListViewModel)
        {
            TourListViewModel = tourListViewModel;
        }
    }
}
