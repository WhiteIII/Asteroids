using _Project.Scripts.SceneController;
using _Project.Scripts.View.Implementation;
using Zenject;

namespace _Project.Scripts.Bootstrap
{
    internal class MainMenuEntryPoint : IInitializable
    {
        private readonly MainMenuViewModel _mainMenuViewModel;
        private readonly IFactory<MainMenuWindow> _menuWindowFactory;
        private readonly IScenesController _scenesController;
        
        public MainMenuEntryPoint(
            MainMenuViewModel mainMenuViewModel,
            IFactory<MainMenuWindow> menuWindowFactory)
        {
            _mainMenuViewModel = mainMenuViewModel;
            _menuWindowFactory = menuWindowFactory;
        }

        public void Initialize()
        {
            MainMenuWindow mainMenuWindow = _menuWindowFactory.Create();
            _mainMenuViewModel.Initialize(_scenesController.GoToMenu);
            mainMenuWindow.Setup(_mainMenuViewModel);
        }
    }
}