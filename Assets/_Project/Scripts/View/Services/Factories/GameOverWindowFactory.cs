using _Project.Scripts.View.Implementation;
using _Project.Scripts.ViewModel.Implementation;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.View.Services
{
    public class GameOverWindowFactory : BaseWindowFactory<GameOverWindow, GameOverWindowViewModel>
    {
        public GameOverWindowFactory(
            GameOverWindowViewModel viewModel,
            GameObject prefab,
            Transform parent,
            IInstantiator instantiator) : base(viewModel, prefab, parent, instantiator)
        {
        }
    }
}