using R3;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Services.Targets.Base
{
    public class BaseKillableTarget : MonoBehaviour, IKillableTargetWithReactiveProperty
    {
        public Subject<Unit> OnKill { get; } = new();
        
        public void Kill() => 
            OnKill.OnNext(Unit.Default);
    }
}