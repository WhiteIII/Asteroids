using _Project.Scripts.ViewModel;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.View.Services
{
    public class BaseWindowFactory<TWindow, TViewModel> : PlaceholderFactory<TWindow>
        where TViewModel : IViewModel
        where TWindow :  Window<TViewModel>
    {
        private readonly TViewModel _viewModel;
        private readonly GameObject _prefab;
        private readonly Transform _parent;
        private readonly IInstantiator _instantiator;
        private readonly WindowsRepository _windowsRepository;

        protected BaseWindowFactory(
            TViewModel viewModel,
            GameObject prefab,
            Transform parent,
            IInstantiator instantiator,
            WindowsRepository windowsRepository)
        {
            _viewModel = viewModel;
            _prefab = prefab;
            _parent = parent;
            _instantiator = instantiator;
            _windowsRepository = windowsRepository;
        }

        public override TWindow Create()
        {
            TWindow window = _instantiator
                .InstantiatePrefab(_prefab, _parent)
                .GetComponent<TWindow>();
            _windowsRepository.Register(window);
            window.Setup(_viewModel);
            return window;
        }
    }
}