using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourPlanner_Ortner_Szuesz.BL.Import_Export;
using TourPlanner_Ortner_Szuesz.DAL.Configuration;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.ViewModels.Commands;
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

        public MenuViewModel(TourListViewModel tourListViewModel, TourLogListViewModel tourLogListViewModel)
        {
            TourListViewModel = tourListViewModel;
            GenerateTourReportCommand = new GenerateTourReportCommand(tourListViewModel, tourLogListViewModel);
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

            ExportDataCSV export = new ExportDataCSV();
            string path = Path.Combine(Directory.GetCurrentDirectory(), TourPlannerConfigurationManager.GetConfig().ExportLocation, "tour_export.csv");
            export.Export(tours, path);
        }

        public void ExportDataJSON()
        {
            ObservableCollection<Tour> tours = TourListViewModel.Tours;

            ExportDataJSON export = new ExportDataJSON();
            string path = Path.Combine(Directory.GetCurrentDirectory(), TourPlannerConfigurationManager.GetConfig().ExportLocation, "tour_export.json");
            export.Export(tours, path);
        }

        public void ImportDataCSV()
        {
            TourListViewModel.Tours.Clear();

            ImportDataCSV import = new ImportDataCSV();
            string path = Path.Combine(Directory.GetCurrentDirectory(), TourPlannerConfigurationManager.GetConfig().ExportLocation, "tour_export.csv");

            TourListViewModel.Tours = import.Import(path);
            TourListViewModel.UpdateUIAfterImport();
        }

        public void ImportDataJSON()
        {
            TourListViewModel.Tours.Clear();

            // import data
            ImportDataJSON import = new ImportDataJSON();
            string path = Path.Combine(Directory.GetCurrentDirectory(), TourPlannerConfigurationManager.GetConfig().ExportLocation, "tour_export.json");
            TourListViewModel.Tours = import.Import(path);
            TourListViewModel.UpdateUIAfterImport();

            // save data in database
            ImportExportFactory.GetImportExportFactoryManager().ImportAllTours(TourListViewModel.Tours);
        }
    }
}
