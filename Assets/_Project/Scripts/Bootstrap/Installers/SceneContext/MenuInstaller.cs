using _Project.Scripts.Bootstrap.EntryPoints;
using _Project.Scripts.Common;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.View.Services;
using _Project.Scripts.ViewModel.Implementation;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class MenuInstaller : MonoInstaller
    {
        [Header("AssetForMenuScene:")]
        [SerializeField] private AssetReference _menuWindowPrefabReference;
        
        public override void InstallBindings()
        {
            Container.Bind<AssetLoader>().AsSingle().WithArguments(new[] { _menuWindowPrefabReference });
            Container.BindInterfacesAndSelfTo<MenuViewModel>().AsSingle();
            Container
                .BindFactoryCustomInterface<
                    MenuWindow, 
                    BaseWindowFactory<MenuWindow, MenuViewModel>, 
                    IFactory<MenuWindow>>()
                .WithFactoryArguments(_menuWindowPrefabReference);
            
            Container.BindInterfacesTo<MenuEntryPoint>().AsSingle();
        }
    }
}