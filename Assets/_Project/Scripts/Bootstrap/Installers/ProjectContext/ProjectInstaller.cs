using _Project.Scripts.Common;
using _Project.Scripts.Gameplay.SaveLoadSystem;
using _Project.Scripts.SceneSwitcher;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SceneController>().AsSingle();
            Container.Bind<LocalAssetsProvider>().AsSingle();
            Container.Bind<SaveLoad>().AsSingle();
        }
    }
}