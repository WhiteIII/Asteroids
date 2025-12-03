using _Project.Scripts.Common.Services.AssetsManagement;
using _Project.Scripts.SceneSwitcher;
using _Project.Scripts.View.Implementation;
using UnityEngine.AddressableAssets;
using Zenject;

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

        public BootstrapEntryPoint(
            ISceneController sceneController,
            IFactory<LoadingWindow> loadingWindowFactory,
            LocalAssetsProvider localAssetsProvider, 
            [Inject(Id = "LoadingWindowAssetReference")]AssetReference loadingWindowAssetReference,
            [Inject(Id = "BestRecordWindowAssetReference")]AssetReference bestRecordWindowAssetReference, 
            IFactory<BestRecordWindow> bestRecordWindowFactory)
        {
            _sceneController = sceneController;
            _loadingWindowFactory = loadingWindowFactory;
            _localAssetsProvider = localAssetsProvider;
            _loadingWindowAssetReference = loadingWindowAssetReference;
            _bestRecordWindowAssetReference = bestRecordWindowAssetReference;
            _bestRecordWindowFactory = bestRecordWindowFactory;
        }

        public async void Initialize()
        {
            await _localAssetsProvider.LoadAsync(_loadingWindowAssetReference);
            await _localAssetsProvider.LoadAsync(_bestRecordWindowAssetReference);
            _loadingWindowFactory.Create();
            _bestRecordWindowFactory.Create();

            await Firebase.FirebaseApp.CheckAndFixDependenciesAsync();
            
            _sceneController.GoToMenu();
        }
    }
}