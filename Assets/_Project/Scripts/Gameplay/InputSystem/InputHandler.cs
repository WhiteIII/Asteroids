using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.InputSystem
{
    public class InputHandler : IInputHandler, ITickable
    {
        private const string VERTICAL = "Vertical";
        private const string HORIZONTAL = "Horizontal";
        
        public ReactiveProperty<float> Vertical { get; } = new();
        public ReactiveProperty<float> Horizontal { get; } = new();
        public Subject<Unit> OnSpacePressed { get; } = new();
        public Subject<Unit> OnEKeyPressed { get; } = new();

        public void Tick()
        {
            Vertical.Value = Input.GetAxis(VERTICAL);
            Horizontal.Value = Input.GetAxis(HORIZONTAL);
            
            if (Input.GetKeyDown(KeyCode.Space))
                OnSpacePressed.OnNext(Unit.Default);
            if (Input.GetKeyDown(KeyCode.E))
                OnEKeyPressed.OnNext(Unit.Default);
        }
    }

    public interface IInputHandler
    {
        ReactiveProperty<float> Vertical { get; }
        ReactiveProperty<float> Horizontal { get; }
        Subject<Unit> OnSpacePressed { get; } 
        Subject<Unit> OnEKeyPressed { get; }
    }
}
