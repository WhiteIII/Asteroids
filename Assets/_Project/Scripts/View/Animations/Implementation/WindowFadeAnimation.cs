using _Project.Scripts.View.Animations.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.View.Animations.Implementation
{
    [RequireComponent(typeof(CanvasGroup))]
    internal class WindowFadeAnimation : MonoBehaviour, IWindowAnimation
    {
        [SerializeField] private float _duration = 0.4f;

        private FadeAnimation _fadeAnimation;
        
        private void Awake() =>
            _fadeAnimation = new FadeAnimation(GetComponent<CanvasGroup>(), _duration);

        public UniTask PlayCloseAnimationAsync() => 
            _fadeAnimation.PlayAnimationAsync(1f, 0f);

        public UniTask PlayShowAnimationAsync() => 
            _fadeAnimation.PlayAnimationAsync(0f, 1f);
    }
}