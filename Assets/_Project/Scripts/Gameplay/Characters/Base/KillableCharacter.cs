using _Project.Scripts.Gameplay.Services.Targets.Base;
using R3;
using System;

namespace _Project.Scripts.Gameplay.Characters.Base
{
    public abstract class KillableCharacter : ReleasedCharacter
    {
        private BaseKillableTarget _target;
        
        public bool IsAlive { get; private set; }
        
        protected void SetupKillableCharacter(Action onKill = null)
        {
            IsAlive = true;
            SetupReleaseCharacter();
            _target = GetComponent<BaseKillableTarget>();

            _target.SetOnKillEvent(onKill);
            _target
                .OnKill
                .Subscribe(_ =>
                {
                    ReleaseCharacter();
                    IsAlive = false;
                })
                .AddTo(this);
        }
        
        public void Revive() =>
            IsAlive = true;
    }
}