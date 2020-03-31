using BoundaryDetector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoundaryDetector.Views
{
    public partial class PanView : ViewCell
    {
        public static BindableProperty DeleteCommandProperty =
            BindableProperty.Create(nameof(DeleteCommand), typeof(ICommand), typeof(PanView));

        public ICommand DeleteCommand
        {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }


        public static BindableProperty DeleteCommandParameterProperty =
            BindableProperty.Create(nameof(DeleteCommandParameter), typeof(object), typeof(PanView));
        public object DeleteCommandParameter
        {
            get => GetValue(DeleteCommandParameterProperty);
            set => SetValue(DeleteCommandParameterProperty, value);
        }




        public static BindableProperty ShowFieldCommandProperty =
            BindableProperty.Create(nameof(ShowFieldCommand), typeof(ICommand), typeof(PanView));

        public ICommand ShowFieldCommand
        {
            get => (ICommand)GetValue(ShowFieldCommandProperty);
            set => SetValue(ShowFieldCommandProperty, value);
        }


        public static BindableProperty ShowFieldCommandParameterProperty =
           BindableProperty.Create(nameof(ShowFieldCommandParameter), typeof(object), typeof(PanView));
        public object ShowFieldCommandParameter
        {
            get => GetValue(ShowFieldCommandParameterProperty);
            set => SetValue(ShowFieldCommandParameterProperty, value);
        }



        public static BindableProperty UpdateFieldCommandProperty =
            BindableProperty.Create(nameof(UpdateFieldCommand), typeof(ICommand), typeof(PanView));

        public ICommand UpdateFieldCommand
        {
            get => (ICommand)GetValue(UpdateFieldCommandProperty);
            set => SetValue(UpdateFieldCommandProperty, value);
        }


        public static BindableProperty UpdateFieldCommandParameterProperty =
           BindableProperty.Create(nameof(UpdateFieldCommandParameter), typeof(object), typeof(PanView));
        public object UpdateFieldCommandParameter
        {
            get => GetValue(UpdateFieldCommandParameterProperty);
            set => SetValue(UpdateFieldCommandParameterProperty, value);
        }
        public PanView()
        {
            InitializeComponent();
            var x = Color.LightGray.MultiplyAlpha(0);
            var a = x.ToHex();
            //EditFieldFrame.BorderColor = Color.FromRgba(211, 211, 211, 1);
        }
        private string OldEntryFieldText = "";
        private void EditFieldButtonClicked(object sender, EventArgs e)
        {
            if (EditFieldLayout.IsVisible)
            {
                if (!SaveClicked)
                {
                    FieldNameEntry.Text = OldEntryFieldText;
                }
                new Animation(x =>
                {
                    EditFieldLayout.HeightRequest = x * EditFieldLayout.MinimumHeightRequest;
                    if (EditFieldLayout.HeightRequest == 0)
                    {
                        EditFieldLayout.IsVisible = false;
                    }

                }, 1, 0, Easing.SpringIn)
                    .Commit(EditFieldLayout, "Collapse");
            }
            else
            {
                OldEntryFieldText = FieldNameEntry.Text;

                new Animation(x =>
                {
                    EditFieldLayout.HeightRequest = x * EditFieldLayout.MinimumHeightRequest;
                    EditFieldLayout.IsVisible = true;
                }, 0, 1, Easing.SpringIn)
                    .Commit(EditFieldLayout, "Expand");
            }

            if (FieldNameEntry.IsEnabled)
            {
                new Animation(x =>
                {
                    EditFieldFrame.BorderColor = Color.FromRgba(.82745099067688, .82745099067688, .82745099067688, x);
                    if(x <= 0)
                    {
                        FieldNameEntry.IsEnabled = false;
                    }
                }, 1, 0, Easing.BounceIn)
                    .Commit(EditFieldFrame, "Disable");
            }
            else
            {
                new Animation(x =>
                {
                    EditFieldFrame.BorderColor = Color.FromRgba(.82745099067688, .82745099067688, .82745099067688, x);
                    if(x >= 1.0)
                    {
                        FieldNameEntry.IsEnabled = true;
                    }
                }, 0, 1, Easing.BounceOut)
                    .Commit(EditFieldFrame, "Enable");
            }

            //R = 0.82745099067688, G = 0.82745099067688, B = 0.82745099067688, A = 1, Hue = 0, Saturation = 0, Luminosity = 0.82745099067688

        }

        private bool SaveClicked = false;
        private void SaveButtonClicked(object sender, EventArgs e)
        {
            SaveClicked = true;
        }
    }
}