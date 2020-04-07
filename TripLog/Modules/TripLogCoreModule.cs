using System;
using Ninject.Modules;
using TripLog.Services;
using TripLog.ViewModels;

namespace TripLog.Modules
{
    public class TripLogCoreModule : NinjectModule
    {
        public override void Load()
        {
            // ViewModels
            Bind<MainViewModel>().ToSelf();
            Bind<NewEntryViewModel>().ToSelf();
            Bind<DetailViewModel>().ToSelf();

            // Core Services
            var tripLogService = new TripLogApiDataService(new Uri("https://entryfunction.azurewebsites.net/"));

            Bind<ITripLogDataService>().ToMethod(x => tripLogService).InSingletonScope();
        }
    }
}
