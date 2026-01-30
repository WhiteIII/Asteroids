using _Project.Scripts.View.Services;
using _Project.Scripts.ViewModel.Implementation;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.View.Implementation
{
    public class MenuWindow : Window<MenuViewModel>
    {
        [SerializeField] private Button _playButton;
        
        private WindowsRepository _repository;

        [Inject] private void Construct(WindowsRepository repository) => 
            _repository = repository;
        
        protected override void OnSetup() => 
            _playButton.onClick.AddListener(ViewModel.GoToGameplay);

        protected override void OnDestroyMethod() => 
            _playButton.onClick.RemoveListener(ViewModel.GoToGameplay);

        protected override void OnOpenAnimationStart()
        {
            _playButton.enabled = true;
            if (ViewModel.WhetherToShowBestRecord)
                _repository.Get<BestRecordWindow>().OpenAsync().Forget();
        }

        protected override void OnCloseAnimationEnd()
        {
            _playButton.enabled = false;
            
            if (ViewModel.WhetherToShowBestRecord == false)
                return;
            
            BestRecordWindow bestRecordWindow = _repository.Get<BestRecordWindow>();
            if (bestRecordWindow.IsOpen) 
                bestRecordWindow.CloseAsync().Forget();
        }
    }
}