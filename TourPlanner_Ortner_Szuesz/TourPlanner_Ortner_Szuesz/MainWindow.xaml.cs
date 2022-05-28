using System.Windows;
using TourPlanner_Ortner_Szuesz.ViewModels;

namespace TourPlanner_Ortner_Szuesz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
