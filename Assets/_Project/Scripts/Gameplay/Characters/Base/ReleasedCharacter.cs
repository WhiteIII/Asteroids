using _Project.Scripts.Gameplay.Services.Components;
using _Project.Scripts.Gameplay.Services.ObjectPools.Base;
using _Project.Scripts.Gameplay.Services.Targets.Base;
using R3;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Characters.Base
{
    [RequireComponent(typeof(CollisionHandler))]
    public abstract class ReleasedCharacter : Character, IItemWithId<string>
    {
        public Subject<string> Release { get; } = new();
        public readonly CompositeDisposable Disposable = new();

        private CollisionHandler _collisionHandler;
        private string _id;

        protected void SetupReleaseCharacter()
        {
            _collisionHandler = GetComponent<CollisionHandler>();
            
            _collisionHandler
                .OnTouchTarget
                .Subscribe(OnTouchTarget)
                .AddTo(Disposable);             
        }
        
        private void OnDestroy() => 
            Disposable.Dispose();
        
        public void SetID(string id) => 
            _id = id;
        
        protected void ReleaseCharacter() => 
            Release.OnNext(_id);
        
        protected abstract void OnTouchTarget(ITarget target);
    }
}