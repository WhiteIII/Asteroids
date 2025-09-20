using System;
using _Project.Scripts.Core.Services.Targets;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.ShootingSystem
{
    public class Bullet : IInitializable, IDisposable
    {
        public readonly Subject<string> OnTouchTarget = new();
        
        private readonly BulletMovement _bulletMovement;
        private readonly GameObject _bulletGameObject;
        private readonly BulletAttackController _attackController;
        private readonly string _id;
        private readonly CompositeDisposable _disposable = new();

        public Bullet(
            BulletMovement bulletMovement,
            GameObject bulletGameObject,
            BulletAttackController attackController,
            string id)
        {
            _bulletMovement = bulletMovement;
            _bulletGameObject = bulletGameObject;
            _attackController = attackController;
            _id = id;
        }

        public void Initialize()
        {
            _attackController.Initialize();
            _attackController
                .OnHit
                .Subscribe(_ => OnTouchTarget.OnNext(_id))
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _attackController.Dispose();
            _disposable.Dispose();
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

        public void SendBulletInTheDirectionWithSpeed(Vector2 direction, float speed)
        {
            _bulletMovement.SetDirection(direction);
            _bulletMovement.SetSpeed(speed);
        }
    }
}
