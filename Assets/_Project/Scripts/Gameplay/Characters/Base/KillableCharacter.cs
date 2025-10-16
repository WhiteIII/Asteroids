using _Project.Scripts.Gameplay.Services.Targets.Base;
using R3;
using System;

namespace _Project.Scripts.Gameplay.Characters.Base
{
    public abstract class KillableCharacter : ReleasedCharacter
    {
        private BaseKillableTarget _target;
        
        protected void SetupKillableCharacter(Action onKill = null)
        {
            SetupReleaseCharacter();
            _target = GetComponent<BaseKillableTarget>();

            _target.SetOnKillEvent(onKill);
            _target
                .OnKill
                .Subscribe(_ => ReleaseCharacter())
                .AddTo(this);
        }
    }
}