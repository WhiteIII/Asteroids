using _Project.Scripts.Common.Services.Analytics.Base.Data;

namespace _Project.Scripts.Common.Services.Analytics.Base
{
    public interface IEventSender
    {
        void SendEvent<T>() where T : IAnalyticData, new();
        void SendEvent<T, TParameter>(T analyticEvent) where T : IAnalyticData<TParameter>;
    }
}