using _Project.Scripts.ViewModel.Implementation;
using R3;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.View.Implementation
{
    public class PlayerPointsWindow : Window<PlayerPointsViewModel>
    {
        [SerializeField] private TMP_Text _pointsText;
                
        protected override void OnSetup() =>
            ViewModel
                .OnPointsChanged
                .Subscribe(x => _pointsText.text = $"Points: {x}")
                .AddTo(this);
    }
}