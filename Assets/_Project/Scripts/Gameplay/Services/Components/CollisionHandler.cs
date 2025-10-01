using _Project.Scripts.Core.Services.Targets;
using R3;
using UnityEngine;

namespace _Project.Scripts.Core.Services.Components
{
    internal class CollisionHandler : MonoBehaviour
    {
        public Subject<ITarget> OnTouchTarget { get; } = new();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ITarget target))
                OnTouchTarget.OnNext(target);
        }
    }
}