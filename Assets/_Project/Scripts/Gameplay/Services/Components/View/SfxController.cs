using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Services.Components.View
{
    public class SfxController : MonoBehaviour
    { 
        [SerializeField] private AudioClip _audioClip;
        
        private AudioSource _audioSource;
        
        [Inject] private void Construct(AudioSource audioSource) => 
            _audioSource = audioSource;

        public async UniTask PlayAudioClipAsync()
        {
            _audioSource.PlayOneShot(_audioClip);
            await UniTask.WaitForSeconds(_audioClip.length);
        }
    }
}
