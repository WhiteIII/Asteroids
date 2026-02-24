using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.View.Implementation
{
    public class PurchaseFailedScreen : Window
    {
        [SerializeField] private Button _closeButton;

        public async UniTask OpenAndWaitToCloseAsync()
        {
            await OpenAsync();
            await WaitForClickAsync(this.GetCancellationTokenOnDestroy());
            await CloseAsync();
        }

        private UniTask WaitForClickAsync(CancellationToken token = default)
        {
            UniTaskCompletionSource uniTaskCompletionSource = new();

            void Handler()
            {
                _closeButton.onClick.RemoveListener(Handler);
                uniTaskCompletionSource.TrySetResult();
            }

            _closeButton.onClick.AddListener(Handler);
            token.Register(() =>
            {
                _closeButton.onClick.RemoveListener(Handler);
                uniTaskCompletionSource.TrySetCanceled();
            });

            return uniTaskCompletionSource.Task;
        }
    }
}