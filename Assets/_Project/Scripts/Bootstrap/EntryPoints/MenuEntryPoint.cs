using System;
using _Project.Scripts.Common;
using _Project.Scripts.Common.Services.AssetsManagement;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.View.Services;
using _Project.Scripts.ViewModel.Implementation;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.Bootstrap.EntryPoints
{
    internal class MenuEntryPoint : IInitializable
    {
        private readonly IFactory<Func<UniTask>, MenuWindow> _menuWindowFactory;
        private readonly WindowsRepository _windowsRepository;
        private readonly AssetLoader _assetLoader;
        private readonly LocalAssetsProvider _localAssetsProvider;
        private readonly LoadingWindowViewModel _loadingWindowViewModel;

        public MenuEntryPoint(
            IFactory<Func<UniTask>, MenuWindow> menuWindowFactory,
            WindowsRepository windowsRepository, 
            AssetLoader assetLoader,
            LocalAssetsProvider localAssetsProvider,
            LoadingWindowViewModel loadingWindowViewModel)
        {
            _menuWindowFactory = menuWindowFactory;
            _windowsRepository = windowsRepository;
            _assetLoader = assetLoader;
            _localAssetsProvider = localAssetsProvider;
            _loadingWindowViewModel = loadingWindowViewModel;
        }

        public async void Initialize()
        {
            await _loadingWindowViewModel.StartLoadingAsync(_assetLoader.GetLoadedAsyncOperations());
            await _windowsRepository.Get<LoadingWindow>().Close();
            await _menuWindowFactory.Create(OnQuitAsync).Open();
        }

        private async UniTask OnQuitAsync()
        {
            await _windowsRepository.TryCloseAndDestroyWindow<MenuWindow>();
            await _windowsRepository.Get<LoadingWindow>().Open();
            _localAssetsProvider.ReleaseAllAssets();
        }
    }
}