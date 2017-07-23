using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health_Station.Models
{
    public class RowingWorkout : Workout
    {
        public RowingWorkout()
        {
            Type = ActivityType.Rowing;
        }

        public const double CALORIES_PER_STROKE = 0.062;
        public const double METERS_PER_STROKE = 10;
        public const double SECONDS_PER_STROKE = 5;

        private int _TotalStrokes = 0;
        public int TotalStrokes
        {
            get
            {
                return _TotalStrokes;
            }

            set
            {
                if (_TotalStrokes != value)
                {
                    _TotalStrokes = value;
                    CaloriesBurned = CALORIES_PER_STROKE * _TotalStrokes;
                    Distance = METERS_PER_STROKE * _TotalStrokes;
                    EndedOn = DateTime.Now;
                    var duration = TimeSpan.FromSeconds(SECONDS_PER_STROKE * _TotalStrokes);
                    StartedOn = (EndedOn - duration);
                }
            }
        }
    }
}
