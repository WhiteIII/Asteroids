using _Project.Scripts.Core.InputSystem;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Ship
{
    [RequireComponent(typeof(Rigidbody))]
    internal class Movement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;
        
        private IInputHandler _inputHandler;
        
        public ReactiveProperty<Vector3> Position { get; private set; }
        
        [Inject]
        private void Construct(IInputHandler inputHandler) =>
            _inputHandler = inputHandler;
        
        public void Initialize() => 
            Position.Value = transform.position;

        private void FixedUpdate() => 
            _rigidbody.AddForce(_inputHandler.MoveAxis.Value * _speed, ForceMode.Acceleration);
    }
}