using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Services.Components.View
{
    public class ImmortalAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject _fbx;
        [SerializeField] private float _coolDown;

        private CancellationTokenSource _tokenSource;

        public void StartAnimation()
        {
            _tokenSource = new CancellationTokenSource();
            PlayAnimationAsync(_tokenSource.Token).Forget();
        }

        public void StopAnimation()
        {
            _tokenSource.Cancel();
            _fbx.SetActive(true);
        }

        private async UniTask PlayAnimationAsync(CancellationToken token)
        {
            while (token.IsCancellationRequested == false)
            {
                _fbx.SetActive(true);
                await UniTask.WaitForSeconds(_coolDown, false, PlayerLoopTiming.Update, token);
                _fbx.SetActive(false);
                await UniTask.WaitForSeconds(_coolDown, false, PlayerLoopTiming.Update, token);
            }
        }
    }
}