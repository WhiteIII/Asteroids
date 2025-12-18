using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.InputSystem;
using UnityEngine;
using Zenject;
using static UnityEngine.Time;

namespace _Project.Scripts.Gameplay.ShipBase
{
    public class ShipMovement : MonoBehaviour, IUpdatable
    {
        private Rigidbody2D _rigidbody;
        private IReadOnlyInputHandler _readOnlyInputHandler;
        private float _speed;

        [Inject]
        private void Construct(IReadOnlyInputHandler readOnlyInputHandler, ShipStatsData stats)
        {
            _speed = stats.MovementSpeed;
            _readOnlyInputHandler = readOnlyInputHandler;
        } 

        private void Awake() => 
            _rigidbody = GetComponent<Rigidbody2D>();
        
        public void GameLoopUpdate() =>
            Move();

        public void SetPosition(Vector2 position) => 
            _rigidbody.MovePosition(position);

        public void StopShip() => 
            _rigidbody.linearVelocity = Vector2.zero;

        private void Move() =>
            _rigidbody.AddForce(
                _rigidbody.transform.rotation * 
                Vector2.up * 
                (Mathf.Max(_readOnlyInputHandler.Vertical.CurrentValue, 0f) * _speed * deltaTime), ForceMode2D.Force);
    }
}