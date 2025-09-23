using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.View
{
    internal class RegularAnimation : MonoBehaviour, IWindowAnimation
    {
        public async UniTask PlayCloseAnimationAsync() => 
            gameObject.SetActive(false);

        public async UniTask PlayShowAnimationAsync() =>
            gameObject.SetActive(true);
    }
}