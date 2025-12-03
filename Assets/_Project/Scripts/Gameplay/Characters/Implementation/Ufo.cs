using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.Characters.Base;
using _Project.Scripts.Gameplay.GameProgress;
using _Project.Scripts.Gameplay.Services.Components;
using _Project.Scripts.Gameplay.Services.Repositories;
using _Project.Scripts.Gameplay.Services.Targets.Base;
using _Project.Scripts.Gameplay.Services.Targets.Implementation;
using _Project.Scripts.Gameplay.ShipBase;
using UnityEngine;
using Zenject;
using static UnityEngine.Mathf;
using static UnityEngine.Time;
using static UnityEngine.Vector2;

namespace _Project.Scripts.Gameplay.Characters.Implementation
{
    [RequireComponent(typeof(UfoTarget))]
    [RequireComponent(typeof(AttackController))]
    [RequireComponent(typeof(SimpleAiMovement))]
    public class Ufo : KillableCharacter
    {
        private AttackController _attackController;
        private ICharacterRepository _characterRepository;
        private IAiAgentMovement _movement;
        private PointsAndKillsAndKillsCounterCounter _pointsAndKillsAndKillsCounterCounter;
        private UfoStatsData _stats;
        private float _currentCooldown;

        public bool PlayerIsClose => Distance(
            Position.CurrentValue, 
            _characterRepository.Ship.Position.CurrentValue) <= _stats.AttackDistance;
        public bool InCooldown => _currentCooldown > .1f;
        public bool IsMovingStoped { get; private set; }

        [Inject]
        private void Construct(ICharacterRepository repository, UfoStatsData stats, PointsAndKillsAndKillsCounterCounter pointsAndKillsAndKillsCounterCounter)
        {
            _characterRepository = repository;
            _pointsAndKillsAndKillsCounterCounter = pointsAndKillsAndKillsCounterCounter;
            _stats = stats;
        } 
        
        protected override void OnAwake()
        {
            _attackController = GetComponent<AttackController>();
            _movement = GetComponent<IAiAgentMovement>();
            _attackController.Initialize(_stats.BulletFlyingSpeed);
            _movement.Initialize(_stats.MovementSpeed);
            SetupKillableCharacter(() => _pointsAndKillsAndKillsCounterCounter.AddKill<Ufo>(_stats.Points));
        }

        private void Update() => 
            _currentCooldown = Max(0, _currentCooldown - deltaTime);
        
        public void Attack()
        {
            _currentCooldown = _stats.AttackCooldown;
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
            if (target is ShipTarget shipTarget && IsVisible)
                shipTarget.Kill();
            else if (target is Barrier _)
                ReleaseCharacter();
        }
    }
}