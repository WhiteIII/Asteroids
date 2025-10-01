using System;
using _Project.Scripts.Core.GameLoopSystem;
using _Project.Scripts.Core.InputSystem;
using _Project.Scripts.Core.Services.Targets;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Ship
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(ShipTarget))]
    [RequireComponent(typeof(AttackController))]
    [RequireComponent(typeof(ShipMovement))]
    [RequireComponent(typeof(RotationController))]
    public class Ship : MonoBehaviour, IInitializableUpdatableObject
    {
        private readonly CompositeDisposable _disposables = new();
        
        private ShipMovement _shipMovement;
        private AttackController _attackController;
        private RotationController _rotationController;
        private IInputHandler _inputHandler;
        
        public Vector2 Position => transform.position;

        [Inject] private void Construct(IInputHandler inputHandler) =>
            _inputHandler = inputHandler;
        
        public void Initialize(
            float movementSpeed,
            float bulletFlyingSpeed,
            float rotationSpeed)
        {
            _shipMovement.Initialize(movementSpeed);
            _attackController.Initialize(bulletFlyingSpeed);
            _rotationController.Initialize(rotationSpeed);
            _inputHandler
                .OnSpacePressed
                .Subscribe(_ => Shoot())
                .AddTo(_disposables);
        }

        private void Awake()
        {
            _shipMovement = GetComponent<ShipMovement>();
            _attackController = GetComponent<AttackController>();
            _rotationController = GetComponent<RotationController>();
        }
        
        public IUpdatable[] GetAllUpdatableObjects() => 
            new IUpdatable[] { _shipMovement, _rotationController };

        private void OnDestroy() => 
            _disposables.Dispose();

        public void SetPosition(Vector2 position) => 
            _shipMovement.SetPosition(position);
        
        public void SetRotation(Quaternion rotation) =>
            _rotationController.SetRotation(rotation);
        
        public void SetOnDeadEvent(Action onDeadEvent) =>
            GetComponent<ShipTarget>()
                .OnKill
                .Subscribe(_ => onDeadEvent?.Invoke())
                .AddTo(_disposables);
        
        private void Shoot() => 
            _attackController.Shoot();
    }
}
