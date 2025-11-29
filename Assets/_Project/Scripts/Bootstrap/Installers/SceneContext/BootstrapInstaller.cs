using _Project.Scripts.Bootstrap.EntryPoints;
using _Project.Scripts.Gameplay.InputSystem;
using _Project.Scripts.Gameplay.Services.Repositories;
using _Project.Scripts.Gameplay.Services.Spawners;
using _Project.Scripts.Data;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.View.Services;
using _Project.Scripts.View.Services.Factories;
using _Project.Scripts.ViewModel.Implementation;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class BootstrapInstaller : MonoInstaller
    {
        [Header("Data:")] 
        [SerializeField] private GameSettingsData _gameSettingsData;
        [SerializeField] private ShipStatsData _shipStatsData;

        [Header("OnScene:")] 
        [SerializeField] private Camera _camera;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private UIRoot _uiRoot;

        [Header("PrefabRefs:")] 
        [SerializeField] private AssetReference _loadingWindowAssetReference;
        [SerializeField] private AssetReference _bestRecordWindowAssetReference;

        public override void InstallBindings()
        {
            Container.Bind<UIRoot>().FromInstance(_uiRoot).AsSingle();
            Container.Bind<WindowsRepository>().AsSingle();
            Container.Bind<WindowCreator>().AsSingle();
            Container.Bind<Camera>().FromInstance(_camera).AsSingle();
            Container.Bind<AudioSource>().FromInstance(_audioSource).AsSingle();
            Container.Bind<ShipStatsData>().FromInstance(_shipStatsData).AsSingle();
            Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<CharactersRepository>().AsSingle();
            Container
                .BindInterfacesTo<SpawnPositionHelper>()
                .AsSingle()
                .WithArguments(_gameSettingsData.SpawnOffsetOutSideCameraVision)
                .MoveIntoAllSubContainers();

            Container
                .Bind<AssetReference>()
                .WithId("LoadingWindowAssetReference")
                .FromInstance(_loadingWindowAssetReference);
            Container
                .Bind<AssetReference>()
                .WithId("BestRecordWindowAssetReference")
                .FromInstance(_bestRecordWindowAssetReference);

            Container.Bind<PlayerBestRecordViewModel>().AsSingle();
            Container.BindFactoryCustomInterface<
                    BestRecordWindow,
                    BaseWindowFactory<BestRecordWindow, PlayerBestRecordViewModel>,
                    IFactory<BestRecordWindow>>()
                .WithFactoryArguments(_bestRecordWindowAssetReference);
            Container.Bind<LoadingWindowViewModel>().AsSingle();
            Container
                .BindFactoryCustomInterface<
                    LoadingWindow,
                    BaseWindowFactory<LoadingWindow, LoadingWindowViewModel>,
                    IFactory<LoadingWindow>>()
                .WithFactoryArguments(_loadingWindowAssetReference);

            Container.BindInterfacesTo<BootstrapEntryPoint>().AsSingle();
        }
    }
}