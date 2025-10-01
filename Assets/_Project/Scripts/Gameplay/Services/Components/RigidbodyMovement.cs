using _Project.Scripts.Core.GameLoopSystem;
using UnityEngine;
using static UnityEngine.Time;

namespace _Project.Scripts.Core.Services.Components
{
    public class RigidbodyMovement : MonoBehaviour, IUpdatable
    {
        private Rigidbody2D _rigidbody;
        private float _speed;

        public Vector2 Direction { get; private set; }
        public Vector2 Position => transform.position;

        private void Awake() => 
            _rigidbody = GetComponent<Rigidbody2D>();
        
        public void GameLoopUpdate() => 
            _rigidbody.MovePosition(_rigidbody.position + Direction * _speed * deltaTime);

        public void SetMovementSpeed(float speed) => 
            _speed = speed;

        public void SetDirection(Vector2 direction) =>
            Direction = direction;
        
        public void SetPosition(Vector2 position) =>
            transform.position = position;
        
        public void SetRotation(Quaternion rotation) =>
            _rigidbody.transform.rotation = rotation;
    }
}
