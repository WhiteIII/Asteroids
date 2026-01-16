using System;
using R3;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Services.Targets.Base
{
    public class BaseKillableTarget : MonoBehaviour, IKillableTargetWithReactiveProperty
    {
        public Observable<Unit> OnKill => _onKillSubject;

        private readonly Subject<Unit> _onKillSubject = new();
        
        private Action _onKill;

        public void SetOnKillEvent(Action onKill) => 
            _onKill = onKill;
        
        public void Kill()
        {
            _onKill?.Invoke();
            _onKillSubject.OnNext(Unit.Default);
        }
    }
}