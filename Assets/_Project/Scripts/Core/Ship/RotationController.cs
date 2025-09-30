using _Project.Scripts.Core.GameLoopSystem;
using _Project.Scripts.Core.InputSystem;
using UnityEngine;
using static UnityEngine.Time;

namespace _Project.Scripts.Core.Ship
{
    public class RotationController : MonoBehaviour, IUpdatable
    {
        private IInputHandler _inputHandler;
        private float _rotationSpeed;

        public void Initialize(
            IInputHandler inputHandler, 
            float rotationSpeed)
        {
            _inputHandler = inputHandler;
            _rotationSpeed = rotationSpeed;
        }

        public void GameLoopUpdate()
        {
            transform.Rotate(
                0f,
                0f,
                -_inputHandler.Horizontal.Value * _rotationSpeed * deltaTime);
        } 
        
        public void SetRotation(Quaternion rotation) => 
            transform.rotation = rotation; 
    }
}