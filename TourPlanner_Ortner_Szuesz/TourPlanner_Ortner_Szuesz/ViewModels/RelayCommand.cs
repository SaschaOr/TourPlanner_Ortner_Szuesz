using System;
using System.Windows.Input;

namespace TourPlanner_Ortner_Szuesz.ViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _ExecuteAction;
        private readonly Predicate<object> _CanExecutePredicate;

        public void Execute(object? parameter)
        {
            _ExecuteAction(parameter);
        }

        public bool CanExecute(object? parameter)
        {
            return _CanExecutePredicate == null ? true : _CanExecutePredicate(parameter);
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if(execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _ExecuteAction = execute;
            _CanExecutePredicate = canExecute;
        }
    }
}
