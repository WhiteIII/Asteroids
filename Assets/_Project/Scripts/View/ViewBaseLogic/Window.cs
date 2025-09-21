using System;
using _Project.Scripts.View.ViewBaseLogic.Animation;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.View.ViewBaseLogic
{
    public abstract class Window<T> : MonoBehaviour, IWindow<T>
        where T : IViewModel
    {
        private IWindowAnimation _animation;
        
        protected T ViewModel { get; private set; }
        
        public void Setup(T viewModel)
        {
            ViewModel = viewModel;
            
            if (ViewModel is IInitializable initializable)
                initializable.Initialize();

            if (TryGetComponent(out _animation) == false)
                gameObject.AddComponent<RegularWindowAnimation>();
        }

        private void OnDestroy()
        {
            if (ViewModel is IDisposable disposable)
                disposable.Dispose();
        }

        public async UniTask Open()
        {
            Enable();
            await _animation.PlayShowAnimation();
        }

        public async UniTask Close()
        {
            Disable();
            await _animation.PlayHideAnimation();
        }

        protected abstract void Enable();
        protected abstract void Disable();
    }
}