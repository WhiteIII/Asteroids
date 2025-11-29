using System;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.ViewModel.Implementation;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.View.Services.Factories
{
    public class MenuWindowFactory : BaseWindowFactory<MenuWindow, MenuViewModel, Func<UniTask>>
    {
        public MenuWindowFactory(
            MenuViewModel viewModel,
            AssetReference prefabReference,
            WindowCreator windowCreator) : base(viewModel, prefabReference, windowCreator)
        {
        }

        public override MenuWindow Create(Func<UniTask> onQiutEvent)
        {
            MenuWindow window = CreateFromCreator();
            ViewModel.SetOnQuitEvent(onQiutEvent);
            return window;
        }
    }
}