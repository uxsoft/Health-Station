using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health_Station.Extensions
{
    public static class NinjectExtensions
    {
        public static void Load(this StandardKernel kernel, NinjectModule module)
        {
            kernel.Load(new NinjectModule[] { module });
        }

        public static void Load<T>(this StandardKernel kernel) where T: NinjectModule, new()
        {
            var module = Activator.CreateInstance<T>();
            kernel.Load(new NinjectModule[] { module });
        }
    }
}
