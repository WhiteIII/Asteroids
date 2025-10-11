using System;
using _Project.Scripts.ViewModel;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.View
{
    public abstract class Window<T> : MonoBehaviour, IWindow<T>
        where T : IViewModel
    {
        protected T ViewModel { get; private set; }
        private IWindowAnimation _animation;
        
        public void Setup(T viewModel)
        {
            ViewModel = viewModel;
            
            if (ViewModel is IInitializable initializable)
                initializable.Initialize();
            
            if (TryGetComponent(out _animation) == false)
                gameObject.AddComponent<RegularAnimation>();

            OnSetup();
        }
        
        private void OnDestroy()
        {
            if (ViewModel is IDisposable disposable)
                disposable.Dispose();
            
            OnDestroyMethod();
        }

        public async UniTask Open()
        {
            Enable();
            await _animation.PlayShowAnimationAsync();
        }

        public async UniTask Close()
        {
            await _animation.PlayCloseAnimationAsync();
            Disable();
        }

        protected virtual void OnSetup() { }
        protected virtual void OnDestroyMethod() { }
        protected virtual void Enable() { }
        protected virtual void Disable() { }
    }
}