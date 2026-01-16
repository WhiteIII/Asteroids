using System;
using _Project.Scripts.Common.Services.Ads.Base;
using _Project.Scripts.SceneSwitcher;
using _Project.Scripts.ViewModel.Base;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.ViewModel.Implementation
{
    public class GameOverWindowViewModel : IViewModel, IInitializable
    {
        private const int REVIVE_COUNT = 1;
        
        private readonly ISceneController _sceneController;
        private readonly IRewardedAd _rewardedAd;
        
        private OnQuitEvent _onQuitEvent;
        private OnReviveEvent _onReviveEvent;
        private int _currentReviveCount;
        
        public bool ReviveIsAcceptable => _currentReviveCount < REVIVE_COUNT;
        
        public GameOverWindowViewModel(ISceneController sceneController, IRewardedAd rewardedAd)
        {
            _sceneController = sceneController;
            _rewardedAd = rewardedAd;
        }
        
        public void Initialize() => 
            _rewardedAd.LoadAdAsync().Forget();

        public void SetOnReviveEvent(OnReviveEvent onQuitEvent) => 
            _onReviveEvent = onQuitEvent;
        
        public void SetOnQuitEvent(OnQuitEvent onQuitEvent) =>
            _onQuitEvent = onQuitEvent;

        public async UniTask Revive()
        {
            _currentReviveCount++;
            bool adCompleteStatus = await _rewardedAd.ShowAdAsync();
            if (adCompleteStatus)
                _onReviveEvent?.Invoke();
            else
                GoToMenu().Forget();
        }
        
        public async UniTask GoToMenu()
        {
            if (_onQuitEvent != null) 
                await _onQuitEvent.Invoke();
            _sceneController.GoToMenu();
        }
    }
}