using _Project.Scripts.SceneSwitcher;

namespace _Project.Scripts.ViewModel.Implementation
{
    public class GameOverWindowViewModel : IViewModel
    {
        private readonly ISceneController _sceneController;
        
        public GameOverWindowViewModel(ISceneController sceneController) => 
            _sceneController = sceneController;
        
        public void GoToMenu() => 
            _sceneController.GoToMenu();
    }
}