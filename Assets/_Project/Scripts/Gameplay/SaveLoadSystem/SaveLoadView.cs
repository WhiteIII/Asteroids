using System;
using System.Threading;
using _Project.Scripts.Common.Services.AssetsManagement;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.View.Services;
using _Project.Scripts.ViewModel.Implementation;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.Gameplay.SaveLoadSystem
{
    public class SaveLoadView : IDisposable
    {
        private readonly AssetReference _saveDataSelectionWindowAssetReference;
        private readonly LocalAssetsProvider _assetsProvider;
        private readonly IFactory<SaveSelectionWindow> _factory;
        private readonly SaveSelectionViewModel _saveSelectionViewModel;
        private readonly WindowsRepository _repository;
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        
        private SaveSelectionWindow _currentWindow;
        
        public SaveLoadView(
            [Inject(Id = "SaveDataSelectionWindowAssetReference")] AssetReference saveDataSelectionWindowAssetReference, 
            LocalAssetsProvider assetsProvider, 
            IFactory<SaveSelectionWindow> factory, 
            SaveSelectionViewModel saveSelectionViewModel,
            WindowsRepository repository)
        {
            _saveDataSelectionWindowAssetReference = saveDataSelectionWindowAssetReference;
            _assetsProvider = assetsProvider;
            _factory = factory;
            _saveSelectionViewModel = saveSelectionViewModel;
            _repository = repository;
        }

        public void Dispose() => 
            _cancellationTokenSource.Cancel();

        public async UniTask<SaveDataType> StartSelectionAsync(DateTime localDateTime, DateTime remoteDateTime)
        {
            await CreateWindowAsync();
            _saveSelectionViewModel.SetDateTime(localDateTime, remoteDateTime);
            await _currentWindow.OpenAsync();
            SaveDataType saveDataType = await _saveSelectionViewModel.StartSelectionAsync(_cancellationTokenSource.Token);
            await _repository.TryCloseAndDestroyWindow<SaveSelectionWindow>();
            _assetsProvider.ReleaseAsset(_saveDataSelectionWindowAssetReference);
            return saveDataType;
        }

        private async UniTask CreateWindowAsync()
        {
            await _assetsProvider.LoadAsync(_saveDataSelectionWindowAssetReference);
            _currentWindow = _factory.Create();
        }
    }
}