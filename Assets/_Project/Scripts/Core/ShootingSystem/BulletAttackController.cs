using System;
using _Project.Scripts.Core.Services.Targets;
using R3;
using Zenject;

namespace _Project.Scripts.Core.ShootingSystem
{
    internal class BulletAttackController : IInitializable, IDisposable
    {
        private readonly CollisionHandler _collisionHandler;
        private readonly CompositeDisposable _disposable = new();

        public readonly Subject<Unit> OnHit = new();
        
        private Type _targetType;

        public BulletAttackController(CollisionHandler collisionHandler) => 
            _collisionHandler = collisionHandler;

        public void Initialize() =>
            _collisionHandler
                .OnTouchTarget
                .Subscribe(Attack)
                .AddTo(_disposable);

        public void Dispose() => 
            _disposable.Dispose();

        public void SetTarget<T>()
            where T : IKillableTarget
        {
            _targetType = typeof(T);
        }
        
        private void Attack(ITarget target)
        {
            if (target is IKillableTarget killableTarget)
            {
                if (killableTarget.GetType() == _targetType)
                    killableTarget.Kill();
            }
            OnHit.OnNext(Unit.Default);
        }
    }
}