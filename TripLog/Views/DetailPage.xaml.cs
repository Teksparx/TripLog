﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using TripLog.Models;
using TripLog.Services;
using TripLog.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TripLog.Views
{
    public partial class DetailPage : ContentPage
    {
        DetailViewModel ViewModel => BindingContext as DetailViewModel;

        public DetailPage()
        {
            InitializeComponent();

        }
        

        
        void UpdateMap()
        {
            if (ViewModel.Entry == null)
            {
                return;
            }

            //Center the map around the log entry's location
            map.MoveToRegion(MapSpan.FromCenterAndRadius(
            new Position(
                ViewModel.Entry.Latitude,
                ViewModel.Entry.Longitude),
                Distance.FromMiles(.5)));

            //Place pin on the map for the log entery's location
            map.Pins.Add(new Pin
                    {
                        Type = PinType.Place,
                        Label = ViewModel.Entry.Title,
                        Position = new Position(ViewModel.Entry.Latitude, ViewModel.Entry.Longitude)
                    });

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel != null)
            {
                ViewModel.PropertyChanged += OnViewModelPropertyChanged;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (ViewModel != null)
            {
                ViewModel.PropertyChanged += OnViewModelPropertyChanged;
            }
        }

        void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DetailViewModel.Entry))
            {
                UpdateMap();
            }
        }

    }
}
