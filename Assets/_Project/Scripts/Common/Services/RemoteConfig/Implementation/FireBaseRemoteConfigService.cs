using System;
using _Project.Scripts.Common.Services.RemoteConfig.Base;
using _Project.Scripts.Common.Services.SerializerDeserializer.Base;
using Cysharp.Threading.Tasks;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using UnityEngine;

namespace _Project.Scripts.Common.Services.RemoteConfig.Implementation
{
    public class FireBaseRemoteConfigService : IRemoteConfigService
    {
        private readonly ISerializerDeserializer _serializerDeserializer;
        
        private FirebaseRemoteConfig Instance => FirebaseRemoteConfig.DefaultInstance;

        public FireBaseRemoteConfigService(ISerializerDeserializer serializerDeserializer) => 
            _serializerDeserializer = serializerDeserializer;

        public async UniTask FetchAsync() =>
            await Instance.FetchAsync(TimeSpan.Zero).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                    Debug.Log("Fetch cancelled");
                else if (task.IsFaulted)
                    Debug.Log("Fetch error: " + task.Exception);
                else if (task.IsCompleted)
                    Debug.Log("Fetch completed");
            });

        public async UniTask ActivateAsync() => 
            await Instance.ActivateAsync();

        public T GetConfig<T>(string key) => 
            _serializerDeserializer.Deserialize<T>(Instance.GetValue(key).StringValue);
    }
}