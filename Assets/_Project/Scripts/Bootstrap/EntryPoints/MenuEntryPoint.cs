using System;
using _Project.Scripts.Common;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.View.Services;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.Bootstrap.EntryPoints
{
    internal class MenuEntryPoint : IInitializable, IDisposable
    {
        private readonly IFactory<MenuWindow> _menuWindowFactory;
        private readonly WindowsRepository _windowsRepository;
        private readonly AssetLoader _assetLoader;
        private readonly LocalAssetProvider _localAssetProvider;
        
        public MenuEntryPoint(
            IFactory<MenuWindow> menuWindowFactory,
            WindowsRepository windowsRepository, 
            AssetLoader assetLoader,
            LocalAssetProvider localAssetProvider)
        {
            _menuWindowFactory = menuWindowFactory;
            _windowsRepository = windowsRepository;
            _assetLoader = assetLoader;
            _localAssetProvider = localAssetProvider;
        }

        public async void Initialize()
        {
            await _assetLoader.LoadAssetsAsync();
            MenuWindow menuWindow = _menuWindowFactory.Create();
            await menuWindow.Open();
        }

        public async void Dispose()
        {
            await _windowsRepository.TryCloseAndDestroyWindow<MenuWindow>();
            _localAssetProvider.ReleaseAllAssets();
        }
    }
}