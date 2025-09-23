using _Project.Scripts.SceneSwitcher;

namespace _Project.Scripts.ViewModel.Implementation
{
    public class MenuViewModel : IViewModel
    {
        private readonly ISceneController _sceneController;

        public MenuViewModel(ISceneController sceneController) => 
            _sceneController = sceneController;

        public void GoToMenu() => 
            _sceneController.GoToMenu();
    }
}
