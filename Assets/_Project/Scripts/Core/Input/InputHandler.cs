using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.InputSystem
{
    public class InputHandler : ITickable, IInitializable, IInputHandler
    {
        private const string VERTICAL = "Vertical";
        private const string HORIZONTAL = "Horizontal";
        
        private Vector2 _inputAxis;

        public ReactiveProperty<Vector2> MoveAxis { get; } = new();
        public Subject<Unit> OnBackspacePressed { get; } = new();

        public void Initialize() => 
            MoveAxis.Value = _inputAxis;

        public void Tick()
        {
            _inputAxis.x = Input.GetAxis(HORIZONTAL); 
            _inputAxis.y = Input.GetAxis(VERTICAL);
            if (Input.GetKey(KeyCode.Backspace))
                OnBackspacePressed.OnNext(Unit.Default);
            MoveAxis.Value = _inputAxis;
        }
    }

    public interface IInputHandler
    {
        ReactiveProperty<Vector2> MoveAxis { get; }
        Subject<Unit> OnBackspacePressed { get; }
    }
}