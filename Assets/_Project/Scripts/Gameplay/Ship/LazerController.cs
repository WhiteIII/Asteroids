using System.Threading;
using _Project.Scripts.Gameplay.GameLoopSystem;
using _Project.Scripts.Gameplay.Services.Targets.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Mathf;
using static UnityEngine.Time;

namespace _Project.Scripts.Gameplay.Ship
{
    public class LazerController : MonoBehaviour, IUpdatable
    {
        [SerializeField] private Lazer _lazer;

        private int _maxChargesCount;
        private float _lifeTime;
        private float _coolDown;
        
        private int _currentChargesCount;
        private float _currentCoolDown;
        private bool _isActive;

        public bool AttackIsDone => _currentChargesCount > 0 && _isActive == false;
        
        public void Initialize(float lifeTime, float coolDown, int maxChargesCount)
        {
            _lifeTime = lifeTime;
            _coolDown = coolDown;
            _maxChargesCount = maxChargesCount;
            _currentChargesCount = _maxChargesCount;
            _currentCoolDown = _coolDown;
        }

        public void SetIgnoredTargetType<T>()
            where T : IKillableTarget
        {
            _lazer.SetIgnoredTargetType<T>();
        }
        
        public void GameLoopUpdate()
        {
            if (_currentChargesCount == _maxChargesCount)
                return;
            
            _currentCoolDown = Min(_coolDown, _currentCoolDown + deltaTime);
            
            if (_currentCoolDown == _coolDown)
            {
                _currentCoolDown = 0;
                _currentChargesCount++;
            }
        }

        public async void Shoot(CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();
            _currentChargesCount--;
            _lazer.ShowLazer();
            _isActive = true;
            await UniTask.WaitForSeconds(_lifeTime);
            token.ThrowIfCancellationRequested();
            _isActive = false;
            _lazer.HideLazer();
        }
    }
}