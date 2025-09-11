using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.InputSystem
{
    internal class InputHandler : MonoBehaviour, IInitializable, IInputHandler
    {
        private const string VERTICAL = "Vertical";
        private const string HORIZONTAL = "Horizontal";
        
        private Vector2 _inputAxis;
        private bool _isActive;
     
        public ReactiveProperty<Vector2> MoveAxis { get; private set; }

        public void Initialize()
        {
            Enable();
            MoveAxis.Value = _inputAxis;
        }

        private void Update()
        {
            if (_isActive == false)
                return;

            _inputAxis = new Vector2(Input.GetAxis(HORIZONTAL), Input.GetAxis(VERTICAL));    
        }
        
        public void Enable() =>
            _isActive = true;
        
        public void Disable() =>
            _isActive = false;
    }

    internal interface IInputHandler
    {
        ReactiveProperty<Vector2> MoveAxis { get; }
    }
}