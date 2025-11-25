using _Project.Scripts.Common;
using _Project.Scripts.ViewModel;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.View.Services
{
    public class BaseWindowFactory<TWindow, TViewModel> : PlaceholderFactory<TWindow>
        where TViewModel : IViewModel
        where TWindow :  Window<TViewModel>
    {
        protected readonly TViewModel ViewModel;

        private readonly UIRoot _uiRoot;
        private readonly IInstantiator _instantiator;
        private readonly WindowsRepository _windowsRepository;
        private readonly LocalAssetProvider _localAssetProvider;
        private readonly AssetReference _prefabReference;

        protected BaseWindowFactory(
            TViewModel viewModel,
            UIRoot uiRoot,
            IInstantiator instantiator,
            WindowsRepository windowsRepository, 
            LocalAssetProvider localAssetProvider, 
            AssetReference prefabReference)
        {
            ViewModel = viewModel;
            _uiRoot = uiRoot;
            _instantiator = instantiator;
            _windowsRepository = windowsRepository;
            _localAssetProvider = localAssetProvider;
            _prefabReference = prefabReference;
        }

        public override TWindow Create()
        {
            TWindow window = CreateWindow();
            window.Setup(ViewModel);
            return window;
        }
        
        protected TWindow CreateWindow()
        {
            TWindow window = _instantiator
                .InstantiatePrefab(_localAssetProvider.GetAsset<GameObject>(_prefabReference))
                .GetComponent<TWindow>();
            _uiRoot.AddWindow(window.transform);
            _windowsRepository.Register(window);
            return window;
        }
    }
}