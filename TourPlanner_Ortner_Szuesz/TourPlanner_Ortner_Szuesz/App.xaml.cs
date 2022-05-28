using Microsoft.Extensions.Logging;
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
            // logging
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            var logger = loggerFactory.CreateLogger("TourPlanner_Ortner_Szuesz");

            // view models
            var tourLogListViewModel = new TourLogListViewModel(logger);
            var tourListViewModel = new TourListViewModel(tourLogListViewModel, logger);
            var routeViewModel = new RouteViewModel(tourListViewModel, logger);
            var menuViewModel = new MenuViewModel(tourListViewModel, tourLogListViewModel, logger);
            var searchbarViewModel = new SearchbarViewModel(tourListViewModel, logger);

            var mainViewModel = new MainWindowViewModel(tourLogListViewModel, tourListViewModel, routeViewModel, menuViewModel, searchbarViewModel);

            var main = new MainWindow(mainViewModel);

            main.Show();
        }
    }
}
