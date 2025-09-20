using R3;
using UnityEngine;

namespace _Project.Scripts.Core.Services.Targets
{
    public abstract class BaseKillableTarget : MonoBehaviour, IKillableTarget
    {
        public readonly Subject<string> OnKill = new();

        private string _id;
        
        public void Initialize(string id) => 
            _id = id;

        public void Kill() => 
            OnKill.OnNext(_id);
    }
}