using System;
using System.Threading;
using _Project.Scripts.ViewModel.Base;
using Cysharp.Threading.Tasks;
using R3;

namespace _Project.Scripts.ViewModel.Implementation
{
    public class SaveSelectionViewModel : IViewModel
    {
        public ReadOnlyReactiveProperty<string> LocalDateTime => _localDateTime;
        public ReadOnlyReactiveProperty<string> RemoteDateTime => _remoteDateTime;
        
        private readonly ReactiveProperty<string> _localDateTime = new();
        private readonly ReactiveProperty<string> _remoteDateTime = new();
        
        private Observable<SaveDataType> _saveDataObservable;
        
        public void SetObservable(Observable<SaveDataType> saveDataObservable) =>
            _saveDataObservable = saveDataObservable;

        public void SetDateTime(DateTime localDateTime, DateTime remoteDateTime)
        {
            _localDateTime.Value = localDateTime.ToString("dd.MM.yyyy HH:mm:ss");
            _remoteDateTime.Value = remoteDateTime.ToString("dd.MM.yyyy HH:mm:ss");
        }
        
        public async UniTask<SaveDataType> StartSelectionAsync(CancellationToken cancellationToken = default) => 
            await _saveDataObservable.FirstAsync(cancellationToken);
    }
}