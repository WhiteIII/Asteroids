using System.Threading;
using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.GameProgress;
using _Project.Scripts.Gameplay.Services.Targets.Base;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using Zenject;
using static UnityEngine.Mathf;
using static UnityEngine.Time;

namespace _Project.Scripts.Gameplay.ShipBase
{
    public class LazerController : MonoBehaviour, IUpdatable
    {
        public readonly ReactiveProperty<int> CurrentChargesCount = new();
        public readonly ReactiveProperty<float> CurrentCoolDown = new();
        
        [SerializeField] private Lazer _lazer;

        private float _coolDown;
        private int _maxChargesCount;
        private float _lifeTime;
        private bool _isActive;

        public bool AttackIsDone => CurrentChargesCount.Value > 0 && _isActive == false;
        
        public void Initialize(float lifeTime, float coolDown, int maxChargesCount)
        {
            _lifeTime = lifeTime;
            _coolDown = coolDown;
            _maxChargesCount = maxChargesCount;
            CurrentChargesCount.Value = _maxChargesCount;
        }

        public void SetIgnoredTargetType<T>()
            where T : IKillableTarget
        {
            _lazer.SetIgnoredTargetType<T>();
        }
        
        public void GameLoopUpdate()
        {
            if (CurrentChargesCount.CurrentValue == _maxChargesCount)
                return;
            
            CurrentCoolDown.Value = Min(_coolDown, CurrentCoolDown.CurrentValue + deltaTime);
            
            if (CurrentCoolDown.CurrentValue == _coolDown)
            {
                CurrentCoolDown.Value = 0;
                CurrentChargesCount.Value++;
            }
        }

        public async UniTask Shoot(CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();
            CurrentChargesCount.Value--;
            _lazer.ShowLazer();
            _isActive = true;
            await UniTask.WaitForSeconds(_lifeTime, cancellationToken: token);
            token.ThrowIfCancellationRequested();
            _isActive = false;
            _lazer.HideLazer();
        }
    }
}