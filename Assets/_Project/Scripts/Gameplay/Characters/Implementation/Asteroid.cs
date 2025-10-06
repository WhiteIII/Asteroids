using _Project.Scripts.Gameplay.Characters.Base;
using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.Services.Components;
using _Project.Scripts.Gameplay.Services.Targets.Base;
using _Project.Scripts.Gameplay.Services.Targets.Implementation;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(AsteroidTarget))]
    [RequireComponent(typeof(RigidbodyMovement))]
    public class Asteroid : 
        KillableCharacter,
        IInitializableUpdatableObject
    {
        private RigidbodyMovement _movement;
        
        public Vector2 Direction => _movement.Direction;

        private void Awake()
        {
            _movement = GetComponent<RigidbodyMovement>();
            SetupKillableCharacter();
        } 
        
        public IUpdatable[] GetAllUpdatableObjects() => 
            new IUpdatable[] { _movement };

        protected override void OnTouchTarget(ITarget target)
        {
            if (target is ShipTarget shipTarget)
                shipTarget.Kill();
            else if (target is Barrier _)
                ReleaseCharacter();
        }
        
        public void SendAsteroidOnDirection(Vector2 direction, float speed)
        {
            _movement.SetDirection(direction);
            _movement.SetMovementSpeed(speed);
        }

        public void SetPosition(Vector2 position) => 
            _movement.SetPosition(position);
    }
}
