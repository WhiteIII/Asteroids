using System;
using System.Threading;
using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.Characters.Base;
using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.InputSystem;
using _Project.Scripts.Gameplay.Services.Components;
using _Project.Scripts.Gameplay.Services.Targets.Implementation;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Ship
{
    [RequireComponent(typeof(ShipTarget))]
    [RequireComponent(typeof(AttackController))]
    [RequireComponent(typeof(ShipMovement))]
    [RequireComponent(typeof(RotationController))]
    [RequireComponent(typeof(LazerController))]
    [RequireComponent(typeof(ActionOnGoingOutOrInCameraVisionField))]
    public class Ship : Character, IInitializableUpdatableObject
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        
        private ShipMovement _shipMovement;
        private AttackController _attackController;
        private RotationController _rotationController;
        private LazerController _lazerController;
        private ActionOnGoingOutOrInCameraVisionField _cameraFieldService;
        private IInputHandler _inputHandler;
        private ShipStatsData _stats;
        private Action _onDeadEvent;
        
        public Observable<float> OnLazerCooldownChanged { get; private set; }
        public Observable<int> OnLazerChargeCountChanged { get; private set; }
        
        [Inject]
        private void Construct(IInputHandler inputHandler, ShipStatsData stats)
        {
            _inputHandler = inputHandler;
            _stats = stats;
        }

        protected override void OnAwake()
        {
            _shipMovement = GetComponent<ShipMovement>();
            _attackController = GetComponent<AttackController>();
            _rotationController = GetComponent<RotationController>();
            _lazerController = GetComponent<LazerController>();
            _cameraFieldService = GetComponent<ActionOnGoingOutOrInCameraVisionField>();
            
            GetComponent<ShipTarget>()
                .OnKill
                .Subscribe(_ => _onDeadEvent?.Invoke())
                .AddTo(this);
            
            _onDeadEvent += _shipMovement.StopShip;
            
            OnLazerChargeCountChanged = _lazerController.CurrentChargesCount;
            OnLazerCooldownChanged = _lazerController.CurrentCoolDown;
            
            _cameraFieldService.Initialize(null, ChangePositionOnGoingOutCameraVisionField);
            _attackController.Initialize(_stats.BulletFlyingSpeed);
            _lazerController.Initialize(
                _stats.LazerActivityTime, 
                _stats.LazerRechargeTime,
                _stats.LazerChargeCount);
            _lazerController.SetIgnoredTargetType<ShipTarget>();
            _inputHandler
                .OnSpacePressed
                .Subscribe(_ => Shoot())
                .AddTo(this);
            _inputHandler
                .OnEKeyPressed
                .Where(_ => _lazerController.AttackIsDone)
                .Subscribe(_ => _lazerController.Shoot(_cancellationTokenSource.Token).Forget())
                .AddTo(this);
        }
        
        public IGameLoopObject[] GetAllGameLoopObjects() => 
            new IGameLoopObject[] { _shipMovement, _rotationController, _lazerController };

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        public override void SetPosition(Vector2 position) => 
            _shipMovement.SetPosition(position);
        
        public void SetRotation(Quaternion rotation) =>
            _rotationController.SetRotation(rotation);
        
        public void AddOnDeadEvent(Action onDeadEvent) =>
            _onDeadEvent += onDeadEvent;
        
        private void Shoot() => 
            _attackController.Shoot<ShipTarget>(transform.rotation * Vector2.up);

        private void ChangePositionOnGoingOutCameraVisionField() => 
            SetPosition(-Position.CurrentValue);
    }
}