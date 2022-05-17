using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.ViewModels;

namespace TourPlanner_Ortner_Szuesz.Views
{
    /// <summary>
    /// Interaction logic for TourLogDialog.xaml
    /// </summary>
    public partial class TourLogDialog : Window
    {
        public TourLogDialog(TourLogListViewModel tourLogListViewModel, Tour tourItem, TourLog tourLogItem)
        {
            DataContext = new TourLogDialogViewModel(tourLogListViewModel, tourItem, tourLogItem, this.Close);
            InitializeComponent();
        }
    }
}
