using _Project.Scripts.Gameplay.SaveLoadSystem;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Common.Services.Ads.Base
{
    public class InterstitialAdDecorator : IInterstitialAd
    {
        private readonly IInterstitialAd _interstitialAd;
        private readonly ISaveLoadAsync _saveLoad;

        public InterstitialAdDecorator(IInterstitialAd interstitialAd, ISaveLoadAsync saveLoad)
        {
            _interstitialAd = interstitialAd;
            _saveLoad = saveLoad;
        }

        public UniTask LoadAdAsync() => 
            _interstitialAd.LoadAdAsync();

        public async UniTask ShowAdAsync()
        {
            PlayerSaveLoadData playerSaveLoadData = await _saveLoad.LoadAsync(); 
            if (playerSaveLoadData.AdsIsOff == false)
                await _interstitialAd.ShowAdAsync(); 
        }
    }
}