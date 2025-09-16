using _Project.Scripts.Core.InputSystem;
using _Project.Scripts.Core.Services.GameCycle;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Ship
{
    public class Movement : IUpdatable, IInitializable
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly Transform _shipTransform;
        private readonly IInputHandler _inputHandler;
        private readonly float _speed;

        public ReactiveProperty<Vector3> Position { get; private set; }
        
        public Movement(
            Rigidbody2D rigidbody,
            Transform shipTransform,
            IInputHandler inputHandler,
            float speed)
        {
            _rigidbody = rigidbody;
            _shipTransform = shipTransform;
            _inputHandler = inputHandler;
            _speed = speed;
        }
        
        public void Initialize() => 
            Position.Value = _shipTransform.position;
        
        public void Update() => 
            _rigidbody.AddForce(_inputHandler.MoveAxis.Value * _speed, ForceMode2D.Force);
    }
}