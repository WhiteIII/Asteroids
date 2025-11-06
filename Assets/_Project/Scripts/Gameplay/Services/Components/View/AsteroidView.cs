using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Services.Components.View
{
    public class AsteroidView : MonoBehaviour
    {
        [SerializeField] private float _rotationDuration;
        
        private Tween _rotationTween;

        private void OnDestroy() => 
            StopAnimation();
        
        public void StartAnimation() =>
            _rotationTween = transform
                .DORotate(new Vector3(0f, 0f, 180f), _rotationDuration)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental);
        
        public void StopAnimation() =>
            _rotationTween.Kill();
    }
}
