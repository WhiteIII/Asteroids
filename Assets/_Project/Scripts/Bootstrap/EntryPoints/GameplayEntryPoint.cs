using _Project.Scripts.Common.Services.Ads.Base;
using _Project.Scripts.Common.Services.AssetsManagement;
using _Project.Scripts.Data.Implementation;
using _Project.Scripts.Data.Services.Repositories.Base;
using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.InputSystem;
using _Project.Scripts.Gameplay.SaveLoadSystem;
using _Project.Scripts.Gameplay.Services.AnalyticEmplementation;
using _Project.Scripts.Gameplay.Services.Factories;
using _Project.Scripts.Gameplay.Services.Repositories;
using _Project.Scripts.Gameplay.Services.Spawners;
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
        private readonly IFactory<OnQuitEvent, OnReviveEvent, GameOverWindow> _gameOverWindowFactory;
        private readonly IGameLoop _gameLoop;
        private readonly WindowsRepository _windowsRepository;
        private readonly IInputHandlerController _inputHandler;
        private readonly AssetLoader _assetLoader;
        private readonly LoadingWindowViewModel _loadingWindowViewModel;
        private readonly PlayerBestRecordSaver _playerBestRecordSaver;
        private readonly StartGameAndEndGameEventSender _eventSender;
        private readonly IInterstitialAd _interstitialAd;
        private readonly IDataRepository _dataRepository;

        public GameplayEntryPoint(
            IFactory<ShipSpawnArgs, Ship> shipFactory,
            CharactersRepository charactersRepository,
            AiActorsRepository aiActorsRepository, 
            SpawnersAndControllersRepository spawnersAndControllersRepository,
            IFactory<ShipStatsWindow> shipStatsWindowFactory, 
            WindowsRepository windowsRepository, 
            IFactory<PlayerPointsWindow> playerPointsWindowFactory, 
            IFactory<OnQuitEvent, OnReviveEvent, GameOverWindow> gameOverWindowFactory,
            IGameLoop gameLoop,
            IInputHandlerController inputHandler, 
            AssetLoader assetLoader, 
            LoadingWindowViewModel loadingWindowViewModel,
            PlayerBestRecordSaver playerBestRecordSaver, 
            StartGameAndEndGameEventSender eventSender, 
            IInterstitialAd interstitialAd, 
            IDataRepository dataRepository)
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
            _loadingWindowViewModel = loadingWindowViewModel;
            _playerBestRecordSaver = playerBestRecordSaver;
            _eventSender = eventSender;
            _interstitialAd = interstitialAd;
            _dataRepository = dataRepository;
        }

        public async void Initialize()
        {
            _eventSender.SendStartGameEvent();
            _windowsRepository.Get<MobileInputWindow>().OpenAsync().Forget();
            await _loadingWindowViewModel.StartLoadingAsync(_assetLoader.GetLoadedAsyncOperations());
            await _windowsRepository.Get<LoadingWindow>().CloseAsync();
            CreateShip();
            _inputHandler.Enable();
            await _shipStatsWindowFactory.Create().OpenAsync();
            await _playerPointsWindowFactory.Create().OpenAsync();
            GameSettingsConfig gameSettingsData = _dataRepository.GetData<GameSettingsConfig>();
            _spawnersAndControllersRepository
                .GetSpawnerController<SpawnerController<AsteroidsSpawner>>()
                .SetSpawnCooldown(gameSettingsData.AsteroidsSpawnCoolDown);
            _spawnersAndControllersRepository
                .GetSpawnerController<SpawnerController<UfoSpawner>>()
                .SetSpawnCooldown(gameSettingsData.UfoSpawnCoolDown);
            _spawnersAndControllersRepository.StartAllSpawners();
            await _interstitialAd.LoadAdAsync();
        }

        private async UniTask OnQuitEvent()
        {
            await UniTask.WhenAll(
                _windowsRepository.TryCloseAndDestroyWindow<ShipStatsWindow>(),
                _windowsRepository.TryCloseAndDestroyWindow<PlayerPointsWindow>(),
                _windowsRepository.TryCloseAndDestroyWindow<GameOverWindow>());
            await _interstitialAd.ShowAdAsync();
            await _windowsRepository.Get<LoadingWindow>().OpenAsync();
            _windowsRepository.Get<MobileInputWindow>().CloseAsync().Forget();
            _aiActorsRepository.Clear();
            _spawnersAndControllersRepository.Clear();
            _charactersRepository.ClearAndDestroyAllCharactersInList();
            _charactersRepository.DestroyAndUnregisterShip();
            _assetLoader.ReleaseAllLoadedAssets();
        }

        private void CreateShip() =>
            _shipFactory.Create(new ShipSpawnArgs
            {
                Position = Vector2.zero,
                Rotation = Quaternion.identity,
                OnDead = async () =>
                {
                    _eventSender.SendEndGameEvent();
                    _spawnersAndControllersRepository.StopAllSpawnerControllers();
                    _inputHandler.Disable();
                    _gameLoop.Pause();
                    _charactersRepository.Ship.StopShip();
                    await _charactersRepository.Ship.PlayDeathAnimationAsync();
                    if (_windowsRepository.TryGet(out MobileInputWindow mobileInputWindow))
                        await mobileInputWindow.CloseAsync();
                    if (_windowsRepository.TryGet(out GameOverWindow gameOverWindow))
                        await gameOverWindow.OpenAsync();
                    else
                        await _gameOverWindowFactory.Create(OnQuitEvent, OnPlayerRevive).OpenAsync();
                    await _playerBestRecordSaver.TrySaveBestRecord();
                }
            });

        private async UniTask OnPlayerRevive()
        {
            await _windowsRepository.Get<GameOverWindow>().CloseAsync();
            if (_windowsRepository.TryGet(out MobileInputWindow mobileInputWindow))
                await mobileInputWindow.OpenAsync();
            _charactersRepository.Ship.Revive();
            _inputHandler.Enable();
            _gameLoop.Resume();
            _spawnersAndControllersRepository.StartAllSpawners();
        }
    }
}