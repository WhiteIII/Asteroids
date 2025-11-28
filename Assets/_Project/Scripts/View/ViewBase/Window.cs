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
        public bool IsOpen { get; private set; }
        
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
            gameObject.SetActive(true);
            IsOpen = true;
            OnOpenAnimationStart();
            await _animation.PlayShowAnimationAsync();
        }

        public async UniTask Close()
        {
            IsOpen = false;
            await _animation.PlayCloseAnimationAsync();
            OnCloseAnimationEnd();
            gameObject.SetActive(false);
        }

        protected virtual void OnSetup() { }
        protected virtual void OnDestroyMethod() { }
        protected virtual void OnOpenAnimationStart() { }
        protected virtual void  OnCloseAnimationEnd() { }
    }
}