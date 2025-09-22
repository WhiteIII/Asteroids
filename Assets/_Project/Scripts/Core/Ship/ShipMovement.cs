using _Project.Scripts.Core.GameLoopSystem;
using _Project.Scripts.Core.InputSystem;
using UnityEngine;
using static UnityEngine.Time;

namespace _Project.Scripts.Core.Ship
{
    public class ShipMovement : IUpdatable
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly IInputHandler _inputHandler;
        private readonly float _speed;

        public ShipMovement(
            Rigidbody2D rigidbody,
            float speed, 
            IInputHandler inputHandler)
        {
            _rigidbody = rigidbody;
            _speed = speed;
            _inputHandler = inputHandler;
        }

        public void Update() =>
            Move();

        public void SetPosition(Vector2 position) => 
            _rigidbody.MovePosition(position);

        private void Move() =>
            _rigidbody.AddForce(
                _rigidbody.transform.rotation *
                Vector2.up *
                Mathf.Max(_inputHandler.Vertical.Value, 0f) * 
                _speed * 
                deltaTime, ForceMode2D.Force);
    }
}