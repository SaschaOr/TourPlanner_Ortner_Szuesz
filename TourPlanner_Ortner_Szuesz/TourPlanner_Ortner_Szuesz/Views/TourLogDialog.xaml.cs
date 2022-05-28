using Microsoft.Extensions.Logging;
using System.Windows;
using TourPlanner_Ortner_Szuesz.ViewModels;

namespace TourPlanner_Ortner_Szuesz.Views
{
    /// <summary>
    /// Interaction logic for TourLogDialog.xaml
    /// </summary>
    public partial class TourLogDialog : Window
    {
        public TourLogDialog(TourLogListViewModel tourLogListViewModel, bool tourLogToAdd, ILogger logger)
        {
            DataContext = new TourLogDialogViewModel(tourLogListViewModel, tourLogToAdd, this.Close, logger);
            InitializeComponent();
        }
    }
}
