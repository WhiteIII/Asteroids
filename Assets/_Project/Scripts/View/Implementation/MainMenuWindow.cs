using _Project.Scripts.View.ViewBaseLogic;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.View.Implementation
{
    public class MainMenuWindow : Window<MainMenuViewModel>
    {
        [SerializeField] private Button _playButton;

        private void Start() => 
            _playButton.onClick.AddListener(ViewModel.GoToMenu);

        private void OnDestroy() => 
            _playButton.onClick.RemoveListener(ViewModel.GoToMenu);

        protected override void Enable() => 
            _playButton.enabled = true;

        protected override void Disable() => 
            _playButton.enabled = false;
    }
}
