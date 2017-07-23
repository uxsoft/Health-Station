using Health_Station.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Health_Station.Models;
using Ninject;
using HealthKit;
using Foundation;
using System.Threading.Tasks;

namespace Health_Station.iOS.Modules
{
    public class IOSHealthService : IHealthService
    {
        private HKHealthStore store = new HKHealthStore();

        //IsStoreAvailable
        public bool IsStoreAvailable()
        {
            return HKHealthStore.IsHealthDataAvailable;
        }

        //HasPermissions
        public bool HasPermissions()
        {
            var access = store.GetAuthorizationStatus(HKObjectType.GetWorkoutType());
            return access.HasFlag(HKAuthorizationStatus.SharingAuthorized);
        }

        //RequestPermissionsAsync
        public Task<bool> RequestPermissionsAsync()
        {
            TaskCompletionSource<bool> tsc = new TaskCompletionSource<bool>();

            var typesToWrite = new NSSet(new[] { HKObjectType.GetWorkoutType() });
            var typesToRead = new NSSet(new[] { HKObjectType.GetWorkoutType() });

            store.RequestAuthorizationToShare(
                    typesToWrite,
                    typesToRead,
                    (success, error) =>
                    {
                        if (error != null)
                            tsc.SetException(new NSErrorException(error));
                        else tsc.SetResult(success);
                    });

            return tsc.Task;
        }

        //SubmitWorkout
        public async Task AddWorkout(Workout workout)
        {
            var w = HKWorkout.Create(
                    (HKWorkoutActivityType)workout.Type,
                    (NSDate)workout.StartedOn,
                    (NSDate)workout.EndedOn,
                    (workout.EndedOn - workout.StartedOn).TotalSeconds,
                    HKQuantity.FromQuantity(HKUnit.Calorie, workout.CaloriesBurned),
                    HKQuantity.FromQuantity(HKUnit.Meter, workout.Distance),
                    new HKMetadata());
            await store.SaveObjectAsync(w);
        }

        //QueryWorkouts
        public Task<IEnumerable<Workout>> GetWorkouts(ActivityType activityType)
        {
            TaskCompletionSource<IEnumerable<Workout>> tsc = new TaskCompletionSource<IEnumerable<Workout>>();
            var predicate = HKQuery
                .GetPredicateForWorkouts((HKWorkoutActivityType)activityType);
            var sort = new NSSortDescriptor(HKSample.SortIdentifierStartDate, false);

            var query = new HKSampleQuery(HKObjectType.GetWorkoutType(), predicate, 0, new[] { sort }, (q, results, error) =>
            {
                if (error != null)
                    tsc.SetException(new NSErrorException(error));
                else tsc.SetResult(results.OfType<HKWorkout>().Select(w => new Workout()
                {
                    StartedOn = (DateTime)w.StartDate,
                    EndedOn = (DateTime)w.EndDate,
                    CaloriesBurned = w.TotalEnergyBurned.GetDoubleValue(HKUnit.Calorie),
                    Distance = w.TotalDistance.GetDoubleValue(HKUnit.Meter),
                    Type = (ActivityType)w.WorkoutActivityType
                }));
            });

            store.ExecuteQuery(query);

            return tsc.Task;
        }
    }
}
