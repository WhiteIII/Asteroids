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
    [RequireComponent(typeof(AsteroidTarget))]
    [RequireComponent(typeof(RigidbodyMovement))]
    public class Asteroid : 
        MonoBehaviour,
        ICharacter,
        IEnableAndDisableItem,
        IItemWithId<string>,
        IInitializableUpdatableObject
    {
        public Subject<string> Release { get; } = new();

        private readonly CompositeDisposable _disposable = new();
        
        private RigidbodyMovement _movement;
        private CollisionHandler _collisionHandler;
        private AsteroidTarget _asteroidTarget;
        private string _id;
        
        public Vector2 Position => _movement.Position;

        private void Awake()
        {
            _movement = GetComponent<RigidbodyMovement>();
            _asteroidTarget = gameObject.GetComponent<AsteroidTarget>();
            _collisionHandler = GetComponent<CollisionHandler>();

            _collisionHandler
                .OnTouchTarget
                .Subscribe(OnTargetTouch)
                .AddTo(_disposable);
            _asteroidTarget
                .OnKill
                .Subscribe(Release.OnNext)
                .AddTo(_disposable);
        } 
        
        public IUpdatable[] GetAllUpdatableObjects() => 
            new IUpdatable[] { _movement };

        public void SetID(string id)
        {
            _id = id;
            _asteroidTarget.SetId(_id);
        }
        
        private void OnDestroy() => 
            _disposable.Dispose();
        
        public void SendAsteroidOnDirection(Vector2 direction, float speed)
        {
            _movement.SetDirection(direction);
            _movement.SetMovementSpeed(speed);
        }

        public void SetPosition(Vector2 position) => 
            _movement.SetPosition(position);

        public void Enable() => 
            gameObject.SetActive(true);
        
        public void Disable() =>
            gameObject.SetActive(false);

        private void OnTargetTouch(ITarget target)
        {
            if (target is ShipTarget shipTarget)
                shipTarget.Kill();
            else if (target is Barrier _)
                Release.OnNext(_id);
        }
    }
}
