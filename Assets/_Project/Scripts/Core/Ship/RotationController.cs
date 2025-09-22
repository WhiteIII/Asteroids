using _Project.Scripts.Core.GameLoopSystem;
using _Project.Scripts.Core.InputSystem;
using UnityEngine;
using static UnityEngine.Time;

namespace _Project.Scripts.Core.Ship
{
    public class RotationController : IUpdatable
    {
        private readonly Transform _shipTransform;
        private readonly IInputHandler _inputHandler;
        private readonly float _rotationSpeed;

        public RotationController(
            Transform shipTransform,
            IInputHandler inputHandler, 
            float rotationSpeed)
        {
            _shipTransform = shipTransform;
            _inputHandler = inputHandler;
            _rotationSpeed = rotationSpeed;
        }

        public void Update()
        {
            _shipTransform.Rotate(
                0f,
                0f,
                -_inputHandler.Horizontal.Value * _rotationSpeed * deltaTime);
        } 
        
        public void SetRotation(Quaternion rotation) => 
            _shipTransform.rotation = rotation; 
    }
}