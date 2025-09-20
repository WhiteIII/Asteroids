using _Project.Scripts.Core.Services.GameCycle;
using UnityEngine;

namespace _Project.Scripts.Core.ShootingSystem
{
    public class BulletMovement : IUpdatable
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly Transform _bulletTransform;

        private float _speed;
        private Vector3 _direction;

        public BulletMovement(
            Rigidbody2D rigidbody,
            Transform bulletTransform)
        {
            _rigidbody = rigidbody;
            _bulletTransform = bulletTransform;
        }

        public void Update() =>
            _rigidbody.MovePosition(
                _bulletTransform.position + 
                _direction * 
                _speed *
                Time.deltaTime);

        public void SetDirection(Vector2 direction) =>
            _direction = direction;
        
        public void SetSpeed(float speed) =>
            _speed = speed;
    }
}