using System;
using _Project.Scripts.Common.Services.InAppPurchase.Base;
using _Project.Scripts.Common.Services.InAppPurchase.Data;
using _Project.Scripts.ViewModel.Base;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.ViewModel.Implementation
{
    public abstract class InAppPurchaseViewModel : IViewModel
    {
        private readonly IInApp _inApp;
        private readonly string _productId;
        
        private Action _onBuyAction;
        
        protected InAppPurchaseViewModel(
            IInApp inApp, 
            InAppPurchaseIdList inAppPurchaseIdList, 
            InAppPurchaseProduct product)
        {
            _inApp = inApp;
            _productId = inAppPurchaseIdList.GetIdByProduct(product);
        }

        public void SetOnBuyAction(Action onBuyAction) => 
            _onBuyAction = onBuyAction;

        public async UniTask<bool> TryBuyProductAsync()
        {
            bool purchaseCompleteStatus = await _inApp.TryBuyProductAsync(_productId);
            if (purchaseCompleteStatus)
            {
                _onBuyAction?.Invoke();
                return true;
            }
            return false;
        }
    }
}