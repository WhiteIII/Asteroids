using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.InputSystem;
using UnityEngine;
using Zenject;
using static UnityEngine.Time;

namespace _Project.Scripts.Gameplay.Ship
{
    public class RotationController : MonoBehaviour, IUpdatable
    {
        private IInputHandler _inputHandler;
        private float _rotationSpeed;

        [Inject]
        private void Construct(IInputHandler inputHandler, ShipStatsData stats)
        {
            _inputHandler = inputHandler;
            _rotationSpeed = stats.RotationSpeed;
        } 

        public void GameLoopUpdate() =>
            transform.Rotate(
                0f,
                0f,
                -_inputHandler.Horizontal.Value * _rotationSpeed * deltaTime);

        public void SetRotation(Quaternion rotation) => 
            transform.rotation = rotation; 
    }
}