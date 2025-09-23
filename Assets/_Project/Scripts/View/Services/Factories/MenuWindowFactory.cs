using _Project.Scripts.View.Implementation;
using _Project.Scripts.ViewModel.Implementation;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.View.Services
{
    public class MenuWindowFactory : PlaceholderFactory<MenuWindow>
    {
        private readonly MenuViewModel _viewModel;
        private readonly GameObject _prefab;
        private readonly Transform _parent;
        private readonly IInstantiator _instantiator;

        public MenuWindowFactory(
            MenuViewModel viewModel,
            GameObject prefab,
            Transform parent,
            IInstantiator instantiator)
        {
            _viewModel = viewModel;
            _prefab = prefab;
            _parent = parent;
            _instantiator = instantiator;
        }

        public override MenuWindow Create()
        {
            MenuWindow window = _instantiator
                .InstantiatePrefab(_prefab, _parent)
                .GetComponent<MenuWindow>();
            window.Setup(_viewModel);
            return window;
        }
    }
}
