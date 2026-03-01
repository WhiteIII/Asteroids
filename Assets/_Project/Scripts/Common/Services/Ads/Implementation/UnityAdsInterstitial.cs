using _Project.Scripts.Common.Services.Ads.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Advertisements;

namespace _Project.Scripts.Common.Services.Ads.Implementation
{
    public class UnityAdsInterstitial : IInterstitialAd, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private const string ANDROID_AD_UNIT_ID = "Interstitial_Android";
        private const string IOS_AD_UNIT_ID = "Interstitial_iOS";
        
        private bool _adIsShowing;
        private bool _isLoading;
        private string _adUnitId;
        
        public UnityAdsInterstitial() =>
            _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? IOS_AD_UNIT_ID
                : ANDROID_AD_UNIT_ID;

        public async UniTask LoadAdAsync()
        {
            _isLoading = true;
            Advertisement.Load(_adUnitId, this);
            await UniTask.WaitWhile(() => _isLoading);
        }
        
        public async UniTask ShowAdAsync()
        {
            _adIsShowing = true;
            Advertisement.Show(_adUnitId, this);
            await UniTask.WaitWhile(() => _adIsShowing);
        }

        public void OnUnityAdsAdLoaded(string placementId) => 
            _isLoading = false;

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

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState) => 
            _adIsShowing = false;
    }
}