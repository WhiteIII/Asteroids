using _Project.Scripts.Core.Services.ObjectPools;
using _Project.Scripts.Core.Services.Spawners;
using _Project.Scripts.Data;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameSettingsData _gameSettingsData;
        [SerializeField] private AsteroidsData _asteroidsData;
        
        public override void InstallBindings()
        {
            Container.Bind<AsteroidsPool>().AsSingle();
            Container.Bind<SmallAsteroidsPool>().AsSingle();
            Container.Bind<AsteroidsSpawner>().AsSingle().WithArguments(_asteroidsData);
            Container
                .BindInterfacesAndSelfTo<SpawnersController>()
                .AsSingle()
                .WithArguments(_gameSettingsData.AsteroidsSpawnCoolDown);
            Container.BindInterfacesTo<GameplayEntryPoint>().AsSingle();
        } 
    }
}