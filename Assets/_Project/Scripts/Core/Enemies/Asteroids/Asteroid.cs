using System;
using _Project.Scripts.Core.Services.Components;
using _Project.Scripts.Core.Services.Targets;
using _Project.Scripts.Core.ShootingSystem;
using R3;
using UnityEngine;

namespace _Project.Scripts.Core.Enemies.Asteroids
{
    public class Asteroid : MonoBehaviour
    {
        public readonly Subject<string> OnTouchBarrier = new();
        
        private readonly CompositeDisposable _disposable = new();
        
        private RigidbodyMovement _bulletMovement;
        private CollisionHandler _collisionHandler;
        private string _id;

        internal void Initialize(
            RigidbodyMovement bulletMovement,
            CollisionHandler collisionHandler,
            string id)
        {
            _bulletMovement  = bulletMovement;
            _collisionHandler = collisionHandler;
            _id = id;

            _collisionHandler
                .OnTouchTarget
                .Subscribe(x => OnTargetTouch(x))
                .AddTo(_disposable);
        }
        
        private void OnDestroy() => 
            _disposable.Dispose();
        
        public void SendBulletOnDirection(Vector2 direction, float speed)
        {
            _bulletMovement.SetDirection(direction);
            _bulletMovement.SetMovementSpeed(speed);
        }

        public void SetPosition(Vector2 position) => 
            _bulletMovement.SetPosition(position);

        public void Enable() => 
            gameObject.SetActive(true);
        
        public void Disable() =>
            gameObject.SetActive(false);

        private void OnTargetTouch(ITarget target)
        {
            if (target is ShipTarget shipTarget)
                shipTarget.Kill();
            else if (target is Barrier _)
                OnTouchBarrier.OnNext(_id);
        }
    }
}
