using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.View
{
    internal class MoveAnimation
    {
        private readonly RectTransform _rectTransform;
        private readonly float _duration;

        public MoveAnimation(RectTransform rectTransform, float duration)
        {
            _rectTransform = rectTransform;
            _duration = duration;
        }

        public async UniTask PlayAnimationAsync(Vector2 from, Vector2 to)
        {
            _rectTransform.anchoredPosition = from;
            await _rectTransform.DOAnchorPos(to, _duration).AsyncWaitForCompletion();
        }
    }
}