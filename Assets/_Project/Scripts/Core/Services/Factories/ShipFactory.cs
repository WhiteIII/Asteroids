using _Project.Scripts.Core.GameLoopSystem;
using _Project.Scripts.Core.InputSystem;
using _Project.Scripts.Core.Ship;
using _Project.Scripts.Data;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Services.Factories
{
    public class ShipFactory : PlaceholderFactory<Ship.Ship>
    {
        private readonly GameObject _shipPrefab;
        private readonly IInstantiator _instantiator;
        private readonly IGameLoopRegisterController _gameLoopRegisterController;
        private readonly IInputHandler _inputHandler;
        private readonly BulletPool _bulletPool;
        private readonly ShipStats _shipStats;

        public ShipFactory(
            GameObject shipPrefab,
            IInstantiator instantiator,
            IGameLoopRegisterController gameLoopRegisterController,
            IInputHandler inputHandler,
            BulletPool bulletPool,
            ShipStats shipStats)
        {
            _shipPrefab = shipPrefab;
            _instantiator = instantiator;
            _gameLoopRegisterController = gameLoopRegisterController;
            _inputHandler = inputHandler;
            _bulletPool = bulletPool;
            _shipStats = shipStats;
        }

        public override Ship.Ship Create()
        {
            Ship.Ship ship = _instantiator
                .InstantiatePrefab(_shipPrefab)
                .GetComponent<Ship.Ship>();
            ship.Initialize(
                _gameLoopRegisterController.Register(
                    new ShipMovement(
                        ship.gameObject.GetComponent<Rigidbody2D>(),
                        _shipStats.MovementSpeed,
                        _inputHandler)),
                new AttackController(
                    ship.transform,
                    _bulletPool,
                    _shipStats.BulletFlyingSpeed,
                    ship.BulletSpawnPoint),
                _inputHandler,
                _gameLoopRegisterController.Register(
                    new RotationController(
                        ship.transform, 
                        _inputHandler, 
                        _shipStats.RotationSpeed)));
            
            return ship;
        }
    }
}