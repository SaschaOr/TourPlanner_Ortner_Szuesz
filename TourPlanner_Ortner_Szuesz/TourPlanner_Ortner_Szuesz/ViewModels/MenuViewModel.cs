using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner_Ortner_Szuesz.BL.Import_Export;
using TourPlanner_Ortner_Szuesz.DAL.Configuration;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.ViewModels.Commands.Import_Export;
using TourPlanner_Ortner_Szuesz.ViewModels.Commands.Reports;

namespace TourPlanner_Ortner_Szuesz.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        public TourListViewModel TourListViewModel { get; set; }
        public ICommand GenerateTourReportCommand { get; }
        public ICommand GenerateSummarizedTourReportCommand { get; }
        public ICommand ExportToursCommand { get; }
        public ICommand ImportToursCommand { get; }

        public ILogger Logger { get; }

        public MenuViewModel(TourListViewModel tourListViewModel, TourLogListViewModel tourLogListViewModel, ILogger logger)
        {
            Logger = logger;
            TourListViewModel = tourListViewModel;
            GenerateTourReportCommand = new GenerateTourReportCommand(tourListViewModel);
            GenerateSummarizedTourReportCommand = new GenerateSummarizedTourReportCommand(tourListViewModel);
            ExportToursCommand = new ExportToursCommand(this);
            ImportToursCommand = new ImportToursCommand(this);
        }

        public bool ToursAvailable()
        {
            if (TourListViewModel.Tours.Count > 0)
            {
                return true;
            }

            return false;
        }

        public void ExportDataCSV()
        {
            ObservableCollection<Tour> tours = TourListViewModel.Tours;

            ExportDataCSV export = new ExportDataCSV(Logger);
            string path = Path.Combine(Directory.GetCurrentDirectory(), TourPlannerConfigurationManager.GetConfig().ExportLocation, "tour_export.csv");
            export.Export(tours, path);
        }

        public void ExportDataJSON()
        {
            ObservableCollection<Tour> tours = TourListViewModel.Tours;

            ExportDataJSON export = new ExportDataJSON(Logger);
            string path = Path.Combine(Directory.GetCurrentDirectory(), TourPlannerConfigurationManager.GetConfig().ExportLocation, "tour_export.json");
            export.Export(tours, path);
        }

        public void ImportDataCSV()
        {
            TourListViewModel.Tours.Clear();

            ImportDataCSV import = new ImportDataCSV(Logger);
            string path = Path.Combine(Directory.GetCurrentDirectory(), TourPlannerConfigurationManager.GetConfig().ExportLocation, "tour_export.csv");

            TourListViewModel.Tours = import.Import(path);
            TourListViewModel.UpdateUIAfterImport();
        }

        public async Task<Tour> ImportDataJSON()
        {
            // delete all tours in database
            ImportExportFactory.GetImportExportFactoryManager(Logger).DeleteAllTours();

            TourListViewModel.Tours.Clear();

            // import data
            ImportDataJSON import = new ImportDataJSON(Logger);
            string path = Path.Combine(Directory.GetCurrentDirectory(), TourPlannerConfigurationManager.GetConfig().ExportLocation, "tour_export.json");
            ObservableCollection<Tour> importedTours = import.Import(path);
            //TourListViewModel.UpdateUIAfterImport();

            // save data in database
            Tour tourItem = await ImportExportFactory.GetImportExportFactoryManager(Logger).ImportAllTours(importedTours);

            // get new tour items
            TourListViewModel.FillTourList();
            TourListViewModel.UpdateUIAfterImport();

            return tourItem;
        }
    }
}
