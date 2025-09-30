using System;
using _Project.Scripts.Core.Services.ObjectPools;
using _Project.Scripts.Core.Services.Targets;
using _Project.Scripts.Core.ShootingSystem;
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

        public void Initialize(
            BulletsPool bulletsPool,
            float bulletFlyingSpeed)
        {
            _bulletsPool = bulletsPool;
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