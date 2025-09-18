using _Project.Scripts.Core.Services.Targets;
using R3;
using UnityEngine;

namespace _Project.Scripts.Core.ShootingSystem
{
    internal class CollisionHandler : MonoBehaviour
    {
        public readonly Subject<ITarget> OnTouchTarget = new();
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ITarget target))
                OnTouchTarget.OnNext(target);
        }
    }
}