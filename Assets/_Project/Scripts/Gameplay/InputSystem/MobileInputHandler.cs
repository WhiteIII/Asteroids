using System;
using _Project.Scripts.ViewModel.Implementation;
using R3;
using Zenject;
using Unit = R3.Unit;

namespace _Project.Scripts.Gameplay.InputSystem
{
    public class MobileInputHandler : IReadOnlyInputHandler, IInputHandlerController, IDisposable, IInitializable
    {
        public ReadOnlyReactiveProperty<float> Vertical { get; }
        public ReadOnlyReactiveProperty<float> Horizontal { get; }
        public Observable<Unit> OnSpacePressed { get; }
        public Observable<Unit> OnEKeyPressed { get; }

        private readonly CompositeDisposable _disposables = new();
        
        private bool _isActive;
        
        public MobileInputHandler(IMobileInputViewModel mobileInputViewModel)
        {
            Vertical = Observable
                .EveryValueChanged(mobileInputViewModel.Axis, axis => axis.CurrentValue.y)
                .Where(_ => _isActive)
                .ToReadOnlyReactiveProperty()
                .AddTo(_disposables);
            Horizontal = Observable
                .EveryValueChanged(mobileInputViewModel.Axis, axis => axis.CurrentValue.x)
                .Where(_ => _isActive)
                .ToReadOnlyReactiveProperty()
                .AddTo(_disposables);
            OnSpacePressed = mobileInputViewModel
                .FireButtonClicked
                .Where(_ => _isActive);
            OnEKeyPressed = mobileInputViewModel
                .LazerFireButtonClicked
                .Where(_ => _isActive);
        }

        public void Initialize() => 
            Enable();

        public void Dispose() => 
            _disposables.Dispose();

        public void Enable() => 
            _isActive = true;

        public void Disable() => 
            _isActive = false;
    }
}