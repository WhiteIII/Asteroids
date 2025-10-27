using System;
using _Project.Scripts.Gameplay.Characters.Base;
using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.Services.Components;
using _Project.Scripts.Gameplay.Services.Targets.Base;
using _Project.Scripts.Gameplay.Services.Targets.Implementation;
using _Project.Scripts.Gameplay.Ship;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Characters
{
    [RequireComponent(typeof(AsteroidTarget))]
    [RequireComponent(typeof(RigidbodyMovement))]
    public class Asteroid : 
        KillableCharacter,
        IInitializableUpdatableObject
    {
        private RigidbodyMovement _movement;
        private PointsCounter _pointsCounter;
        private int _points;
        
        public Vector2 Direction => _movement.Direction;

        [Inject] private void Construct(PointsCounter pointsCounter) =>
            _pointsCounter = pointsCounter;

        protected override void OnAwake() => 
            _movement = GetComponent<RigidbodyMovement>();

        public void SetupAsteroid(Action onDeadAction = null)
        {
            SetupKillableCharacter(() =>
            {
                onDeadAction?.Invoke();
                _pointsCounter.AddPoints(_points);
            });
        }
        
        public IUpdatable[] GetAllUpdatableObjects() => 
            new IUpdatable[] { _movement };

        protected override void OnTouchTarget(ITarget target)
        {
            if (target is ShipTarget shipTarget && IsVisible)
                shipTarget.Kill();
            else if (target is Barrier _)
                ReleaseCharacter();
        }
        
        public void SetPoints(int points) => 
            _points = points;
        
        public void SendAsteroidOnDirection(Vector2 direction, float speed)
        {
            _movement.SetDirection(direction);
            _movement.SetMovementSpeed(speed);
        }

        public override void SetPosition(Vector2 position) => 
            _movement.SetPosition(position);
    }
}