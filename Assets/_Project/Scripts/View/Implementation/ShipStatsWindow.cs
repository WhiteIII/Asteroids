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
        [SerializeField] private TMP_Text _rotationText;
        [SerializeField] private TMP_Text _velocityText;

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
                .Subscribe(x => _positionText.text = $"Ship position: {x}")
                .AddTo(this);
            ViewModel
                .OnRotationChanged
                .Subscribe(x =>  _rotationText.text = $"Ship rotation: {x}")
                .AddTo(this);
            ViewModel
                .OnVelocityChanged
                .Subscribe(x => _velocityText.text = $"Ship velocity: {x:F}")
                .AddTo(this);
        }
    }
}