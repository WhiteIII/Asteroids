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
            Container.BindInterfacesAndSelfTo<CharactersRepository>().AsSingle().MoveIntoAllSubContainers();
            Container
                .BindInterfacesTo<SpawnPositionHelper>()
                .AsSingle()
                .WithArguments(_camera, _gameSettingsData.SpawnOffsetOutSideCameraVision)
                .MoveIntoAllSubContainers();
            
            Container.BindInterfacesTo<BootstrapEntryPoint>().AsSingle();
        }
    }
}
