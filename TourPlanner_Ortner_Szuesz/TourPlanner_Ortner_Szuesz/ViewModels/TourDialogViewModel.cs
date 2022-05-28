using Microsoft.Extensions.Logging;
using System;
using System.Windows;
using System.Windows.Input;
using TourPlanner_Ortner_Szuesz.BL;
using TourPlanner_Ortner_Szuesz.Models;
using TourPlanner_Ortner_Szuesz.Models.Enums;
using TourPlanner_Ortner_Szuesz.ViewModels.Commands.Tours;

namespace TourPlanner_Ortner_Szuesz.ViewModels
{
    public class TourDialogViewModel : ViewModelBase
    {
        public int TourId { get; set; }
        public string TourName { get; set; }
        public string TourDescription { get; set; }
        public string TourStartLocation { get; set; }
        public string TourEndLocation { get; set; }
        public string TourTransportType { get; set; }

        public string DialogHeading { get; set; }
        public int ComboTransportTypeIndex { get; set; }
        public bool TourToAdd { get; set; }

        public Action CloseDialog { get; }
        public ICommand SubmitTourCommand { get; }
        public TourListViewModel TourListViewModel { get; set; }
        public ILogger Logger { get; }

        public TourDialogViewModel(TourListViewModel tourListViewModel, bool tourToAdd, Action closeDialog, ILogger logger)
        {
            DialogHeading = "Create Tour";
            TourId = 0;
            ComboTransportTypeIndex = (int)TransportTypes.Car;
            TourListViewModel = tourListViewModel;
            Logger = logger;
            TourToAdd = tourToAdd;

            // updating existing tour item
            if (!tourToAdd)
            {
                DialogHeading = $"Update {tourListViewModel.SelectedTour.Name}";

                // fill in values
                FillTourValuesInTextBox();
            }

            SubmitTourCommand = new SubmitTourCommand(this);
            CloseDialog = closeDialog;
        }

        private void FillTourValuesInTextBox()
        {
            Tour tourItem = TourListViewModel.SelectedTour;

            TourId = tourItem.Id;
            TourName = tourItem.Name;
            TourDescription = tourItem.Description;
            TourStartLocation = tourItem.StartLocation;
            TourEndLocation = tourItem.EndLocation;
            TourTransportType = tourItem.TransportType.ToString();

            ComboTransportTypeIndex = (int)tourItem.TransportType;
        }

        public bool ValidateInput()
        {
            if (string.IsNullOrEmpty(TourName)
                || string.IsNullOrEmpty(TourDescription)
                || string.IsNullOrEmpty(TourStartLocation)
                || string.IsNullOrEmpty(TourEndLocation)
                || string.IsNullOrEmpty(TourTransportType))
            {
                return false;
            }

            return true;
        }

        public async void AddNewTourItem()
        {
            Tour tourItem = GetTourItemFromTextFields();

            if (tourItem == null)
            {
                MessageBox.Show("Error while adding tour. Please try again!");
                return;
            }

            try
            {
                tourItem = await TourManagerFactory.GetTourFactoryManager(Logger).CreateItem(tourItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while saving tour. Error: {ex.Message}");
            }

            // show new tour in UI
            TourListViewModel.AddNewTourToList(tourItem);

            // close dialog
            CloseDialog();
        }

        public async void UpdateTourItem()
        {
            Tour tourItem = GetTourItemFromTextFields();

            if (tourItem == null)
            {
                MessageBox.Show("Error while updating tour. Please try again!");
                return;
            }

            try
            {
                tourItem = await TourManagerFactory.GetTourFactoryManager(Logger).UpdateItem(tourItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while saving tour update. Error: {ex.Message}");
            }

            // update UI
            TourListViewModel.UpdateTourList(tourItem);

            // close dialog
            CloseDialog();
        }

        private Tour GetTourItemFromTextFields()
        {
            Tour tourItem;

            try
            {
                tourItem = new Tour(TourId,
                    TourName,
                    TourDescription,
                    TourStartLocation,
                    TourEndLocation,
                    (TransportTypes)Enum.Parse(typeof(TransportTypes), TourTransportType));

            }
            catch
            {
                MessageBox.Show("Some inputs may not be in the right format! Please check your inputs!");
                return null;
            }

            return tourItem;
        }
    }
}
