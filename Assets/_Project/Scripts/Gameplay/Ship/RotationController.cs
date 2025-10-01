using _Project.Scripts.Core.GameLoopSystem;
using _Project.Scripts.Core.InputSystem;
using UnityEngine;
using Zenject;
using static UnityEngine.Time;

namespace _Project.Scripts.Core.Ship
{
    public class RotationController : MonoBehaviour, IUpdatable
    {
        private IInputHandler _inputHandler;
        private float _rotationSpeed;

        [Inject] private void Construct(IInputHandler inputHandler) => 
            _inputHandler = inputHandler;
        
        public void Initialize(
            float rotationSpeed)
        {
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