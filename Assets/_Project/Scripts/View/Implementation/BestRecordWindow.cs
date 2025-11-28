using _Project.Scripts.ViewModel.Implementation;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.View.Implementation
{
    public class BestRecordWindow : Window<PlayerBestRecordViewModel>
    {
        [SerializeField] private TMP_Text _bestRecordText;

        protected override void OnSetup() =>
            SetCurrentScore();

        protected override void OnOpenAnimationStart() =>
            SetCurrentScore();

        private void SetCurrentScore() =>
            _bestRecordText.text = $"Score: {ViewModel.BestRecord}";
    }
}