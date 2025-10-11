using _Project.Scripts.Bootstrap.EntryPoints;
using _Project.Scripts.Gameplay.Characters;
using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.Services.Factories;
using _Project.Scripts.Gameplay.Services.ObjectPools;
using _Project.Scripts.Gameplay.Services.Spawners;
using _Project.Scripts.Gameplay.Ship;
using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.Ai.Base;
using _Project.Scripts.Gameplay.Characters.Base;
using _Project.Scripts.Gameplay.Services.Repositories;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class GameplayInstaller : MonoInstaller
    {
        [Header("Data")]
        [SerializeField] private GameSettingsData _gameSettingsData;
        [SerializeField] private AsteroidsData _asteroidsData;
        [SerializeField] private UfoStatsData _ufoStatsData;
        
        [Header("Prefabs")]
        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private Ship _shipPrefab;
        [SerializeField] private Asteroid _smallAsteroidPrefab;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Ufo _ufoPrefab;
        
        public override void InstallBindings()
        {
            Container.Bind<GameSettingsData>().AsSingle();
            Container.BindInterfacesTo<GameLoop>().AsSingle();
            Container.BindInterfacesTo<GameLoopRegisterController>().AsSingle();
            Container.BindInterfacesTo<GameLoopCreator>().AsSingle();
            Container.Bind<AiActorsRepository>().AsSingle();
            Container.Bind<AiActorCreator>().AsSingle();
            Container.Bind<CharacterCreator>().AsSingle();
            Container
                .BindFactoryCustomInterface<Ship, ShipFactory, IFactory<Ship>>()
                .WithFactoryArguments(_shipPrefab);
            Container
                .BindFactoryCustomInterface<Ufo, UfoFactory, IFactory<Ufo>>()
                .WithFactoryArguments(_ufoPrefab, _ufoStatsData);
            Container.BindInterfacesAndSelfTo<SmallAsteroidsPool>().AsSingle().WithArguments(_smallAsteroidPrefab);
            Container.BindInterfacesAndSelfTo<AsteroidsPool>().AsSingle().WithArguments(_asteroidsData, _asteroidPrefab);
            Container.BindInterfacesAndSelfTo<BulletsPool>().AsSingle().WithArguments(_bulletPrefab);
            Container.BindInterfacesAndSelfTo<UfoPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidsSpawner>().AsSingle().WithArguments(_asteroidsData);
            Container.BindInterfacesAndSelfTo<UfoSpawner>().AsSingle();
            Container
                .BindInterfacesAndSelfTo<SpawnerController<AsteroidsSpawner>>()
                .AsSingle()
                .WithArguments(_gameSettingsData.AsteroidsSpawnCoolDown);
            Container
                .BindInterfacesAndSelfTo<SpawnerController<UfoSpawner>>()
                .AsSingle()
                .WithArguments(_gameSettingsData.UfoSpawnCoolDown);
            Container.Bind<SpawnersAndControllersRepository>().AsSingle();

            Container.BindInterfacesTo<GameplayEntryPoint>().AsSingle();
        } 
    }
}