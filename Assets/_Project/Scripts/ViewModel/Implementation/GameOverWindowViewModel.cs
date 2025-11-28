using System;
using _Project.Scripts.SceneSwitcher;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.ViewModel.Implementation
{
    public class GameOverWindowViewModel : IViewModel
    {
        private readonly ISceneController _sceneController;

        private Func<UniTask> _onQuitEvent; 
        
        public GameOverWindowViewModel(ISceneController sceneController) => 
            _sceneController = sceneController;
        
        public void SetOnQuitEvent(Func<UniTask> onQuitEvent) =>
            _onQuitEvent = onQuitEvent;

        public async void GoToMenu()
        {
            if (_onQuitEvent != null) 
                await _onQuitEvent.Invoke();
            _sceneController.GoToMenu();
        }
    }
}