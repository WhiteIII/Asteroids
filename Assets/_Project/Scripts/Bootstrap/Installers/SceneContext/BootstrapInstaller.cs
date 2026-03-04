using _Project.Scripts.Bootstrap.EntryPoints;
using _Project.Scripts.Common.Services.InAppPurchase.Data;
using _Project.Scripts.Common.Services.InAppPurchase.Implementation;
using _Project.Scripts.Common.Services.RemoteConfig.Implementation;
using _Project.Scripts.Common.Services.SaveLoadCloud;
using _Project.Scripts.Data.Services.Repositories.Implementation;
using _Project.Scripts.Gameplay.SaveLoadSystem;
using _Project.Scripts.Gameplay.Services.Repositories;
using _Project.Scripts.Gameplay.Services.Spawners;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.View.Services;
using _Project.Scripts.View.Services.Factories;
using _Project.Scripts.ViewModel.Implementation;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers.SceneContext
{
    internal class BootstrapInstaller : MonoInstaller
    {
        [Header("Data")]
        [SerializeField] private LocalDataRepository _localDataRepository;
        [SerializeField] private InAppPurchaseIdList _inAppPurchaseIdList;

        [Header("OnScene:")] 
        [SerializeField] private Camera _camera;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private UIRoot _uiRoot;

        [Header("PrefabRefs:")] 
        [SerializeField] private AssetReference _loadingWindowAssetReference;
        [SerializeField] private AssetReference _bestRecordWindowAssetReference;
        [SerializeField] private AssetReference _saveDataSelectionWindowAssetReference;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SaveLoad>().AsSingle();
            Container.BindInterfacesTo<UnityCloudLoadSave>().WhenInjectedInto<SaveLoadDecorator>();
            Container.BindInterfacesTo<SaveLoadDecorator>().AsSingle();
            Container.Bind<InAppPurchaseIdList>().FromInstance(_inAppPurchaseIdList).AsSingle();
            Container.BindInterfacesTo<UnityInApp>().AsSingle();
            Container.BindInterfacesTo<FireBaseRemoteConfigService>().AsSingle();
            Container.BindInterfacesTo<RemoteConfigDefaultsInitializer>().AsSingle().WithArguments(_localDataRepository);
            Container.BindInterfacesTo<RemoteConfigRepository>().AsSingle();
            Container.Bind<UIRoot>().FromInstance(_uiRoot).AsSingle();
            Container.Bind<WindowsRepository>().AsSingle();
            Container.Bind<WindowCreator>().AsSingle();
            Container.Bind<Camera>().FromInstance(_camera).AsSingle();
            Container.Bind<AudioSource>().FromInstance(_audioSource).AsSingle();
            Container.BindInterfacesAndSelfTo<CharactersRepository>().AsSingle();
            Container.BindInterfacesTo<SpawnPositionHelper>().AsSingle().MoveIntoAllSubContainers();

            Container.Bind<AssetReference>().WithId("LoadingWindowAssetReference")
                .FromInstance(_loadingWindowAssetReference);
            Container.Bind<AssetReference>().WithId("BestRecordWindowAssetReference")
                .FromInstance(_bestRecordWindowAssetReference);
            Container.Bind<AssetReference>().WithId("SaveDataSelectionWindowAssetReference")
                .FromInstance(_saveDataSelectionWindowAssetReference);
            
            Container.Bind<PlayerBestRecordViewModel>().AsSingle();
            Container.BindFactoryCustomInterface<
                    BestRecordWindow,
                    BaseWindowFactory<BestRecordWindow, PlayerBestRecordViewModel>,
                    IFactory<BestRecordWindow>>()
                .WithFactoryArguments(_bestRecordWindowAssetReference);
            Container.Bind<LoadingWindowViewModel>().AsSingle();
            Container.BindFactoryCustomInterface<
                    LoadingWindow,
                    BaseWindowFactory<LoadingWindow, LoadingWindowViewModel>,
                    IFactory<LoadingWindow>>()
                .WithFactoryArguments(_loadingWindowAssetReference);
            Container.Bind<SaveSelectionViewModel>().AsSingle();
            Container.BindFactoryCustomInterface<
                    SaveSelectionWindow,
                    BaseWindowFactory<SaveSelectionWindow, SaveSelectionViewModel>,
                    IFactory<SaveSelectionWindow>>()
                .WithFactoryArguments(_saveDataSelectionWindowAssetReference);;
            Container.BindInterfacesAndSelfTo<SaveLoadView>().AsSingle();
            
            Container.BindInterfacesTo<BootstrapEntryPoint>().AsSingle();
        }
    }
}