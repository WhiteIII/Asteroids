using System;
using _Project.Scripts.Core.Services.Repositories;
using _Project.Scripts.Core.Ship;
using _Project.Scripts.SceneSwitcher;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap
{
    public class GameplayEntryPoint : IInitializable
    {
        private readonly IFactory<Ship> _shipFactory;
        private readonly CharactersRepository _charactersRepository;
        private readonly ISceneController _sceneController;

        public GameplayEntryPoint(
            IFactory<Ship> shipFactory,
            CharactersRepository charactersRepository, 
            ISceneController sceneController)
        {
            _shipFactory = shipFactory;
            _charactersRepository = charactersRepository;
            _sceneController = sceneController;
        }

        public void Initialize()
        {
            _charactersRepository.RegisterShip(_shipFactory.Create());
            SetShip();
        }
        
        private void SetShip()
        {
            _charactersRepository.Ship.SetPosition(Vector2.zero);
            _charactersRepository.Ship.SetRotation(Quaternion.identity);
            _charactersRepository.Ship.SetOnDeadEvent(() =>
            {
                _charactersRepository.Clear();
                _sceneController.GoToMenu();
            });
        }
    }
}
