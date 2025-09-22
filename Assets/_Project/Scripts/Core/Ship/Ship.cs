using _Project.Scripts.Core.InputSystem;
using _Project.Scripts.Core.Services.Targets;
using R3;
using UnityEngine;

namespace _Project.Scripts.Core.Ship
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(ShipTarget))]
    public class Ship : MonoBehaviour
    {
        private readonly CompositeDisposable _disposables = new();
        
        private ShipMovement _shipMovement;
        private AttackController _attackController;
        private RotationController _rotationController;
        
        public Vector2 Position => transform.position;

        public void Initialize(
            ShipMovement shipMovement,
            AttackController attackController,
            IInputHandler inputHandler,
            RotationController rotationController)
        {
            _shipMovement = shipMovement;
            _attackController = attackController;
            _rotationController = rotationController;
            inputHandler
                .OnBackspacePressed
                .Subscribe(_ => Shoot())
                .AddTo(_disposables);
        }
        
        private void OnDestroy() => 
            _disposables.Dispose();
        
        public void SetPosition(Vector2 position) => 
            _shipMovement.SetPosition(position);
        
        public void SetRotation(Quaternion rotation) =>
            _rotationController.SetRotation(rotation);
        
        public void Enable() => 
            gameObject.SetActive(true);
        
        public void Disable() =>
            gameObject.SetActive(false);

        private void Shoot() => 
            _attackController.Shoot();
    }
}
