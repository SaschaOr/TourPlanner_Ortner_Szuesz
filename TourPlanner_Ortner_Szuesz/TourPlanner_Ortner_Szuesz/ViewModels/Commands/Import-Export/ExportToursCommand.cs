namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands.Import_Export
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
            MenuViewModel.ExportDataJSON();
        }
    }
}
