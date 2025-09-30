using _Project.Scripts.Core.InputSystem;
using _Project.Scripts.Core.Services.ObjectPools;
using _Project.Scripts.Data;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Services.Factories
{
    public class ShipFactory : PlaceholderFactory<Ship.Ship>
    {
        private readonly GameObject _shipPrefab;
        private readonly IInstantiator _instantiator;
        private readonly IInputHandler _inputHandler;
        private readonly BulletsPool _bulletsPool;
        private readonly ShipStats _shipStats;

        public ShipFactory(
            GameObject shipPrefab,
            IInstantiator instantiator,
            IInputHandler inputHandler,
            BulletsPool bulletsPool,
            ShipStats shipStats)
        {
            _shipPrefab = shipPrefab;
            _instantiator = instantiator;
            _inputHandler = inputHandler;
            _bulletsPool = bulletsPool;
            _shipStats = shipStats;
        }

        public override Ship.Ship Create()
        {
            Ship.Ship ship = _instantiator
                .InstantiatePrefab(_shipPrefab)
                .GetComponent<Ship.Ship>();
            ship.Initialize(
                _inputHandler, 
                _bulletsPool, 
                _shipStats.MovementSpeed,
                _shipStats.BulletFlyingSpeed, 
                _shipStats.RotationSpeed);
            
            return ship;
        }
    }
}