using System.Collections.Generic;
using _Project.Scripts.Common.Services.SerializerDeserializer.Base;
using _Project.Scripts.Data.Implementation;
using _Project.Scripts.Data.Services.Repositories.Implementation;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using Zenject;

namespace _Project.Scripts.Bootstrap.EntryPoints
{
    public class RemoteConfigDefaultsInitializer : IInitializable
    {
        private readonly ISerializerDeserializer _serializerDeserializer;
        private readonly LocalDataRepository _localDataRepository;

        public RemoteConfigDefaultsInitializer(
            ISerializerDeserializer serializerDeserializer, 
            LocalDataRepository localDataRepository)
        {
            _serializerDeserializer = serializerDeserializer;
            _localDataRepository = localDataRepository;
        }

        public async void Initialize()
        {
            Dictionary<string, object> defaults = new();
            (ShipStatsConfig, UfoStatsConfig, GameSettingsConfig, AsteroidsConfig) configs = CreateDefaultConfigs();
            
            defaults.Add("ship_stats", _serializerDeserializer.Serialize(configs.Item1));
            defaults.Add("ufo_stats", _serializerDeserializer.Serialize(configs.Item2));
            defaults.Add("game_settings", _serializerDeserializer.Serialize(configs.Item3));
            defaults.Add("asteroids_stats", _serializerDeserializer.Serialize(configs.Item4));
            
            await FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults).ContinueWithOnMainThread(_ => { });
        }

        private (ShipStatsConfig, UfoStatsConfig, GameSettingsConfig, AsteroidsConfig) CreateDefaultConfigs() => 
            (new ShipStatsConfig().SetData(_localDataRepository.GetData<ShipStatsData>()), 
                new UfoStatsConfig().SetData(_localDataRepository.GetData<UfoStatsData>()), 
                new GameSettingsConfig().SetData(_localDataRepository.GetData<GameSettingsData>()), 
                new AsteroidsConfig().SetData(_localDataRepository.GetData<AsteroidsData>()));
    }
}