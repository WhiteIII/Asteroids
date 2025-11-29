using _Project.Scripts.Common;
using _Project.Scripts.ViewModel;
using _Project.Scripts.ViewModel.Base;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.View.Services.Factories
{
    public class WindowCreator
    {
        private readonly UIRoot _uiRoot;
        private readonly IInstantiator _instantiator;
        private readonly WindowsRepository _windowsRepository;
        private readonly LocalAssetsProvider _localAssetsProvider;

        public WindowCreator(
            UIRoot uiRoot,
            IInstantiator instantiator,
            WindowsRepository windowsRepository,
            LocalAssetsProvider localAssetsProvider)
        {
            _uiRoot = uiRoot;
            _instantiator = instantiator;
            _windowsRepository = windowsRepository;
            _localAssetsProvider = localAssetsProvider;
        }

        public TWindow Create<TWindow, TViewModel>(TViewModel viewModel, AssetReference prefabReference)
            where TViewModel : IViewModel
            where TWindow : Window<TViewModel>
        {
            TWindow window = _instantiator
                .InstantiatePrefab(_localAssetsProvider.GetAsset<GameObject>(prefabReference))
                .GetComponent<TWindow>();
            _uiRoot.AddWindow(window.transform);
            _windowsRepository.Register(window);
            window.Setup(viewModel);
            
            return window;
        }        
    }
}