using _Project.Scripts.ViewModel.Implementation;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.View.Implementation
{
    public class MenuWindow : Window<MenuViewModel>
    {
        [SerializeField] private Button _playButton;

        protected override void OnSetup() => 
            _playButton.onClick.AddListener(ViewModel.GoToGameplay);

        protected override void OnDestroyMethod() => 
            _playButton.onClick.RemoveListener(ViewModel.GoToGameplay);

        protected override void Enable() => 
            _playButton.enabled = true;

        protected override void Disable() => 
            _playButton.enabled = false;
    }
}
