using System;
using _Project.Scripts.Core.Enemies;
using _Project.Scripts.Core.Services.ObjectPools;
using _Project.Scripts.Core.Services.Targets;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Ship
{
    public class AttackController : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        
        private BulletsPool _bulletsPool;
        private float _bulletFlyingSpeed;

        [Inject] private void Construct(BulletsPool bulletsPool) => 
            _bulletsPool = bulletsPool;
        
        public void Initialize(
            float bulletFlyingSpeed)
        {
            _bulletFlyingSpeed = bulletFlyingSpeed;
        }
        
        public void OnDestroy() => 
            _bulletsPool.Dispose();

        public void Shoot()
        {
            Bullet bullet = _bulletsPool.Get(_spawnPoint.position);
            bullet.SetRotation(_spawnPoint.rotation);
            bullet.SetDirection(transform.rotation * Vector2.up);
            bullet.SetFlySpeed(_bulletFlyingSpeed);
            bullet.SetIgnoreTarget<ShipTarget>();
        }
    }
}