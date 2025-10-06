using System;
using _Project.Scripts.Gameplay.Services.Repositories;
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

        public GameplayEntryPoint(
            IFactory<Ship> shipFactory,
            CharactersRepository charactersRepository, 
            ISceneController sceneController,
            AiActorsRepository aiActorsRepository)
        {
            _shipFactory = shipFactory;
            _charactersRepository = charactersRepository;
            _sceneController = sceneController;
            _aiActorsRepository = aiActorsRepository;
        }

        public void Initialize()
        {
            _shipFactory.Create();
            SetupShip();
        }
        
        public void Dispose()
        {
            _aiActorsRepository.Clear();
            _charactersRepository.ClearAllCharactersList();
            _charactersRepository.UnregisterShip();
        }
        
        private void SetupShip()
        {
            _charactersRepository.Ship.SetPosition(Vector2.zero);
            _charactersRepository.Ship.SetRotation(Quaternion.identity);
            _charactersRepository.Ship.SetOnDeadEvent(_sceneController.GoToMenu);
        }

    }
}
