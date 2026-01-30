using System;
using System.Threading;
using _Project.Scripts.Common.Services.Analytics.Base;
using _Project.Scripts.Common.Services.Analytics.Implementation.Data;
using _Project.Scripts.Data;
using _Project.Scripts.Data.Base;
using _Project.Scripts.Data.Implementation;
using _Project.Scripts.Data.Services.Repositories.Base;
using _Project.Scripts.Gameplay.Characters.Base;
using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.GameProgress;
using _Project.Scripts.Gameplay.InputSystem;
using _Project.Scripts.Gameplay.Services.Components;
using _Project.Scripts.Gameplay.Services.Components.View;
using _Project.Scripts.Gameplay.Services.Targets.Implementation;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.ShipBase
{
    [RequireComponent(typeof(ShipTarget))]
    [RequireComponent(typeof(AttackController))]
    [RequireComponent(typeof(ShipMovement))]
    [RequireComponent(typeof(RotationController))]
    [RequireComponent(typeof(LazerController))]
    [RequireComponent(typeof(ActionOnGoingOutOrInCameraVisionField))]
    [RequireComponent(typeof(ImmortalAnimation))]
    public class Ship : Character, IInitializableUpdatableObject
    {
        [SerializeField] private DeathAnimationController _deathAnimationController;
        [SerializeField] private float _immortalTime;
        
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        
        private IEventSender _eventSender;
        private WeaponsUsageCounter _weaponsUsageCounter;
        private ShipMovement _shipMovement;
        private AttackController _attackController;
        private RotationController _rotationController;
        private LazerController _lazerController;
        private ActionOnGoingOutOrInCameraVisionField _cameraFieldService;
        private IReadOnlyInputHandler _inputHandler;
        private ShipStatsConfig _stats;
        private ImmortalAnimation _immortalAnimation;
        private bool _isImmortal;
        
        public Observable<float> OnLazerCooldownChanged { get; private set; }
        public Observable<int> OnLazerChargeCountChanged { get; private set; }
        
        [Inject]
        private void Construct(
            IReadOnlyInputHandler readOnlyInputHandler,
            IDataRepository dataRepository,
            WeaponsUsageCounter weaponsUsageCounter,
            IEventSender eventSender)
        {
            _inputHandler = readOnlyInputHandler;
            _stats = dataRepository.GetData<ShipStatsConfig>();
            _weaponsUsageCounter = weaponsUsageCounter;
            _eventSender = eventSender;
        }

        protected override void OnAwake()
        {
            _shipMovement = GetComponent<ShipMovement>();
            _attackController = GetComponent<AttackController>();
            _rotationController = GetComponent<RotationController>();
            _lazerController = GetComponent<LazerController>();
            _cameraFieldService = GetComponent<ActionOnGoingOutOrInCameraVisionField>();
            _immortalAnimation = GetComponent<ImmortalAnimation>();
            
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
                .Subscribe(_ => ShootByLazer())
                .AddTo(this);
            _isImmortal = false;
        }
        
        public IGameLoopObject[] GetAllGameLoopObjects() => 
            new IGameLoopObject[] { _shipMovement, _rotationController, _lazerController };

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        public void SetPosition(Vector2 position) => 
            _shipMovement.SetPosition(position);
        
        public void SetRotation(Quaternion rotation) =>
            _rotationController.SetRotation(rotation);
        
        public void SetOnDeadEvent(Action onDeadEvent) =>
            GetComponent<ShipTarget>()
                .OnKill
                .Where(_ => _isImmortal == false)
                .Subscribe(_ => onDeadEvent?.Invoke())
                .AddTo(this);

        public async UniTask PlayDeathAnimationAsync()
        {
            DisableFbx();
            await _deathAnimationController.PlayAnimationAsync();
        }

        public async void Revive()
        {
            EnableFbx();
            _isImmortal = true;
            _immortalAnimation.StartAnimation();
            await UniTask.WaitForSeconds(_immortalTime);
            _immortalAnimation.StopAnimation();
            _isImmortal =  false;
        }

        public void StopShip() => 
            _shipMovement.StopShip();

        private void ShootByLazer()
        {
            _eventSender.SendEvent<AnalyticDataOnLazerUsed>();
            _lazerController.Shoot(_cancellationTokenSource.Token).Forget();
            _weaponsUsageCounter.AddLazerUsageCount();
        }
        
        private void Shoot()
        {
            _weaponsUsageCounter.AddBaseWeaponUsageCount();
            _attackController.Shoot<ShipTarget>(transform.rotation * Vector2.up);
        }

        private void ChangePositionOnGoingOutCameraVisionField() => 
            SetPosition(-Position.CurrentValue);
    }
}