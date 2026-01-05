using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Common.Services.Ads.Base
{
    public interface IInterstitialAd
    {
        UniTask LoadAdAsync();
        UniTask ShowAdAsync();
    }
}
