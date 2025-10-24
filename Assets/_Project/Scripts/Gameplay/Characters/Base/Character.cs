using System;
using _Project.Scripts.Gameplay.Services.Components;
using _Project.Scripts.Gameplay.Services.ObjectPools.Base;
using R3;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Characters.Base
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CheckObjectForGoingOutOfLineOfSight))]
    public abstract class Character : 
        MonoBehaviour,
        ICharacter,
        IEnableAndDisableItem
    {
        public ReadOnlyReactiveProperty<Vector3> Position { get; private set; }
        public ReadOnlyReactiveProperty<Vector3> Rotation { get; private set; }
        public ReadOnlyReactiveProperty<float> Acceleration { get; private set; }
        
        private CheckObjectForGoingOutOfLineOfSight _checkObjectForGoingOutOfLineOfSight;

        public bool IsVisible => _checkObjectForGoingOutOfLineOfSight.IsVisible;
        
        private void Awake()
        {
            _checkObjectForGoingOutOfLineOfSight = GetComponent<CheckObjectForGoingOutOfLineOfSight>();
            Position = Observable
                .EveryValueChanged(transform, x => x.position)
                .ToReadOnlyReactiveProperty()
                .AddTo(this);
            Rotation = Observable
                .EveryValueChanged(transform, x => x.rotation.eulerAngles)
                .ToReadOnlyReactiveProperty()
                .AddTo(this);
            Acceleration = Observable
                .EveryValueChanged(GetComponent<Rigidbody2D>(), x => x.linearVelocity.magnitude)
                .ToReadOnlyReactiveProperty()
                .AddTo(this);
            OnAwake();
        }

        public void Enable() => 
            gameObject.SetActive(true);

        public void Disable() => 
            gameObject.SetActive(false);

        public abstract void SetPosition(Vector2 position); 
        
        protected virtual void OnAwake() { }
    }
}