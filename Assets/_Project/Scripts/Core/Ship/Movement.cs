using R3;
using UnityEngine;

namespace _Project.Scripts.Core.Ship
{
    [RequireComponent(typeof(Rigidbody))]
    internal class Movement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        
        public ReactiveProperty<Vector3> Position { get; private set; }
        
        public void Initialize() => 
            Position.Value = transform.position;

        private void FixedUpdate()
        {
            //_rigidbody.AddForce();   
        }
    }
}