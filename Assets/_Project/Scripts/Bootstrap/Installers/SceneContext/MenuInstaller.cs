using _Project.Scripts.Bootstrap.EntryPoints;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.View.Services;
using _Project.Scripts.ViewModel.Implementation;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class MenuInstaller : MonoInstaller
    {
        private const string ID = "MenuWindow";
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MenuViewModel>().AsSingle();
            Container
                .BindFactoryCustomInterface<
                    UniTask<MenuWindow>, 
                    BaseWindowFactory<MenuWindow, MenuViewModel>, 
                    IFactory<UniTask<MenuWindow>>>()
                .WithFactoryArguments(ID);
            
            Container.BindInterfacesTo<MenuEntryPoint>().AsSingle();
        }
    }
}