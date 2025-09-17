using _Project.Scripts.Core.InputSystem;
using _Project.Scripts.Core.Services;
using _Project.Scripts.Core.Ship;
using _Project.Scripts.Core.Stats;
using _Project.Scripts.Data;
using _Project.Scripts.SceneController;
using Zenject;

namespace _Project.Scripts.Bootstrap
{
    internal class EntryPoint : IInitializable
    {
        private readonly IInputHandler _inputHandler;
        private readonly ShipStats _shipStats;
        private readonly ShipDefaultStats _shipDefaultStats;
        private readonly IFactory<Ship> _shipFactory;
        private readonly IScenesController _scenesController;

        public EntryPoint(
            IInputHandler inputHandler, 
            IFactory<Ship> shipFactory, 
            ShipDefaultStats shipDefaultStats,
            ShipStats shipStats,
            IScenesController scenesController)
        {
            _inputHandler = inputHandler;
            _shipFactory = shipFactory;
            _shipDefaultStats = shipDefaultStats;
            _shipStats = shipStats;
            _scenesController = scenesController;
        }

        public void Initialize()
        {
            Ship ship = _shipFactory.Create();
            SetShipStats();
            ship.Initialize();
            _scenesController.GoToMenu();
        }

        private void SetShipStats()
        {
            _shipStats.SetMovementSpeed(_shipDefaultStats.MovementSpeed);
        }
    }
}
