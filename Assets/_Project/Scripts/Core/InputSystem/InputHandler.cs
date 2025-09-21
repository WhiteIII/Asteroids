using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.InputSystem
{
    public class InputHandler : IInputHandler, ITickable
    {
        private const string VERTICAL = "Vertical";
        private const string HORIZONTAL = "Horizontal";
        
        public ReactiveProperty<float> Vertical { get; } = new();
        public ReactiveProperty<float> Horizontal { get; } = new();
        public Subject<Unit> OnBackspacePressed { get; } = new();
        
        public void Tick()
        {
            Vertical.Value = Input.GetAxis(VERTICAL);
            Horizontal.Value = Input.GetAxis(HORIZONTAL);
            
            if (Input.GetKey(KeyCode.Backspace))
                OnBackspacePressed.OnNext(Unit.Default);
        }
    }

    public interface IInputHandler
    {
        ReactiveProperty<float> Vertical { get; }
        ReactiveProperty<float> Horizontal { get; }
        Subject<Unit> OnBackspacePressed { get; } 
    }
}
