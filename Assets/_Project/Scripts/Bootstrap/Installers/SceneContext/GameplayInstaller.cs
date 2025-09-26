using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings() => 
            Container.BindInterfacesTo<GameplayEntryPoint>().AsSingle();
    }
}