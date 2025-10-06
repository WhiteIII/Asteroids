using _Project.Scripts.Gameplay.Ai.Base;
using _Project.Scripts.Gameplay.Services.Repositories;
using _Project.Scripts.Gameplay.Ship;
using _Project.Scripts.Gameplay.Enemies.Base;
using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.Services.Components;
using _Project.Scripts.Gameplay.Services.Targets.Base;
using _Project.Scripts.Gameplay.Services.Targets.Implementation;
using UnityEngine;
using Zenject;
using static UnityEngine.Mathf;
using static UnityEngine.Time;
using static UnityEngine.Vector2;

namespace _Project.Scripts.Gameplay.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(UfoTarget))]
    [RequireComponent(typeof(GameObjectContext))]
    public class Ufo : KillableCharacter, IInitializableUpdatableObject
    {
        private AttackController _attackController;
        private ICharacterRepository _characterRepository;
        private AiAgentMovement _movement;
        private AiActor _actor;
        private float _attackDistance;
        private float _attackCooldown;
        private float _currentCooldown;

        public bool PlayerIsClose => Distance(Position, _characterRepository.Ship.Position) <= _attackDistance;
        public bool InCooldown => _currentCooldown > .1f;
        public bool IsMovingStoped { get; private set; }
        
        [Inject] private void Construct(AiActor actor) => 
            _actor = actor;
        
        public void Initialize(
            float bulletFlyingSpeed,
            float attackDistance,
            float attackCooldown)
        {
            _attackDistance = attackDistance;
            _attackCooldown = attackCooldown;
            
            SetupKillableCharacter();
            _attackController.Initialize(bulletFlyingSpeed);
        }

        private void Update() => 
            _currentCooldown = Max(0, _currentCooldown - deltaTime);

        public IUpdatable[] GetAllUpdatableObjects() => 
            new IUpdatable[] { _actor };

        public void Attack()
        {
            _currentCooldown = _attackCooldown;
            _attackController.Shoot<UfoTarget, AsteroidTarget>(_characterRepository.Ship.Position - Position);
        }

        public void MoveToPlayer()
        {
            IsMovingStoped = false;
            _movement.MoveTo(_characterRepository.Ship.Position);
        }

        public void StopMoving()
        {
            _movement.StopMoving();
            IsMovingStoped = true;
        }

        protected override void OnTouchTarget(ITarget target)
        {
            throw new System.NotImplementedException();
        }

    }
}