using System;
using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.Services.Repositories;
using _Project.Scripts.Gameplay.Services.Spawners;
using _Project.Scripts.Gameplay.Ship;
using _Project.Scripts.SceneSwitcher;
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
        private readonly SpawnersRepository _spawnersRepository;
        private readonly SpawnerControllerCreator<UfoSpawner> _ufoSpawnerControllerCreator;
        private readonly SpawnerControllerCreator<AsteroidsSpawner> _asteroidSpawnerControllerCreator;
        
        public GameplayEntryPoint(
            IFactory<Ship> shipFactory,
            CharactersRepository charactersRepository, 
            ISceneController sceneController,
            AiActorsRepository aiActorsRepository, 
            SpawnersRepository spawnersRepository, 
            SpawnerControllerCreator<UfoSpawner> ufoSpawnerControllerCreator,
            SpawnerControllerCreator<AsteroidsSpawner> asteroidSpawnerControllerCreator)
        {
            _shipFactory = shipFactory;
            _charactersRepository = charactersRepository;
            _sceneController = sceneController;
            _aiActorsRepository = aiActorsRepository;
            _spawnersRepository = spawnersRepository;
            _ufoSpawnerControllerCreator = ufoSpawnerControllerCreator;
            _asteroidSpawnerControllerCreator = asteroidSpawnerControllerCreator;
        }

        public void Initialize()
        {
            SetupShip();
            SetupSpawners();
        }
        
        public void Dispose()
        {
            _aiActorsRepository.Clear();
            _spawnersRepository.Clear();
            _charactersRepository.ClearAllCharactersList();
            _charactersRepository.UnregisterShip();
        }

        public void SetupSpawners()
        {
            _ufoSpawnerControllerCreator.Create();
            _asteroidSpawnerControllerCreator.Create();
        }
        
        private void SetupShip()
        {
            _shipFactory.Create();
            _charactersRepository.Ship.SetPosition(Vector2.zero);
            _charactersRepository.Ship.SetRotation(Quaternion.identity);
            _charactersRepository.Ship.SetOnDeadEvent(_sceneController.GoToMenu);
        }

    }
}
