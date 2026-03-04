using _Project.Scripts.Common.Services.AssetsManagement;
using _Project.Scripts.Common.Services.RemoteConfig.Base;
using _Project.Scripts.SceneSwitcher;
using _Project.Scripts.View.Implementation;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using Application = UnityEngine.Application;

namespace _Project.Scripts.Bootstrap.EntryPoints
{
    public class BootstrapEntryPoint : IInitializable
    {
        private readonly ISceneController _sceneController;
        private readonly IFactory<LoadingWindow> _loadingWindowFactory;
        private readonly IFactory<BestRecordWindow> _bestRecordWindowFactory;
        private readonly LocalAssetsProvider _localAssetsProvider;
        private readonly AssetReference _loadingWindowAssetReference;
        private readonly AssetReference _bestRecordWindowAssetReference;
        private readonly IRemoteConfigService _remoteConfigService;

        public BootstrapEntryPoint(
            ISceneController sceneController,
            IFactory<LoadingWindow> loadingWindowFactory,
            LocalAssetsProvider localAssetsProvider, 
            [Inject(Id = "LoadingWindowAssetReference")]AssetReference loadingWindowAssetReference,
            [Inject(Id = "BestRecordWindowAssetReference")]AssetReference bestRecordWindowAssetReference, 
            IFactory<BestRecordWindow> bestRecordWindowFactory,
            IRemoteConfigService remoteConfigService)
        {
            _sceneController = sceneController;
            _loadingWindowFactory = loadingWindowFactory;
            _localAssetsProvider = localAssetsProvider;
            _loadingWindowAssetReference = loadingWindowAssetReference;
            _bestRecordWindowAssetReference = bestRecordWindowAssetReference;
            _bestRecordWindowFactory = bestRecordWindowFactory;
            _remoteConfigService = remoteConfigService;
        }

        public async void Initialize()
        {
            await _localAssetsProvider.LoadAsync(_loadingWindowAssetReference);
            await _localAssetsProvider.LoadAsync(_bestRecordWindowAssetReference);
            _loadingWindowFactory.Create();
            
            await UnityServices.InitializeAsync();
            if (Application.internetReachability != NetworkReachability.NotReachable)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            await Firebase.FirebaseApp.CheckAndFixDependenciesAsync();
            await _remoteConfigService.FetchAsync();
            await _remoteConfigService.ActivateAsync();
            
            _bestRecordWindowFactory.Create();
            
            _sceneController.GoToMenu();
        }
    }
}