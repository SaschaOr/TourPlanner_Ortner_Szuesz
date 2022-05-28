namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands.Search
{
    public class SearchTourCommand : CommandBase
    {
        public SearchbarViewModel SearchbarViewModel { get; }

        public SearchTourCommand(SearchbarViewModel searchbarViewModel)
        {
            SearchbarViewModel = searchbarViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            SearchbarViewModel.SearchItems();
        }
    }
}
