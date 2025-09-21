using System;
using _Project.Scripts.Core.Services.Targets;
using R3;
using UnityEngine;

namespace _Project.Scripts.Core.ShootingSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CollisionHandler))]
    internal class Bullet : MonoBehaviour
    {
        public readonly Subject<string> OnHit = new();

        private readonly CompositeDisposable _disposables = new();
        
        private BulletMovement _bulletMovement;
        private CollisionHandler _collisionHandler;
        private string _id;
        private Type _ignoreTargetType;

        internal void Initialize(string id, BulletMovement bulletMovement)
        {
            _id = id;
            _bulletMovement = bulletMovement;
        }

        private void Start()
        {
            _collisionHandler
                .OnTouchTarget
                .Subscribe(x => Hit(x))
                .AddTo(_disposables);
            _collisionHandler = GetComponent<CollisionHandler>();
        }

        private void OnDestroy() => 
            _disposables.Dispose();

        private void Hit(ITarget target)
        {
            if (target is IKillableTarget killableTarget)
            {
                if (killableTarget.GetType() != _ignoreTargetType)
                    killableTarget.Kill();
            }
            OnHit.OnNext(_id);
        }
        
        public void SetDirection(Vector2 diraction) =>
            _bulletMovement.SetDirection(diraction);

        public void SetFlySpeed(float speed) =>
            _bulletMovement.SetMovementSpeed(speed);

        public void SetIgnoreTarget<T>()
            where T : ITarget
        {
            _ignoreTargetType = typeof(T);
        }
    }
}
