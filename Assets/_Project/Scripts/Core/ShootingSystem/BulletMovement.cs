using _Project.Scripts.Core.GameLoopSystem;
using UnityEngine;
using static UnityEngine.Time;

namespace _Project.Scripts.Core.ShootingSystem
{
    internal class BulletMovement : IUpdatable
    {
        private readonly Rigidbody2D _rigidbody;
        
        private Vector2 _direction;
        private float _speed;

        public BulletMovement(Rigidbody2D rigidbody) =>
            _rigidbody = rigidbody;

        public void Update() => 
            _rigidbody.MovePosition(_rigidbody.position + _direction * _speed * deltaTime);

        public void SetMovementSpeed(float speed) => 
            _speed = speed;

        public void SetDirection(Vector2 diraction) =>
            _direction = diraction;
        
        public void SetPosition(Vector2 position) =>
            _rigidbody.position = position;
    }
}