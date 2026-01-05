using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace _Project.Scripts.Bootstrap.EntryPoints
{
    public class UnityAdsInitializer : IInitializable, IUnityAdsInitializationListener
    {
        private UnityAdsInitializerData _data;
        private string _gameId;
        
        public UnityAdsInitializer(UnityAdsInitializerData data) => 
            _data = data;

        public void Initialize()
        {
            #if UNITY_IOS
                _gameId = _data.IOSGameId;
            #elif UNITY_ANDROID
                _gameId = _data.AndroidGameId;
            #elif UNITY_EDITOR
                _gameId = _data.AndroidGameId;
            #endif
            
            if (!Advertisement.isInitialized && Advertisement.isSupported)
                Advertisement.Initialize(_gameId, _data.TestMode, this);
        }

        public void OnInitializationComplete() => 
            Debug.Log("Unity Ads initialization complete.");

        public void OnInitializationFailed(UnityAdsInitializationError error, string message) => 
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public struct UnityAdsInitializerData
    {
        public string AndroidGameId;
        public string IOSGameId;
        public bool TestMode;
    }
}