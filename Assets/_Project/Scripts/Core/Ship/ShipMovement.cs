using _Project.Scripts.Core.InputSystem;
using UnityEngine;
using static UnityEngine.Time;

namespace _Project.Scripts.Core.Ship
{
    public class ShipMovement
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly Transform _shipTransform;
        private readonly IInputHandler _inputHandler;
        private readonly float _speed;

        public ShipMovement(
            Rigidbody2D rigidbody,
            Transform shipTransform,
            float speed)
        {
            _rigidbody = rigidbody;
            _shipTransform = shipTransform;
            _speed = speed;
        }

        public void Update() =>
            Move();

        public void SetPosition(Vector2 position) => 
            _rigidbody.MovePosition(position);

        private void Move() =>
            _rigidbody.AddForce(
                _shipTransform.rotation *
                Vector2.up *
                Mathf.Max(_inputHandler.Vertical.Value, 0f) * 
                _speed * 
                deltaTime, ForceMode2D.Force);
    }
}