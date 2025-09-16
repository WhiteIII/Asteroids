using _Project.Scripts.Core.InputSystem;
using _Project.Scripts.Core.Services.GameCycle;
using _Project.Scripts.Core.Ship;
using _Project.Scripts.Core.Stats;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Services
{
    internal class ShipFactory : IFactory<Ship.Ship>
    {
        private readonly IInstantiator _instantiator;
        private readonly IGameCycleRegisterController _gameCycleRegisterController;
        private readonly GameObject _shipPrefab;
        private readonly IInputHandler _inputHandler;
        private readonly IShipStats _shipStats;

        public ShipFactory(
            IInstantiator instantiator,
            IGameCycleRegisterController gameCycleRegisterController,
            GameObject shipPrefab,
            IInputHandler inputHandler,
            IShipStats shipStats)
        {
            _instantiator = instantiator;
            _gameCycleRegisterController = gameCycleRegisterController;
            _shipPrefab = shipPrefab;
            _inputHandler = inputHandler;
            _shipStats = shipStats;
        }

        public Ship.Ship Create()
        {
            GameObject shipGameObject = _instantiator.InstantiatePrefab(_shipPrefab);
            Object.DontDestroyOnLoad(shipGameObject);
            return _instantiator.Instantiate<Ship.Ship>(new object[]
            {
                _gameCycleRegisterController.Register(new Movement(
                    shipGameObject.GetComponent<Rigidbody2D>(),
                    shipGameObject.transform,
                    _inputHandler,
                    _shipStats.MovementSpeed)),
            });;
        }
    }
}
