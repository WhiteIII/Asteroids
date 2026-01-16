using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Common.Services.Ads.Base
{
    public interface IRewardedAd
    {
        UniTask LoadAdAsync();
        UniTask<bool> ShowAdAsync();
    }
}