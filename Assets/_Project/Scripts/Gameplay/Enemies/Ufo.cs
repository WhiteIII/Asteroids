using _Project.Scripts.Gameplay.Services.ObjectPools.Base;
using _Project.Scripts.Gameplay.Services.Repositories;
using _Project.Scripts.Gameplay.Ship;
using _Project.Scripts.Gameplay.Enemies.Base;
using _Project.Scripts.Gameplay.Services.Components;
using _Project.Scripts.Gameplay.Services.Targets;
using _Project.Scripts.Gameplay.Services.Targets.Base;
using _Project.Scripts.Gameplay.Services.Targets.Implementation;
using R3;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(UfoTarget))]
    public class Ufo : KillableCharacter
    {
        private AttackController _attackController;
        private ICharacterRepository _characterRepository;
        private AiAgentMovement _movement;
        
        public void Initialize(float bulletFlyingSpeed)
        {
            SetupKillableCharacter();
            _attackController.Initialize(bulletFlyingSpeed);
        }
        
        public void Attack() => 
            _attackController.Shoot<UfoTarget, AsteroidTarget>(_characterRepository.Ship.Position - Position);

        public void MoveToPlayer() => 
            _movement.MoveTo(_characterRepository.Ship.Position);

        public void StopMoving() =>
            _movement.StopMoving();
        
        protected override void OnTouchTarget(ITarget target)
        {
            throw new System.NotImplementedException();
        }
    }
}