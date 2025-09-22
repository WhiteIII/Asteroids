using System;
using _Project.Scripts.Core.Services.Targets;
using _Project.Scripts.Core.ShootingSystem;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Ship
{
    public class AttackController
    {
        private readonly Transform _shipTransform;
        private readonly BulletPool _bulletPool;
        private readonly float _bulletFlyingSpeed;

        public AttackController(
            Transform shipTransform, 
            BulletPool bulletPool,
            float bulletFlyingSpeed)
        {
            _shipTransform = shipTransform;
            _bulletPool = bulletPool;
            _bulletFlyingSpeed = bulletFlyingSpeed;
        }

        public void Shoot()
        {
            Bullet bullet = _bulletPool.Get();
            bullet.SetDirection(_shipTransform.rotation * Vector2.up);
            bullet.SetFlySpeed(_bulletFlyingSpeed);
            bullet.SetIgnoreTarget<ShipTarget>();
        }
    }
}