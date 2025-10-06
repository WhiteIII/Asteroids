using System;
using _Project.Scripts.Gameplay.Characters.Base;
using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.Services.Components;
using _Project.Scripts.Gameplay.Services.Targets;
using _Project.Scripts.Gameplay.Services.Targets.Base;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(RigidbodyMovement))]
    public class Bullet :
        ReleasedCharacter,
        IInitializableUpdatableObject
    {
        private RigidbodyMovement _bulletMovement;
        private Type _ignoreTargetType;
        private Type _secondIgnoreTargetType;

        private void Awake()
        {
            SetupReleaseCharacter();
            _bulletMovement = GetComponent<RigidbodyMovement>();
        }

        public IUpdatable[] GetAllUpdatableObjects() => 
            new IUpdatable[] { _bulletMovement };
        
        public void SetDirection(Vector2 direction) =>
            _bulletMovement.SetDirection(direction);

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

        public void SetIgnoreTarget<T1, T2>()
            where T1 : ITarget
            where T2 : ITarget
        {
            _ignoreTargetType = typeof(T1);
            _secondIgnoreTargetType = typeof(T2);
        }
        
        protected override void OnTouchTarget(ITarget target)
        {
            if (target is not IKillableTarget killableTarget)
            {
                ReleaseCharacter();
                return;
            }

            Type killableTargetType = killableTarget.GetType();
            if (killableTargetType != _ignoreTargetType)
            {
                killableTarget.Kill();
                ReleaseCharacter();
            }
            else if (_secondIgnoreTargetType != null)
            {
                if (killableTargetType != _secondIgnoreTargetType)
                {
                    killableTarget.Kill();
                    ReleaseCharacter();
                }
            }
        }
    }
}
