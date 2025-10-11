using _Project.Scripts.Bootstrap.EntryPoints;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.View.Services;
using _Project.Scripts.ViewModel.Implementation;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class MenuInstaller : MonoInstaller
    {
        [SerializeField] private MenuWindow _menuWindowPrefab;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MenuViewModel>().AsSingle();
            Container
                .BindFactoryCustomInterface<
                    MenuWindow, 
                    BaseWindowFactory<MenuWindow, MenuViewModel>, 
                    IFactory<MenuWindow>>()
                .WithFactoryArguments(_menuWindowPrefab);
            
            Container.BindInterfacesTo<MenuEntryPoint>().AsSingle();
        }
    }
}