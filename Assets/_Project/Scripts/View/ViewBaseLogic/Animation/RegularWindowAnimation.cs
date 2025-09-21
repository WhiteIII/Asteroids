using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.View.ViewBaseLogic.Animation
{
    public class RegularWindowAnimation : MonoBehaviour, IWindowAnimation
    {
        public async UniTask PlayShowAnimation() =>
            gameObject.SetActive(true);
        
        public async UniTask PlayHideAnimation() =>
            gameObject.SetActive(false);
    }
}