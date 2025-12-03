using System.Collections.Generic;
using _Project.Scripts.Common.Services.Analytics.Base;
using _Project.Scripts.Common.Services.Analytics.Base.Data;
using Firebase.Analytics;

namespace _Project.Scripts.Common.Services.Analytics.Implementation
{
    public class FireBaseEventSender : IEventSender
    {
        public void SendEvent<T>(T analyticEvent) where T : IAnalyticData => 
            FirebaseAnalytics.LogEvent(analyticEvent.ID);

        public void SendEvent<TData, TParametor>(TData analyticEvent) 
            where TData : IAnalyticData<TParametor>
        {
            foreach (KeyValuePair<string, TParametor> parameter in analyticEvent.Data)
                FirebaseAnalytics.LogEvent(analyticEvent.ID, parameter.Key, parameter.Value.ToString());
        }
    }
}
