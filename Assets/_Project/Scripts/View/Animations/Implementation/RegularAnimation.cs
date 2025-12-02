using _Project.Scripts.View.Animations.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.View.Animations.Implementation
{
    internal class RegularAnimation : MonoBehaviour, IWindowAnimation
    {
        public async UniTask PlayCloseAnimationAsync()
        {
            gameObject.SetActive(false);
            await UniTask.Yield();
        }

        public async UniTask PlayShowAnimationAsync()
        {
            gameObject.SetActive(true);
            await UniTask.Yield();
        }
    }
}