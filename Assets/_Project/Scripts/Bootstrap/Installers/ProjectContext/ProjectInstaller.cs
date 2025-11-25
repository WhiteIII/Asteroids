using _Project.Scripts.Common;
using _Project.Scripts.SceneSwitcher;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SceneController>().AsSingle();
            Container.Bind<LocalAssetProvider>().AsSingle();
        }
    }
}