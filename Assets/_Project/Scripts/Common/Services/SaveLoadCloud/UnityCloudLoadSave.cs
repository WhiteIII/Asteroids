using System;
using System.Collections.Generic;
using _Project.Scripts.Common.Services.SerializerDeserializer.Base;
using _Project.Scripts.Gameplay.SaveLoadSystem;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;

namespace _Project.Scripts.Common.Services.SaveLoadCloud
{
    public class UnityCloudLoadSave : ISaveLoadAsync
    {
        private static string SAVE_DATA_KEY = "base_save_data"; 
        
        private readonly ISerializerDeserializer _serializerDeserializer;
        private readonly ISaveLoad _localSaveLoad;

        public UnityCloudLoadSave(ISerializerDeserializer serializerDeserializer, ISaveLoad localSaveLoad)
        {
            _serializerDeserializer = serializerDeserializer;
            _localSaveLoad = localSaveLoad;
        }

        public async UniTask<PlayerSaveLoadData> LoadAsync()
        {
            if (AuthenticationService.Instance.IsAuthorized == false)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            
            Dictionary<string, Item> playerData = await CloudSaveService.Instance.Data.Player
                .LoadAsync(new HashSet<string> { SAVE_DATA_KEY });
            if (playerData.ContainsKey(SAVE_DATA_KEY) == false)
                return await InitializeFirstPlayerData();
            return _serializerDeserializer.Deserialize<PlayerSaveLoadData>(
                playerData[SAVE_DATA_KEY].Value.GetAs<string>());
        }

        public async UniTask SaveAsync(PlayerSaveLoadData data) => 
            await CloudSaveService.Instance.Data.Player.SaveAsync(new Dictionary<string, object> 
                { {SAVE_DATA_KEY, _serializerDeserializer.Serialize(data) } });

        private async UniTask<PlayerSaveLoadData> InitializeFirstPlayerData()
        {
            if (AuthenticationService.Instance.IsAuthorized == false)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            
            PlayerSaveLoadData data = _localSaveLoad.Load();
            await SaveAsync(data);
            return data;
        }  
    }
}