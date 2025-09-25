using _Project.Scripts.Core.GameLoopSystem;
using _Project.Scripts.Core.InputSystem;
using _Project.Scripts.Core.Services.Factories;
using _Project.Scripts.Core.Services.ObjectPools;
using _Project.Scripts.Core.Services.Repositories;
using _Project.Scripts.Core.Ship;
using _Project.Scripts.Core.ShootingSystem;
using _Project.Scripts.Data;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.View.Services;
using _Project.Scripts.ViewModel.Implementation;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class BootstrapInstaller : MonoInstaller
    {
        [Header("Data")]
        [SerializeField] private ShipStats _shipStats;
        
        [Header("CorePrefabs")]
        [SerializeField] private GameObject _shipPrefab;
        [SerializeField] private GameObject _bulletPrefab;

        [Header("UI")] 
        [SerializeField] private Transform _uiRoot;
        
        [Header("UIPrefabs")] 
        [SerializeField] private GameObject _menuWindowPrefab;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<InputHandler>().AsSingle();
            Container.BindInterfacesTo<GameLoop>().AsSingle();
            Container.BindInterfacesTo<GameLoopRegisterController>().AsSingle();
            Container
                .BindFactoryCustomInterface<Bullet, BulletFactory, IFactory<Bullet>>()
                .WithFactoryArguments(_bulletPrefab)
                .MoveIntoAllSubContainers();
            Container.BindInterfacesAndSelfTo<BulletsPool>().AsSingle().MoveIntoAllSubContainers();
            Container
                .BindFactoryCustomInterface<Ship, ShipFactory, IFactory<Ship>>()
                .WithFactoryArguments(_shipPrefab, _shipStats)
                .MoveIntoAllSubContainers();
            Container.BindInterfacesAndSelfTo<CharactersRepository>().AsSingle().MoveIntoAllSubContainers();

            Container.Bind<WindowsRepository>().AsSingle().MoveIntoAllSubContainers();
            Container.Bind<MenuViewModel>().AsSingle().MoveIntoAllSubContainers();
            Container
                .BindFactoryCustomInterface<MenuWindow, MenuWindowFactory, IFactory<MenuWindow>>()
                .WithFactoryArguments(_menuWindowPrefab, _uiRoot)
                .MoveIntoAllSubContainers();
            
            Container.BindInterfacesTo<BootstrapEntryPoint>().AsSingle();
        }
    }
}
