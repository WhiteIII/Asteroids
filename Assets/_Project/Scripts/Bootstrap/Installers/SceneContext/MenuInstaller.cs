using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings() =>
            Container.BindInterfacesTo<MenuEntryPoint>().AsSingle();
    }
}