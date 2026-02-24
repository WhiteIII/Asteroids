using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Common.Services.InAppPurchase.Base
{
    public interface IInApp
    {
        UniTask<bool> TryBuyProductAsync(string productId);
    }
}