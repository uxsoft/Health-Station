using Health_Station.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Health_Station.iOS.Modules
{
    public class IOSModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IHealthService>().To<IOSHealthService>();
        }
    }
}
