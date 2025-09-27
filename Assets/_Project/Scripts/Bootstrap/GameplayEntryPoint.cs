using System;
using _Project.Scripts.Core.Services.Repositories;
using _Project.Scripts.Core.Ship;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Bootstrap
{
    public class GameplayEntryPoint : IInitializable, IDisposable
    {
        private readonly IFactory<Ship> _shipFactory;
        private readonly CharactersRepository _charactersRepository;

        public GameplayEntryPoint(
            IFactory<Ship> shipFactory,
            CharactersRepository charactersRepository)
        {
            _shipFactory = shipFactory;
            _charactersRepository = charactersRepository;
        }

        public void Initialize()
        {
            _charactersRepository.RegisterShip(_shipFactory.Create());
            SetShip();
        }
        
        public void Dispose()
        {
            _charactersRepository.Clear();
        }

        private void SetShip()
        {
            _charactersRepository.Ship.SetPosition(Vector2.zero);
            _charactersRepository.Ship.SetRotation(Quaternion.identity);
        }
    }
}
