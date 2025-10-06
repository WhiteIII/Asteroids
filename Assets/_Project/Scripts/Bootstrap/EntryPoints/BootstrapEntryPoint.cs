using _Project.Scripts.SceneSwitcher;
using Zenject;

namespace _Project.Scripts.Bootstrap.EntryPoints
{
    public class BootstrapEntryPoint : IInitializable
    {
        private readonly ISceneController _sceneController;

        public BootstrapEntryPoint(ISceneController sceneController) => 
            _sceneController = sceneController;

        public void Initialize() =>
            _sceneController.GoToMenu();
    }
}