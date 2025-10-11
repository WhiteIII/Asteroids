using System;
using _Project.Scripts.Gameplay.Services.Components;
using _Project.Scripts.Gameplay.Services.Targets.Base;
using R3;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Ship
{
    [RequireComponent(typeof(CollisionHandler))]
    public class Lazer : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new();
        
        private Type _ignoredTargetType; 

        public void SetIgnoredTargetType<T>()
            where T : IKillableTarget
        {
            _ignoredTargetType = typeof(T);
        }
        
        private void Awake() =>
            GetComponent<CollisionHandler>()
                .OnTouchTarget
                .Subscribe(OnLazerEnter)
                .AddTo(_disposable);
        
        private void OnDestroy() => 
            _disposable.Dispose();

        private void OnLazerEnter(ITarget target)
        {
            Type currentTargetType = target.GetType();
            if (currentTargetType == _ignoredTargetType)
                return;
            if (target is IKillableTarget killableTarget)
                killableTarget.Kill();
        }
        
        public void ShowLazer() => 
            gameObject.SetActive(true);

        public void HideLazer() => 
            gameObject.SetActive(false);
    }
}