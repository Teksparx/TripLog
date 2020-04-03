using System;
using System.Collections.Generic;
using TripLog.ViewModels;
using Xamarin.Forms;
using System.ComponentModel;
using System.Linq;
using TripLog.Services;

namespace TripLog.Views
{
    public partial class NewEntryPage : ContentPage
    {
        NewEntryViewModel ViewModel => BindingContext as NewEntryViewModel;
        public NewEntryPage()
        {
            InitializeComponent();

            BindingContextChanged += Page_BindingContextChanged;

        }

        void Page_BindingContextChanged(object sender, EventArgs e)
        {
            ViewModel.ErrorsChanged += ViewModel_ErrorChanged; 
        }

        private void ViewModel_ErrorChanged(object sender, DataErrorsChangedEventArgs e)
        { 
            var propHasErrors = (ViewModel.GetErrors(e.PropertyName) as List<string>)?.Any() == true;

            switch (e.PropertyName)
            {
                case nameof(ViewModel.Title) :
                    title.LabelColor = propHasErrors ? Color.Red : Color.Black;
                    break;
                case nameof(ViewModel.Rating):
                    rating.LabelColor = propHasErrors ? Color.Red : Color.Black;
                    break;
                default:
                    break;
            }
        }
    }
}
