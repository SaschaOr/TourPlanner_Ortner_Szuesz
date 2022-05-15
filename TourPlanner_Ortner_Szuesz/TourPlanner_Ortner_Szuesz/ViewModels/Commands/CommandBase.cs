using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands
{
    public abstract class CommandBase : ICommand
    {
        private readonly Predicate<object> canExecutePredicate;

        public abstract void Execute(object parameter);

        public virtual bool CanExecute(object parameter)
        {
            return canExecutePredicate == null ? true : canExecutePredicate(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
