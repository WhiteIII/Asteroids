using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.View
{
    internal class ShipStatsWindowAnimation : MonoBehaviour, IWindowAnimation
    {
        [Header("ShipStatsRectTransforms:")]
        [SerializeField] private MoveAnimationConfig[] _configs;
        
        [Header("Settings:")]
        [SerializeField] private float _duration = 0.2f;
        [SerializeField] private float _cooldown = 0.05f;
        
        private void Awake()
        {
            foreach (MoveAnimationConfig config in _configs)
                config.MoveAnimation = new MoveAnimation(config.RectTransform, _duration);
        }
        
        public async UniTask PlayCloseAnimationAsync()
        {
            List<UniTask> tasks = new();
            foreach (MoveAnimationConfig animationConfig in _configs)
            {
                tasks.Add(animationConfig.MoveAnimation.PlayAnimationAsync(animationConfig.To, animationConfig.From));
                await UniTask.WaitForSeconds(_cooldown);
            }
            await UniTask.WhenAll(tasks);
        }

        public async UniTask PlayShowAnimationAsync()
        {
            List<UniTask> tasks = new();
            foreach (MoveAnimationConfig animationConfig in _configs)
            {
                tasks.Add(animationConfig.MoveAnimation.PlayAnimationAsync(animationConfig.From, animationConfig.To));
                await UniTask.WaitForSeconds(_cooldown);
            }
            await UniTask.WhenAll(tasks);
        }

        [Serializable]
        private class MoveAnimationConfig
        { 
            public MoveAnimation MoveAnimation; 
            
            [field: SerializeField] public Vector2 From { get; private set; }
            [field: SerializeField] public Vector2 To { get; private set; }
            [field: SerializeField] public RectTransform RectTransform { get; private set; }
        }
    }
}