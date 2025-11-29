using System;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.ViewModel.Implementation;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.View.Services.Factories
{
    public class GameOverWindowFactory : BaseWindowFactory<GameOverWindow, GameOverWindowViewModel, Func<UniTask>>
    {
        public GameOverWindowFactory(
            GameOverWindowViewModel viewModel,
            AssetReference prefabReference,
            WindowCreator windowCreator) : base(viewModel, prefabReference, windowCreator)
        {
        }

        public override GameOverWindow Create(Func<UniTask> onQiutEvent)
        {
            GameOverWindow window = CreateFromCreator();
            ViewModel.SetOnQuitEvent(onQiutEvent);
            return window;
        }
    }
}