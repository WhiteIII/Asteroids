using System.Threading;
using _Project.Scripts.View.Animations.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.View.Animations.Implementation
{
    internal class RegularAnimation : MonoBehaviour, IWindowAnimation
    {
        public async UniTask PlayCloseAnimationAsync(CancellationToken cancellationToken = default)
        {
            gameObject.SetActive(false);
            await UniTask.Yield(cancellationToken);
        }

        public async UniTask PlayShowAnimationAsync(CancellationToken cancellationToken = default)
        {
            gameObject.SetActive(true);
            await UniTask.Yield(cancellationToken);
        }
    }
}