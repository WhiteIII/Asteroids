using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.InputSystem
{
    public class KeyBoardInputHandler : IReadOnlyInputHandler, IInputHandlerController, ITickable
    {
        private const string VERTICAL = "Vertical";
        private const string HORIZONTAL = "Horizontal";
        
        public ReadOnlyReactiveProperty<float> Vertical => _vertical;
        public ReadOnlyReactiveProperty<float> Horizontal => _horizontal;
        public Observable<Unit> OnSpacePressed => _onSpacePressed;
        public Observable<Unit> OnEKeyPressed => _onEKeyPressed;
        
        private readonly Subject<Unit> _onSpacePressed = new();
        private readonly Subject<Unit> _onEKeyPressed = new();
        private readonly ReactiveProperty<float> _vertical = new();
        private readonly ReactiveProperty<float> _horizontal = new();

        private bool _isActive;
        
        public void Enable() => 
            _isActive = true;
        
        public void Disable() => 
            _isActive = false;
        
        public void Tick()
        {
            if (_isActive == false)
                return;
            
            _vertical.Value = Input.GetAxis(VERTICAL);
            _horizontal.Value = Input.GetAxis(HORIZONTAL);
            
            if (Input.GetKeyDown(KeyCode.Space))
                _onSpacePressed.OnNext(Unit.Default);
            if (Input.GetKeyDown(KeyCode.E))
                _onEKeyPressed.OnNext(Unit.Default);
        }
    }
}