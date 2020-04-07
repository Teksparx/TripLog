﻿using Ninject.Modules;
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
        }
    }
}
