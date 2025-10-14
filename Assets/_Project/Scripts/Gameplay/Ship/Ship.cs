using System;
using System.Threading;
using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.Characters.Base;
using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.InputSystem;
using _Project.Scripts.Gameplay.Services.Targets.Implementation;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Ship
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(ShipTarget))]
    [RequireComponent(typeof(AttackController))]
    [RequireComponent(typeof(ShipMovement))]
    [RequireComponent(typeof(RotationController))]
    [RequireComponent(typeof(LazerController))]
    public class Ship : Character, IInitializable, IInitializableUpdatableObject
    {
        private readonly CompositeDisposable _disposables = new();
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        
        private ShipMovement _shipMovement;
        private AttackController _attackController;
        private RotationController _rotationController;
        private LazerController _lazerController;
        private IInputHandler _inputHandler;
        private ShipStatsData _stats;
        
        public Observable<float> OnLazerCooldownChanged { get; private set; }
        public Observable<int> OnLazerChargeCountChanged { get; private set; }
        
        [Inject]
        private void Construct(IInputHandler inputHandler, ShipStatsData stats)
        {
            _inputHandler = inputHandler;
            _stats = stats;
        }
        
        public void Initialize()
        {
            _attackController.Initialize(_stats.BulletFlyingSpeed);
            _lazerController.Initialize(
                _stats.LazerActivityTime, 
                _stats.LazerRechargeTime,
                _stats.LazerChargeCount);
            _lazerController.SetIgnoredTargetType<ShipTarget>();
            _inputHandler
                .OnSpacePressed
                .Subscribe(_ => Shoot())
                .AddTo(_disposables);
            _inputHandler
                .OnEKeyPressed
                .Where(_ => _lazerController.AttackIsDone)
                .Subscribe(_ => _lazerController.Shoot(_cancellationTokenSource.Token).Forget())
                .AddTo(_disposables);
        }

        protected override void OnAwake()
        {
            _shipMovement = GetComponent<ShipMovement>();
            _attackController = GetComponent<AttackController>();
            _rotationController = GetComponent<RotationController>();
            _lazerController = GetComponent<LazerController>();

            OnLazerChargeCountChanged = _lazerController.CurrentChargesCount;
            OnLazerCooldownChanged = _lazerController.CurrentCoolDown;
        }
        
        public IUpdatable[] GetAllUpdatableObjects() => 
            new IUpdatable[] { _shipMovement, _rotationController, _lazerController };

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _disposables.Dispose();
        }

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
            _attackController.Shoot<ShipTarget>(transform.rotation * Vector2.up);
    }
}