using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Services.Components.View
{
    [RequireComponent(typeof(Animator))]
    public class DeathAnimationController : MonoBehaviour
    {
        private const string PLAY_DEATH_ANIMATION = "PlayDeathAnimation";
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            gameObject.SetActive(false);
        }

        public async UniTask PlayAnimationAsync()
        {
            gameObject.SetActive(true);
            _animator.SetTrigger(PLAY_DEATH_ANIMATION);
            await UniTask.WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
            gameObject.SetActive(false);
        }
    }
}
