using System;
using _Project.Scripts.Core.Services.ObjectPools;
using _Project.Scripts.Core.Services.Targets;
using _Project.Scripts.Core.ShootingSystem;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Ship
{
    public class AttackController : IDisposable
    {
        private readonly Transform _shipTransform;
        private readonly Transform _spawnPoint;
        private readonly BulletsPool _bulletsPool;
        private readonly float _bulletFlyingSpeed;

        public AttackController(
            Transform shipTransform, 
            BulletsPool bulletsPool,
            float bulletFlyingSpeed, 
            Transform spawnPoint)
        {
            _shipTransform = shipTransform;
            _bulletsPool = bulletsPool;
            _bulletFlyingSpeed = bulletFlyingSpeed;
            _spawnPoint = spawnPoint;
        }
        
        public void Dispose() => 
            _bulletsPool.Dispose();

        public void Shoot()
        {
            Bullet bullet = _bulletsPool.Get();
            bullet.SetPosition(_spawnPoint.position);
            bullet.SetRotation(_spawnPoint.rotation);
            bullet.SetDirection(_shipTransform.rotation * Vector2.up);
            bullet.SetFlySpeed(_bulletFlyingSpeed);
            bullet.SetIgnoreTarget<ShipTarget>();
        }
    }
}