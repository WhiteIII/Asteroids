using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.Services.Repositories;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.Factories
{
    public class ShipFactory : PlaceholderFactory<Ship.Ship>
    {
        private readonly GameObject _shipPrefab;
        private readonly IGameLoopCreator _creator;
        private readonly ShipStats _shipStats;
        private readonly CharactersRepository _repository;

        public ShipFactory(
            GameObject shipPrefab,
            IGameLoopCreator creator,
            ShipStats shipStats,
            CharactersRepository repository)
        {
            _shipPrefab = shipPrefab;
            _creator = creator;
            _shipStats = shipStats;
            _repository = repository;
        }

        public override Ship.Ship Create()
        {
            Ship.Ship ship = _repository.RegisterShip(_creator
                .Create<Ship.Ship>(_shipPrefab));
            ship.Initialize(
                _shipStats.MovementSpeed,
                _shipStats.BulletFlyingSpeed, 
                _shipStats.RotationSpeed);
            
            return ship;
        }
    }
}