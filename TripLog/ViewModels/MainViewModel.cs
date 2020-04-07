using System;
using System.Collections.ObjectModel;
using TripLog.Models;
using TripLog.Services;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        readonly ITripLogDataService _tripLogService;
        //readonly IBlobCache _cache;

        ObservableCollection<TripLogEntry> _logEntries;
        public ObservableCollection<TripLogEntry> LogEntries
        {
            get => _logEntries;
            set
            {
                _logEntries = value;
                OnPropertyChanged();
            }
        }

        public Command<TripLogEntry> ViewCommand => new Command<TripLogEntry>(async entry => await NavService.NavigateTo<DetailViewModel, TripLogEntry>(entry));

        public Command NewCommand => new Command(async () => await NavService.NavigateTo<NewEntryViewModel>());

        Command _refreshCommand;
        public Command RefreshCommand => _refreshCommand ?? (_refreshCommand = new Command(LoadEntries));

        public MainViewModel(INavService navService, ITripLogDataService tripLogService /*,IBlobCache cache*/)
            : base(navService)
        {
            _tripLogService = tripLogService;
            //_cache = cache;

            LogEntries = new ObservableCollection<TripLogEntry>();
        }

        public override void Init()
        {
            LoadEntries();
        }

        async void LoadEntries()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                var entries = await _tripLogService.GetEntriesAsync();
                LogEntries = new ObservableCollection<TripLogEntry>(entries);
                // Load from local cache and then immediately load from API
                //_cache.GetAndFetchLatest("entries", async () => await _tripLogService.GetEntriesAsync())
                //    .Subscribe(entries =>
                //    {
                //        LogEntries = new ObservableCollection<TripLogEntry>(entries);
                //        IsBusy = false;
                //    });
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
