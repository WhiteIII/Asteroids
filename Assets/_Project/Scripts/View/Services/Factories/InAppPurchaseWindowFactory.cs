using System;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.ViewModel.Implementation;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.View.Services.Factories
{
    public class InAppPurchaseWindowFactory<TWindow, TViewModel> : BaseWindowFactory<TWindow, TViewModel, Action>
        where TWindow : InAppPurchaseWindow<TViewModel>
        where TViewModel : InAppPurchaseViewModel
    {
        public InAppPurchaseWindowFactory(
            TViewModel viewModel, 
            AssetReference prefabReference, 
            WindowCreator windowCreator) : base(viewModel, prefabReference, windowCreator)
        {
        }

        public override TWindow Create(Action onSuccessfulParchase)
        {
            TWindow window = CreateFromCreator();
            ViewModel.SetOnBuyAction(onSuccessfulParchase);
            return window;
        }
    }
}