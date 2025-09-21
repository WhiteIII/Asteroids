using R3;
using UnityEngine;

namespace _Project.Scripts.Core.Services.Targets
{
    internal class ShipTarget : MonoBehaviour, IKillableTarget
    {
        public Subject<Unit> OnKill = new();
        
        public void Kill() => 
            OnKill.OnNext(Unit.Default);
    }
}