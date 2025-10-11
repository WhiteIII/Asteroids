using System;
using _Project.Scripts.Gameplay.Services.Repositories;
using _Project.Scripts.Gameplay.Ship;
using _Project.Scripts.SceneSwitcher;
using _Project.Scripts.View.Implementation;
using _Project.Scripts.View.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap.EntryPoints
{
    public class GameplayEntryPoint : IInitializable,  IDisposable
    {
        private readonly IFactory<Ship> _shipFactory;
        private readonly CharactersRepository _charactersRepository;
        private readonly ISceneController _sceneController;
        private readonly AiActorsRepository _aiActorsRepository;
        private readonly SpawnersAndControllersRepository _spawnersAndControllersRepository;
        private readonly IFactory<ShipStatsWindow> _shipStatsWindowFactory;
        private readonly WindowsRepository _windowsRepository;

        public GameplayEntryPoint(
            IFactory<Ship> shipFactory,
            CharactersRepository charactersRepository, 
            ISceneController sceneController,
            AiActorsRepository aiActorsRepository, 
            SpawnersAndControllersRepository spawnersAndControllersRepository,
            IFactory<ShipStatsWindow> shipStatsWindowFactory, 
            WindowsRepository windowsRepository)
        {
            _shipFactory = shipFactory;
            _charactersRepository = charactersRepository;
            _sceneController = sceneController;
            _aiActorsRepository = aiActorsRepository;
            _spawnersAndControllersRepository = spawnersAndControllersRepository;
            _shipStatsWindowFactory = shipStatsWindowFactory;
            _windowsRepository = windowsRepository;
        }

        public async void Initialize()
        {
            SetupShip();
            await _shipStatsWindowFactory.Create().Open();
        }
        
        public async void Dispose()
        {
            await _windowsRepository.TryCloseAndDestroyWindow<ShipStatsWindow>();
            _aiActorsRepository.Clear();
            _spawnersAndControllersRepository.Clear();
            _charactersRepository.ClearAllCharactersList();
            _charactersRepository.UnregisterShip();
        }

        private void SetupShip()
        {
            _shipFactory.Create();
            _charactersRepository.Ship.SetPosition(Vector2.zero);
            _charactersRepository.Ship.SetRotation(Quaternion.identity);
            _charactersRepository.Ship.SetOnDeadEvent(() =>
            {
                _spawnersAndControllersRepository.StopAllSpawnerControllers();
                _sceneController.GoToMenu();
            });
        }

    }
}
