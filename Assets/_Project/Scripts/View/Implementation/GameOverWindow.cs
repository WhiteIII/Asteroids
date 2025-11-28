using _Project.Scripts.ViewModel.Implementation;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.View.Implementation
{
    public class GameOverWindow : Window<GameOverWindowViewModel>
    {
        [SerializeField] private Button _goToMenuButton;
        
        protected override void OnSetup() => 
            _goToMenuButton.onClick.AddListener(ViewModel.GoToMenu);

        protected override void OnDestroyMethod() => 
            _goToMenuButton.onClick.RemoveListener(ViewModel.GoToMenu);

        protected override void OnOpenAnimationStart() => 
            _goToMenuButton.enabled = true;

        protected override void OnCloseAnimationEnd() =>
            _goToMenuButton.enabled = false;
    }
}