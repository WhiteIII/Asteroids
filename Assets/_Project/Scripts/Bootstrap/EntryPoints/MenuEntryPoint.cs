using System;
using _Project.Scripts.Common.Services.AssetsManagement;
using _Project.Scripts.Gameplay.SaveLoadSystem;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.View.Services;
using _Project.Scripts.ViewModel.Implementation;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
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
        private readonly AssetReference _offAdsWindowAssetReference;
        private readonly SaveLoad _saveLoad;
        private readonly IFactory<Action, OffAdsWindow> _offAdsWindowFactory;
        
        public MenuEntryPoint(
            IFactory<Func<UniTask>, MenuWindow> menuWindowFactory,
            WindowsRepository windowsRepository, 
            AssetLoader assetLoader,
            LocalAssetsProvider localAssetsProvider,
            LoadingWindowViewModel loadingWindowViewModel, 
            SaveLoad saveLoad, 
            [Inject(Id = "OffAdsWindowAssetReference")] AssetReference offAdsWindowAssetReference, 
            IFactory<Action, OffAdsWindow> offAdsWindowFactory)
        {
            _menuWindowFactory = menuWindowFactory;
            _windowsRepository = windowsRepository;
            _assetLoader = assetLoader;
            _localAssetsProvider = localAssetsProvider;
            _loadingWindowViewModel = loadingWindowViewModel;
            _saveLoad = saveLoad;
            _offAdsWindowAssetReference = offAdsWindowAssetReference;
            _offAdsWindowFactory = offAdsWindowFactory;
        }

        public async void Initialize()
        {
            bool adsIsOff = _saveLoad.Load().AdsIsOff;
            if (adsIsOff == false)
                _assetLoader.AddAsset(_offAdsWindowAssetReference);
            await _loadingWindowViewModel.StartLoadingAsync(_assetLoader.GetLoadedAsyncOperations());
            await _windowsRepository.Get<LoadingWindow>().CloseAsync();
            await _menuWindowFactory.Create(OnQuitAsync).OpenAsync();
            if (adsIsOff == false)
                await _offAdsWindowFactory.Create(OnOffAdsAction).OpenAsync();
        }

        private void OnOffAdsAction()
        {
            PlayerSaveLoadData saveLoadData = _saveLoad.Load();
            saveLoadData.AdsIsOff = true;
            _saveLoad.Save(saveLoadData);
        }
        
        private async UniTask OnQuitAsync()
        {
            await _windowsRepository.TryCloseAndDestroyWindow<OffAdsWindow>();
            await _windowsRepository.TryCloseAndDestroyWindow<MenuWindow>();
            await _windowsRepository.Get<LoadingWindow>().OpenAsync();
            _localAssetsProvider.ReleaseAllAssets();
        }
    }
}