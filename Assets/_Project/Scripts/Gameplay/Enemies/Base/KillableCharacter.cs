using _Project.Scripts.Gameplay.Services.Targets;
using _Project.Scripts.Gameplay.Services.Targets.Base;
using R3;

namespace _Project.Scripts.Gameplay.Enemies.Base
{
    public abstract class KillableCharacter : ReleasedCharacter
    {
        private IKillableTargetWithReactiveProperty _target;
        
        protected void SetupKillableCharacter()
        {
            SetupReleaseCharacter();
            _target = GetComponent<IKillableTargetWithReactiveProperty>();

            _target
                .OnKill
                .Subscribe(_ => ReleaseCharacter())
                .AddTo(Disposable);
        }
        
        protected void KillCharacter() =>
            _target.Kill();
    }
}