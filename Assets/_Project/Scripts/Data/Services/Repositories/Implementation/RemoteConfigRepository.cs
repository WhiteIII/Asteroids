using System;
using System.Collections.Generic;
using _Project.Scripts.Common.Services.RemoteConfig.Base;
using _Project.Scripts.Data.Base;
using _Project.Scripts.Data.Implementation;
using _Project.Scripts.Data.Services.Repositories.Base;

namespace _Project.Scripts.Data.Services.Repositories.Implementation
{
    public class RemoteConfigRepository : IDataRepository
    {
        private readonly IRemoteConfigService _remoteConfigService;
        private readonly Dictionary<string, Type> _configKeysAndTypes;
        private readonly List<IData> _dataList = new();

        public RemoteConfigRepository(IRemoteConfigService remoteConfigService)
        {
            _remoteConfigService = remoteConfigService;
            _configKeysAndTypes = new Dictionary<string, Type>
            {
                { "asteroids_stats", typeof(AsteroidsConfig) },
                { "game_settings", typeof(GameSettingsConfig) },
                { "ship_stats", typeof(ShipStatsConfig) },
                { "ufo_stats", typeof(UfoStatsConfig) }
            };
        }

        public T GetData<T>() 
            where T : class, IData
        {
            if (TryGetFromDataList(out T concreteData))
                return concreteData;

            foreach (KeyValuePair<string, Type> value in _configKeysAndTypes)
            {
                if (value.Value == typeof(T))
                {
                    T data = _remoteConfigService.GetConfig<T>(value.Key);
                    _dataList.Add(data);
                    return data;
                }
            }
            return null;
        }

        private bool TryGetFromDataList<T>(out T concreteData)
        {
            concreteData = default;
            foreach (IData data in _dataList)
            {
                if (data is T tData)
                {
                    concreteData = tData;
                    return true;
                }
            }
            return false;
        }
    }
}