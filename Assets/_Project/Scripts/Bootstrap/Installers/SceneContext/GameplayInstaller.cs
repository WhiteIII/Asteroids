using _Project.Scripts.Core.Enemies;
using _Project.Scripts.Core.GameLoopSystem;
using _Project.Scripts.Core.Services.Factories;
using _Project.Scripts.Core.Services.ObjectPools;
using _Project.Scripts.Core.Services.Spawners;
using _Project.Scripts.Core.Ship;
using _Project.Scripts.Data;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class GameplayInstaller : MonoInstaller
    {
        [Header("Data")]
        [SerializeField] private GameSettingsData _gameSettingsData;
        [SerializeField] private AsteroidsData _asteroidsData;
        [SerializeField] private ShipStats _shipStats;
        
        [Header("Prefabs")]
        [SerializeField] private GameObject _asteroidPrefab;
        [SerializeField] private GameObject _shipPrefab;
        [SerializeField] private GameObject _smallAsteroidPrefab;
        [SerializeField] private GameObject _bulletPrefab;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameLoop>().AsSingle();
            Container.BindInterfacesTo<GameLoopRegisterController>().AsSingle();
            Container.BindInterfacesTo<GameLoopCreator>().AsSingle();
            Container
                .BindFactoryCustomInterface<Bullet, CharacterFactory<Bullet>, IFactory<Bullet>>()
                .WithFactoryArguments(_bulletPrefab);
            Container
                .BindFactoryCustomInterface<Ship, ShipFactory, IFactory<Ship>>()
                .WithFactoryArguments(_shipPrefab, _shipStats);
            Container
                .BindFactoryCustomInterface<Asteroid, CharacterFactory<Asteroid>, IFactory<Asteroid>>()
                .WithId("AsteroidsFactory")
                .WithFactoryArguments(_asteroidPrefab);
            Container
                .BindFactoryCustomInterface<Asteroid, CharacterFactory<Asteroid>, IFactory<Asteroid>>()
                .WithId("SmallAsteroidsFactory")
                .WithFactoryArguments(_smallAsteroidPrefab);
            Container.Bind<AsteroidsPool>().AsSingle();
            Container.Bind<SmallAsteroidsPool>().AsSingle();
            Container.Bind<BulletsPool>().AsSingle();
            Container.Bind<AsteroidsSpawner>().AsSingle().WithArguments(_asteroidsData);
            Container
                .BindInterfacesAndSelfTo<SpawnersController>()
                .AsSingle()
                .WithArguments(_gameSettingsData.AsteroidsSpawnCoolDown);
            Container.BindInterfacesTo<GameplayEntryPoint>().AsSingle();
        } 
    }
}