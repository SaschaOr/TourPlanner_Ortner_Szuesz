using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner_Ortner_Szuesz.BL;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.ViewModels.Commands.Search;

namespace TourPlanner_Ortner_Szuesz.ViewModels
{
    public class SearchbarViewModel : ViewModelBase
    {
        public TourListViewModel TourListViewModel { get; set; }
        public string SearchString { get; set; }
        public ICommand SearchTourCommand { get; }
        public ILogger Logger { get; }

        public SearchbarViewModel(TourListViewModel tourListViewModel, ILogger logger)
        {
            Logger = logger;
            TourListViewModel = tourListViewModel;
            SearchTourCommand = new SearchTourCommand(this);
        }

        public void SearchItems()
        {
            TourListViewModel.Tours.Clear();
            ITourManager manager = TourManagerFactory.GetTourFactoryManager(Logger);
            
            // add search result to tours
            foreach(Tour tour in manager.GetSearchResults(SearchString))
            {
                TourListViewModel.Tours.Add(tour);
            }

            TourListViewModel.UpdateUIAfterImport();
        }
    }
}
