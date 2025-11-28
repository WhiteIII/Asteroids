using _Project.Scripts.ViewModel.Implementation;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.View.Implementation
{
    public class LoadingWindow : Window<LoadingWindowViewModel>
    {
        [SerializeField] private Slider _slider;

        protected override void OnSetup() =>
            ViewModel
                .LoadingProgress
                .Subscribe(x => _slider.value = x)
                .AddTo(this);

        protected override void OnOpenAnimationStart() => 
            ViewModel.ResetLoadingProgress();
    }
}
