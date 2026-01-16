using _Project.Scripts.View.Implementation;
using _Project.Scripts.ViewModel.Implementation;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.View.Services.Factories
{
    public class GameOverWindowFactory : BaseWindowFactory<GameOverWindow, GameOverWindowViewModel, 
        OnQuitEvent, OnReviveEvent>
    {
        public GameOverWindowFactory(
            GameOverWindowViewModel viewModel,
            AssetReference prefabReference,
            WindowCreator windowCreator) : base(viewModel, prefabReference, windowCreator)
        {
        }

        public override GameOverWindow Create(OnQuitEvent onQuitEvent, OnReviveEvent onReviveEvent)
        {
            GameOverWindow window = CreateFromCreator();
            ViewModel.SetOnQuitEvent(onQuitEvent);
            ViewModel.SetOnReviveEvent(onReviveEvent);
            return window;
        }
    }
}