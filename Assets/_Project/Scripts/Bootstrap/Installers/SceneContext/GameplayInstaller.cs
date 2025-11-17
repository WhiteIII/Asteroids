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
using Cysharp.Threading.Tasks;
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
        
        public override void InstallBindings()
        {
            Container.Bind<GameSettingsData>().FromInstance(_gameSettingsData).AsSingle();
            Container.Bind<AsteroidsData>().FromInstance(_asteroidsData).AsSingle();
            Container.Bind<UfoStatsData>().FromInstance(_ufoStatsData).AsSingle();
            Container.BindInterfacesAndSelfTo<PointsCounter>().AsSingle();
            Container.BindInterfacesTo<GameLoop>().AsSingle();
            Container.BindInterfacesTo<GameLoopRegisterController>().AsSingle();
            Container.BindInterfacesTo<GameLoopCreator>().AsSingle();
            Container.Bind<AiActorsRepository>().AsSingle();
            Container.Bind<AiActorCreator>().AsSingle();
            Container.Bind<CharacterCreator>().AsSingle();
            Container
                .BindFactoryCustomInterface<UniTask<Ship>, ShipFactory, IFactory<UniTask<Ship>>>();
            Container
                .BindFactoryCustomInterface<UniTask<Ufo>, UfoFactory, IFactory<UniTask<Ufo>>>();
            Container.BindInterfacesAndSelfTo<SmallAsteroidsPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidsPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletsPool>().AsSingle();
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

            Container.BindInterfacesTo<GameplayEntryPoint>().AsSingle();
        } 
    }
}