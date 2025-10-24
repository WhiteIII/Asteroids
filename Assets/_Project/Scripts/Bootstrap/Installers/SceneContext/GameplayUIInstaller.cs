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
        [SerializeField] private PlayerPointsWindow _playerPointsWindowPrefab;
        
        public override void InstallBindings()
        {
            Container.Bind<GameOverWindowViewModel>().AsSingle();
            Container.Bind<ShipStatsViewModel>().AsSingle();
            Container.Bind<PlayerPointsViewModel>().AsSingle();
            
            Container
                .BindFactoryCustomInterface<
                    PlayerPointsWindow, 
                    BaseWindowFactory<PlayerPointsWindow, PlayerPointsViewModel>,
                    IFactory<PlayerPointsWindow>>()
                .WithFactoryArguments(_playerPointsWindowPrefab);
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