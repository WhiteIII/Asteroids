using _Project.Scripts.Core.Services.Targets;
using UnityEngine;

namespace _Project.Scripts.Core.Ship
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(ShipTarget))]
    internal class Ship : MonoBehaviour
    {
        private ShipMovement _shipMovement;
        private AttackController _attackController;

        public void Initialize(
            ShipMovement shipMovement,
            AttackController attackController)
        {
            _shipMovement = shipMovement;
            _attackController = attackController;
        }
        
        public Vector2 Position => transform.position;
        
        public void SetPosition(Vector2 position) => 
            _shipMovement.SetPosition(position);
        
        public void Enable() => 
            gameObject.SetActive(true);
        
        public void Disable() =>
            gameObject.SetActive(false);
    }
}
