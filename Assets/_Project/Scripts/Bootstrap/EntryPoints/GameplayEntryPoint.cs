using System;
using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.InputSystem;
using _Project.Scripts.Gameplay.Services.Repositories;
using _Project.Scripts.Gameplay.Ship;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.View.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap.EntryPoints
{
    public class GameplayEntryPoint : IInitializable, IDisposable
    {
        private readonly IFactory<UniTask<Ship>> _shipFactory;
        private readonly CharactersRepository _charactersRepository;
        private readonly AiActorsRepository _aiActorsRepository;
        private readonly SpawnersAndControllersRepository _spawnersAndControllersRepository;
        private readonly IFactory<UniTask<ShipStatsWindow>> _shipStatsWindowFactory;
        private readonly IFactory<UniTask<PlayerPointsWindow>> _playerPointsWindowFactory;
        private readonly IFactory<UniTask<GameOverWindow>> _gameOverWindowFactory;
        private readonly IGameLoop _gameLoop;
        private readonly WindowsRepository _windowsRepository;
        private readonly InputHandler _inputHandler;

        public GameplayEntryPoint(
            IFactory<UniTask<Ship>> shipFactory,
            CharactersRepository charactersRepository,
            AiActorsRepository aiActorsRepository, 
            SpawnersAndControllersRepository spawnersAndControllersRepository,
            IFactory<UniTask<ShipStatsWindow>> shipStatsWindowFactory, 
            WindowsRepository windowsRepository, 
            IFactory<UniTask<PlayerPointsWindow>> playerPointsWindowFactory, 
            IFactory<UniTask<GameOverWindow>> gameOverWindowFactory,
            IGameLoop gameLoop,
            InputHandler inputHandler)
        {
            _shipFactory = shipFactory;
            _charactersRepository = charactersRepository;
            _aiActorsRepository = aiActorsRepository;
            _spawnersAndControllersRepository = spawnersAndControllersRepository;
            _shipStatsWindowFactory = shipStatsWindowFactory;
            _windowsRepository = windowsRepository;
            _playerPointsWindowFactory = playerPointsWindowFactory;
            _gameOverWindowFactory = gameOverWindowFactory;
            _gameLoop = gameLoop;
            _inputHandler = inputHandler;
        }

        public async void Initialize()
        {
            await SetupShip();
            _inputHandler.Enable();
            
            ShipStatsWindow shipStatsWindow = await _shipStatsWindowFactory.Create();
            PlayerPointsWindow playerPointsWindow = await _playerPointsWindowFactory.Create();
            await shipStatsWindow.Open();
            await playerPointsWindow.Open();
        }
        
        public async void Dispose()
        {
            await _windowsRepository.TryCloseAndDestroyWindow<ShipStatsWindow>();
            await _windowsRepository.TryCloseAndDestroyWindow<PlayerPointsWindow>();
            await _windowsRepository.TryCloseAndDestroyWindow<GameOverWindow>();
            _aiActorsRepository.Clear();
            _spawnersAndControllersRepository.Clear();
            _charactersRepository.DestroyAllCharacters();
            _charactersRepository.DestroyShip();
        }

        private async UniTask SetupShip()
        { 
            await _shipFactory.Create();
            _charactersRepository.Ship.SetPosition(Vector2.zero);
            _charactersRepository.Ship.SetRotation(Quaternion.identity);
            _charactersRepository.Ship.SetOnDeadEvent(async () =>
            {
                _spawnersAndControllersRepository.StopAllSpawnerControllers();
                _inputHandler.Disable();
                _gameLoop.Pause();
                _charactersRepository.Ship.StopShip();
                await _charactersRepository.Ship.PlayDeathAnimationAsync();
                GameOverWindow gameOverWindow = await _gameOverWindowFactory.Create();
                await gameOverWindow.Open();
            });
        }
    }
}