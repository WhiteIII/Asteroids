using _Project.Scripts.Core.GameLoopSystem;
using UnityEngine;
using static UnityEngine.Time;

namespace _Project.Scripts.Core.Services.Components
{
    public class RigidbodyMovement : MonoBehaviour, IUpdatable
    {
        private Rigidbody2D _rigidbody;
        private Vector2 _direction;
        private float _speed;

        public Vector2 Position => _rigidbody.position;

        private void Awake() => 
            _rigidbody = GetComponent<Rigidbody2D>();
        
        public void GameLoopUpdate() => 
            _rigidbody.MovePosition(_rigidbody.position + _direction * _speed * deltaTime);

        public void SetMovementSpeed(float speed) => 
            _speed = speed;

        public void SetDirection(Vector2 diraction) =>
            _direction = diraction;
        
        public void SetPosition(Vector2 position) =>
            _rigidbody.transform.position = position;
        
        public void SetRotation(Quaternion rotation) =>
            _rigidbody.transform.rotation = rotation;
    }
}
