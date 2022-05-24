using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner_Ortner_Szuesz.BL;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Models.Enums;

namespace TourPlanner_Ortner_Szuesz.ViewModels.Commands
{
    public class SubmitTourCommand : CommandBase
    {
        public TourListViewModel TourListViewModel { get; }
        public TourDialogViewModel TourDialogViewModel { get; }
        public bool CreateTour { get; set; }

        public SubmitTourCommand(TourListViewModel tourListViewMode, TourDialogViewModel tourDialogViewModel, bool createTour)
        {
            TourListViewModel = tourListViewMode;
            TourDialogViewModel = tourDialogViewModel;
            CreateTour = createTour;
        }

        public override bool CanExecute(object parameter)
        {
            bool fieldsAreNotNull = true;

            if(string.IsNullOrEmpty(TourDialogViewModel.TourName)
                || string.IsNullOrEmpty(TourDialogViewModel.TourDescription)
                || string.IsNullOrEmpty(TourDialogViewModel.TourStartLocation)
                || string.IsNullOrEmpty(TourDialogViewModel.TourEndLocation))
            {
                fieldsAreNotNull = false;
            }

            return fieldsAreNotNull && base.CanExecute(parameter);
        }

        public override async void Execute(object parameter)
        {
            
            Tour tourItem = new Tour(TourDialogViewModel.TourId,
                TourDialogViewModel.TourName,
                TourDialogViewModel.TourDescription,
                TourDialogViewModel.TourStartLocation,
                TourDialogViewModel.TourEndLocation,
                (TransportTypes)Enum.Parse(typeof(TransportTypes), TourDialogViewModel.TourTransportType));

            try
            {
                if(CreateTour)
                {
                    // create tour
                    tourItem = await TourManagerFactory.GetTourFactoryManager().CreateItem(tourItem);
                    TourListViewModel.AddNewTourToList(tourItem);
                }
                else
                {
                    // update tour
                    // ERROR PREVENTION WHEN EG. GATEWAY TIMEOUT FROM MAPQUEST
                    tourItem = await TourManagerFactory.GetTourFactoryManager().UpdateItem(tourItem);
                    TourListViewModel.UpdateTourList(tourItem);
                }
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("Something went wrong. Please check your inputs!");
                throw new NullReferenceException(); // weglassen?
                return;
            }

            TourDialogViewModel.Close();
        }
    }
}
