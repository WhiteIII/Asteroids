using UnityEngine;
using static UnityEngine.Time;

namespace _Project.Scripts.Gameplay.Services.Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SimpleAiMovement : MonoBehaviour, IAiAgentMovement
    {
        private Rigidbody2D _rigidbody;
        private float _movementSpeed;
        
        private void Awake() =>
            _rigidbody = GetComponent<Rigidbody2D>();

        public void Initialize(float movementSpeed) =>
            _movementSpeed = movementSpeed;

        public void MoveTo(Vector2 shipPosition) =>
            _rigidbody.MovePosition(
                _rigidbody.position + 
                (shipPosition - _rigidbody.position).normalized * 
                _movementSpeed * 
                deltaTime);

        public void SetPosition(Vector2 position) => 
            transform.position = position;
    }
}