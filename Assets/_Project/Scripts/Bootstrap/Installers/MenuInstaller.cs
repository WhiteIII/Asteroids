using _Project.Scripts.View.Implementation;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class MenuInstaller : MonoInstaller
    {
        [SerializeField] private RectTransform _menuParent;
        [SerializeField] private GameObject _menuPrefab;

        public override void InstallBindings()
        {
            Container.Bind<MainMenuViewModel>().AsSingle();
            Container
                .BindFactoryCustomInterface<MainMenuWindow, MenuWindowFactory, IFactory<MainMenuWindow>>()
                .WithFactoryArguments(_menuPrefab, _menuParent);
            Container.BindInterfacesTo<MainMenuEntryPoint>().AsSingle();
        }
    }
}