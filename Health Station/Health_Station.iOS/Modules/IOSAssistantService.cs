using Intents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Health_Station.iOS.Modules
{
    public class IOSAssistantService
    {
        public Task<bool> RequestPermissionsAsync()
        {
            var tcs = new TaskCompletionSource<bool>();

            INPreferences.RequestSiriAuthorization(status => tcs.SetResult(
                status == INSiriAuthorizationStatus.Authorized || 
                status == INSiriAuthorizationStatus.Restricted));

            return tcs.Task;
        }

        public bool HasPermissions()
        {
            return INPreferences.SiriAuthorizationStatus == INSiriAuthorizationStatus.Authorized ||
                INPreferences.SiriAuthorizationStatus == INSiriAuthorizationStatus.Restricted;
        }
    }
}
