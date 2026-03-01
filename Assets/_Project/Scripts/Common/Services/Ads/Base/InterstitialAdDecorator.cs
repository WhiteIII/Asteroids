using _Project.Scripts.Gameplay.SaveLoadSystem;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Common.Services.Ads.Base
{
    public class InterstitialAdDecorator : IInterstitialAd
    {
        private readonly IInterstitialAd _interstitialAd;
        private readonly ISaveLoad _saveLoad;

        public InterstitialAdDecorator(IInterstitialAd interstitialAd, ISaveLoad saveLoad)
        {
            _interstitialAd = interstitialAd;
            _saveLoad = saveLoad;
        }

        public UniTask LoadAdAsync() => 
            _interstitialAd.LoadAdAsync();

        public async UniTask ShowAdAsync()
        {
            if (_saveLoad.Load().AdsIsOff == false)
                await _interstitialAd.ShowAdAsync(); 
        }
    }
}