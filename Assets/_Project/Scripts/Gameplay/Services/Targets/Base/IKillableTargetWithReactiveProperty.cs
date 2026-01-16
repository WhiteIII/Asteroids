using R3;

namespace _Project.Scripts.Gameplay.Services.Targets.Base
{
    public interface IKillableTargetWithReactiveProperty : IKillableTarget
    {
        Observable<Unit> OnKill { get; }
    }
}