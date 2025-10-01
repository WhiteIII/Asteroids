using System;
using _Project.Scripts.Core.Enemies.Base;
using _Project.Scripts.Core.GameLoopSystem;
using _Project.Scripts.Core.Services.Components;
using _Project.Scripts.Core.Services.ObjectPools.Base;
using _Project.Scripts.Core.Services.Targets;
using R3;
using UnityEngine;

namespace _Project.Scripts.Core.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CollisionHandler))]
    [RequireComponent(typeof(RigidbodyMovement))]
    public class Bullet :
        MonoBehaviour,
        ICharacter,
        IEnableAndDisableItem, 
        IItemWithId<string>,
        IInitializableUpdatableObject
    {
        private readonly CompositeDisposable _disposables = new();
        
        private RigidbodyMovement _bulletMovement;
        private CollisionHandler _collisionHandler;
        private Type _ignoreTargetType;
        private string _id;
     
        public Subject<string> Release { get; } = new();

        public void SetID(string id) => 
            _id = id;

        private void Awake()
        {
            _collisionHandler = GetComponent<CollisionHandler>();
            _bulletMovement = GetComponent<RigidbodyMovement>();
            _collisionHandler
                .OnTouchTarget
                .Subscribe(x => Hit(x))
                .AddTo(_disposables);
        }

        private void OnDestroy() => 
            _disposables.Dispose();
        
        public IUpdatable[] GetAllUpdatableObjects() => 
            new IUpdatable[] { _bulletMovement };

        private void Hit(ITarget target)
        {
            if (target is IKillableTarget killableTarget)
            {
                if (killableTarget.GetType() != _ignoreTargetType)
                    killableTarget.Kill();
            }
            Release.OnNext(_id);
        }
        
        public void SetDirection(Vector2 diraction) =>
            _bulletMovement.SetDirection(diraction);

        public void SetFlySpeed(float speed) =>
            _bulletMovement.SetMovementSpeed(speed);
        
        public void SetPosition(Vector2 position) => 
            _bulletMovement.SetPosition(position);
        
        public void SetRotation(Quaternion rotation) =>
            _bulletMovement.SetRotation(rotation);
        
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