using System;
using _Project.Scripts.Common;
using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.InputSystem;
using _Project.Scripts.Gameplay.Services.Repositories;
using _Project.Scripts.Gameplay.Ship;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.View.Services;
using _Project.Scripts.ViewModel.Implementation;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap.EntryPoints
{
    public class GameplayEntryPoint : IInitializable, IDisposable
    {
        private readonly IFactory<Ship> _shipFactory;
        private readonly CharactersRepository _charactersRepository;
        private readonly AiActorsRepository _aiActorsRepository;
        private readonly SpawnersAndControllersRepository _spawnersAndControllersRepository;
        private readonly IFactory<ShipStatsWindow> _shipStatsWindowFactory;
        private readonly IFactory<PlayerPointsWindow> _playerPointsWindowFactory;
        private readonly IFactory<GameOverWindow> _gameOverWindowFactory;
        private readonly IGameLoop _gameLoop;
        private readonly WindowsRepository _windowsRepository;
        private readonly InputHandler _inputHandler;
        private readonly AssetLoader _assetLoader;
        private readonly LocalAssetProvider _localAssetProvider;
        private readonly LoadingWindowViewModel _loadingWindowViewModel;

        public GameplayEntryPoint(
            IFactory<Ship> shipFactory,
            CharactersRepository charactersRepository,
            AiActorsRepository aiActorsRepository, 
            SpawnersAndControllersRepository spawnersAndControllersRepository,
            IFactory<ShipStatsWindow> shipStatsWindowFactory, 
            WindowsRepository windowsRepository, 
            IFactory<PlayerPointsWindow> playerPointsWindowFactory, 
            IFactory<GameOverWindow> gameOverWindowFactory,
            IGameLoop gameLoop,
            InputHandler inputHandler, 
            AssetLoader assetLoader, 
            LocalAssetProvider localAssetProvider, 
            LoadingWindowViewModel loadingWindowViewModel)
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
            _assetLoader = assetLoader;
            _localAssetProvider = localAssetProvider;
            _loadingWindowViewModel = loadingWindowViewModel;
        }

        public async void Initialize()
        {
            LoadingWindow loadingWindow = _windowsRepository.Get<LoadingWindow>();
            await loadingWindow.Open();
            await _loadingWindowViewModel.StartLoadingAsync(_assetLoader.GetLoadedAsyncOperations());
            await loadingWindow.Close();
            SetupShip();
            _inputHandler.Enable();
            await _shipStatsWindowFactory.Create().Open();
            await _playerPointsWindowFactory.Create().Open();
            _spawnersAndControllersRepository.StartAllSpawners();
        }
        
        public async void Dispose()
        {
            await UniTask.WhenAll(
                _windowsRepository.TryCloseAndDestroyWindow<ShipStatsWindow>(),
                _windowsRepository.TryCloseAndDestroyWindow<PlayerPointsWindow>(),
                _windowsRepository.TryCloseAndDestroyWindow<GameOverWindow>());
            await _windowsRepository.Get<LoadingWindow>().Open();
            _aiActorsRepository.Clear();
            _spawnersAndControllersRepository.Clear();
            _charactersRepository.ClearAllCharactersList();
            _charactersRepository.UnregisterShip();
            _localAssetProvider.ReleaseAllAssets();
        }

        private void SetupShip()
        {
            _shipFactory.Create();
            _charactersRepository.Ship.SetPosition(Vector2.zero);
            _charactersRepository.Ship.SetRotation(Quaternion.identity);
            _charactersRepository.Ship.SetOnDeadEvent(async () =>
            {
                _spawnersAndControllersRepository.StopAllSpawnerControllers();
                _inputHandler.Disable();
                _gameLoop.Pause();
                _charactersRepository.Ship.StopShip();
                await _charactersRepository.Ship.PlayDeathAnimationAsync();
                _gameOverWindowFactory.Create().Open().Forget();
            });
        }
    }
}