using _Project.Scripts.View.Implementation;
using _Project.Scripts.View.Services;
using _Project.Scripts.ViewModel.Implementation;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class GameplayUIInstaller : MonoInstaller
    {
        [SerializeField] private GameOverWindow _gameOverWindowPrefab;
        [SerializeField] private ShipStatsWindow _shipStatsWindowPrefab;
        
        public override void InstallBindings()
        {
            Container.Bind<GameOverWindowViewModel>().AsSingle();
            Container.Bind<ShipStatsViewModel>().AsSingle();
            Container
                .BindFactoryCustomInterface<
                    GameOverWindow, 
                    BaseWindowFactory<GameOverWindow, GameOverWindowViewModel>, 
                    IFactory<GameOverWindow>>()
                .WithFactoryArguments(_gameOverWindowPrefab);
            Container
                .BindFactoryCustomInterface<
                    ShipStatsWindow, 
                    ShipStatsWindowFactory, 
                    IFactory<ShipStatsWindow>>()
                .WithFactoryArguments(_shipStatsWindowPrefab);
        }
    }
}