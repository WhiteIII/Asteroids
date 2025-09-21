using _Project.Scripts.Core.Services.Targets;
using R3;
using UnityEngine;

namespace _Project.Scripts.Core.ShootingSystem
{
    internal class CollisionHandler : MonoBehaviour
    {
        public Subject<ITarget> OnTouchTarget { get; } = new();

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out ITarget target))
                OnTouchTarget.OnNext(target);
        }
    }
}