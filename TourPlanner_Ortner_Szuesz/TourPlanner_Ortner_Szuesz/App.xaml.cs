using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner_Ortner_Szuesz.BL;
using TourPlanner_Ortner_Szuesz.ViewModels;

namespace TourPlanner_Ortner_Szuesz
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var tourLogListViewModel = new TourLogListViewModel();
            var tourListViewModel = new TourListViewModel(tourLogListViewModel);
            var routeViewModel = new RouteViewModel(tourListViewModel);
            var menuViewModel = new MenuViewModel(tourListViewModel, tourLogListViewModel);
            var searchbarViewModel = new SearchbarViewModel(tourListViewModel);

            var mainViewModel = new MainWindowViewModel(tourLogListViewModel, tourListViewModel, routeViewModel, menuViewModel, searchbarViewModel);

            var main = new MainWindow(mainViewModel);

            main.Show();
        }
    }
}
