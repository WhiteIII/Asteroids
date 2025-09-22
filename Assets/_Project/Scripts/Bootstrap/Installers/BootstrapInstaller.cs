using _Project.Scripts.Core.GameLoopSystem;
using _Project.Scripts.Core.InputSystem;
using _Project.Scripts.Core.Services.Factories;
using _Project.Scripts.Core.Ship;
using _Project.Scripts.Core.ShootingSystem;
using _Project.Scripts.Data;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private ShipStats _shipStats;
        [SerializeField] private GameObject _shipPrefab;
        [SerializeField] private GameObject _bulletPrefab;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<InputHandler>().AsSingle();
            Container.BindInterfacesTo<GameLoop>().AsSingle();
            Container.BindInterfacesTo<GameLoopRegisterController>().AsSingle();
            Container
                .BindFactoryCustomInterface<Bullet, BulletFactory, IFactory<Bullet>>()
                .WithFactoryArguments(_bulletPrefab)
                .MoveIntoAllSubContainers();
            Container.BindInterfacesTo<BulletPool>().AsSingle();
            Container
                .BindFactoryCustomInterface<Ship, ShipFactory, IFactory<Ship>>()
                .WithFactoryArguments(_shipPrefab)
                .MoveIntoAllSubContainers();
        }
    }
}