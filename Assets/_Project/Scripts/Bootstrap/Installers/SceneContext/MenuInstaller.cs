using System;
using _Project.Scripts.Bootstrap.EntryPoints;
using _Project.Scripts.Common.Services.AssetsManagement;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.View.Services.Factories;
using _Project.Scripts.ViewModel.Implementation;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers.SceneContext
{
    internal class MenuInstaller : MonoInstaller
    {
        [Header("AssetForMenuScene:")]
        [SerializeField] private AssetReference _menuWindowPrefabReference;
        [SerializeField] private AssetReference _offAdsWindowAssetReference;

        public override void InstallBindings()
        {
            Container.Bind<AssetLoader>().AsSingle().WithArguments(new[] { _menuWindowPrefabReference });
            Container
                .Bind<AssetReference>()
                .WithId("OffAdsWindowAssetReference")
                .FromInstance(_offAdsWindowAssetReference);
            
            Container.BindInterfacesAndSelfTo<MenuViewModel>().AsSingle();
            Container
                .BindFactoryCustomInterface<
                    Func<UniTask>,
                    MenuWindow, 
                    MenuWindowFactory, 
                    IFactory<Func<UniTask>, MenuWindow>>()
                .WithFactoryArguments(_menuWindowPrefabReference);
            Container.Bind<OffAdsPurchaseViewModel>().AsSingle();
            Container.BindFactoryCustomInterface<
                    Action,
                    OffAdsWindow, 
                    InAppPurchaseWindowFactory<OffAdsWindow, OffAdsPurchaseViewModel>, 
                    IFactory<Action, OffAdsWindow>>()
                .WithFactoryArguments(_offAdsWindowAssetReference);
            
            Container.BindInterfacesTo<MenuEntryPoint>().AsSingle();
        }
    }
}