using _Project.Scripts.Core.Enemies.Asteroids;
using _Project.Scripts.Core.GameLoopSystem;
using _Project.Scripts.Core.InputSystem;
using _Project.Scripts.Core.Services.Factories;
using _Project.Scripts.Core.Services.ObjectPools;
using _Project.Scripts.Core.Services.Repositories;
using _Project.Scripts.Core.Services.Spawners;
using _Project.Scripts.Core.Ship;
using _Project.Scripts.Core.ShootingSystem;
using _Project.Scripts.Data;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class BootstrapInstaller : MonoInstaller
    {
        [Header("Data")]
        [SerializeField] private GameSettingsData _gameSettingsData;
        [SerializeField] private ShipStats _shipStats;
        [SerializeField] private AsteroidsData _asteroidsData;
        
        [Header("CorePrefabs")]
        [SerializeField] private GameObject _shipPrefab;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private GameObject _asteroidPrefab;
        [SerializeField] private GameObject _smallAsteroidPrefab;
        
        [Header("OnScene")]
        [SerializeField] private Camera _camera;
        
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
            Container
                .BindFactoryCustomInterface<Asteroid, AsteroidsFactory, IFactory<Asteroid>>()
                .WithId("AsteroidsFactory")
                .WithFactoryArguments(_asteroidPrefab);
            Container
                .BindFactoryCustomInterface<Asteroid, AsteroidsFactory, IFactory<Asteroid>>()
                .WithId("SmallAsteroidsFactory")
                .WithFactoryArguments(_smallAsteroidPrefab);
            Container.BindInterfacesAndSelfTo<CharactersRepository>().AsSingle().MoveIntoAllSubContainers();
            Container
                .Bind<SpawnPositionHelper>()
                .AsSingle()
                .WithArguments(_camera, _gameSettingsData.SpawnOffsetOutSideCameraVision);
            Container.Bind<AsteroidsPool>().WithId("AsteroidsPool").AsSingle();
            //Container.BindInterfacesTo<AsteroidsSpawner>().AsSingle();
            
            Container.BindInterfacesTo<BootstrapEntryPoint>().AsSingle();
        }
    }
}
