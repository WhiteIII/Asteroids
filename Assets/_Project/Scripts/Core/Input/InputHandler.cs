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
     
        public ReactiveProperty<Vector2> MoveAxis { get; private set; }

        public void Initialize() => 
            MoveAxis.Value = _inputAxis;

        public void Tick() =>
            _inputAxis = new Vector2(
                Input.GetAxis(HORIZONTAL), 
                Input.GetAxis(VERTICAL));
    }

    public interface IInputHandler
    {
        ReactiveProperty<Vector2> MoveAxis { get; }
    }
}