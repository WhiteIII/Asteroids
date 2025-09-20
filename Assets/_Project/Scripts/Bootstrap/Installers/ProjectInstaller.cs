using _Project.Scripts.Core.InputSystem;
using _Project.Scripts.Core.Services;
using _Project.Scripts.Core.Services.GameCycle;
using _Project.Scripts.Core.Ship;
using _Project.Scripts.Core.ShootingSystem;
using _Project.Scripts.Core.Stats;
using _Project.Scripts.Data;
using _Project.Scripts.SceneController;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class GameInstaller : MonoInstaller
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject _shipPrefab;
        [SerializeField] private GameObject _bulletPrefab;
    
        [Header("Data")]
        [SerializeField] private ShipDefaultStats _shipDefaultStats;
    
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ShipStats>().AsSingle();
            Container.BindInterfacesTo<InputHandler>().AsSingle();
            Container.BindInterfacesTo<GameCycleRepository>().AsSingle();
            Container.BindInterfacesTo<GameCycleRegisterController>().AsSingle();
            Container.BindInterfacesTo<ScenesController>().AsSingle();
            Container
                .BindFactoryCustomInterface<Ship, ShipFactory, IFactory<Ship>>()
                .WithFactoryArguments(_shipPrefab);
            Container
                .BindFactoryCustomInterface<string, Transform, Bullet, BulletFactory, IFactory<string, Transform, Bullet>>()
                .WithFactoryArguments(_bulletPrefab)
                .MoveIntoAllSubContainers();
            Container.BindInterfacesTo<EntryPoint>().AsSingle().WithArguments(_shipDefaultStats);
        }
    }
}