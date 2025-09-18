using System;
using _Project.Scripts.Core.Services.Targets;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.ShootingSystem
{
    internal class Bullet : IInitializable, IDisposable
    {
        private readonly BulletMovement _bulletMovement;
        private readonly GameObject _bulletGameObject;
        private readonly BulletAttackController _attackController;
        
        public void Initialize()
        {
            _attackController.Initialize();
        }

        public void Dispose()
        {
            _attackController.Dispose();
        }
        
        public void SetTarget<T>()
            where T : IKillableTarget
        {
            _attackController.SetTarget<T>();
        }
        
        public void Enable() => 
            _bulletGameObject.SetActive(true);
        
        public void Disable() =>
            _bulletGameObject.SetActive(false);
        
        public void SendBulletInTheDirection(Vector2 direction) =>
            _bulletMovement.SetDirection(direction);
    }
}
