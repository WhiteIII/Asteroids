using R3;

namespace _Project.Scripts.Gameplay.Services.Targets.Base
{
    public interface IKillableTargetWithReactiveProperty : IKillableTarget
    {
        Subject<Unit> OnKill { get; }
    }
}