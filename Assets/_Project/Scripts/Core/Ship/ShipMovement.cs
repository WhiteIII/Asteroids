using _Project.Scripts.Core.InputSystem;
using _Project.Scripts.Core.Services.GameCycle;
using _Project.Scripts.Core.Stats;
using R3;
using UnityEngine;
using Zenject;
using static UnityEngine.Mathf;
using static UnityEngine.Time;

namespace _Project.Scripts.Core.Ship
{
    public class ShipMovement : IUpdatable, IInitializable
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly Transform _shipTransform;
        private readonly IInputHandler _inputHandler;
        private readonly IMovementSpeedStats _speedStats;

        public ReactiveProperty<Vector3> Position { get; } = new();
        
        public ShipMovement(
            Rigidbody2D rigidbody,
            Transform shipTransform,
            IInputHandler inputHandler,
            IMovementSpeedStats speedStats)
        {
            _rigidbody = rigidbody;
            _shipTransform = shipTransform;
            _inputHandler = inputHandler;
            _speedStats = speedStats;
        }
        
        public void Initialize() => 
            Position.Value = _shipTransform.position;
        
        public void SetPosition(Vector3 position) => 
            _rigidbody.MovePosition(position);
        
        public void Update()
        {
            _rigidbody.AddForce(
                _shipTransform.rotation * 
                Vector2.up *
                Max(_inputHandler.MoveAxis.Value.y, 0f) * 
                _speedStats.MovementSpeed * 
                deltaTime, ForceMode2D.Force);
            Position.Value = _shipTransform.position;
        }
    }
}