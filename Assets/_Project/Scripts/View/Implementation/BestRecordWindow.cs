using _Project.Scripts.ViewModel.Implementation;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.View.Implementation
{
    public class BestRecordWindow : Window<PlayerBestRecordViewModel>
    {
        [SerializeField] private TMP_Text _bestRecordText;

        protected override void OnSetup() =>
            SetCurrentScore();

        protected override UniTask OnOpenAnimationStart()
        {
            SetCurrentScore();
            return UniTask.CompletedTask;
        }

        private void SetCurrentScore() =>
            _bestRecordText.text = $"Score: {ViewModel.BestRecord}";
    }
}