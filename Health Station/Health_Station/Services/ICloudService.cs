using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health_Station.Services
{
    public interface ICloudService
    {
        Task Add<T>(T item);

        Task<IEnumerable<T>> GetWorkouts<T>();

    }
}
