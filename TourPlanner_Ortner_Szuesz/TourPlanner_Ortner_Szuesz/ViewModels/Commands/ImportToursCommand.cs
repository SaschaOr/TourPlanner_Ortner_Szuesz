﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands
{
    public class ImportToursCommand : CommandBase
    {
        public MenuViewModel MenuViewModel { get; }

        public ImportToursCommand(MenuViewModel menuViewModel)
        {
            MenuViewModel = menuViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            MenuViewModel.ImportData();
        }
    }
}