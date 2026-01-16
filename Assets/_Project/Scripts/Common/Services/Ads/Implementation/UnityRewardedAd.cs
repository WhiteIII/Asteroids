using _Project.Scripts.Common.Services.Ads.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace _Project.Scripts.Common.Services.Ads.Implementation
{
    public class UnityRewardedAd : IRewardedAd, IUnityAdsLoadListener, IUnityAdsShowListener, IInitializable
    {
        private const string ANDROID_AD_UNIT_ID = "Interstitial_Android";
        private const string IOS_AD_UNIT_ID = "Interstitial_iOS";
        
        private bool _adIsShowing;
        private bool _isLoading;
        private string _adUnitId;
        private bool _adIsCompleted;
        
        public void Initialize() =>
            _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? IOS_AD_UNIT_ID
                : ANDROID_AD_UNIT_ID;
        
        public async UniTask LoadAdAsync()
        {
            _isLoading = true;
            Advertisement.Load(_adUnitId, this);
            await UniTask.WaitWhile(() => _isLoading);
        }

        public async UniTask<bool> ShowAdAsync()
        {
            _adIsShowing = true;
            Advertisement.Show(_adUnitId, this);
            await UniTask.WaitWhile(() => _adIsShowing);
            return _adIsCompleted;
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) => 
            Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) => 
            Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");

        public void OnUnityAdsShowStart(string placementId)
        {
        }

        public void OnUnityAdsShowClick(string placementId)
        {
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            _adIsShowing = false;
            _adIsCompleted = showCompletionState == UnityAdsShowCompletionState.COMPLETED;
        }
    }
}