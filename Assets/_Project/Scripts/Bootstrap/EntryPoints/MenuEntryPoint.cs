using System;
using _Project.Scripts.Common;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.View.Services;
using _Project.Scripts.ViewModel.Implementation;
using Zenject;

namespace _Project.Scripts.Bootstrap.EntryPoints
{
    internal class MenuEntryPoint : IInitializable, IDisposable
    {
        private readonly IFactory<MenuWindow> _menuWindowFactory;
        private readonly WindowsRepository _windowsRepository;
        private readonly AssetLoader _assetLoader;
        private readonly LocalAssetProvider _localAssetProvider;
        private readonly LoadingWindowViewModel _loadingWindowViewModel;
        
        public MenuEntryPoint(
            IFactory<MenuWindow> menuWindowFactory,
            WindowsRepository windowsRepository, 
            AssetLoader assetLoader,
            LocalAssetProvider localAssetProvider,
            LoadingWindowViewModel loadingWindowViewModel)
        {
            _menuWindowFactory = menuWindowFactory;
            _windowsRepository = windowsRepository;
            _assetLoader = assetLoader;
            _localAssetProvider = localAssetProvider;
            _loadingWindowViewModel = loadingWindowViewModel;
        }

        public async void Initialize()
        {
            LoadingWindow loadingWindow = _windowsRepository.Get<LoadingWindow>();
            await loadingWindow.Open();
            await _loadingWindowViewModel.StartLoadingAsync(_assetLoader.GetLoadedAsyncOperations());
            await loadingWindow.Close();
            await _menuWindowFactory.Create().Open();
        }

        public async void Dispose()
        {
            await _windowsRepository.TryCloseAndDestroyWindow<MenuWindow>();
            _localAssetProvider.ReleaseAllAssets();
        }
    }
}