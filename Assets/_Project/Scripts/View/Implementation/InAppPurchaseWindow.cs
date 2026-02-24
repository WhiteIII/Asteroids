using _Project.Scripts.ViewModel.Implementation;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.View.Implementation
{
    public abstract class InAppPurchaseWindow<T> : Window<T>
        where T : InAppPurchaseViewModel
    {
        [SerializeField] private Button _buyButton;
        [SerializeField] private PurchaseFailedScreen _purchaseFailedScreen;

        protected override void OnSetup() => 
            _buyButton.onClick.AddListener(TryBuyProduct);

        protected override void OnDestroyMethod() => 
            _buyButton.onClick.RemoveListener(TryBuyProduct);

        protected virtual async void TryBuyProduct()
        {
            _buyButton.interactable = false;
            bool purchaseStatus = await ViewModel.TryBuyProductAsync();
            if (purchaseStatus == false)
                await OpenFailedScreenAndWaitToCloseAsync();
            else
                await CloseAsync();
            _buyButton.interactable = true;
        }

        protected UniTask OpenFailedScreenAndWaitToCloseAsync() =>  
            _purchaseFailedScreen.OpenAndWaitToCloseAsync();
    }
}