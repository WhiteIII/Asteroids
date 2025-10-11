using _Project.Scripts.ViewModel.Implementation;
using R3;
using UnityEngine;
using TMPro;

namespace _Project.Scripts.View.Implementation
{
    public class ShipStatsWindow : Window<ShipStatsViewModel>
    {
        [SerializeField] private TMP_Text _chargeCountText;
        [SerializeField] private TMP_Text _cooldownText;

        private readonly CompositeDisposable _disposables = new();
        
        protected override void OnSetup()
        {
            ViewModel
                .OnChargeCountChanged
                .Subscribe(x => _chargeCountText.text = $"Lazer charge count: {x} / {ViewModel.MaxChargeCount}")
                .AddTo(_disposables);
            ViewModel
                .OnCooldownChanged
                .Subscribe(x => _cooldownText.text = $"Lazer cooldown: {x:F} / {ViewModel.LazerCooldown}")
                .AddTo(_disposables);
        }
    }
}