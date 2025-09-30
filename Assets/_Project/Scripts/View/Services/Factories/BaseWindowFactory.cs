using _Project.Scripts.ViewModel;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.View.Services
{
    public abstract class BaseWindowFactory<TWindow, TViewModel> : PlaceholderFactory<TWindow>
        where TViewModel : IViewModel
        where TWindow :  Window<TViewModel>
    {
        private readonly TViewModel _viewModel;
        private readonly GameObject _prefab;
        private readonly Transform _parent;
        private readonly IInstantiator _instantiator;

        protected BaseWindowFactory(
            TViewModel viewModel,
            GameObject prefab,
            Transform parent,
            IInstantiator instantiator)
        {
            _viewModel = viewModel;
            _prefab = prefab;
            _parent = parent;
            _instantiator = instantiator;
        }

        public override TWindow Create()
        {
            TWindow window = _instantiator
                .InstantiatePrefab(_prefab, _parent)
                .GetComponent<TWindow>();
            window.Setup(_viewModel);
            return window;
        }
    }
}