using _Project.Scripts.Core.Services.GameCycle;
using _Project.Scripts.Core.Stats;
using UnityEngine;

namespace _Project.Scripts.Core.ShootingSystem
{
    internal class BulletMovement : IUpdatable
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly IBulletSpeedStats _bulletSpeedStats;
        private readonly Transform _bulletTransform;
        
        private Vector3 _direction;

        public BulletMovement(
            Rigidbody2D rigidbody,
            IBulletSpeedStats bulletSpeedStats,
            Transform bulletTransform)
        {
            _rigidbody = rigidbody;
            _bulletSpeedStats = bulletSpeedStats;
            _bulletTransform = bulletTransform;
        }

        public void Update() =>
            _rigidbody.MovePosition(
                _bulletTransform.position + 
                _direction * 
                _bulletSpeedStats.BulletSpeed *
                Time.deltaTime);

        public void SetDirection(Vector2 direction) =>
            _direction = direction;
    }
}