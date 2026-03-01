using System;
using _Project.Scripts.View.Animations.Base;
using _Project.Scripts.View.Animations.Implementation;
using _Project.Scripts.ViewModel.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.View
{
    public abstract class Window<T> : Window, IWindow<T>
        where T : IViewModel
    {
        protected T ViewModel { get; private set; }
        
        public void Setup(T viewModel)
        {
            ViewModel = viewModel;
            
            if (ViewModel is IInitializable initializable)
                initializable.Initialize();

            OnSetup();
        }
        
        private void OnDestroy()
        {
            if (ViewModel is IDisposable disposable)
                disposable.Dispose();
            
            OnDestroyMethod();
        }
    }

    public abstract class Window : MonoBehaviour, IWindow
    {
        private IWindowAnimation _animation;
        
        public bool IsOpen { get; private set; }

        private void Awake()
        {
            if (TryGetComponent(out _animation) == false)
                gameObject.AddComponent<RegularAnimation>();
        }
        
        public async UniTask OpenAsync()
        {
            gameObject.SetActive(true);
            IsOpen = true;
            OnOpenAnimationStart();
            await _animation.PlayShowAnimationAsync(this.GetCancellationTokenOnDestroy());
        }

        public async UniTask CloseAsync()
        {
            IsOpen = false;
            await _animation.PlayCloseAnimationAsync(this.GetCancellationTokenOnDestroy());
            OnCloseAnimationEnd();
            gameObject.SetActive(false);
        }
        
        protected virtual void OnSetup() { }
        protected virtual void OnDestroyMethod() { }
        protected virtual void OnOpenAnimationStart() { }
        protected virtual void  OnCloseAnimationEnd() { }
    }
}