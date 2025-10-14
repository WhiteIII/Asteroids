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
        [SerializeField] private TMP_Text _positionText;

        protected override void OnSetup()
        {
            ViewModel
                .OnChargeCountChanged
                .Subscribe(x => _chargeCountText.text = $"Lazer charge count: {x} / {ViewModel.MaxChargeCount}")
                .AddTo(this);
            ViewModel
                .OnCooldownChanged
                .Subscribe(x => _cooldownText.text = $"Lazer cooldown: {x:F} / {ViewModel.LazerCooldown}")
                .AddTo(this);
            ViewModel
                .OnPositionChanged
                .Subscribe(x => _positionText.text = $"Lazer position: {x}")
                .AddTo(this);
        }
    }
}