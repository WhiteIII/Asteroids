using _Project.Scripts.Core.Services.Spawners;
using _Project.Scripts.Data;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameSettingsData _gameSettingsData;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<SpawnersController>()
                .AsSingle()
                .WithArguments(_gameSettingsData.AsteroidsSpawnCoolDown);
            Container.BindInterfacesTo<GameplayEntryPoint>().AsSingle();
        } 
    }
}