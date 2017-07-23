using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health_Station.Models
{
    public class Workout
    {
        public double CaloriesBurned { get; set; }
        public DateTime StartedOn { get; set; }
        public DateTime EndedOn { get; set; }
        public ActivityType Type { get; set; }

        /// <summary>
        /// Distance in metres
        /// </summary>
        public double Distance { get; set; }
    }
}
