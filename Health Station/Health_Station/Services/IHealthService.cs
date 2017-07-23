using Health_Station.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health_Station.Services
{
    public interface IHealthService
    {
        //IsStoreAvailable
        bool IsStoreAvailable();

        //HasPermissions
        bool HasPermissions();

        //RequestPermissionsAsync
        Task<bool> RequestPermissionsAsync();

        //SubmitWorkout
        Task AddWorkout(Workout workout);

        //QueryWorkouts
        Task<IEnumerable<Workout>> GetWorkouts(ActivityType activityType);
    }
}
