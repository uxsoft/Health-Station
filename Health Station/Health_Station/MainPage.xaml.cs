using Health_Station.Models;
using Health_Station.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Health_Station
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Refresh();
        }

        public IHealthService Health { get { return App.Container.Get<IHealthService>(); } }
        public bool HasHealth { get; set; } = false;

        async Task Refresh()
        {
            if (Health.IsStoreAvailable())
            {
                if (Health.HasPermissions())
                    HasHealth = true;
                else
                    HasHealth = await Health.RequestPermissionsAsync();
            }
            else HasHealth = false;

            if (HasHealth)
            {
                var workouts = await Health.GetWorkouts(ActivityType.Rowing);
                var totalDistance = workouts.Sum(w => w.Distance);
                txtTotal.Text = $"Total distance {totalDistance / 1000}km";
            }
            else
            {
                txtTotal.Text = "Failed to gain access to HealthKit";
            }
        }

        private async void btnSubmit_Clicked(object sender, EventArgs e)
        {
            var CurrentWorkout = new RowingWorkout()
            {
                TotalStrokes = int.Parse(txtReps.Text)
            };

            await Health.AddWorkout(CurrentWorkout);
            txtReps.Text = "";
            await Refresh();
        }
    }
}
