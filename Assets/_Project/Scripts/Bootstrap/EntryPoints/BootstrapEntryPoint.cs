using _Project.Scripts.Common;
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
        private readonly LocalAssetProvider _localAssetProvider;
        private readonly AssetReference _loadingWindowAssetReference;

        public BootstrapEntryPoint(
            ISceneController sceneController,
            IFactory<LoadingWindow> loadingWindowFactory,
            LocalAssetProvider localAssetProvider, 
            AssetReference loadingWindowAssetReference)
        {
            _sceneController = sceneController;
            _loadingWindowFactory = loadingWindowFactory;
            _localAssetProvider = localAssetProvider;
            _loadingWindowAssetReference = loadingWindowAssetReference;
        }

        public async void Initialize()
        {
            await _localAssetProvider.LoadAsync(_loadingWindowAssetReference);
            _loadingWindowFactory.Create();
            _sceneController.GoToMenu();
        }
    }
}