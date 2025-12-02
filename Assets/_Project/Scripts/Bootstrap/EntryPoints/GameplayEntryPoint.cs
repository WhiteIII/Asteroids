using System;
using _Project.Scripts.Common;
using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.InputSystem;
using _Project.Scripts.Gameplay.SaveLoadSystem;
using _Project.Scripts.Gameplay.Services.Factories;
using _Project.Scripts.Gameplay.Services.Repositories;
using _Project.Scripts.Gameplay.ShipBase;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.View.Services;
using _Project.Scripts.ViewModel.Implementation;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap.EntryPoints
{
    public class GameplayEntryPoint : IInitializable
    {
        private readonly IFactory<ShipSpawnArgs, Ship> _shipFactory;
        private readonly CharactersRepository _charactersRepository;
        private readonly AiActorsRepository _aiActorsRepository;
        private readonly SpawnersAndControllersRepository _spawnersAndControllersRepository;
        private readonly IFactory<ShipStatsWindow> _shipStatsWindowFactory;
        private readonly IFactory<PlayerPointsWindow> _playerPointsWindowFactory;
        private readonly IFactory<Func<UniTask>, GameOverWindow> _gameOverWindowFactory;
        private readonly IGameLoop _gameLoop;
        private readonly WindowsRepository _windowsRepository;
        private readonly InputHandler _inputHandler;
        private readonly AssetLoader _assetLoader;
        private readonly LocalAssetsProvider _localAssetsProvider;
        private readonly LoadingWindowViewModel _loadingWindowViewModel;
        private readonly SaveLoad _saveLoad;
        private readonly IPointsCounter _pointsCounter;

        public GameplayEntryPoint(
            IFactory<ShipSpawnArgs, Ship> shipFactory,
            CharactersRepository charactersRepository,
            AiActorsRepository aiActorsRepository, 
            SpawnersAndControllersRepository spawnersAndControllersRepository,
            IFactory<ShipStatsWindow> shipStatsWindowFactory, 
            WindowsRepository windowsRepository, 
            IFactory<PlayerPointsWindow> playerPointsWindowFactory, 
            IFactory<Func<UniTask>, GameOverWindow> gameOverWindowFactory,
            IGameLoop gameLoop,
            InputHandler inputHandler, 
            AssetLoader assetLoader, 
            LocalAssetsProvider localAssetsProvider, 
            LoadingWindowViewModel loadingWindowViewModel,
            IPointsCounter pointsCounter, 
            SaveLoad saveLoad)
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
            _localAssetsProvider = localAssetsProvider;
            _loadingWindowViewModel = loadingWindowViewModel;
            _pointsCounter = pointsCounter;
            _saveLoad = saveLoad;
        }

        public async void Initialize()
        {
            await _loadingWindowViewModel.StartLoadingAsync(_assetLoader.GetLoadedAsyncOperations());
            await _windowsRepository.Get<LoadingWindow>().Close();
            CreateShip();
            _inputHandler.Enable();
            await _shipStatsWindowFactory.Create().Open();
            await _playerPointsWindowFactory.Create().Open();
            _spawnersAndControllersRepository.StartAllSpawners();
        }

        private async UniTask OnQuitEvent()
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
            _localAssetsProvider.ReleaseAllAssets();
        }
        
        private void CreateShip() =>
            _shipFactory.Create(new ShipSpawnArgs
            {
                Position = Vector2.zero,
                Rotation = Quaternion.identity,
                OnDead = async () =>
                {
                    TryWriteBestRecord();
                    _spawnersAndControllersRepository.StopAllSpawnerControllers();
                    _inputHandler.Disable();
                    _gameLoop.Pause();
                    _charactersRepository.Ship.StopShip();
                    await _charactersRepository.Ship.PlayDeathAnimationAsync();
                    _gameOverWindowFactory.Create(OnQuitEvent).Open().Forget();
                }
            });

        private void TryWriteBestRecord()
        {
            PlayerSaveLoadData saveLoadData = _saveLoad.Load();
            if (saveLoadData.BestRecord < _pointsCounter.Points.CurrentValue)
            {
                saveLoadData.BestRecord = _pointsCounter.Points.CurrentValue;
                _saveLoad.Save(saveLoadData);
            }
        }
    }
}