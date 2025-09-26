using _Project.Scripts.View.Implementation;
using _Project.Scripts.View.Services;
using _Project.Scripts.ViewModel.Implementation;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class UIInstaller : MonoInstaller
    {
        [Header("OnScene")]
        [SerializeField] private Transform _uiRoot;
        
        [Header("Prefabs")] 
        [SerializeField] private GameObject _menuWindowPrefab;

        public override void InstallBindings()
        {
            Container.Bind<WindowsRepository>().AsSingle().MoveIntoAllSubContainers();
            Container.Bind<MenuViewModel>().AsSingle().MoveIntoAllSubContainers();
            Container
                .BindFactoryCustomInterface<MenuWindow, MenuWindowFactory, IFactory<MenuWindow>>()
                .WithFactoryArguments(_menuWindowPrefab, _uiRoot)
                .MoveIntoAllSubContainers();
        }
    }
}