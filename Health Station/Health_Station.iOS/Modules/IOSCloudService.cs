using CloudKit;
using Foundation;
using Health_Station.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Health_Station.Services;

namespace Health_Station.iOS.Modules
{
    public class IOSCloudService : ICloudService
    {
        public CKDatabase Db { get { return CKContainer.DefaultContainer.PrivateCloudDatabase; } }

        public async Task Add<T>(T item)
        {
            var record = new CKRecord(typeof(T).Name);
            record[typeof(T).Name] = new NSString(JsonConvert.SerializeObject(item));
            await Db.SaveRecordAsync(record);
        }

        public async Task<IEnumerable<T>> GetWorkouts<T>()
        {
            var query = new CKQuery(typeof(T).Name, NSPredicate.FromValue(true));
            var records = await Db.PerformQueryAsync(query, CKRecordZone.DefaultRecordZone().ZoneId);

            return records.Select(r => JsonConvert.DeserializeObject<T>(r[typeof(T).Name].ToString()));
        }
    }
}
