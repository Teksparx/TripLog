using System;
using System.Collections.Generic;
using System.Linq;
using TripLog.Models;
using TripLog.Services;
using TripLog.ViewModels;
using Xamarin.Forms;

namespace TripLog.Views
{
    public partial class MainPage : ContentPage
    {
        MainViewModel ViewModel => BindingContext as MainViewModel;

        public MainPage()
        {
            InitializeComponent();


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel?.Init();
        }
    }
}
