using System;
using _Project.Scripts.Core.InputSystem;
using _Project.Scripts.Core.Services.Targets;
using _Project.Scripts.Core.ShootingSystem;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Ship
{
    internal class AttackController : IInitializable, IDisposable
    {
        private readonly IInputHandler _inputHandler;
        private readonly Transform _shipTransform;
        private readonly BulletPool _bulletPool;
        private readonly float _bulletFlyingSpeed;
        private readonly CompositeDisposable _disposables = new();
        
        public void Initialize() =>
            _inputHandler
                .OnBackspacePressed
                .Subscribe(_ => Shoot())
                .AddTo(_disposables);

        public void Dispose() => 
            _disposables.Dispose();

        private void Shoot()
        {
            Bullet bullet = _bulletPool.Get();
            bullet.SetDirection(_shipTransform.rotation * Vector2.up);
            bullet.SetFlySpeed(_bulletFlyingSpeed);
            bullet.SetIgnoreTarget<ShipTarget>();
        }
    }
}