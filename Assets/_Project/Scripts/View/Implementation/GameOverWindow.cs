using _Project.Scripts.ViewModel.Implementation;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.View.Implementation
{
    public class GameOverWindow : Window<GameOverWindowViewModel>
    {
        [SerializeField] private Button _goToMenuButton;
        [SerializeField] private Button _reviveButton;

        protected override void OnSetup()
        {
            Observable<Unit> goToMenuObservable = _goToMenuButton.OnClickAsObservable();
            Observable<Unit> reviveObservable = _reviveButton.OnClickAsObservable();
            
            goToMenuObservable.Subscribe(_ => ViewModel.GoToMenu().Forget()).AddTo(this);
            reviveObservable.Subscribe(_ => ViewModel.Revive().Forget()).AddTo(this);
        }

        protected override void OnOpenAnimationStart()
        {
            _goToMenuButton.enabled = true;

            if (ViewModel.ReviveIsAcceptable)
            {
                _reviveButton.gameObject.SetActive(true);   
                _reviveButton.enabled = true;
            }
            else 
                _reviveButton.gameObject.SetActive(false);
        }

        protected override void OnCloseAnimationEnd()
        {
            _goToMenuButton.enabled = false;
            _reviveButton.enabled = false;
        }
    }
}