using _Project.Scripts.SceneController;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings() => 
            Container.BindInterfacesTo<ScenesController>().AsSingle();
    }
}