using System;
using _Project.Scripts.Bootstrap.EntryPoints;
using _Project.Scripts.Common;
using _Project.Scripts.Common.Services.AssetsManagement;
using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.Ai.Base;
using _Project.Scripts.Gameplay.Characters.Base;
using _Project.Scripts.Gameplay.Characters.Implementation;
using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.GameProgress;
using _Project.Scripts.Gameplay.Services.AnalyticEmplementation;
using _Project.Scripts.Gameplay.Services.Factories;
using _Project.Scripts.Gameplay.Services.ObjectPools;
using _Project.Scripts.Gameplay.Services.Repositories;
using _Project.Scripts.Gameplay.Services.Spawners;
using _Project.Scripts.Gameplay.ShipBase;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.View.Services.Factories;
using _Project.Scripts.ViewModel.Implementation;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers.SceneContext
{
    internal class GameplayInstaller : MonoInstaller
    {
        [Header("Data")]
        [SerializeField] private GameSettingsData _gameSettingsData;
        [SerializeField] private AsteroidsData _asteroidsData;
        [SerializeField] private UfoStatsData _ufoStatsData;
        
        [Header("AssetForGameplayScene:")]
        [SerializeField] private AssetReference _asteroidPrefabReference;
        [SerializeField] private AssetReference _shipPrefabReference;
        [SerializeField] private AssetReference _smallAsteroidPrefabReference;
        [SerializeField] private AssetReference _bulletPrefabReference;
        [SerializeField] private AssetReference _ufoPrefabReference;
        [SerializeField] private AssetReference _playerPointsWindowPrefabReference;
        [SerializeField] private AssetReference _shipStatsWindowPrefabReference;
        [SerializeField] private AssetReference _gameOverWindowPrefabReference;
        
        public override void InstallBindings()
        {
            Container.Bind<GameSettingsData>().FromInstance(_gameSettingsData).AsSingle();
            Container.Bind<AsteroidsData>().FromInstance(_asteroidsData).AsSingle();
            Container.Bind<UfoStatsData>().FromInstance(_ufoStatsData).AsSingle();
            Container.Bind<AssetLoader>().AsSingle().WithArguments(new[]
            {
                _asteroidPrefabReference, 
                _shipPrefabReference,  
                _smallAsteroidPrefabReference, 
                _bulletPrefabReference, 
                _ufoPrefabReference,
                _playerPointsWindowPrefabReference,
                _shipStatsWindowPrefabReference,
                _gameOverWindowPrefabReference
            });
            Container.BindInterfacesAndSelfTo<WeaponsUsageCounter>().AsSingle();
            Container.BindInterfacesAndSelfTo<PointsAndKillsAndKillsCounterCounter>().AsSingle();
            Container.Bind<StartGameAndEndGameEventSender>().AsSingle();
            Container.BindInterfacesTo<GameLoop>().AsSingle();
            Container.BindInterfacesTo<GameLoopRegisterController>().AsSingle();
            Container.BindInterfacesTo<GameLoopCreator>().AsSingle();
            Container.Bind<AiActorsRepository>().AsSingle();
            Container.Bind<AiActorCreator>().AsSingle();
            Container.Bind<CharacterCreator>().AsSingle();
            Container
                .BindFactoryCustomInterface<ShipSpawnArgs, Ship, ShipFactory, IFactory<ShipSpawnArgs, Ship>>()
                .WithFactoryArguments(_shipPrefabReference);
            Container
                .BindFactoryCustomInterface<Ufo, UfoFactory, IFactory<Ufo>>()
                .WithFactoryArguments(_ufoPrefabReference);
            Container.BindInterfacesAndSelfTo<SmallAsteroidsPool>().AsSingle().WithArguments(_smallAsteroidPrefabReference);
            Container.BindInterfacesAndSelfTo<AsteroidsPool>().AsSingle().WithArguments(_asteroidPrefabReference);
            Container.BindInterfacesAndSelfTo<BulletsPool>().AsSingle().WithArguments(_bulletPrefabReference);
            Container.BindInterfacesAndSelfTo<UfoPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidsSpawner>().AsSingle();
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
            
            Container.Bind<GameOverWindowViewModel>().AsSingle();
            Container.Bind<ShipStatsViewModel>().AsSingle();
            Container.Bind<PlayerPointsViewModel>().AsSingle();
            
            Container
                .BindFactoryCustomInterface<
                    PlayerPointsWindow, 
                    BaseWindowFactory<PlayerPointsWindow, PlayerPointsViewModel>,
                    IFactory<PlayerPointsWindow>>()
                .WithFactoryArguments(_playerPointsWindowPrefabReference);
            Container
                .BindFactoryCustomInterface<
                    Func<UniTask>,
                    GameOverWindow, 
                    GameOverWindowFactory,
                    IFactory<Func<UniTask>, GameOverWindow>>()
                .WithFactoryArguments(_gameOverWindowPrefabReference);
            Container
                .BindFactoryCustomInterface<
                    ShipStatsWindow, 
                    ShipStatsWindowFactory, 
                    IFactory<ShipStatsWindow>>()
                .WithFactoryArguments(_shipStatsWindowPrefabReference);

            Container.BindInterfacesTo<GameplayEntryPoint>().AsSingle();
        } 
    }
}