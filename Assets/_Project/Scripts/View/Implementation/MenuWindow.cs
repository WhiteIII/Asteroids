using _Project.Scripts.View.Services;
using _Project.Scripts.ViewModel.Implementation;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using R3;

namespace _Project.Scripts.View.Implementation
{
    public class MenuWindow : Window<MenuViewModel>
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;
        
        private WindowsRepository _repository;

        [Inject] private void Construct(WindowsRepository repository) => 
            _repository = repository;

        protected override void OnSetup()
        {
            _playButton.OnClickAsObservable().Subscribe(_ => ViewModel.GoToGameplay()).AddTo(this);
            _quitButton.OnClickAsObservable().Subscribe(_ => Application.Quit()).AddTo(this);
        } 

        protected override async UniTask OnOpenAnimationStart()
        {
            await ViewModel.GetBestRecordAsync();
            _playButton.enabled = true;
            if (ViewModel.WhetherToShowBestRecord)
                await _repository.Get<BestRecordWindow>().OpenAsync();
        }

        protected override UniTask OnCloseAnimationEnd()
        {
            _playButton.enabled = false;
            
            if (ViewModel.WhetherToShowBestRecord == false)
                return UniTask.CompletedTask;
            
            BestRecordWindow bestRecordWindow = _repository.Get<BestRecordWindow>();
            if (bestRecordWindow.IsOpen) 
                bestRecordWindow.CloseAsync().Forget();
            return UniTask.CompletedTask;
        }
    }
}