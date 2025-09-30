using _Project.Scripts.Core.GameLoopSystem;
using _Project.Scripts.Core.InputSystem;
using UnityEngine;
using static UnityEngine.Time;

namespace _Project.Scripts.Core.Ship
{
    public class ShipMovement : MonoBehaviour, IUpdatable
    {
        private Rigidbody2D _rigidbody;
        private IInputHandler _inputHandler;
        private float _speed;

        public void Initialize(
            float speed, 
            IInputHandler inputHandler)
        {
            _speed = speed;
            _inputHandler = inputHandler;
        }

        private void Awake() => 
            _rigidbody = GetComponent<Rigidbody2D>();
        
        public void GameLoopUpdate() =>
            Move();

        public void SetPosition(Vector2 position) => 
            _rigidbody.MovePosition(position);

        private void Move() =>
            _rigidbody.AddForce(
                _rigidbody.transform.rotation *
                Vector2.up *
                Mathf.Max(_inputHandler.Vertical.Value, 0f) * 
                _speed * 
                deltaTime, ForceMode2D.Force);
    }
}