using _Project.Scripts.View.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class BootstrapUIInstaller : MonoInstaller
    {
        [SerializeField] private UIRoot _uiRoot;
        
        public override void InstallBindings()
        {
            Container.Bind<UIRoot>().FromInstance(_uiRoot).AsSingle();
            Container.Bind<WindowsRepository>().AsSingle().MoveIntoAllSubContainers();
        }
    }
}