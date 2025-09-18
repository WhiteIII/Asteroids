using _Project.Scripts.Core.InputSystem;
using _Project.Scripts.Core.Services.GameCycle;
using _Project.Scripts.Core.Stats;
using R3;
using UnityEngine;
using static UnityEngine.Time;

namespace _Project.Scripts.Core.Ship
{
    public class RotationController : IUpdatable
    {
        private readonly IInputHandler _inputHandler;
        private readonly Transform _shipTransform;
        private readonly IRotationSpeedStats _rotationSpeedStats;

        public readonly ReactiveProperty<Quaternion> ShipRotation = new();
        
        public RotationController(
            IInputHandler inputHandler, 
            Transform shipTransform,
            IRotationSpeedStats rotationSpeedStats)
        {
            _inputHandler = inputHandler;
            _shipTransform = shipTransform;
            _rotationSpeedStats = rotationSpeedStats;
        }

        public void Update()
        {
            _shipTransform.Rotate(
                0f,
                0f,
                -_inputHandler.MoveAxis.Value.x * _rotationSpeedStats.RotationSpeed * deltaTime);
            ShipRotation.Value = _shipTransform.rotation;
        } 
        
        public void SetRotation(Quaternion rotation) => 
            _shipTransform.rotation = rotation; 
    }
}