using System.Collections.Generic;

namespace _Project.Scripts.Common.Services.Analytics.Base.Data
{
    public interface IAnalyticData
    {
        string ID { get; }
    }

    public interface IAnalyticData<T> : IAnalyticData
    {
        IReadOnlyDictionary<string, T> Data { get; }
    }
}