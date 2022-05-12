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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TourPlanner_Ortner_Szuesz.BL;
using TourPlanner_Ortner_Szuesz.ViewModels;

namespace TourPlanner_Ortner_Szuesz.Views
{
    /// <summary>
    /// Interaktionslogik für TourDescriptionView.xaml
    /// </summary>
    public partial class TourDescriptionView : UserControl
    {
        public TourDescriptionView()
        {
            InitializeComponent();
            //this.DataContext = new TourListViewModel(TourManagerFactory.GetFactoryManager());
        }
    }
}
