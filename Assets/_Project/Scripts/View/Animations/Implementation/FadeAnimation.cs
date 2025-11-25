using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.View
{
    internal class FadeAnimation
    {   
        private readonly CanvasGroup _canvasGroup;
        private readonly float _duration;
        
        public FadeAnimation(CanvasGroup canvasGroup, float duration)
        {
            _canvasGroup = canvasGroup;
            _duration = duration;
        }
        
        public async UniTask PlayAnimationAsync(float from, float to) 
        {
            _canvasGroup.alpha = from;
            await _canvasGroup.DOFade(to, _duration).AsyncWaitForCompletion();
        }
    }
}