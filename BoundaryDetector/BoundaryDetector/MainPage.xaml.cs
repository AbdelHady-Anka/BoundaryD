using BoundaryDetector.Models;
using BoundaryDetector.Persistence;
using BoundaryDetector.Services;
using BoundaryDetector.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
namespace BoundaryDetector
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        protected override bool OnBackButtonPressed()
        {
            SearchBox.Unfocus();
            base.OnBackButtonPressed();
            if (Math.Abs(lastTranslation) > 0)
            {
                CloseBottomSheet();
                return true;
            }
            return false;
        }



        void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            MessagingCenter.Send(this, Events.SearchBarTextChanged, e.NewTextValue);
        }


        private double _currentYOfBottomSheet, _totalYBeforeCompleted;


        void SearchBar_Focused(object sender, FocusEventArgs e)
        {
            OpenBottomSheet();
        }


        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    var translateY = Math.Max(Math.Min(0, _currentYOfBottomSheet + e.TotalY), -Math.Abs((Height * .1) - Height));
                    bottomSheet.TranslateTo(bottomSheet.X, translateY, 50);
                    _totalYBeforeCompleted = e.TotalY;
                    break;

                case GestureStatus.Completed:
                    _currentYOfBottomSheet = bottomSheet.TranslationY;
                    var finalTranslation = Math.Max(Math.Min(0, -1000), -Math.Abs(_getClosestLockState(e)));
                    bottomSheet.TranslateTo(bottomSheet.X, finalTranslation, 700, Easing.SpringOut);
                    lastTranslation = finalTranslation;
                    break;

            }

        }
        private double _getClosestLockState(PanUpdatedEventArgs e)
        {

            double TranslationY = e.TotalY + _currentYOfBottomSheet;
            var lockStates = new double[] { 0, .4, .9 };

            var distance = Math.Abs(TranslationY);
            var currentProportion = distance / Height;

            var smallestDistance = 10000.0;
            var closestIndex = 0;
            for (var i = 0; i < lockStates.Length; i++)
            {
                var state = lockStates[i];
                var absoluteDistance = Math.Abs(state - currentProportion);
                if (absoluteDistance < smallestDistance)
                {
                    smallestDistance = absoluteDistance;
                    closestIndex = i;
                }
            }

            if (_totalYBeforeCompleted > 0 && (lockStates[closestIndex] - currentProportion) > 0)
            {
                int nextStateIndex = lockStates.Length - 1;
                closestIndex = 0;
            }

            if (_totalYBeforeCompleted < 0 && (lockStates[closestIndex] - currentProportion) < 0)
            {
                closestIndex = closestIndex + 1 > lockStates.Length - 1 ? closestIndex : lockStates.Length - 1;
            }
            var selectedLockState = lockStates[closestIndex];

            var TranslateToLockState = _getProportionCoordinate(selectedLockState);

            return TranslateToLockState;
        }

        private double _getProportionCoordinate(double proportion)
          => proportion * Height;

        private double lastTranslation = 0;
        void OpenBottomSheet()
        {
            var finalTranslation = Math.Max(Math.Min(0, -1000), -Math.Abs(_getProportionCoordinate(.9)));
            bottomSheet.TranslateTo(bottomSheet.X, finalTranslation, 150, Easing.SpringIn);
            lastTranslation = finalTranslation;
        }

        void CloseBottomSheet()
        {
            var finalTranslation = -Math.Abs(_getProportionCoordinate(0));
            bottomSheet.TranslateTo(bottomSheet.X, finalTranslation, 150, Easing.SpringIn);
            lastTranslation = finalTranslation;
        }
        public MainPage()
        {
            MessagingCenter.Subscribe<GFieldViewModel>(this, Events.ClearMapClicked, ClearMap);
            MessagingCenter.Subscribe<GFieldViewModel, bool>(this, Events.MapClicked, MapClicked);
            MessagingCenter.Subscribe<GFieldViewModel>(this, Events.FieldSelected, FieldSelected);
            MessagingCenter.Subscribe<GFieldViewModel>(this, Events.GFiledAdded, GFieldAdded);
            InitializeComponent();
        }

        private void GFieldAdded(GFieldViewModel obj)
        {
            ClearMap(null);
            CloseBottomSheet();
        }

        private void FieldSelected(GFieldViewModel obj)
        {
            CloseBottomSheet();
        }

        public GFieldViewModel ViewModel
        {
            get
            {
                return BindingContext as GFieldViewModel;
            }
            set
            {
                BindingContext = value;
            }
        }

        public void MapClicked(GFieldViewModel source, bool anyResult)
        { 
            AddGFieldButton.IsEnabled = anyResult;
            ManageLayout.IsVisible = anyResult;
            SearchLayout.IsVisible = !anyResult;
        }
        private void AddGFieldButton_Clicked(object sender, EventArgs e)
        {
            OpenBottomSheet();
            SearchLayout.IsVisible = false;
            ManageLayout.IsVisible = true;
        }


        private void ClearMap(GFieldViewModel source)
        {
            AddGFieldButton.IsEnabled = false;
            ManageLayout.IsVisible = false;
            SearchLayout.IsVisible = true;
        }

    }

}
