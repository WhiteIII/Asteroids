using _Project.Scripts.Gameplay.Characters.Base;
using _Project.Scripts.Gameplay.Services.Repositories;
using _Project.Scripts.Gameplay.Ship;
using _Project.Scripts.Gameplay.Services.Components;
using _Project.Scripts.Gameplay.Services.Targets.Base;
using _Project.Scripts.Gameplay.Services.Targets.Implementation;
using UnityEngine;
using Zenject;
using static UnityEngine.Mathf;
using static UnityEngine.Time;
using static UnityEngine.Vector2;

namespace _Project.Scripts.Gameplay.Characters
{
    [RequireComponent(typeof(UfoTarget))]
    [RequireComponent(typeof(AttackController))]
    [RequireComponent(typeof(SimpleAiMovement))]
    public class Ufo : KillableCharacter
    {
        private AttackController _attackController;
        private ICharacterRepository _characterRepository;
        private IAiAgentMovement _movement;
        private float _attackDistance;
        private float _attackCooldown;
        private float _currentCooldown;

        public bool PlayerIsClose => Distance(
            Position.CurrentValue, 
            _characterRepository.Ship.Position.CurrentValue) <= _attackDistance;
        public bool InCooldown => _currentCooldown > .1f;
        public bool IsMovingStoped { get; private set; }
        
        [Inject] private void Construct(ICharacterRepository repository) => 
            _characterRepository = repository;
        
        public void Initialize(
            float bulletFlyingSpeed,
            float attackDistance,
            float attackCooldown,
            float movementSpeed)
        {
            _attackDistance = attackDistance;
            _attackCooldown = attackCooldown;
            
            SetupKillableCharacter();
            _attackController.Initialize(bulletFlyingSpeed);
            _movement.Initialize(movementSpeed);
        }

        protected override void OnAwake()
        {
            _attackController = GetComponent<AttackController>();
            _movement = GetComponent<IAiAgentMovement>();
        }

        private void Update() => 
            _currentCooldown = Max(0, _currentCooldown - deltaTime);
        
        public void Attack()
        {
            _currentCooldown = _attackCooldown;
            _attackController.Shoot<UfoTarget, AsteroidTarget>(
                (_characterRepository.Ship.Position.CurrentValue - Position.CurrentValue).normalized);
        }

        public void MoveToPlayer()
        {
            IsMovingStoped = false;
            _movement.MoveTo(_characterRepository.Ship.Position.CurrentValue);
        }

        public void StopMoving() =>
            IsMovingStoped = true;

        public void SetPosition(Vector2 position) => 
            _movement.SetPosition(position);

        protected override void OnTouchTarget(ITarget target)
        {
            //throw new System.NotImplementedException();
        }
    }
}