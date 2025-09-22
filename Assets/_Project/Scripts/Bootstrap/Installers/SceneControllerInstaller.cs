using _Project.Scripts.SceneSwitcher;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class SceneControllerInstaller : MonoInstaller
    {
        public override void InstallBindings() =>
            Container.BindInterfacesTo<SceneController>().AsSingle();
    }
}