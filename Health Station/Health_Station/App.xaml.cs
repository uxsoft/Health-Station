using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Health_Station
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Health_Station.MainPage();
        }

        public static StandardKernel Container { get; set; } = new StandardKernel();

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
