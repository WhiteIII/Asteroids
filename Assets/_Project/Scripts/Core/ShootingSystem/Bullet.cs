using System;
using _Project.Scripts.Core.Services.Targets;
using R3;
using UnityEngine;

namespace _Project.Scripts.Core.ShootingSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CollisionHandler))]
    public class Bullet : MonoBehaviour
    {
        public readonly Subject<string> OnHit = new();

        private readonly CompositeDisposable _disposables = new();
        
        private BulletMovement _bulletMovement;
        private CollisionHandler _collisionHandler;
        private Type _ignoreTargetType;
        private string _id;

        internal void Initialize(BulletMovement bulletMovement) =>
            _bulletMovement = bulletMovement;
        
        internal void SetID(string id) => 
            _id = id;

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
        
        public void SetPosition(Vector2 position) => 
            _bulletMovement.SetPosition(position);
        
        public void SetIgnoreTarget<T>()
            where T : ITarget
        {
            _ignoreTargetType = typeof(T);
        }
        
        public void Enable() => 
            gameObject.SetActive(true);
        
        public void Disable() =>
            gameObject.SetActive(false);
    }
}
