using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands
{
    public class ExportToursCommand : CommandBase
    {
        public MenuViewModel MenuViewModel { get; }

        public ExportToursCommand(MenuViewModel menuViewModel)
        {
            MenuViewModel = menuViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            bool toursAvailable = MenuViewModel.ToursAvailable();
            return toursAvailable && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            MenuViewModel.ExportData();
        }
    }
}
