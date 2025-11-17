using _Project.Scripts.View.Implementation;
using _Project.Scripts.View.Services;
using _Project.Scripts.ViewModel.Implementation;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap.Installers
{
    internal class GameplayUIInstaller : MonoInstaller
    {
        private const string PLAYER_POINTS_WINDOW_ID = "PlayerPointsView";
        private const string SHIP_STATS_WINDOW_ID = "ShipStatsWindow";
        private const string GAMEOVER_WINDOW_ID = "GameOverWindow";
        
        public override void InstallBindings()
        {
            Container.Bind<GameOverWindowViewModel>().AsSingle();
            Container.Bind<ShipStatsViewModel>().AsSingle();
            Container.Bind<PlayerPointsViewModel>().AsSingle();
            
            Container
                .BindFactoryCustomInterface<
                    UniTask<PlayerPointsWindow>, 
                    BaseWindowFactory<PlayerPointsWindow, PlayerPointsViewModel>,
                    IFactory<UniTask<PlayerPointsWindow>>>()
                .WithFactoryArguments(PLAYER_POINTS_WINDOW_ID);
            Container
                .BindFactoryCustomInterface<
                    UniTask<GameOverWindow>, 
                    BaseWindowFactory<GameOverWindow, GameOverWindowViewModel>, 
                    IFactory<UniTask<GameOverWindow>>>()
                .WithFactoryArguments(GAMEOVER_WINDOW_ID);
            Container
                .BindFactoryCustomInterface<
                    UniTask<ShipStatsWindow>, 
                    ShipStatsWindowFactory, 
                    IFactory<UniTask<ShipStatsWindow>>>()
                .WithFactoryArguments(SHIP_STATS_WINDOW_ID);
        }
    }
}