using _Project.Scripts.Bootstrap.EntryPoints;
using _Project.Scripts.Gameplay.InputSystem;
using _Project.Scripts.Gameplay.Services.Repositories;
using _Project.Scripts.Gameplay.Services.Spawners;
using _Project.Scripts.Data;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class BootstrapInstaller : MonoInstaller
    {
        [Header("Data")]
        [SerializeField] private GameSettingsData _gameSettingsData;
        [SerializeField] private ShipStatsData _shipStatsData;

        [Header("OnScene")]
        [SerializeField] private Camera _camera;
        
        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(_camera).AsSingle();
            Container.Bind<ShipStatsData>().FromInstance(_shipStatsData).AsSingle();
            Container.BindInterfacesTo<InputHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<CharactersRepository>().AsSingle();
            Container
                .BindInterfacesTo<SpawnPositionHelper>()
                .AsSingle()
                .WithArguments(_gameSettingsData.SpawnOffsetOutSideCameraVision)
                .MoveIntoAllSubContainers();
            
            Container.BindInterfacesTo<BootstrapEntryPoint>().AsSingle();
        }
    }
}
