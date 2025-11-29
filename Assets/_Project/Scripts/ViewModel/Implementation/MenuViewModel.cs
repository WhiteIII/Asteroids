using System;
using _Project.Scripts.SceneSwitcher;
using _Project.Scripts.ViewModel.Base;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.ViewModel.Implementation
{
    public class MenuViewModel : IViewModel
    {
        private readonly ISceneController _sceneController;
        private readonly PlayerBestRecordViewModel _playerBestRecordViewModel;
        
        private Func<UniTask> _onQuitEvent;

        public bool WhetherToShowBestRecord
        {
            get
            {
                if (_playerBestRecordViewModel.BestRecord > 0)
                    return true;
                return false;
            }
        }

        public MenuViewModel(ISceneController sceneController, PlayerBestRecordViewModel playerBestRecordViewModel)
        {
            _sceneController = sceneController;
            _playerBestRecordViewModel = playerBestRecordViewModel;
        }

        public void SetOnQuitEvent(Func<UniTask> onQuitEvent) =>
            _onQuitEvent =  onQuitEvent;

        public async void GoToGameplay()
        {
            if (_onQuitEvent != null)
                await _onQuitEvent.Invoke();
            _sceneController.GoToGameplay();
        }
    }
}